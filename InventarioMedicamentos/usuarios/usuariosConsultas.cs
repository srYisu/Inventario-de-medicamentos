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
        public bool IniciarSesion(string usuario, string contraseña, out string tipo, out int id)
        {
            tipo = string.Empty;
            id = -1; // Valor por defecto (invalido)

            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                try
                {
                    conn.Open();
                    // Consulta modificada para obtener tanto 'tipo' como 'id_usuario'
                    string query = @"SELECT id_usuario, tipo 
                            FROM usuarios 
                            WHERE nombre = @usuario 
                            AND contraseña = @contraseña 
                            AND disponible = 1";

                    using (MySqlCommand comando = new MySqlCommand(query, conn))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@contraseña", contraseña);

                        using (MySqlDataReader lector = comando.ExecuteReader())
                        {
                            if (lector.Read())
                            {
                                // Obtenemos ambos valores desde la base de datos
                                id = lector.GetInt32("id_usuario");
                                tipo = lector.GetString("tipo");
                                return true;
                            }
                            else
                            {
                                return false; // Usuario no encontrado o credenciales incorrectas
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
        public bool GuardarUsuario(string nombre, string correo, string tipo, string contrasena, int disp)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO usuarios (nombre, correo, tipo, contraseña, disponible) 
                             VALUES (@nombre, @correo, @tipo, @contrasena, @disp)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@disp", disp);

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
                string queryEliminar = @"UPDATE usuarios SET disponible = 0 WHERE id_usuario = @idUsuario";
                using (MySqlCommand cmdEliminar = new MySqlCommand(queryEliminar, conn))
                {
                    cmdEliminar.Parameters.AddWithValue("@idUsuario", idUsuario);
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
                     FROM usuarios WHERE disponible = 1";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    row["Contrasena"] = "********"; // Ocultar contraseñas
                }
                return dt;
            }
        }
        public bool ActualizarUsuario(int idUsuario, string nombre, string correo, string tipo, string contrasena, int disponible)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE usuarios 
                         SET nombre = @nombre, 
                             correo = @correo, 
                             tipo = @tipo, 
                             contraseña = @contrasena, 
                             disponible = @disponible 
                         WHERE id_usuario = @idUsuario";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@disponible", disponible);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool CambiarContrasena(int idUsuario, string nuevaContrasena)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE usuarios 
                         SET contraseña = @nuevaContrasena 
                         WHERE id_usuario = @idUsuario";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@nuevaContrasena", nuevaContrasena);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
