using InventarioMedicamentos.conexion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
        public bool IniciarSesion(string usuario, string contraseña, out string tipo)
        {
            tipo = string.Empty;

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT tipo FROM usuarios WHERE nombre = @usuario AND contraseña = @contraseña";

                    using (MySqlCommand comando = new MySqlCommand(query, conn))
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
        public bool GuardarUsuario(string nombre, string correo, string tipo, string contrasena)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO usuarios (nombre, correo, tipo, contraseña) 
                             VALUES (@nombre, @correo, @tipo, @contrasena)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Método para eliminar un medicamento
        public bool EliminarUsuario(int idUsuario)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // Elimina
                string queryEliminar = @"DELETE FROM usurios WHERE id_usuario = @idUsuario";
                using (MySqlCommand cmdEliminar = new MySqlCommand(queryEliminar, conn))
                {
                    cmdEliminar.Parameters.AddWithValue("@idMedicamento", idUsuario);
                    return cmdEliminar.ExecuteNonQuery() > 0;
                }
            }
        }
        public DataTable ConsultarUsuarios()
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"SELECT id_usuario AS ID, 
                            nombre AS 'Usuario', 
                            correo AS 'Correo Electronico', 
                            tipo AS 'Tipo', 
                            contraseña AS 'Contrasena'
                     FROM usuarios";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
