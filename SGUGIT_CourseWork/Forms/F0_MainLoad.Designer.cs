﻿namespace SGUGIT_CourseWork.Forms
{
    partial class F0_MainLoad
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
            this.header1 = new SGUGIT_CourseWork.customControl.Header();
            this.cs_FormUpdater1 = new SGUGIT_CourseWork.customControl.cs_FormUpdater();
            this.SuspendLayout();
            // 
            // header1
            // 
            this.header1.Dock = System.Windows.Forms.DockStyle.Top;
            this.header1.Form = this;
            this.header1.Location = new System.Drawing.Point(0, 0);
            this.header1.MaximumSize = new System.Drawing.Size(0, 25);
            this.header1.MinimumSize = new System.Drawing.Size(0, 25);
            this.header1.Name = "header1";
            this.header1.Size = new System.Drawing.Size(800, 25);
            this.header1.TabIndex = 0;
            this.header1.Text = "header1";
            // 
            // cs_FormUpdater1
            // 
            this.cs_FormUpdater1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
            this.cs_FormUpdater1.BackColorNum = 2;
            this.cs_FormUpdater1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cs_FormUpdater1.ForeColorNum = 1;
            this.cs_FormUpdater1.Form = this;
            this.cs_FormUpdater1.onColorUpdate = false;
            this.cs_FormUpdater1.onFormUpdate = false;
            // 
            // F0_MainLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.header1);
            this.Font = new System.Drawing.Font("Golos Text", 14F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "F0_MainLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F0_MainLoad";
            this.ResumeLayout(false);

        }

        #endregion

        private customControl.cs_FormUpdater cs_FormUpdater1;
        private customControl.Header header1;
    }
}