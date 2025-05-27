using InventarioMedicamentos.conexion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMedicamentos.medicamentos
{
    internal class consultasMedicamentos
    {
        private Conexion conexion;

        public consultasMedicamentos()
        {
            conexion = new Conexion();
        }

        // Método para agregar un medicamento
        public int GuardarMedicamento(string descripcion, string unidad, int fondoFijo, DateTime fechaCaducidad)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO medicamentos (descripcion, unidad, fondo_fijo, fecha_caducidad) 
                             VALUES (@descripcion, @unidad, @fondoFijo, @fechaCaducidad)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@unidad", unidad);
                    cmd.Parameters.AddWithValue("@fondoFijo", fondoFijo);
                    cmd.Parameters.AddWithValue("@fechaCaducidad", fechaCaducidad);

                    cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
            }
        }

        // Método para eliminar un medicamento
        public bool EliminarMedicamento(int idMedicamento)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // Elimina
                string queryEliminar = @"DELETE FROM medicamentos WHERE id_medicamento = @idMedicamento";
                using (MySqlCommand cmdEliminar = new MySqlCommand(queryEliminar, conn))
                {
                    cmdEliminar.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    return cmdEliminar.ExecuteNonQuery() > 0;
                }
            }
        }

        // Método para actualizar un medicamento
        public bool ActualizarMedicamento(int idMedicamento, string descripcion, string unidad, int nuevoFondoFijo, DateTime fechaCaducidad, int idUsuario)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // Primero obtenemos el fondo fijo actual
                int fondoFijoActual;
                string queryGetCurrent = "SELECT fondo_fijo FROM medicamentos WHERE id_medicamento = @idMedicamento";
                using (MySqlCommand cmd = new MySqlCommand(queryGetCurrent, conn))
                {
                    cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    fondoFijoActual = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Calculamos la diferencia
                int diferencia = nuevoFondoFijo - fondoFijoActual;
                string tipoMovimiento = diferencia > 0 ? "Ingreso" : "Retiro";
                int cantidadMovimiento = Math.Abs(diferencia);

                // Actualizamos el medicamento
                string queryUpdate = @"UPDATE medicamentos
                            SET descripcion = @descripcion, 
                                unidad = @unidad, 
                                fondo_fijo = @fondoFijo, 
                                fecha_caducidad = @fechaCaducidad
                            WHERE id_medicamento = @idMedicamento";
                using (MySqlCommand cmd = new MySqlCommand(queryUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@unidad", unidad);
                    cmd.Parameters.AddWithValue("@fondoFijo", nuevoFondoFijo);
                    cmd.Parameters.AddWithValue("@fechaCaducidad", fechaCaducidad.ToString("yyyy-MM-dd"));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Si hubo cambio en el fondo fijo y la actualización fue exitosa, registramos el movimiento
                    if (diferencia != 0 && rowsAffected > 0)
                    {
                        string queryMovimiento = @"INSERT INTO movimientos (id_medicamento, id_usuario, fecha, tipo_movimiento, cantidad)
                                        VALUES (@idMedicamento, @idUsuario, @fecha, @tipo, @cantidad)";

                        using (MySqlCommand cmdMov = new MySqlCommand(queryMovimiento, conn))
                        {
                            cmdMov.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                            cmdMov.Parameters.AddWithValue("@idUsuario", idUsuario);
                            cmdMov.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                            cmdMov.Parameters.AddWithValue("@tipo", tipoMovimiento);
                            cmdMov.Parameters.AddWithValue("@cantidad", cantidadMovimiento);

                            cmdMov.ExecuteNonQuery();
                        }
                    }

                    return rowsAffected > 0;
                }
            }
        }

        // Método para consultar todos los medicamentos
        public DataTable ConsultarMedicamentos(string filtroNombre = "", string filtroUnidad = "")
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"SELECT id_medicamento AS ID, 
                        descripcion AS Medicamento, 
                        unidad AS Unidad, 
                        fondo_fijo AS 'Fondo Fijo', 
                        fecha_caducidad AS 'Fecha Caducidad'
                 FROM medicamentos WHERE 1=1"; // Truco para facilitar añadir condiciones

                if (!string.IsNullOrEmpty(filtroNombre))
                {
                    query += " AND descripcion LIKE CONCAT(@filtroNombre, '%')";
                }

                if (!string.IsNullOrEmpty(filtroUnidad))
                {
                    query += " AND unidad = @filtroUnidad";
                }

                query += " ORDER BY descripcion";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                if (!string.IsNullOrEmpty(filtroNombre))
                {
                    cmd.Parameters.AddWithValue("@filtroNombre", filtroNombre);
                }

                if (!string.IsNullOrEmpty(filtroUnidad))
                {
                    cmd.Parameters.AddWithValue("@filtroUnidad", filtroUnidad);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        
        // Metodo para restar medicamentos
        public bool RestarMedicamento(int idMedicamento, int nuevoValor)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // Retira medicamentos
                string queryRestar = @"UPDATE medicamentos 
                                    SET fondo_fijo = @nuevoValor
                                    WHERE id_medicamento = @idMedicamento";
                using (MySqlCommand cmdRestar = new MySqlCommand(queryRestar, conn))
                {
                    cmdRestar.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    cmdRestar.Parameters.AddWithValue("@nuevoValor", nuevoValor);
                    return cmdRestar.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
