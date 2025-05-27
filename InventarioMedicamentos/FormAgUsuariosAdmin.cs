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

            bool resultado = consultas.GuardarUsuario(nombre, correo, tipo, contrasena);
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
            AgregarBotonesAccion();
            AjustarAnchoColumnas();
        }
        private void AgregarBotonesAccion()
        {
            // Configuración común para ambos botones
            Action<DataGridViewButtonColumn, string, string> configurarBoton = (col, texto, nombre) =>
            {
                col.Name = nombre;
                col.Text = texto;
                col.HeaderText = "Acción";
                col.UseColumnTextForButtonValue = true;
                col.Width = 80;
                col.FlatStyle = FlatStyle.Flat;
                col.DefaultCellStyle.BackColor = nombre == "Editar" ? Color.LightBlue : Color.LightCoral;
                col.DefaultCellStyle.ForeColor = Color.Black;
            };

            // Botón Editar
            DataGridViewButtonColumn colEditar = new DataGridViewButtonColumn();
            configurarBoton(colEditar, "Editar", "Editar");
            dgvUsuarios.Columns.Add(colEditar);

            // Botón Eliminar
            DataGridViewButtonColumn colEliminar = new DataGridViewButtonColumn();
            configurarBoton(colEliminar, "Eliminar", "Eliminar");
            dgvUsuarios.Columns.Add(colEliminar);
        }
        private void AjustarAnchoColumnas()
        {
            // Ajustar automáticamente el ancho de las columnas de datos
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Fijar un ancho específico para las columnas de botones
            dgvUsuarios.Columns["Editar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvUsuarios.Columns["Eliminar"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        }
        private void EliminarUsuario(int idUsuario)
        {
            // Confirmar antes de eliminar
            DialogResult result = MessageBox.Show(
                $"¿Está seguro que desea eliminar el usuario con ID: {idUsuario}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    //int id = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);
                    consultas.EliminarUsuario(idUsuario);
                    MessageBox.Show("Usuario eliminado correctamente");
                    CargarTabla(); // Recargar datos después de eliminar
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar usuario: {ex.Message}");
                }
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            GuardarUsuario();
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Verificar que el click no sea en el encabezado y que sea en nuestras columnas de botones
            if (e.RowIndex >= 0)
            {
                // Obtener el ID del usuario de la fila clickeada
                int idUsuario = Convert.ToInt32(dgvUsuarios.SelectedRows[0].Cells[0].Value);

                if (dgvUsuarios.Columns[e.ColumnIndex].Name == "Editar")
                {
                    MessageBox.Show($"Funcionalidad de edición no implementada para el usuario con ID: {idUsuario}");
                }
                else if (dgvUsuarios.Columns[e.ColumnIndex].Name == "Eliminar")
                {
                    EliminarUsuario(idUsuario);
                }
            }
        }
    }
}
