﻿namespace SGUGIT_CourseWork.Forms
{
    partial class F1_Window
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.режимРедактированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.рабочиеОкнаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.новоеОкноToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StripLevel1 = new System.Windows.Forms.ToolStripMenuItem();
            this.StripLevel2 = new System.Windows.Forms.ToolStripMenuItem();
            this.StripLevel3 = new System.Windows.Forms.ToolStripMenuItem();
            this.StripLevel4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.режимРедактированияToolStripMenuItem,
            this.рабочиеОкнаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(780, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // режимРедактированияToolStripMenuItem
            // 
            this.режимРедактированияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripDataBase,
            this.StripLevel1,
            this.StripLevel2,
            this.StripLevel3,
            this.StripLevel4});
            this.режимРедактированияToolStripMenuItem.Name = "режимРедактированияToolStripMenuItem";
            this.режимРедактированияToolStripMenuItem.Size = new System.Drawing.Size(144, 20);
            this.режимРедактированияToolStripMenuItem.Text = "Рабочее пространство";
            // 
            // StripDataBase
            // 
            this.StripDataBase.Name = "StripDataBase";
            this.StripDataBase.Size = new System.Drawing.Size(180, 22);
            this.StripDataBase.Text = "База данных";
            this.StripDataBase.Click += new System.EventHandler(this.StripMenu_Space_Click);
            // 
            // рабочиеОкнаToolStripMenuItem
            // 
            this.рабочиеОкнаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.новоеОкноToolStripMenuItem,
            this.toolStripSeparator2,
            this.закрытьToolStripMenuItem});
            this.рабочиеОкнаToolStripMenuItem.Name = "рабочиеОкнаToolStripMenuItem";
            this.рабочиеОкнаToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.рабочиеОкнаToolStripMenuItem.Text = "Рабочие окна";
            // 
            // новоеОкноToolStripMenuItem
            // 
            this.новоеОкноToolStripMenuItem.Name = "новоеОкноToolStripMenuItem";
            this.новоеОкноToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.новоеОкноToolStripMenuItem.Text = "Новое окно";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.закрытьToolStripMenuItem.Text = "Закрыть";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(780, 492);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(780, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StripLevel1
            // 
            this.StripLevel1.Name = "StripLevel1";
            this.StripLevel1.Size = new System.Drawing.Size(180, 22);
            this.StripLevel1.Text = "I Уровень";
            // 
            // StripLevel2
            // 
            this.StripLevel2.Name = "StripLevel2";
            this.StripLevel2.Size = new System.Drawing.Size(180, 22);
            this.StripLevel2.Text = "II Уровень";
            // 
            // StripLevel3
            // 
            this.StripLevel3.Name = "StripLevel3";
            this.StripLevel3.Size = new System.Drawing.Size(180, 22);
            this.StripLevel3.Text = "III Уровень";
            // 
            // StripLevel4
            // 
            this.StripLevel4.Name = "StripLevel4";
            this.StripLevel4.Size = new System.Drawing.Size(180, 22);
            this.StripLevel4.Text = "IV Уровень";
            // 
            // F1_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 538);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "F1_Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F1_Window";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem рабочиеОкнаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem новоеОкноToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem режимРедактированияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StripDataBase;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripLevel1;
        private System.Windows.Forms.ToolStripMenuItem StripLevel2;
        private System.Windows.Forms.ToolStripMenuItem StripLevel3;
        private System.Windows.Forms.ToolStripMenuItem StripLevel4;
    }
}