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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormInformes));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            PictureBoxSalir = new Guna.UI2.WinForms.Guna2PictureBox();
            ((System.ComponentModel.ISupportInitialize)PictureBoxSalir).BeginInit();
            SuspendLayout();
            // 
            // PictureBoxSalir
            // 
            PictureBoxSalir.Cursor = Cursors.Hand;
            PictureBoxSalir.CustomizableEdges = customizableEdges1;
            PictureBoxSalir.Image = (Image)resources.GetObject("PictureBoxSalir.Image");
            PictureBoxSalir.ImageRotate = 0F;
            PictureBoxSalir.Location = new Point(12, 779);
            PictureBoxSalir.Name = "PictureBoxSalir";
            PictureBoxSalir.ShadowDecoration.CustomizableEdges = customizableEdges2;
            PictureBoxSalir.Size = new Size(70, 70);
            PictureBoxSalir.SizeMode = PictureBoxSizeMode.StretchImage;
            PictureBoxSalir.TabIndex = 48;
            PictureBoxSalir.TabStop = false;
            PictureBoxSalir.Click += PictureBoxSalir_Click;
            // 
            // FormInformes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 861);
            Controls.Add(PictureBoxSalir);
            Name = "FormInformes";
            Text = "FormInformes";
            Load += FormInformes_Load;
            ((System.ComponentModel.ISupportInitialize)PictureBoxSalir).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox PictureBoxSalir;
    }
}