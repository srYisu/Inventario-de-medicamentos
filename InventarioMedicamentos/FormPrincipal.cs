using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioMedicamentos
{
    public partial class FormPrincipal : Form
    {
        private AbrirForms gestor = new AbrirForms();
        public FormPrincipal()
        {
            InitializeComponent();
            gestor.CargarPantalla(pnlPrincipal, new FormMenu(this));
        }

        public void NavegarA(Form nuevaPantalla)
        {
            gestor.CargarPantalla(pnlPrincipal, nuevaPantalla);
        }
    }
}
