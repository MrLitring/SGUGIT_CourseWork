using SGUGIT_CourseWork.HelperCode;
using System;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using SGUGIT_CourseWork.HelperCode.SqlCode;
using System.Collections.Generic;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F2_NewDataBase : Form
    {

        public F2_NewDataBase()
        {
            InitializeComponent();

            textBox2.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            switch ((sender as System.Windows.Forms.Button).Name)
            {
                case "buttonCancel":
                    {
                        this.Close(); 
                        break;
                    }

                case "buttonCreate":
                    {
                        if(
                            ((textBox1.Text.Replace(" ","") == "") ||
                            (textBox2.Text.Replace(" ", "") == "") ||
                            (textBox3.Text.Replace(" ", "") == "")) == true)
                        {
                            MessageBox.Show("Не должно быть пустых строк", "Owubka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        string fullPath = Path.Combine(@textBox2.Text, $"{textBox1.Text}.db");
                        CreateDataBase(fullPath);
                        break;
                    }

                case "buttonBrowser1":
                    {
                        textBox2.Text = FileDialog();
                        break;
                    }

                case "buttonBrowser2":
                    {
                        textBox3.Text = FIleBrowser("SQLite файл (*.sqlite)|*.sqlite");
                        break;
                    }

            }

        }

        private string FileDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "Выберите папку для сохранения файла"
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
                return @dialog.SelectedPath;
            else
                return @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private string FIleBrowser(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = filter
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                return openFileDialog.FileName; ;
            }
            else
                return null;

        }

        private void CreateDataBase(string fullPath)
        {
            if (File.Exists(fullPath) == true)
            {
                MessageBox.Show("База данных с таким именем уже существует!", "owubka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteConnection.CreateFile(fullPath);
            DataBaseCreate baseCreate = new DataBaseCreate(
                textBox3.Text,
                fullPath
                );

            baseCreate.CreateNewTables();
            this.Close();
        }

    }

    internal class DataBaseCreate
    {
        //private const string firstTableName = "FirstData";
        //private const string secondTableName = "SecondData";

        //private string pathOldDataBase;
        //private string pathNewDataBase;

        //SQLiteConnection oldConnect;
        //SQLiteConnection newConnect;


        //public DataBaseCreate(string pathOldDataBase, string pathNewDataBase)
        //{
        //    this.pathOldDataBase = pathOldDataBase;
        //    this.pathNewDataBase = pathNewDataBase;
        //}

        //public void CreateNewTables()
        //{
        //    oldConnect = new SQLiteConnection($"Data Source = {pathOldDataBase}; Version = 3;");
        //    newConnect = new SQLiteConnection($"Data Source = {pathNewDataBase}; Version = 3;");

        //    oldConnect.Open();
        //    newConnect.Open();

        //    FirstTable();
        //    SecondTable_CreateAndInsertData();

        //    MessageBox.Show("База данных создана - Успешно");

        //    newConnect.Close();
        //    oldConnect.Close();
        //}

        //private void FirstTable()
        //{
        //    int countColumn = FirstData_CountColumn();
        //    string[] columns = new string[countColumn];
        //    string[] types = new string[countColumn];
        //    for (int i = 0; i < countColumn; i++)
        //    {
        //        columns[i] = $"\" {i}\"";
        //        types[i] = "Real";
        //    }

        //    columns[0] = "Эпоха";
        //    types[0] = "Integer";

        //    FirstData_CreateTable(columns, types);
        //    FirstData_FillTable(columns, countColumn);
        //}

        //private void SecondTable_CreateAndInsertData()
        //{
        //    DataToCreateTable createTable = new DataToCreateTable(
        //        secondTableName,
        //        new string[] { "A", "E", "BlockCount", "Image" },
        //        new string[] { "Real", "Real", "integer", "BLOB" }
        //        );
        //    //SQLData data = new SQLData(GeneralData.TableName_Second, );


        //    DataToInsert insert = new DataToInsert(
        //        secondTableName,
        //        new string[] { "A", "E", "BlockCount", "Image" },
        //        new object[] { 0.1, 0.01, 1 }
        //        );

        //    createTable.SetConnection(newConnect);
        //    createTable.ExecuteCreate();

        //    insert.SetConnection(newConnect);
        //    insert.ExecuteInsert();
        //}

        //private int FirstData_CountColumn()
        //{
        //    string query = "PRAGMA table_info(Данные)";
        //    int count = 0;

        //    using (SQLiteCommand command = new SQLiteCommand(query, oldConnect))
        //    {
        //        SQLiteDataReader reader = command.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                count++;
        //            }
        //        }
        //    }

        //    return count;
        //}

        //private void FirstData_CreateTable(string[] columns, string[] types)
        //{
        //    DataToCreateTable tableCreater = new DataToCreateTable(
        //        firstTableName,
        //        columns,
        //        types
        //        );

        //    tableCreater.SetConnection(newConnect);
        //    tableCreater.ExecuteCreate();
        //}

        //private void FirstData_FillTable(string[] columns, int сountColumn)
        //{
        //    DataToInsert insert = new DataToInsert(firstTableName, columns, new object[] { });
        //    insert.SetConnection(newConnect);
        //    using (SQLiteCommand command = new SQLiteCommand("Select * from (Данные);", oldConnect))
        //    {
        //        SQLiteDataReader reader = command.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            object[] doubles = new object[сountColumn];
        //            while (reader.Read())
        //            {
        //                for (int i = 0; i < сountColumn; i++)
        //                {
        //                    doubles[i] = reader.GetDouble(i);
        //                }
        //                insert.UpdateValues(doubles);
        //                insert.ExecuteInsert();
        //            }

        //        }

        //    }
        //}

    }


    internal class TableCreate
    {
        private string name;
        private SQLiteConnection connection;
        private List<PointColumn> colums;

       public TableCreate(SQLiteConnection connect,  string name)
        {
            this.connection = connect;
            this.name = name;
            colums = new List<PointColumn>();
        }

        public void ExecuteNewTable()
        {

        }
        
        public void DataOldTable(SQLiteConnection oldConnect, string tableName = "Данные")
        {


            oldConnect.Open();
            SQLiteCommand command = new SQLiteCommand($"Select * from {tableName};");
            SQLiteDataReader reader = command.ExecuteReader();
            
            if(reader.HasRows)
            {

            }




            oldConnect.Close();
        }

        public void DataAddColumn()
        {

        }


        private int ColumnCount(SQLiteConnection connect, string tableName)
        {
            int count = 0;
            string query = $"GRAGMA table_info([{tableName}])";
            connect.Open();



            return count;
        }
    }
}

