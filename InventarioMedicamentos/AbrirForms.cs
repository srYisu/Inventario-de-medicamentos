using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMedicamentos
{
    public class AbrirForms
    {
        public void CargarPantalla(System.Windows.Forms.Panel contenedor, Form nuevaPantalla)
        {
            if (contenedor.Controls.Count > 0)
            {
                var actual = contenedor.Controls[0] as Form;
                if (actual != null)
                {
                    actual.Close();
                    actual.Dispose();
                }
                contenedor.Controls.Clear();
            }

            nuevaPantalla.TopLevel = false;
            nuevaPantalla.FormBorderStyle = FormBorderStyle.None;
            nuevaPantalla.Dock = DockStyle.Fill;
            contenedor.Controls.Add(nuevaPantalla);
            nuevaPantalla.Show();
        }
    }
}
