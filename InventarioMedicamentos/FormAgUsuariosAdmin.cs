using InventarioMedicamentos.usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventarioMedicamentos
{
    public partial class FormAgUsuariosAdmin : Form
    {
        usuariosConsultas consultas;
        private FormPrincipal navegador;
        int usuarioID;
        public FormAgUsuariosAdmin(FormPrincipal navegador)
        {
            InitializeComponent();
            consultas = new usuariosConsultas();
            this.navegador = navegador;
            CargarTabla();
        }

        private void FormAgUsuariosAdmin_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#394D44");
            AplicarEsquinasRedondeadas(panelNaranja, 10);
            AplicarEsquinasRedondeadas(panelRojo, 10);
            AplicarEsquinasRedondeadas(panelAnadirUsuario, 50);
        }

        private void AplicarEsquinasRedondeadas(Panel panel, int radio)
        {
            panel.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
                GraphicsPath path = CrearRectanguloRedondeado(rect, radio);
                panel.Region = new Region(path);
            };

            // Forzar el repintado para aplicar el cambio al cargar
            panel.Invalidate();
        }

        private GraphicsPath CrearRectanguloRedondeado(Rectangle rect, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            int diametro = radio * 2;

            // Esquinas redondeadas
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diametro, diametro, 180, 90); // Esquina superior izquierda
            path.AddArc(rect.Right - diametro, rect.Y, diametro, diametro, 270, 90); // Superior derecha
            path.AddArc(rect.Right - diametro, rect.Bottom - diametro, diametro, diametro, 0, 90); // Inferior derecha
            path.AddArc(rect.X, rect.Bottom - diametro, diametro, diametro, 90, 90); // Inferior izquierda
            path.CloseFigure();

            return path;
        }

        private void PictureBoxSalir_Click(object sender, EventArgs e)
        {
            navegador.NavegarA(new FormMenu(navegador));
        }
        void LimpiarCampos()
        {
            txtConfirmarContrasena.Clear();
            txtContrasena.Clear();
            txtCorreoElectrónico.Clear();
            txtNombreDeUsuario.Clear();
            cmbTipoDeUsuario.SelectedIndex = -1;
            usuarioID = 0; // Reiniciar el ID del usuario
        }
        void GuardarUsuario()
        {
            if (string.IsNullOrEmpty(txtNombreDeUsuario.Text) || string.IsNullOrEmpty(txtCorreoElectrónico.Text)
                || string.IsNullOrEmpty(cmbTipoDeUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text)
                || string.IsNullOrEmpty(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            else if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, intente nuevamente.");
                return;
            }
            string nombre = txtNombreDeUsuario.Text;
            string correo = txtCorreoElectrónico.Text;
            string tipo = cmbTipoDeUsuario.SelectedItem.ToString();
            string contrasena = txtContrasena.Text;

            bool resultado = consultas.GuardarUsuario(nombre, correo, tipo, contrasena, 1);
            LimpiarCampos();
            CargarTabla();
            if (resultado)
            {
                MessageBox.Show("Usuario guardado correctamente.");
            }
            else
            {
                MessageBox.Show("Error al guardar el usuario. Por favor, intente nuevamente.");
            }
        }
        private void CargarTabla()
        {

            dgvUsuarios.DataSource = consultas.ConsultarUsuarios();
            // Hacer todas las celdas de solo lectura
            dgvUsuarios.ReadOnly = true;

            // Deshabilitar edición directamente en el control
            dgvUsuarios.EditMode = DataGridViewEditMode.EditProgrammatically;

            // Opcional: Deshabilitar el menú contextual que podría permitir edición
            dgvUsuarios.ContextMenuStrip = null;
        }
        private void EliminarUsuario()
        {
            DataGridViewRow row = dgvUsuarios.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);

            // Confirmar antes de eliminar
            DialogResult result = MessageBox.Show(
                $"¿Está seguro que desea eliminar el usuario con ID: {id}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    //int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);
                    consultas.EliminarUsuario(id);
                    MessageBox.Show("Usuario eliminado correctamente");
                    CargarTabla(); // Recargar datos después de eliminar
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}");
                }
            }
        }
        private void LlenarCampos()
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvUsuarios.SelectedRows[0];
                txtNombreDeUsuario.Text = row.Cells[1].Value.ToString();
                txtCorreoElectrónico.Text = row.Cells[2].Value.ToString();
                cmbTipoDeUsuario.Text = row.Cells[3].Value.ToString();
                usuarioID = Convert.ToInt32(row.Cells[0].Value);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un medicamento para editar.");
            }
        }
        private void ActualizarUsuario()
        {
            if (string.IsNullOrEmpty(txtNombreDeUsuario.Text) || string.IsNullOrEmpty(txtCorreoElectrónico.Text)
     || string.IsNullOrEmpty(cmbTipoDeUsuario.Text) || string.IsNullOrEmpty(txtContrasena.Text)
     || string.IsNullOrEmpty(txtConfirmarContrasena.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.");
                return;
            }
            else if (txtContrasena.Text != txtConfirmarContrasena.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, intente nuevamente.");
                return;
            }
            string nombre = txtNombreDeUsuario.Text;
            string correo = txtCorreoElectrónico.Text;
            string tipo = cmbTipoDeUsuario.SelectedItem.ToString();
            string contrasena = txtContrasena.Text;
            bool resultado = consultas.ActualizarUsuario(usuarioID, nombre, correo, tipo, contrasena, 1);
            LimpiarCampos();
            CargarTabla();
            if (resultado)
            {
                MessageBox.Show("Usuario actualizado correctamente.");
            }
            else
            {
                MessageBox.Show("Error al actualizar el usuario. Por favor, intente nuevamente.");
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (btnAgregar.Text == "Agregar")
            {
                GuardarUsuario();
                LimpiarCampos();
            }
            else if (btnAgregar.Text == "Actualizar")
            {
                ActualizarUsuario();
                btnAgregar.Text = "Agregar";
                btnEditar.Text = "Editar";
                btnEliminar.Visible = true;
                btnLimpiarCampos.Visible = true;
                LimpiarCampos();
            }
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panelAnadirUsuario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarUsuario();
        }

        private void btnLimpiarCampos_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (btnEditar.Text == "Editar")
            {
                btnAgregar.Text = "Actualizar";
                btnEditar.Text = "Cancelar";
                btnEliminar.Visible = false;
                btnLimpiarCampos.Visible = false;
                LlenarCampos();
            }
            else if (btnEditar.Text == "Cancelar")
            {
                btnEditar.Text = "Editar";
                btnAgregar.Text = "Agregar";
                btnEliminar.Visible = true;
                btnLimpiarCampos.Visible = true;
                LimpiarCampos();
            }
        }
    }
}
