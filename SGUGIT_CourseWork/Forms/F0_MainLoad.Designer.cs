namespace SGUGIT_CourseWork.Forms
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
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripNewDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.StripOpenDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.StripClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuWorkBench = new System.Windows.Forms.ToolStripMenuItem();
            this.StripEditDataBase = new System.Windows.Forms.ToolStripMenuItem();
            this.рабочиеОкнаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripNewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обАвтореToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripNewDataBase,
            this.StripOpenDataBase,
            this.toolStripSeparator2,
            this.StripClose});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // StripNewDataBase
            // 
            this.StripNewDataBase.Name = "StripNewDataBase";
            this.StripNewDataBase.Size = new System.Drawing.Size(180, 22);
            this.StripNewDataBase.Text = "Новый проект";
            this.StripNewDataBase.Click += new System.EventHandler(this.MenuStrip_File_Click);
            // 
            // StripOpenDataBase
            // 
            this.StripOpenDataBase.Name = "StripOpenDataBase";
            this.StripOpenDataBase.Size = new System.Drawing.Size(180, 22);
            this.StripOpenDataBase.Text = "Открыть";
            this.StripOpenDataBase.Click += new System.EventHandler(this.MenuStrip_File_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // StripClose
            // 
            this.StripClose.Name = "StripClose";
            this.StripClose.Size = new System.Drawing.Size(180, 22);
            this.StripClose.Text = "Выход";
            this.StripClose.Click += new System.EventHandler(this.MenuStrip_File_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Font = new System.Drawing.Font("Golos Text", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.MenuWorkBench,
            this.рабочиеОкнаToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 32);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(816, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuWorkBench
            // 
            this.MenuWorkBench.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripEditDataBase});
            this.MenuWorkBench.Name = "MenuWorkBench";
            this.MenuWorkBench.Size = new System.Drawing.Size(165, 20);
            this.MenuWorkBench.Text = "Рабочее пространство";
            this.MenuWorkBench.Click += new System.EventHandler(this.MenuStrip_WorkBench_Click);
            // 
            // StripEditDataBase
            // 
            this.StripEditDataBase.Name = "StripEditDataBase";
            this.StripEditDataBase.Size = new System.Drawing.Size(180, 22);
            this.StripEditDataBase.Text = "База данных";
            this.StripEditDataBase.Click += new System.EventHandler(this.MenuStrip_WorkBench_Click);
            // 
            // рабочиеОкнаToolStripMenuItem
            // 
            this.рабочиеОкнаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripNewWindow});
            this.рабочиеОкнаToolStripMenuItem.Name = "рабочиеОкнаToolStripMenuItem";
            this.рабочиеОкнаToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.рабочиеОкнаToolStripMenuItem.Text = "Рабочие окна";
            this.рабочиеОкнаToolStripMenuItem.Click += new System.EventHandler(this.MenuStrip_Windows_Click);
            // 
            // StripNewWindow
            // 
            this.StripNewWindow.Name = "StripNewWindow";
            this.StripNewWindow.Size = new System.Drawing.Size(149, 22);
            this.StripNewWindow.Text = "Новое окно";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обАвтореToolStripMenuItem,
            this.обПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // обАвтореToolStripMenuItem
            // 
            this.обАвтореToolStripMenuItem.Name = "обАвтореToolStripMenuItem";
            this.обАвтореToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.обАвтореToolStripMenuItem.Text = "Об авторе";
            // 
            // обПрограммеToolStripMenuItem
            // 
            this.обПрограммеToolStripMenuItem.Name = "обПрограммеToolStripMenuItem";
            this.обПрограммеToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.обПрограммеToolStripMenuItem.Text = "Об программе";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(32, 19);
            this.toolStripMenuItem4.Text = "toolStripMenuItem4";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 478);
            this.panel1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 488);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 11, 0);
            this.statusStrip1.Size = new System.Drawing.Size(816, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // F0_MainLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 510);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.Black;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "F0_MainLoad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F0_MainLoad";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StripNewDataBase;
        private System.Windows.Forms.ToolStripMenuItem StripOpenDataBase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem StripClose;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обАвтореToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem обПрограммеToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuWorkBench;
        private System.Windows.Forms.ToolStripMenuItem StripEditDataBase;
        private System.Windows.Forms.ToolStripMenuItem рабочиеОкнаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StripNewWindow;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}