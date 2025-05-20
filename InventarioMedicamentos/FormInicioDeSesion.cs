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
            AplicarEsquinasRedondeadas(panelLogin, 100);
            AplicarEsquinasRedondeadas(panelNaranja, 5);
            AplicarEsquinasRedondeadas(panelRojo, 5);
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

        private void AplicarEsquinasRedondeadas(Control control, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(control.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(control.Width - radio, control.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, control.Height - radio, radio, radio), 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void panelNaranja_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
