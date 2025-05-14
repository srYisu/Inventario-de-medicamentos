namespace InventarioMedicamentos
{
    partial class InicioSesion
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
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnIniciarSesion = new Button();
            txtContrasena = new TextBox();
            txtUsuario = new TextBox();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(277, 361);
            label4.Name = "label4";
            label4.Size = new Size(141, 15);
            label4.TabIndex = 13;
            label4.Text = "¿Olvidaste tu contraseña?";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 10);
            label3.Name = "label3";
            label3.Size = new Size(769, 65);
            label3.TabIndex = 12;
            label3.Text = "IMSS BIENESTAR Puerto Peñasco";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(276, 317);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 11;
            label2.Text = "Contraseña";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(276, 254);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 10;
            label1.Text = "Usuario";
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(368, 390);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(96, 23);
            btnIniciarSesion.TabIndex = 9;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(277, 335);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(254, 23);
            txtContrasena.TabIndex = 8;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(277, 272);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(254, 23);
            txtUsuario.TabIndex = 7;
            // 
            // InicioSesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnIniciarSesion);
            Controls.Add(txtContrasena);
            Controls.Add(txtUsuario);
            Name = "InicioSesion";
            Text = "InicioSesion";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnIniciarSesion;
        private TextBox txtContrasena;
        private TextBox txtUsuario;
    }
}