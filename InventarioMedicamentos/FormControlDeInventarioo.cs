using InventarioMedicamentos.medicamentos;
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
        private int medicamentoId = 0;
        public FormControlDeInventarioo()
        {
            InitializeComponent();
            consultasMedicamentos = new consultasMedicamentos();
            CargarMedicamentos();
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
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
            dgvMedicamentos.DataSource = consultasMedicamentos.ConsultarProfesores();
        }
        private void LimpiarCampos()
        {
            cmbUnidades.SelectedIndex = -1;
            txtFondoFijo.Clear();
            dtpFechaCaducidad.Value = DateTime.Now;
            txtMedicamento.Clear();
        }
        private void GuardarMedicamento()
        {
            string descripcion = txtMedicamento.Text;
            string unidad = cmbUnidades.Text;
            int fondoFijo = int.Parse(txtFondoFijo.Text);
            DateTime fechaCaducidad = DateTime.Parse(dtpFechaCaducidad.Text);
            try
            {
                consultasMedicamentos.GuardarMedicanmento(descripcion, unidad, fondoFijo, fechaCaducidad);
                MessageBox.Show("Medicamento guardado correctamente.");
                LimpiarCampos();
                CargarMedicamentos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el medicamento: " + ex.Message);
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
                dtpFechaCaducidad.Value = DateTime.Parse(row.Cells[4].Value.ToString());
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
            DateTime fechaCaducidad = DateTime.Parse(dtpFechaCaducidad.Text);
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
            FormInicioDeSesion formInicioDeSesion = new FormInicioDeSesion();
            formInicioDeSesion.Show();
            this.Hide();
        }
    }
}
