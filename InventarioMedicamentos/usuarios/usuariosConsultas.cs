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
        private Conexion conexion;

        public usuariosConsultas()
        {
            conexion = new Conexion();
        }
        public bool IniciarSesion(string usuario, string contraseña, out bool esAdministrador)
        {
            esAdministrador = false;

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT permisoAdmin FROM usuarios WHERE usuario = @usuario AND contraseña = @contraseña";

                    using (MySqlCommand comando = new MySqlCommand(query, conn))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contraseña", contraseña); // Si usas hash, aquí iría la contraseña hasheada

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                esAdministrador = lector.GetBoolean("permisoAdmin");
                                return true; // Usuario encontrado
                            }
                            else
                            {
                                return false; // No encontrado
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores, por ejemplo:
                    Console.WriteLine("Error al iniciar sesión: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
