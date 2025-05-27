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
                string query = @"SELECT id_movimiento AS ID, 
                                    id_medicamento AS 'ID Medicamento', 
                                    id_usuario AS 'ID Usuario', 
                                    fecha AS 'Fecha', 
                                    tipo_movimiento AS 'Movimiento',
                                    cantidad AS Cantidad
                             FROM movimientos";
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

                // Base de la consulta
                string query = @"SELECT id_movimiento AS ID, 
                                id_medicamento AS 'ID Medicamento', 
                                id_usuario AS 'ID Usuario', 
                                fecha AS 'Fecha', 
                                tipo_movimiento AS 'Movimiento',
                                cantidad AS Cantidad
                         FROM movimientos
                         WHERE fecha BETWEEN @desde AND @hasta";

                // Agrega condición por tipo de operación si se especifica
                if (operacion == "Ambos")
                {
                     query = @"SELECT id_movimiento AS ID, 
                                id_medicamento AS 'ID Medicamento', 
                                id_usuario AS 'ID Usuario', 
                                fecha AS 'Fecha', 
                                tipo_movimiento AS 'Movimiento',
                                cantidad AS Cantidad
                         FROM movimientos
                         WHERE fecha BETWEEN @desde AND @hasta";
                }
                else if (!string.IsNullOrEmpty(operacion))
                {
                    query += " AND tipo_movimiento = @operacion";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@desde", desde.Date);
                cmd.Parameters.AddWithValue("@hasta", hasta.Date);

                if (!string.IsNullOrEmpty(operacion))
                {
                    cmd.Parameters.AddWithValue("@operacion", operacion);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
