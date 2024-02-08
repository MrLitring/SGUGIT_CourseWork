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
        public string pathOldDataBase;
        public string pathNewDataBase;
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
                        string fullPath = Path.Combine(pathNewDataBase, $"{textBox1.Text}.db");

                        CreateDataBase(fullPath);

                        break;
                    }

                case "buttonBrowser1":
                    {
                        FolderBrowserDialog dialog = new FolderBrowserDialog();
                        dialog.Description = "Выберите папку для сохранения файла";

                        DialogResult result = dialog.ShowDialog();

                        if (result == DialogResult.OK)
                            pathNewDataBase = @dialog.SelectedPath;
                        else
                            pathNewDataBase = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        textBox2.Text = pathNewDataBase;
                        break;
                    }

                case "buttonBrowser2":
                    {
                        pathOldDataBase = FIleBrowser("SQLite файл (*.sqlite)|*.sqlite");
                        textBox3.Text = pathOldDataBase;
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
            DataBaseCreate baseCreate = new DataBaseCreate(
                pathTxt,
                pathOldDataBase,
                fullPath
                );

            baseCreate.CreateNewTables();
            this.Close();
        }




    }

    internal class DataBaseCreate
    {
        string pathTxt;
        string pathOldDataBase;
        string pathNewDataBase;

        SQLiteConnection oldConnect;
        SQLiteConnection newConnect;
        int CountColumn = 0;


        public DataBaseCreate(string pathTxt, string pathOldDataBase, string pathNewDataBase)
        {
            this.pathTxt = pathTxt;
            this.pathOldDataBase = pathOldDataBase;
            this.pathNewDataBase = pathNewDataBase;
        }

        public void CreateNewTables()
        {
            oldConnect = new SQLiteConnection($"Data Source = {pathOldDataBase}; Version = 3;");
            newConnect = new SQLiteConnection($"Data Source = {pathNewDataBase}; Version = 3;");

            oldConnect.Open();
            newConnect.Open();

            if (pathOldDataBase.EndsWith(".sqlite"))
                DataBase_Sqlite();

            SecondTable();
            MessageBox.Show("База данных создана - Успешно");
            newConnect.Close();
            oldConnect.Close();
        }

        private void DataBase_Sqlite()
        {
            string query = "PRAGMA table_info(Данные)";

            using (SQLiteCommand command = new SQLiteCommand(query, oldConnect))
            {
                SQLiteDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CountColumn++;
                    }
                }
            }

            string[] columns = new string[CountColumn];
            for (int i = 0; i < CountColumn; i++)
            {
                columns[i] =$"\" {i}\"";
            }
            string[] types = new string[CountColumn];
            for (int i = 0; i < CountColumn; i++)
            {
                types[i] = "Real";
            }
            columns[0] = "Эпоха";
            types[0] = "Integer";

            HelperCode.SqlCode.DataToCreateTable tableCreater = new DataToCreateTable(
                "FirstData",
                columns,
                types
                );

            tableCreater.SetConnection(newConnect);
            tableCreater.ExecuteCreate();


            query = "Select * from (Данные);";
            using(SQLiteCommand command = new SQLiteCommand(query,oldConnect))
            {
                SQLiteDataReader reader = command.ExecuteReader();
                HelperCode.SqlCode.DataToInsert insert = new DataToInsert("FirstData", columns, new object[] { });
                insert.SetConnection(newConnect);

                if (reader.HasRows)
                {
                    object[] doubles = new object[CountColumn];
                    while (reader.Read())
                    {
                        for(int i = 0 ; i < CountColumn; i++)
                        {
                            doubles[i] = reader.GetDouble(i);
                        }
                        insert.UpdateValues(doubles);
                        insert.ExecuteInsert();
                    }
                    
                }
            }
        }


        private void FisrtTable()
        {

        }

        private void SecondTable()
        {
            HelperCode.SqlCode.DataToCreateTable createTable = new DataToCreateTable(
                "SecondData",
                new string[] { "A", "E", "BlockCount", "Image" },
                new string[] { "Real", "Real", "integer", "BLOB"}
                );

            createTable.SetConnection(newConnect);
            createTable.ExecuteCreate();

            HelperCode.SqlCode.DataToInsert insert = new DataToInsert(
                "SecondData",
                new string[] { "A", "E", "BlockCount", "Image" },
                new object[] {0.1, 0.01, 1, 0}
                );

            insert.SetConnection(newConnect);
            insert.ExecuteInsert();
        }


    }

}

