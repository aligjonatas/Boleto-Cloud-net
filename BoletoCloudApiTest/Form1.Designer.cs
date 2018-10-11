namespace BoletoCloudApiTest
{
    partial class Form1
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
            this.btnCriaBoleto = new System.Windows.Forms.Button();
            this.btnConsultaBoleto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCriaBoleto
            // 
            this.btnCriaBoleto.Location = new System.Drawing.Point(22, 29);
            this.btnCriaBoleto.Name = "btnCriaBoleto";
            this.btnCriaBoleto.Size = new System.Drawing.Size(210, 23);
            this.btnCriaBoleto.TabIndex = 1;
            this.btnCriaBoleto.Text = "Cria Boleto";
            this.btnCriaBoleto.UseVisualStyleBackColor = true;
            this.btnCriaBoleto.Click += new System.EventHandler(this.btnCriaBoleto_Click);
            // 
            // btnConsultaBoleto
            // 
            this.btnConsultaBoleto.Location = new System.Drawing.Point(22, 81);
            this.btnConsultaBoleto.Name = "btnConsultaBoleto";
            this.btnConsultaBoleto.Size = new System.Drawing.Size(210, 23);
            this.btnConsultaBoleto.TabIndex = 2;
            this.btnConsultaBoleto.Text = "Consulta Boleto";
            this.btnConsultaBoleto.UseVisualStyleBackColor = true;
            this.btnConsultaBoleto.Click += new System.EventHandler(this.btnConsultaBoleto_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 202);
            this.Controls.Add(this.btnConsultaBoleto);
            this.Controls.Add(this.btnCriaBoleto);
            this.Name = "Form1";
            this.Text = "Testes";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCriaBoleto;
        private System.Windows.Forms.Button btnConsultaBoleto;
    }
}

