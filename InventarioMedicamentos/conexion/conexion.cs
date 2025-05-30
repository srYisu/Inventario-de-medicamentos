using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace InventarioMedicamentos.conexion
{
    internal class Conexion
    {
        private static string connectionString = "";

        // Ruta del archivo de configuración (junto al ejecutable)
        private static string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.txt");

        // Método para cargar el connectionString desde el archivo
        private static void CargarConnectionString()
        {
            try
            {
                if (File.Exists(configFilePath))
                {
                    connectionString = File.ReadAllText(configFilePath).Trim();
                }
                else
                {
                    // Si no existe, crea uno por defecto
                    connectionString = "server=localhost;user=root;password=;database=control_medicamentos";
                    File.WriteAllText(configFilePath, connectionString);
                    MessageBox.Show("Archivo config.txt creado. Por favor, configura la conexión.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer config.txt: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método para probar la conexión
        public void PruebaConexion()
        {
            CargarConnectionString(); // Asegura que el connectionString esté actualizado
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Conexión exitosa.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Método para probar la conexión
        public bool ComprobarConexion()
        {
            CargarConnectionString(); // Asegura que el connectionString esté actualizado
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // Método para obtener la conexión
        public MySqlConnection ObtenerConexion()
        {
            CargarConnectionString(); // Actualiza el connectionString antes de devolver la conexión
            return new MySqlConnection(connectionString);
        }
    }
}
