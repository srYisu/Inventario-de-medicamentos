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
    public partial class FormCambiarContrasena : Form
    {
        private FormPrincipal navegador;
        private usuariosConsultas usuariosConsultas;
        public FormCambiarContrasena(FormPrincipal navegador)
        {
            InitializeComponent();
            this.navegador = navegador;
            usuariosConsultas = new usuariosConsultas();
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }

        private void FormCambiarContrasena_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            AplicarEsquinasRedondeadas(panelCambiarContrasena, 50);
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
        private void cambiarContrasena()
        {
            string nuevaContrasena = txtConfirmarContrasena.Text;
            string confirmarContrasena = txtConfirmarContrasena.Text;
            string contrasenaActual = txtContrasenaActual.Text.Trim();
            if (nuevaContrasena != confirmarContrasena)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (contrasenaActual != UsuarioActual.contrasena)
            {
                MessageBox.Show("La contraseña actual es incorrecta. Por favor, inténtelo de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
                // Aquí se llamaría al método para cambiar la contraseña en la base de datos
                usuariosConsultas.CambiarContrasena(UsuarioActual.IdUsuario, nuevaContrasena);
            MessageBox.Show("Contraseña cambiada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            navegador.NavegarA(new FormMenu(navegador));
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            cambiarContrasena();
        }
    }
}
