using InventarioMedicamentos.medicamentos;
using InventarioMedicamentos.movimientos;
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
    public partial class FormControlDeInventarioo : Form
    {
        private consultasMedicamentos consultasMedicamentos;
        private consultasMovimientos mov;
        private int medicamentoId = 0;
        private FormPrincipal navegador;
        public FormControlDeInventarioo(FormPrincipal navegador)
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
        private void AumentarTamano()
        {
            panelNaranja.Size = new Size(1920, 1080);
        }
        private void CargarMedicamentos()
        {
            dgvMedicamentos.DataSource = consultasMedicamentos.ConsultarMedicamentos();
        }
        private void LimpiarCampos()
        {
            cmbUnidades.SelectedIndex = -1;
            txtFondoFijo.Clear();
            txtFechaCaducidad.Text = "dd/mm/yyyy";
            txtFechaCaducidad.ForeColor = Color.Gray;
            txtMedicamento.Clear();
        }
        private void GuardarMedicamento()
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(txtMedicamento.Text))
            {
                MessageBox.Show("Debe ingresar una descripción para el medicamento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMedicamento.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbUnidades.Text) || cmbUnidades.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar una unidad de medida válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUnidades.Focus();
                return;
            }

            // Validar fondo fijo
            if (!int.TryParse(txtFondoFijo.Text, out int fondoFijo) || fondoFijo <= 0)
            {
                MessageBox.Show("El fondo fijo debe ser un número entero positivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFondoFijo.SelectAll();
                txtFondoFijo.Focus();
                return;
            }

            // Validar fecha de caducidad
            if (!DateTime.TryParse(txtFechaCaducidad.Text, out DateTime fechaCaducidad))
            {
                MessageBox.Show("Formato de fecha inválido. Use el formato dd/MM/yyyy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFechaCaducidad.SelectAll();
                txtFechaCaducidad.Focus();
                return;
            }

            if (fechaCaducidad.Date <= DateTime.Today)
            {
                MessageBox.Show("La fecha de caducidad debe ser posterior al día actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFechaCaducidad.SelectAll();
                txtFechaCaducidad.Focus();
                return;
            }

            try
            {
                // Obtener valores limpios
                string descripcion = txtMedicamento.Text.Trim();
                string unidad = cmbUnidades.Text.Trim();

                // Guardar medicamento
                int idUltimoMedicamento = consultasMedicamentos.GuardarMedicamento(descripcion, unidad, fondoFijo, fechaCaducidad);

                if (idUltimoMedicamento <= 0)
                {
                    MessageBox.Show("No se pudo obtener el ID del medicamento guardado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Registrar movimiento (deberías obtener el ID de usuario real)
                int idUsuario = 2; // Implementar esta función
                if (idUsuario <= 0)
                {
                    MessageBox.Show("No se pudo identificar al usuario actual.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    idUsuario = 2; // Valor por defecto solo si es aceptable
                }

                DateTime fechaHora = DateTime.Now;
                string tipo = "Ingreso";

                bool movimientoGuardado = mov.GuardarMovimiento(idUltimoMedicamento, idUsuario, fechaHora, tipo, fondoFijo);

                if (!movimientoGuardado)
                {
                    MessageBox.Show("El medicamento se guardó pero hubo un problema al registrar el movimiento.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Mostrar éxito y actualizar
                MessageBox.Show("Medicamento guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                CargarMedicamentos();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Error de formato: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("El valor ingresado es demasiado grande.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al guardar el medicamento: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Considera registrar el error en un log: Logger.Error(ex, "Error en GuardarMedicamento");
            }
        }

        private void EliminarMedicamento()
        {
            if (dgvMedicamentos.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este medicamento?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvMedicamentos.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        consultasMedicamentos.EliminarMedicamento(id);
                        MessageBox.Show("Medicamento eliminado correctamente.");
                        CargarMedicamentos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar el medicamento: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un medicamento para eliminar.");
            }
        }
        private void LlenarCampos()
        {
            if (dgvMedicamentos.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvMedicamentos.SelectedRows[0];
                txtMedicamento.Text = row.Cells[1].Value.ToString();
                cmbUnidades.Text = row.Cells[2].Value.ToString();
                txtFondoFijo.Text = row.Cells[3].Value.ToString();
                txtFechaCaducidad.Text = row.Cells[4].Value.ToString();
                txtFechaCaducidad.ForeColor = Color.Black;
                medicamentoId = Convert.ToInt32(row.Cells[0].Value);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un medicamento para editar.");
            }
        }
        private void EditarMedicamento()
        {
            string descripcion = txtMedicamento.Text;
            string unidad = cmbUnidades.Text;
            int fondoFijo = int.Parse(txtFondoFijo.Text);
            DateTime fechaCaducidad = DateTime.Parse(txtFechaCaducidad.Text);
            try
            {
                consultasMedicamentos.ActualizarMedicamento(medicamentoId, descripcion, unidad, fondoFijo, fechaCaducidad);
                MessageBox.Show("Medicamento editado correctamente.");
                LimpiarCampos();
                CargarMedicamentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar el medicamento: " + ex.Message);
            }

        }

        private void FormControlDeInventarioo_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
            txtFechaCaducidad.ForeColor = Color.Gray;
            txtFechaCaducidad.Text = "dd/mm/yyyy";
        }

        private void txtMedicamento_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUnidades_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFondoFijo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtFechaDeCaducidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Agregar")
            {
                GuardarMedicamento();
            }
            else
            {
                EditarMedicamento();
                btnAgregar.Text = "Agregar";
                btnEditar.Text = "Editar";
                LimpiarCampos();
                btnEliminar.Visible = true;
                btnLimpiar.Visible = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (btnEditar.Text == "Cancelar")
            {
                LimpiarCampos();
                btnAgregar.Text = "Agregar";
                btnEditar.Text = "Editar";
                btnEliminar.Visible = true;
                btnLimpiar.Visible = true;
            }
            else
            {
                LlenarCampos();
                btnEditar.Text = "Cancelar";
                btnAgregar.Text = "Guardar cambios";
                btnEliminar.Visible = false;
                btnLimpiar.Visible = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarMedicamento();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void dgvMedicamentos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormControlDeInventarioo_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                panelNaranja.Size = new Size(1920, 92);
                panelRojo.Size = new Size(1920, 84);
                lblTitulo.Location = new Point(650, 8);
                dgvMedicamentos.Size = new Size(1200, 800);
                PictureBoxSalir.Location = new Point(25, 950);

            }
            else if (this.WindowState == FormWindowState.Normal)
            {
                panelNaranja.Size = new Size(1589, 92);
                panelRojo.Size = new Size(1597, 84);
                lblTitulo.Location = new Point(509, 8);
                dgvMedicamentos.Size = new Size(862, 697);
                PictureBoxSalir.Location = new Point(12, 812);
            }
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }

        private void txtFechaCaducidad_TextChanged(object sender, EventArgs e)
        {
            string texto = txtFechaCaducidad.Text.Replace("/", ""); // Elimina las barras existentes
            if (texto.Length > 8) texto = texto.Substring(0, 8); // Máximo 8 dígitos

            // Agrega las barras automáticamente
            if (texto.Length >= 5)
                txtFechaCaducidad.Text = texto.Insert(2, "/").Insert(5, "/");
            else if (texto.Length >= 3)
                txtFechaCaducidad.Text = texto.Insert(2, "/");
            else
                txtFechaCaducidad.Text = texto;

            txtFechaCaducidad.SelectionStart = txtFechaCaducidad.Text.Length; // Mantén el cursor al final
        }

        private void txtFechaCaducidad_Enter(object sender, EventArgs e)
        {
            if (txtFechaCaducidad.Text == "dd/mm/yyyy")
            {
                txtFechaCaducidad.Text = "";
                txtFechaCaducidad.ForeColor = Color.Black;
            }
        }

        private void txtFechaCaducidad_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFechaCaducidad.Text))
            {
                txtFechaCaducidad.ForeColor = Color.Gray;
                txtFechaCaducidad.Text = "dd/mm/yyyy";
            }
        }

        private void txtFechaCaducidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = txtBusqueda.Text.Trim();
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
                        DataGridViewRow row = dgvMedicamentos.Rows[e.RowIndex];

                        // Semaforización
                        // Orden CORRECTO de condiciones (de más específica a más general)
                        if (diferencia.TotalDays < 0) // Ya caducó
                        {
                            // Aplicar estilo negro a toda la fila
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                cell.Style.BackColor = Color.Black;
                                cell.Style.ForeColor = Color.White;
                                cell.Style.Font = new Font(dgvMedicamentos.Font, FontStyle.Bold);
                                cell.Style.SelectionBackColor = Color.Black;
                                cell.Style.SelectionForeColor = Color.White;
                            }
                            e.Value = "⚠️ " + fechaCaducidad.ToString("dd/MM/yyyy") + " (CADUCADO)";
                        }
                        else if (diferencia.TotalDays < 182) // Menos de 6 meses (pero no caducado)
                        {
                            e.CellStyle.BackColor = Color.LightCoral;
                            e.CellStyle.ForeColor = Color.DarkRed;
                            e.CellStyle.Font = new Font(dgvMedicamentos.Font, FontStyle.Bold);
                            e.CellStyle.SelectionBackColor = Color.LightCoral;
                        }
                        else if (diferencia.TotalDays < 365) // Entre 6-12 meses
                        {
                            e.CellStyle.BackColor = Color.LightGoldenrodYellow;
                            e.CellStyle.ForeColor = Color.DarkGoldenrod;
                            e.CellStyle.SelectionBackColor = Color.LightGoldenrodYellow;
                        }
                        else // Más de 1 año
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
            // Verifica que sea la columna de fecha de caducidad
            if (dgvMedicamentos.Columns[e.ColumnIndex].Name == "Fondo Fijo" && e.Value != null)
            {
                try
                {
                    int cantidad = Convert.ToInt32(e.Value);

                        // Semaforización
                        if (cantidad <= 3) // Rojo: menos de 3 
                        {
                            //e.CellStyle.BackColor = Color.LightCoral;
                            e.CellStyle.ForeColor = Color.DarkRed;
                            e.CellStyle.Font = new Font(dgvMedicamentos.Font, FontStyle.Bold);
                            e.CellStyle.SelectionBackColor = Color.LightCoral;
                            e.Value = "⚠️ " + cantidad.ToString()+ "(CRÍTICO)";
                        }
                        else if (cantidad < 6) // Amarillo: entre 3 y 9 
                        {
                          //  e.CellStyle.BackColor = Color.LightGoldenrodYellow;
                            e.CellStyle.ForeColor = Color.DarkGoldenrod;
                            e.CellStyle.SelectionBackColor = Color.LightGoldenrodYellow;
                            e.Value = "❕ " + cantidad.ToString();
                        }
                        else // Verde: más de 9 
                        {
                        //    e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkGreen;
                            e.CellStyle.SelectionBackColor = Color.LightGreen;
                        }
                    
                }
                catch
                {
                }
            }
        }
    }
}
