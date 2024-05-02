﻿using SGUGIT_CourseWork.HelperCode;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F0_MainLoad : Form
    {
        private FormHelperCode formHelper;



        public F0_MainLoad()
        {
            InitializeComponent();
            GeneralData.MainConnection = new SQLiteConnection();

            formHelper = new FormHelperCode();
            formHelper.ControlOut_Set(panel1);
        }



        private void F0_MainLoad_Load(object sender, EventArgs e)
        {
            if(GeneralData.MainConnection.State == System.Data.ConnectionState.Open)
            {
                this.toolStripStatusLabel1.Text = GeneralData.DataBasePath;
            }
        }

        private void DataBaseOpen(string path)
        {
            if (path == null) return;

            formHelper.CloseAllForms();
            GeneralData.MainConnection.Close();
            GeneralData.DataBasePath = path;
            GeneralData.MainConnection = new SQLiteConnection(GeneralData.GenerateConnection_string(path));
            GeneralData.MainConnection.Open();
            GeneralData.DataFullUpdate();
            this.toolStripStatusLabel1.Text = GeneralData.DataBasePath;
        }

        //
        // Action MenuStrip onClick
        //
        private void MenuStrip_File_Click(object sender, EventArgs e)
        {
            switch ((sender as ToolStripItem).Name)
            {
                case "StripClose":
                    {
                        Application.Exit();
                        break;
                    }

                case "StripNewDataBase":
                    {
                        formHelper.PageLoad(new F2_NewDataBase());
                        break;
                    }
                case "StripOpenDataBase":
                    {
                        DataBaseOpen(formHelper.FIleBrowser("DB files(*.db)|*.db"));
                        break;
                    }
            }

        }

        private void MenuStrip_WorkBench_Click(object sender, EventArgs e)
        {
            if (GeneralData.MainConnection.State != System.Data.ConnectionState.Open) return;

            switch ((sender as ToolStripMenuItem).Name)
            {
                case "StripEditDataBase":
                    {
                        formHelper.PageLoad(new P1_DataBase());
                        break;
                    }
                case "StripLevel1":
                    {
                        HelperCode.FormOpenCode.OpenForm(new P2_Level1(), panel1);
                        break;
                    }
                case "StripLevel2":
                    {
                        HelperCode.FormOpenCode.OpenForm(new P3_Level2(), panel1);
                        break;
                    }
                case "StripLevel3":
                    {
                        //HelperCode.FormOpenCode.OpenForm(new P2_Level1(), panel1);
                        break;
                    }
                case "StripLevel4":
                    {
                        //HelperCode.FormOpenCode.OpenForm(new P2_Level1(), panel1);
                        break;
                    }
                case "StripTest":
                    {
                        //HelperCode.FormOpenCode.OpenForm(new P2_Level1(), panel1);
                        break;
                    }
            }
        }

        private void MenuStrip_Windows_Click(object sender, EventArgs e)
        {
            string named = (sender as ToolStripMenuItem).Name;
            switch (named)
            {
                case "StripNewWindow":
                    {
                        F0_MainLoad form = new F0_MainLoad();
                        form.Show();
                        form.FormBorderStyle = FormBorderStyle.SizableToolWindow;

                        break;
                    }
            }
        }

        

    }
}
