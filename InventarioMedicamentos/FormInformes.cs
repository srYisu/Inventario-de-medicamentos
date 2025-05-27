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
    public partial class FormInformes : Form
    {
        private FormPrincipal navegador;
        private consultasMovimientos movimientos;
        public FormInformes(FormPrincipal navegador)
        {
            InitializeComponent();
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            this.navegador = navegador;
            movimientos = new consultasMovimientos();
            CargarMovimientos();
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
        }
        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            DateTime desde = dtpDesde.Value;
            DateTime hasta = dtpHasta.Value;

            string movimiento = cmbTIpoOperacion.SelectedItem.ToString();

            dgvInformes.DataSource = movimientos.ConsultarMovimientosConFiltro(desde,hasta,movimiento);

        }
    }
}
