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
    public partial class FormMenu : Form
    {
        AbrirForms open = new AbrirForms();
        private FormPrincipal navegador;
        public FormMenu(FormPrincipal navegador)
        {
            InitializeComponent();
            this.navegador = navegador;
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            AplicarEsquinasRedondeadas(panelBotones, 30);
            AplicarEsquinasRedondeadas(panelInventario, 25);
            AplicarEsquinasRedondeadas(panelInformes, 25);
            lblInformes.Cursor = Cursors.Hand;
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

        private void PictureBoxInventario_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormControlDeInventarioo(navegador));

        }

        private void PictureBoxInformes_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormInformes(navegador));
        }

        private void lblControlDeInventario_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormControlDeInventarioo(navegador));
        }

        private void lblInformes_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormInformes(navegador));
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            FormInicioDeSesion formInicioDeSesion = new FormInicioDeSesion();
            formInicioDeSesion.Show();
            this.Hide();
        }

        private void labelInventario_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormControlDeInventarioo(navegador));
        }

        private void pbbtnCambiarContrasena_Click(object sender, EventArgs e)
        {
            FormCambiarContrasena formCambiarContrasena = new FormCambiarContrasena();
            formCambiarContrasena.Show();
            this.Hide();
        }

        private void pbbtnAgUsuariosAdmin_Click(object sender, EventArgs e)
        {
            FormAgUsuariosAdmin formAgUsuariosAdmin = new FormAgUsuariosAdmin();
            formAgUsuariosAdmin.Show();
            this.Hide();
        }

        private void PictureBoxRetiros_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new RetiroMedicamentos(navegador));
        }

        private void lblRetiros_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new RetiroMedicamentos(navegador));
        }
    }
}
