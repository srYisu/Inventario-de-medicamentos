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
    public partial class FormAgUsuariosAdmin : Form
    {
        usuariosConsultas consultas;
        private FormPrincipal navegador;
        public FormAgUsuariosAdmin(FormPrincipal navegador)
        {
            InitializeComponent();
            consultas = new usuariosConsultas();
            this.navegador = navegador;
        }

        private void FormAgUsuariosAdmin_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            AplicarEsquinasRedondeadas(panelAnadirUsuario, 50);
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

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }
        void LimpiarCampos()
        {
            txtConfirmarContrasena.Clear();
            txtContrasena.Clear();
            txtCorreoElectrónico.Clear();
            txtNombreDeUsuario.Clear();
            cmbTipoDeUsuario.SelectedIndex = -1;
        }
        void GuardarUsuario()
        {
            if (string.IsNullOrEmpty(txtNombreDeUsuario.Text) || string.IsNullOrEmpty(txtCorreoElectrónico.Text)
                || string.IsNullOrEmpty(cmbTipoDeUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text)
                || string.IsNullOrEmpty(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            else if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, intente nuevamente.");
                return;
            }
            string nombre = txtNombreDeUsuario.Text;
            string correo = txtCorreoElectrónico.Text;
            string tipo = cmbTipoDeUsuario.SelectedItem.ToString();
            string contrasena = txtContrasena.Text;

            bool resultado = consultas.GuardarUsuario(nombre, correo, tipo, contrasena);
            LimpiarCampos();
            if (resultado)
            {
                MessageBox.Show("Usuario guardado correctamente.");
            }
            else
            {
                MessageBox.Show("Error al guardar el usuario. Por favor, intente nuevamente.");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            GuardarUsuario();
        }
    }
}
