using InventarioMedicamentos.medicamentos;
using InventarioMedicamentos.movimientos;
using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventarioMedicamentos.conexion;
using InventarioMedicamentos.usuarios;

namespace InventarioMedicamentos
{
    public partial class FormInformes : Form
    {
        private exportarInforme exportar;
        private Conexion conexion;
        private FormPrincipal navegador;
        private consultasMovimientos movimientos;
        public FormInformes(FormPrincipal navegador)
        {
            InitializeComponent();
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            this.navegador = navegador;
            exportar = new exportarInforme();
            conexion = new Conexion();
            movimientos = new consultasMovimientos();
            CargarMovimientos();
            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;
            lblEstado.Visible = false;
        }

        private void FormInformes_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
        }

        private void AplicarEsquinasRedondeadas(Panel panel, int radio)
        {
            panel.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
                GraphicsPath path = CrearRectanguloRedondeado(rect, radio);
                panel.Region = new Region(path);
            };

            // Forzar el repintado para aplicar el cambio al cargar
            panel.Invalidate();
        }

        private GraphicsPath CrearRectanguloRedondeado(Rectangle rect, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            int diametro = radio * 2;

            // Esquinas redondeadas
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diametro, diametro, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - diametro, rect.Y, diametro, diametro, 270, 90); // Superior derecha
            path.AddArc(rect.Right - diametro, rect.Bottom - diametro, diametro, diametro, 0, 90); // Inferior derecha
            path.AddArc(rect.X, rect.Bottom - diametro, diametro, diametro, 90, 90); // Inferior izquierda
            path.CloseFigure();

            return path;
        }
        private void CargarMovimientos()
        {
            dgvInformes.DataSource = movimientos.ConsultarMovimientos();
            // Hacer todas las celdas de solo lectura
            dgvInformes.ReadOnly = true;

            // Deshabilitar edición directamente en el control
            dgvInformes.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Opcional: Deshabilitar el menú contextual que podría permitir edición
            dgvInformes.ContextMenuStrip = null;
        }
        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;

            string movimiento = "";
            if (string.IsNullOrEmpty(cmbTIpoOperacion.Text))
            {
                movimiento = "Ambos";
            }
            else { movimiento = cmbTIpoOperacion.SelectedItem.ToString(); }

            dgvInformes.DataSource = movimientos.ConsultarMovimientosConFiltro(desde, hasta, movimiento);
            // Hacer todas las celdas de solo lectura
            dgvInformes.ReadOnly = true;

            // Deshabilitar edición directamente en el control
            dgvInformes.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Opcional: Deshabilitar el menú contextual que podría permitir edición
            dgvInformes.ContextMenuStrip = null;

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }


        public async Task ExportarYEnviarMovimientos(DateTime fechaDesde, DateTime fechaHasta, string destinatario, string tipoMovimiento = "")
        {
            await Task.Run(() =>
            {
                string rutaExcel = "";
                try
                {
                    // Query base
                    string query = @"SELECT 
                        m.id_movimiento AS ID, 
                        med.descripcion AS 'Medicamento', 
                        u.nombre AS 'Usuario', 
                        DATE_FORMAT(m.fecha, '%d/%m/%Y') AS 'Fecha', 
                        m.tipo_movimiento AS 'Movimiento',
                        m.cantidad AS Cantidad
                     FROM movimientos m
                     INNER JOIN medicamentos med ON m.id_medicamento = med.id_medicamento
                     INNER JOIN usuarios u ON m.id_usuario = u.id_usuario
                     WHERE m.fecha BETWEEN @desde AND @hasta";

                    // Agregar filtro por tipo si es necesario
                    if (!string.IsNullOrEmpty(tipoMovimiento) && tipoMovimiento != "Ambos")
                    {
                        query += " AND m.tipo_movimiento = @tipoMovimiento";
                    }

                    // Ruta temporal para el archivo
                    rutaExcel = Path.Combine(Path.GetTempPath(), $"Movimientos_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

                    // Exportar a Excel
                    using (var conn = conexion.ObtenerConexion())
                    {
                        conn.Open();
                        var cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@desde", fechaDesde);
                        cmd.Parameters.AddWithValue("@hasta", fechaHasta.AddDays(1).AddSeconds(-1));

                        if (!string.IsNullOrEmpty(tipoMovimiento) && tipoMovimiento != "Ambos")
                        {
                            cmd.Parameters.AddWithValue("@tipoMovimiento", tipoMovimiento);
                        }

                        var dataTable = new DataTable();
                        using (var reader = cmd.ExecuteReader())
                        {
                            dataTable.Load(reader);
                        }

                        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                        using (var package = new ExcelPackage())
                        {
                            var worksheet = package.Workbook.Worksheets.Add("Datos");
                            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);
                            package.SaveAs(new FileInfo(rutaExcel));
                        }
                    }

                    // Enviar por correo
                     exportar.EnviarCorreoConExcel(rutaExcel, destinatario);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al exportar y enviar movimientos: " + ex.Message);
                }
                finally
                {
                    // Intentar eliminar el archivo temporal
                    if (File.Exists(rutaExcel))
                    {
                        try
                        {
                            File.Delete(rutaExcel);
                        }
                        catch { /* Ignorar errores de eliminación */ }
                    }
                }
            }
            );
                
        }
        private async void btnDescargarInforme_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Exportando y enviando...";
            lblEstado.Visible = true;
            btnDescargarInforme.Enabled = false;

            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;
            string destinatario = UsuarioActual.correo;
            string tipoMovimiento = cmbTIpoOperacion.SelectedItem != null ? cmbTIpoOperacion.SelectedItem.ToString() : "Ambos";

            try
            {
                await ExportarYEnviarMovimientos(desde, hasta, destinatario, tipoMovimiento);
                MessageBox.Show("El archivo fue exportado y enviado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                lblEstado.Visible = false;
                btnDescargarInforme.Enabled = true;
            }
        }
    }
}
