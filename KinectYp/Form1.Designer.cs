namespace KinectYp {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.lblNormal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNormal
            // 
            this.lblNormal.AutoSize = true;
            this.lblNormal.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNormal.Location = new System.Drawing.Point(114, 30);
            this.lblNormal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNormal.Name = "lblNormal";
            this.lblNormal.Size = new System.Drawing.Size(119, 38);
            this.lblNormal.TabIndex = 4;
            this.lblNormal.Text = "Rechts";
            this.lblNormal.Click += new System.EventHandler(this.lblNormal_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 104);
            this.Controls.Add(this.lblNormal);
            this.Location = new System.Drawing.Point(1800, 300);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Spieler 1 Ausrichtung";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNormal;
    }
}

