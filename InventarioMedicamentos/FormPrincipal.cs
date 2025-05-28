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
            gestor.CargarPantalla(pnlPrincipal, new FormInicioDeSesion(this));
            this.MaximizeBox = false; // Desactiva el botón de maximizar
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public void NavegarA(Form nuevaPantalla)
        {
            gestor.CargarPantalla(pnlPrincipal, nuevaPantalla);
        }

        private void FormPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
