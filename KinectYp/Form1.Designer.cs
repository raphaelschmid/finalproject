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
            this.lblKick = new System.Windows.Forms.Label();
            this.lblFootPosition = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.lblHeadPosition = new System.Windows.Forms.Label();
            this.lblNormal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblKick
            // 
            this.lblKick.AutoSize = true;
            this.lblKick.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKick.Location = new System.Drawing.Point(16, 9);
            this.lblKick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKick.Name = "lblKick";
            this.lblKick.Size = new System.Drawing.Size(120, 33);
            this.lblKick.TabIndex = 0;
            this.lblKick.Text = "Ausgabe";
            this.lblKick.Click += new System.EventHandler(this.lblKick_Click);
            // 
            // lblFootPosition
            // 
            this.lblFootPosition.AutoSize = true;
            this.lblFootPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFootPosition.Location = new System.Drawing.Point(14, 410);
            this.lblFootPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFootPosition.Name = "lblFootPosition";
            this.lblFootPosition.Size = new System.Drawing.Size(185, 44);
            this.lblFootPosition.TabIndex = 1;
            this.lblFootPosition.Text = "FootRight";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(17, 845);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(226, 29);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "evtl. Fehlermeldung";
            // 
            // lblHeadPosition
            // 
            this.lblHeadPosition.AutoSize = true;
            this.lblHeadPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadPosition.Location = new System.Drawing.Point(14, 606);
            this.lblHeadPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeadPosition.Name = "lblHeadPosition";
            this.lblHeadPosition.Size = new System.Drawing.Size(110, 44);
            this.lblHeadPosition.TabIndex = 3;
            this.lblHeadPosition.Text = "Head";
            // 
            // lblNormal
            // 
            this.lblNormal.AutoSize = true;
            this.lblNormal.Font = new System.Drawing.Font("Consolas", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNormal.Location = new System.Drawing.Point(19, 337);
            this.lblNormal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNormal.Name = "lblNormal";
            this.lblNormal.Size = new System.Drawing.Size(105, 33);
            this.lblNormal.TabIndex = 4;
            this.lblNormal.Text = "normal";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 883);
            this.Controls.Add(this.lblNormal);
            this.Controls.Add(this.lblHeadPosition);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblFootPosition);
            this.Controls.Add(this.lblKick);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKick;
        private System.Windows.Forms.Label lblFootPosition;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblHeadPosition;
        private System.Windows.Forms.Label lblNormal;
    }
}

