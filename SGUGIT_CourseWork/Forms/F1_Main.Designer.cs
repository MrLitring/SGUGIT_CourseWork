namespace SGUGIT_CourseWork.Forms
{
    partial class F1_Main
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
            this.cs_FormUpdater1 = new SGUGIT_CourseWork.customControl.cs_FormUpdater();
            this.SuspendLayout();
            // 
            // cs_FormUpdater1
            // 
            this.cs_FormUpdater1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(205)))), ((int)(((byte)(219)))));
            this.cs_FormUpdater1.BackColorNum = 4;
            this.cs_FormUpdater1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.cs_FormUpdater1.ForeColorNum = 1;
            this.cs_FormUpdater1.Form = this;
            this.cs_FormUpdater1.onColorUpdate = false;
            this.cs_FormUpdater1.onFormUpdate = false;
            // 
            // F1_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Font = new System.Drawing.Font("Golos Text", 14F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(45)))), ((int)(((byte)(68)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "F1_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F1_Main";
            this.ResumeLayout(false);

        }

        #endregion

        private customControl.cs_FormUpdater cs_FormUpdater1;
    }
}