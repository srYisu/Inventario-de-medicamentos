using Google.Protobuf.WellKnownTypes;
using InventarioMedicamentos.medicamentos;
using InventarioMedicamentos.movimientos;
using InventarioMedicamentos.usuarios;
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

namespace InventarioMedicamentos
{
    public partial class RetiroMedicamentos : Form
    {
        private FormPrincipal navegador;
        private consultasMedicamentos consultasMedicamentos;
        private consultasMovimientos mov;
        private int medicamentoId = 0;
        private int cantidadDisponible = 0;
        public RetiroMedicamentos(FormPrincipal navegador)
        {
            InitializeComponent();
            consultasMedicamentos = new consultasMedicamentos();
            mov = new consultasMovimientos();
            CargarMedicamentos();
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            this.navegador = navegador;
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
        private void CargarMedicamentos()
        {
            dgvMedicamentos.DataSource = consultasMedicamentos.ConsultarMedicamentos();
            // Hacer todas las celdas de solo lectura
            dgvMedicamentos.ReadOnly = true;

            // Deshabilitar edición directamente en el control
            dgvMedicamentos.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Opcional: Deshabilitar el menú contextual que podría permitir edición
            dgvMedicamentos.ContextMenuStrip = null;
        }
        private void LimpiarCampos()
        {
            txtCantidadRetirar.Text = "";
            txtMedicamento.Text = "";
            txtUnidades.Text = "";
            cantidadDisponible = 0;
            medicamentoId = 0;
        }
        private void RetiroMedicamentos_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
        }

        private void dgvMedicamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvMedicamentos.Rows[e.RowIndex];

                string descripcion = fila.Cells["Medicamento"].Value.ToString();
                string unidad = fila.Cells["Unidad"].Value.ToString();
                medicamentoId = Convert.ToInt32(fila.Cells["id"].Value);
                cantidadDisponible = Convert.ToInt32(fila.Cells["Fondo Fijo"].Value);


                // Puedes mostrar los valores por ejemplo en TextBoxes:
                txtMedicamento.Text = descripcion;
                txtUnidades.Text = unidad;
            }
        }

        private void btnRetirar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMedicamento.Text) ||
    string.IsNullOrEmpty(txtUnidades.Text))
            {
                MessageBox.Show("Por favor seleccione un medicamento de la tabla", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ❗ Detener ejecución si faltan campos
            }

            int retiro;
            if (!int.TryParse(txtCantidadRetirar.Text, out retiro) || retiro <= 0 || retiro > cantidadDisponible)
            {
                MessageBox.Show("Por favor ingrese una cantidad válida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // ❗ Detener ejecución si la cantidad no es válida
            }

            int resultado = cantidadDisponible - retiro;

            if (consultasMedicamentos.RestarMedicamento(medicamentoId, resultado))
            {
                // Aquí podrías agregar también el movimiento
                int idUsuario = UsuarioActual.IdUsuario; // Ejemplo, puede venir del login
                string tipo = "Retiro";
                DateTime fechaHora = DateTime.Now;
                mov.GuardarMovimiento(medicamentoId, idUsuario, fechaHora, tipo, retiro);

                MessageBox.Show("Retiro exitoso", "Medicamento retirado");
                CargarMedicamentos();
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Error al retirar el medicamento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscador.Text.Trim();
            dgvMedicamentos.DataSource = consultasMedicamentos.ConsultarMedicamentos(textoBusqueda);
        }

        private void dgvMedicamentos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Verifica que sea la columna de fecha de caducidad
            if (dgvMedicamentos.Columns[e.ColumnIndex].Name == "Fecha Caducidad" && e.Value != null)
            {
                try
                {
                    DateTime fechaCaducidad;
                    if (DateTime.TryParse(e.Value.ToString(), out fechaCaducidad))
                    {
                        TimeSpan diferencia = fechaCaducidad - DateTime.Now;

                        // Semaforización
                        if (diferencia.TotalDays < 182) // Rojo: menos de 30 días
                        {
                            e.CellStyle.BackColor = Color.LightCoral;
                            e.CellStyle.ForeColor = Color.DarkRed;
                            e.CellStyle.Font = new Font(dgvMedicamentos.Font, FontStyle.Bold);
                            e.CellStyle.SelectionBackColor = Color.LightCoral;
                        }
                        else if (diferencia.TotalDays < 365) // Amarillo: entre 30 y 90 días
                        {
                            e.CellStyle.BackColor = Color.LightGoldenrodYellow;
                            e.CellStyle.ForeColor = Color.DarkGoldenrod;
                            e.CellStyle.SelectionBackColor = Color.LightGoldenrodYellow;
                        }
                        else // Verde: más de 90 días
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkGreen;
                            e.CellStyle.SelectionBackColor = Color.LightGreen;
                        }
                    }
                }
                catch
                {
                    // Manejo de error por si la conversión falla
                }
            }
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }
    }
}
