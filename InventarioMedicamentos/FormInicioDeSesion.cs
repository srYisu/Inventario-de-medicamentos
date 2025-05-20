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


namespace InventarioMedicamentos
{
    public partial class FormInicioDeSesion : Form
    {
        private usuariosConsultas usuarios;

        public FormInicioDeSesion()
        {
            InitializeComponent();
            InicializarInterfaz();
            usuarios = new usuariosConsultas();
        }

        private void FormInicioDeSesion_Load(object sender, EventArgs e)
        {

        }

        private void InicializarInterfaz()
        {
            // Propiedades del Form
            this.Text = "Inicio de Sesion";
            this.Size = new Size(800, 600);
            this.BackColor = Color.FromArgb(153, 140, 101);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel Encabezado
            panelEncabezado = new Guna2Panel();
            panelEncabezado.FillColor = Color.FromArgb(111, 0, 42);
            panelEncabezado.Dock = DockStyle.Top;
            panelEncabezado.Height = 100;
            this.Controls.Add(panelEncabezado);

            // Texto Bienvenido
            labelBienvenido = new Guna2HtmlLabel();
            labelBienvenido.Text = "Bienvenido";
            labelBienvenido.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            labelBienvenido.ForeColor = Color.White;
            labelBienvenido.Dock = DockStyle.Fill;
            labelBienvenido.TextAlignment = ContentAlignment.MiddleCenter;
            panelEncabezado.Controls.Add(labelBienvenido);

            // Panel de Login
            panelLogin = new Guna2Panel();
            panelLogin.FillColor = Color.LightGray;
            panelLogin.BorderRadius = 25;
            panelLogin.Size = new Size(350, 450);
            panelLogin.Location = new Point((this.ClientSize.Width - panelLogin.Width) / 2, 130);
            this.Controls.Add(panelLogin);

            // Imagen logo
            pictureBoxLogo = new Guna2PictureBox();
            pictureBoxLogo.Image = Properties.Resources.logoIMSS.png;
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.Size = new Size(120, 120);
            pictureBoxLogo.Location = new Point((panelLogin.Width - 120) / 2, 20);
            panelLogin.Controls.Add(pictureBoxLogo);

            // TextBox Usuario
            txtUsuario = new Guna2TextBox();
            txtUsuario.PlaceholderText = "Usuario";
            txtUsuario.BorderRadius = 10;
            txtUsuario.Size = new Size(250, 36);
            txtUsuario.Location = new Point(50, 160);
            panelLogin.Controls.Add(txtUsuario);

            // TextBox Contraseña
            txtContrasena = new Guna2TextBox();
            txtContrasena.PlaceholderText = "Contraseña";
            txtContrasena.UseSystemPasswordChar = true;
            txtContrasena.BorderRadius = 10;
            txtContrasena.Size = new Size(250, 36);
            txtContrasena.Location = new Point(50, 210);
            panelLogin.Controls.Add(txtContrasena);

            // Link Olvidaste tu contraseña
            linkOlvidaste = new LinkLabel();
            linkOlvidaste.Text = "¿Olvidaste tu contraseña?";
            linkOlvidaste.LinkColor = Color.Blue;
            linkOlvidaste.AutoSize = true;
            linkOlvidaste.Location = new Point(85, 255);
            panelLogin.Controls.Add(linkOlvidaste);

            // Botón Iniciar Sesión
            btnIniciarSesion = new Guna2Button();
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.FillColor = Color.FromArgb(153, 112, 0);
            btnIniciarSesion.ForeColor = Color.White;
            btnIniciarSesion.BorderRadius = 10;
            btnIniciarSesion.Size = new Size(250, 40);
            btnIniciarSesion.Location = new Point(50, 290);
            btnIniciarSesion.Click += BtnIniciarSesion_Click;
            panelLogin.Controls.Add(btnIniciarSesion);
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string contraseña = txtContrasena.Text;
            string usuario = txtUsuario.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contraseña))
            {
                MessageBox.Show("Por favor, ingrese su usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool esAdministrador;
            bool acceso = usuarios.IniciarSesion(usuario, contraseña, out esAdministrador);

            if (acceso)
            {
                if (esAdministrador)
                {
                    MessageBox.Show("Bienvenido Administrador", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Bienvenido Usuario comun y corriente :D", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
