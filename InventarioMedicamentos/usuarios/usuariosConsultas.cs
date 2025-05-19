using InventarioMedicamentos.conexion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMedicamentos.usuarios
{
    internal class usuariosConsultas
    {
        private Conexion conn;

        public usuariosConsultas()
        {
            conn = new Conexion();
        }
        public bool IniciarSesion(string usuario, string contraseña, out string tipo)
        {
            tipo = string.Empty;

            using (MySqlConnection conexion = conn.ObtenerConexion())
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT tipo FROM usuarios WHERE nombre = @usuario AND contraseña = @contraseña";

                    using (MySqlCommand comando = new MySqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contraseña", contraseña);

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                tipo = lector.GetString("tipo");
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al iniciar sesión: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
