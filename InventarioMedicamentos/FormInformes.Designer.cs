namespace InventarioMedicamentos
{
    partial class FormInformes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInformes));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            PictureBoxSalir = new Guna.UI2.WinForms.Guna2PictureBox();
            panelNaranja = new Guna.UI2.WinForms.Guna2Panel();
            panelRojo = new Guna.UI2.WinForms.Guna2Panel();
            lblTitulo = new Guna.UI2.WinForms.Guna2HtmlLabel();
            dgvInformes = new Guna.UI2.WinForms.Guna2DataGridView();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)PictureBoxSalir).BeginInit();
            panelNaranja.SuspendLayout();
            panelRojo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInformes).BeginInit();
            SuspendLayout();
            // 
            // PictureBoxSalir
            // 
            PictureBoxSalir.Cursor = Cursors.Hand;
            PictureBoxSalir.CustomizableEdges = customizableEdges7;
            PictureBoxSalir.Image = (Image)resources.GetObject("PictureBoxSalir.Image");
            PictureBoxSalir.ImageRotate = 0F;
            PictureBoxSalir.Location = new Point(12, 779);
            PictureBoxSalir.Name = "PictureBoxSalir";
            PictureBoxSalir.ShadowDecoration.CustomizableEdges = customizableEdges8;
            PictureBoxSalir.Size = new Size(70, 70);
            PictureBoxSalir.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxSalir.TabIndex = 48;
            PictureBoxSalir.TabStop = false;
            PictureBoxSalir.Click += PictureBoxSalir_Click;
            // 
            // panelNaranja
            // 
            panelNaranja.BackColor = Color.FromArgb(163, 126, 51);
            panelNaranja.Controls.Add(panelRojo);
            panelNaranja.CustomizableEdges = customizableEdges11;
            panelNaranja.Location = new Point(-1, -5);
            panelNaranja.Name = "panelNaranja";
            panelNaranja.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelNaranja.Size = new Size(1586, 92);
            panelNaranja.TabIndex = 49;
            // 
            // panelRojo
            // 
            panelRojo.BackColor = Color.FromArgb(105, 32, 58);
            panelRojo.Controls.Add(lblTitulo);
            panelRojo.CustomizableEdges = customizableEdges9;
            panelRojo.Location = new Point(0, 0);
            panelRojo.Name = "panelRojo";
            panelRojo.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelRojo.Size = new Size(1586, 84);
            panelRojo.TabIndex = 8;
            // 
            // lblTitulo
            // 
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI Semibold", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(669, 8);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(239, 67);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "INFORMES";
            // 
            // dgvInformes
            // 
            dgvInformes.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle5.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvInformes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(100, 88, 255);
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvInformes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvInformes.ColumnHeadersHeight = 25;
            dgvInformes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.White;
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle7.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            dgvInformes.DefaultCellStyle = dataGridViewCellStyle7;
            dgvInformes.GridColor = Color.FromArgb(231, 229, 255);
            dgvInformes.Location = new Point(99, 314);
            dgvInformes.Name = "dgvInformes";
            dgvInformes.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = Color.White;
            dataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            dgvInformes.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            dgvInformes.RowHeadersVisible = false;
            dgvInformes.Size = new Size(1436, 531);
            dgvInformes.TabIndex = 50;
            dgvInformes.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dgvInformes.ThemeStyle.AlternatingRowsStyle.Font = new Font("Segoe UI", 9F);
            dgvInformes.ThemeStyle.AlternatingRowsStyle.ForeColor = SystemColors.ControlText;
            dgvInformes.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvInformes.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dgvInformes.ThemeStyle.BackColor = Color.White;
            dgvInformes.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            dgvInformes.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dgvInformes.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvInformes.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvInformes.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvInformes.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvInformes.ThemeStyle.HeaderStyle.Height = 25;
            dgvInformes.ThemeStyle.ReadOnly = false;
            dgvInformes.ThemeStyle.RowsStyle.BackColor = Color.White;
            dgvInformes.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInformes.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvInformes.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dgvInformes.ThemeStyle.RowsStyle.Height = 25;
            dgvInformes.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dgvInformes.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(194, 179);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(440, 17);
            guna2HtmlLabel1.TabIndex = 51;
            guna2HtmlLabel1.Text = "Nota: Desconozco que sse hacce aqui, asi que pon los elementos y yo los decoro ;p";
            // 
            // FormInformes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 861);
            Controls.Add(guna2HtmlLabel1);
            Controls.Add(dgvInformes);
            Controls.Add(panelNaranja);
            Controls.Add(PictureBoxSalir);
            Name = "FormInformes";
            Text = "FormInformes";
            Load += FormInformes_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBoxSalir).EndInit();
            panelNaranja.ResumeLayout(false);
            panelRojo.ResumeLayout(false);
            panelRojo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInformes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox PictureBoxSalir;
        private Guna.UI2.WinForms.Guna2Panel panelNaranja;
        private Guna.UI2.WinForms.Guna2Panel panelRojo;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitulo;
        private Guna.UI2.WinForms.Guna2DataGridView dgvInformes;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
    }
}