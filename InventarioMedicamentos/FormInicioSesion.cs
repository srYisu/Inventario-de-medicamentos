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

namespace InventarioMedicamentos
{
    public partial class FormInicioSesion : Form
    {
        private usuariosConsultas usuarios;
        public FormInicioSesion()
        {
            InitializeComponent();
            usuarios = new usuariosConsultas();
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

        private void FormInicioSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
