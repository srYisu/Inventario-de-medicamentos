namespace InventarioMedicamentos
{
    partial class FormInicioSesion
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
            label5 = new Label();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(273, 360);
            label4.Name = "label4";
            label4.Size = new Size(141, 15);
            label4.TabIndex = 27;
            label4.Text = "¿Olvidaste tu contraseña?";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(769, 65);
            label3.TabIndex = 26;
            label3.Text = "IMSS BIENESTAR Puerto Peñasco";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(272, 316);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 25;
            label2.Text = "Contraseña";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(272, 253);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 24;
            label1.Text = "Usuario";
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Location = new Point(364, 389);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(96, 23);
            btnIniciarSesion.TabIndex = 23;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.UseVisualStyleBackColor = true;
            // 
            // txtContrasena
            // 
            txtContrasena.Location = new Point(273, 334);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.Size = new Size(254, 23);
            txtContrasena.TabIndex = 22;
            // 
            // txtUsuario
            // 
            txtUsuario.Location = new Point(273, 271);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(254, 23);
            txtUsuario.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(88, 425);
            label5.Name = "label5";
            label5.Size = new Size(38, 15);
            label5.TabIndex = 28;
            label5.Text = "label5";
            // 
            // FormInicioSesion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnIniciarSesion);
            Controls.Add(txtContrasena);
            Controls.Add(txtUsuario);
            Name = "FormInicioSesion";
            Text = "FormInicioSesion";
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
        private Label label5;
    }
}