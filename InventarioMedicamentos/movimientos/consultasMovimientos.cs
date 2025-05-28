using Guna.UI2.WinForms.Suite;
using InventarioMedicamentos.conexion;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMedicamentos.movimientos
{
    internal class consultasMovimientos
    {
        Conexion conexion = new Conexion();
        public bool GuardarMovimiento(int idMedicamento, int idUsuario, DateTime fechaHora, string tipo, int cantidad)
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"INSERT INTO movimientos (id_medicamento, id_usuario, fecha, tipo_movimiento, cantidad)
                               VALUES (@idMedicamento, @idUsuario, @fechaHora, @tipo, @cantidad)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idMedicamento", idMedicamento);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@fechaHora", fechaHora);
                    cmd.Parameters.AddWithValue("@tipo", tipo);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Método para consultar todos los medicamentos
        public DataTable ConsultarMovimientos()
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();
                string query = @"SELECT 
                            m.id_movimiento AS ID, 
                            med.descripcion AS 'Medicamento', 
                            u.nombre AS 'Usuario', 
                            m.fecha AS 'Fecha', 
                            m.tipo_movimiento AS 'Movimiento',
                            m.cantidad AS Cantidad
                        FROM movimientos m
                        INNER JOIN medicamentos med ON m.id_medicamento = med.id_medicamento
                        INNER JOIN usuarios u ON m.id_usuario = u.id_usuario
                        ORDER BY m.fecha DESC";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
        // Método para consultar todos los medicamentos
        public DataTable ConsultarMovimientosConFiltro(DateTime desde, DateTime hasta, string operacion = "")
        {
            using (MySqlConnection conn = conexion.ObtenerConexion())
            {
                conn.Open();

                // Base de la consulta con JOINs para obtener nombres
                string query = @"SELECT 
                            m.id_movimiento AS ID, 
                            med.descripcion AS 'Medicamento', 
                            u.nombre AS 'Usuario', 
                            m.fecha AS 'Fecha', 
                            m.tipo_movimiento AS 'Movimiento',
                            m.cantidad AS Cantidad
                         FROM movimientos m
                         INNER JOIN medicamentos med ON m.id_medicamento = med.id_medicamento
                         INNER JOIN usuarios u ON m.id_usuario = u.id_usuario
                         WHERE m.fecha BETWEEN @desde AND @hasta";

                // Agrega condición por tipo de operación si se especifica (excepto para "Ambos")
                if (!string.IsNullOrEmpty(operacion) && operacion != "Todos")
                {
                    query += " AND m.tipo_movimiento = @operacion";
                }

                // Crear y configurar el comando SQL
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@desde", desde.Date);
                cmd.Parameters.AddWithValue("@hasta", hasta.Date.AddDays(1).AddSeconds(-1)); // Para incluir todo el día hasta

                if (!string.IsNullOrEmpty(operacion) && operacion != "Todos")
                {
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                }

                // Ejecutar consulta y devolver resultados
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
