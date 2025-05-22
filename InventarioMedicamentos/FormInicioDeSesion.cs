using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using InventarioMedicamentos.conexion;
using InventarioMedicamentos.usuarios;
using Guna.UI2.WinForms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;



namespace InventarioMedicamentos
{
    public partial class FormInicioDeSesion : Form
    {
        private usuariosConsultas usuarios;
        private Conexion conexion;
        public FormInicioDeSesion()
        {
            InitializeComponent();
            usuarios = new usuariosConsultas();
            conexion = new Conexion();
        }

        private void FormInicioDeSesion_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
            AplicarEsquinasRedondeadas(panelLogin, 50);
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            //conexion.PruebaConexion();
            string contraseña = txtContrasena.Text;
            string usuario = txtUsuario.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            usuariosConsultas inicioSesion = new usuariosConsultas();
            string rol;

            bool accesoConcedido = inicioSesion.IniciarSesion(usuario, contraseña, out rol);

            if (accesoConcedido)
            {
                switch (rol)
                {
                    case "Administrador":
                        MessageBox.Show("Acceso concedido: puede gestionar todo el sistema.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "Supervisor":
                        MessageBox.Show("Acceso concedido: puede gestionar el inventario.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        MessageBox.Show("Acceso concedido: puede retirar medicamentos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Usuario o contraseña incorrectos.");
            }
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

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

        private void panelNaranja_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
