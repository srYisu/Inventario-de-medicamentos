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
        private Conexion conexion;
        public FormInicioSesion()
        {
            InitializeComponent();
            usuarios = new usuariosConsultas();
            conexion = new Conexion();
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

        private void FormInicioSesion_Load(object sender, EventArgs e)
        {

        }
    }
}