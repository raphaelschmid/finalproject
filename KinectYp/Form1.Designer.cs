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
            this.SuspendLayout();
            // 
            // lblKick
            // 
            this.lblKick.AutoSize = true;
            this.lblKick.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKick.Location = new System.Drawing.Point(13, 37);
            this.lblKick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKick.Name = "lblKick";
            this.lblKick.Size = new System.Drawing.Size(157, 52);
            this.lblKick.TabIndex = 0;
            this.lblKick.Text = "normal";
            this.lblKick.Click += new System.EventHandler(this.lblKick_Click);
            // 
            // lblFootPosition
            // 
            this.lblFootPosition.AutoSize = true;
            this.lblFootPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFootPosition.Location = new System.Drawing.Point(13, 119);
            this.lblFootPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFootPosition.Name = "lblFootPosition";
            this.lblFootPosition.Size = new System.Drawing.Size(212, 52);
            this.lblFootPosition.TabIndex = 1;
            this.lblFootPosition.Text = "FootRight";
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblError.Location = new System.Drawing.Point(13, 756);
            this.lblError.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(302, 38);
            this.lblError.TabIndex = 2;
            this.lblError.Text = "evtl. Fehlermeldung";
            // 
            // lblHeadPosition
            // 
            this.lblHeadPosition.AutoSize = true;
            this.lblHeadPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadPosition.Location = new System.Drawing.Point(13, 335);
            this.lblHeadPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeadPosition.Name = "lblHeadPosition";
            this.lblHeadPosition.Size = new System.Drawing.Size(127, 52);
            this.lblHeadPosition.TabIndex = 3;
            this.lblHeadPosition.Text = "Head";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 883);
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
    }
}

