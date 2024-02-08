using SGUGIT_CourseWork.HelperCode;
using System;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Microsoft.Office.Interop.Excel;
using SGUGIT_CourseWork.HelperCode.SqlCode;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F2_NewDataBase : Form
    {
        public string pathExcell;
        public string pathDB;
        public string pathTxt;
        public string nameDB;


        private FormMoved formMoved;

        public F2_NewDataBase()
        {
            InitializeComponent();
            formMoved = new FormMoved(this);
            formMoved.ControlAdd(this);
            formMoved.ControlAdd(label1);
            formMoved.ControlAdd(label2);
            formMoved.ControlAdd(label3);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string buttonName = (sender as System.Windows.Forms.Button).Name;

            switch (buttonName)
            {
                case "buttonCancel":
                    {
                        this.Close();
                        break;
                    }

                case "buttonCreate":
                    {
                        string fullPath = Path.Combine(pathDB, $"{textBox1.Text}.db");

                        CreateDataBase(fullPath);

                        break;
                    }

                case "buttonBrowser1":
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        dialog.Description = "Выберите папку для сохранения файла";
                        
                        DialogResult result = dialog.ShowDialog();

                        if (result == DialogResult.OK)
                            pathDB = @dialog.SelectedPath;
                        else
                            pathDB = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        textBox2.Text = @pathDB;
                        break;
                    }

                case "buttonBrowser2":
                    {
                        pathExcell = FIleBrowser("SQLite файл (*.sqlite)|*.sqlite");
                        textBox3.Text = pathExcell;
                        break;
                    }

                case "buttonBrowser3":
                    {
                        pathTxt = FIleBrowser("Текстовые файл (*.txt)|*.txt");
                        textBox4.Text = pathTxt;
                        break;
                    }


            }

        }

        private string FIleBrowser(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = filter;

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                HelperCode.SqlCode.SqlMainData.SQLConnection =
                    new SQLiteConnection("Data Source=" + openFileDialog.FileName + ";Version = 3;");
                HelperCode.SqlCode.SqlMainData.SQLConnection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = HelperCode.SqlCode.SqlMainData.SQLConnection;

                return openFileDialog.FileName; ;
            }
            else
                return null;

        }


        private void CreateDataBase(string fullPath)
        {
            SQLiteConnection.CreateFile(fullPath);



            using (SQLiteConnection connection = new SQLiteConnection($@"Data Source = {fullPath}; Version = 3;"))
            {
                HelperCode.SqlCode.SqlMainData.SQLConnection = connection;

                //DataToCreateTable table = new DataToCreateTable()
            }

        }
        private class DataBaseCreate
        {
            int CountColumn = 0;
            int countRow = 0;


            DataBaseCreate()
            {

            }
        }


    }

}

