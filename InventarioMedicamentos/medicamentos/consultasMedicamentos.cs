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
        public int GuardarMedicanmento(string descripcion, string unidad, int fondoFijo, DateTime fechaCaducidad)
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
        public bool ActualizarMedicamento(int idMedicamento, string descripcion, string unidad, int fondoFijo, DateTime fechaCaducidad)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"UPDATE medicamentos
                             SET descripcion = @descripcion, unidad = @unidad, fondo_fijo = @fondoFijo, fecha_caducidad = @fechaCaducidad
                             WHERE id_medicamento = @idMedicamento";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@unidad", unidad);
                    cmd.Parameters.AddWithValue("@fondoFijo", fondoFijo);
                    cmd.Parameters.AddWithValue("@fechaCaducidad", fechaCaducidad.ToString("yyyy-MM-dd"));

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Método para consultar todos los medicamentos
        public DataTable ConsultarProfesores()
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"SELECT id_medicamento AS ID, 
                                    descripcion AS Medicamento, 
                                    unidad AS Unidad, 
                                    fondo_fijo AS 'Fondo Fijo', 
                                    fecha_caducidad AS 'Fecha Caducidad'
                             FROM medicamentos";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
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
