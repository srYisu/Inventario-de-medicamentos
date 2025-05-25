using Google.Protobuf.WellKnownTypes;
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
    public partial class RetiroMedicamentos : Form
    {
        private consultasMedicamentos consultasMedicamentos;
        private int medicamentoId = 0;
        private int cantidadDisponible = 0;
        public RetiroMedicamentos()
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
        private void CargarMedicamentos()
        {
            dgvMedicamentos.DataSource = consultasMedicamentos.ConsultarProfesores();
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
            if (string.IsNullOrEmpty(txtMedicamento.Text) || string.IsNullOrEmpty(txtCantidadRetirar.Text) || string.IsNullOrEmpty(txtUnidades.Text))
            {
                MessageBox.Show("Por favor seleccione un medicamento de la tabla", "Error", buttons: MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                int retiro = Convert.ToInt32(txtCantidadRetirar.Text);
                if (retiro < 0 || retiro > cantidadDisponible)
                {
                    MessageBox.Show("Por favor ingrese una cantidad valida", "Error", buttons: MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                int resultado = cantidadDisponible - retiro;
                if (consultasMedicamentos.RestarMedicamento(medicamentoId, resultado))
                {
                    MessageBox.Show("Retiro exitoso", "Medicamento retirado");
                    CargarMedicamentos();
                    LimpiarCampos();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
