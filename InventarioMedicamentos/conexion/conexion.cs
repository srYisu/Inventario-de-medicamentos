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
        private const string connectionString = $"server=localhost;user=root;password=;database=control_medicamentos";
        //método para probar la conexión a la base de datos
        public void PruebaConexion()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Conexión exitosa a la base de datos.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
