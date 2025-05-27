using MySql.Data.MySqlClient;
using OfficeOpenXml;
using System.Data;
using System.Net.Mail;
using System.Net;
using InventarioMedicamentos.conexion;

namespace InventarioMedicamentos
{
    internal class exportarInforme
    {
        Conexion conexion = new Conexion();
        public void ExportarTablaExcelConParametros(string query, string rutaExcel, DateTime fechaDesde, DateTime fechaHasta)
        {
            using (var conn = conexion.ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@desde", fechaDesde);
                cmd.Parameters.AddWithValue("@hasta", fechaHasta.AddDays(1).AddSeconds(-1));

                var reader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(reader);

                // Crear nuevo DataTable para los datos formateados
                DataTable formattedTable = new DataTable();

                // Copiar estructura y cambiar tipo para columnas DateTime
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.DataType == typeof(DateTime))
                    {
                        formattedTable.Columns.Add(column.ColumnName, typeof(string));
                    }
                    else
                    {
                        formattedTable.Columns.Add(column.ColumnName, column.DataType);
                    }
                }

                // Copiar datos con formato aplicado
                foreach (DataRow row in dataTable.Rows)
                {
                    var newRow = formattedTable.NewRow();
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (column.DataType == typeof(DateTime) && !row.IsNull(column))
                        {
                            newRow[column.ColumnName] = ((DateTime)row[column]).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            newRow[column.ColumnName] = row[column];
                        }
                    }
                    formattedTable.Rows.Add(newRow);
                }

                // Exportar el nuevo DataTable
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Datos");
                    worksheet.Cells["A1"].LoadFromDataTable(formattedTable, true);
                    package.SaveAs(new FileInfo(rutaExcel));
                }
            }
        }
        public void EnviarCorreoConExcel(string rutaExcel, string destinatario)
        {
            MailMessage mail = new MailMessage("chetosflaminconsalsa@gmail.com", destinatario);
            mail.Subject = "Datos exportados";
            mail.Body = "Adjunto encontrarás el archivo Excel.";

            mail.Attachments.Add(new Attachment(rutaExcel));

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("chetosflaminconsalsa@gmail.com", "ctcf vllt xxry pztg"); // Usa App Password si usas Gmail
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
