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
        FormHelperCode formHelperCode;



        public F2_NewDataBase()
        {
            InitializeComponent();

            formHelperCode = new FormHelperCode();
        }



        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != string.Empty )
            {
                if(textBox3.Text != string.Empty)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), textBox1.Text + ".db");
                    CreateDataBase(path);
                }
            }

        }

        private void buttonBrowser_Click(object sender, EventArgs e)
        {
            string filter = "SQL lite(*.sqlite)|*.sqlite|SQL DB(*.db)|*.db";
            textBox3.Text = formHelperCode.FIleBrowser(filter);

            if (textBox3.Text != null)
            {
                int count = 0;
                List<string> list = new List<string>();
                Tables_Info(
                    new SQLiteConnection(GeneralData.Generate_SQLConnection(textBox3.Text)),
                    out count,
                    out list);


                comboBox1.Items.Clear();
                comboBox1.Items.Add("None");
                for (int i = 0; i < count; i++)
                    comboBox1.Items.Add(list[i]);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void CreateDataBase(string fullPath)
        {
            if (File.Exists(fullPath) == true)
            {
                MessageBox.Show("База данных с таким именем уже существует!", "owubka", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SQLiteConnection.CreateFile(fullPath);
            SQLiteConnection connection = new SQLiteConnection(GeneralData.Generate_SQLConnection(fullPath));
            GeneralData.MainConnection = connection;
            connection.Open();


            SQLTableManager table_first = Table_First(connection);
            table_first.Execute();
            SQLTableManager table_second = Table_Second(connection);
            table_second.Execute();

            SQLiteConnection old = new SQLiteConnection(GeneralData.Generate_SQLConnection(textBox3.Text));
            DataLoad(old, connection, table_first.MinCount);

            SQLDataManager data = new SQLDataManager(GeneralData.TableName_Second, connection, SQLDataManager.executionNumber.Insert);
            data.AddValue("A", GeneralData.smoothValue);
            data.Execute();
            data = new SQLDataManager(GeneralData.TableName_Second, connection, SQLDataManager.executionNumber.Update);
            data.AddValue("E", GeneralData.assureValue);
            data.Execute();
            data = new SQLDataManager(GeneralData.TableName_Second, connection, SQLDataManager.executionNumber.Update);
            data.AddValue("BlockCount", GeneralData.blockCount);
            data.Execute();


            connection.Close();
            this.Close();
        }

        private SQLTableManager Table_First(SQLiteConnection connection)
        {
            SQLTableManager table = new SQLTableManager(connection, GeneralData.TableName_First);
            List<string> names;
            List<string> values;
            string name = "Данные";
            if (comboBox1.SelectedIndex >= 1)
                name = comboBox1.Items[comboBox1.SelectedIndex].ToString();

            SQLiteConnection oldCon = new SQLiteConnection(GeneralData.Generate_SQLConnection(textBox3.Text));
            Table_Columns(oldCon, name, out _, out names, out values);
            table.AddColumnName(names, values);

            return table;
        }

        private SQLTableManager Table_Second(SQLiteConnection connection)
        {
            SQLTableManager table = new SQLTableManager(connection, GeneralData.TableName_Second);
            table.AddColumnName("A", SQLTableManager.ValueType.real);
            table.AddColumnName("E", SQLTableManager.ValueType.real);
            table.AddColumnName("BlockCount", SQLTableManager.ValueType.integer);
            table.AddColumnName("Image", SQLTableManager.ValueType.blob);

            return table;
        }

        private void DataLoad(SQLiteConnection oldConnection, SQLiteConnection newConnection, int min)
        {
            oldConnection.Open();

            string oldQuery = $"Select * from Данные";
            if (comboBox1.SelectedIndex >= 1) oldQuery = $"Select * from {comboBox1.SelectedItem}";
            SQLiteCommand command = new SQLiteCommand(oldQuery, oldConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<SQLDataManager> datas = new List<SQLDataManager>();

            if(reader.HasRows)
            {

                while(reader.Read())
                {
                    SQLDataManager data = new SQLDataManager(
                        GeneralData.TableName_First, 
                        newConnection,
                        SQLDataManager.executionNumber.Insert);

                    for(int i = 0; i < min; i++)
                    {
                        data.AddValue(reader.GetName(i), Convert.ToDouble(reader.GetValue(i)));
                    }
                    datas.Add(data);
                }
            }


            foreach (SQLDataManager data in datas)
                data.Execute();

        }

        private void Table_Columns(SQLiteConnection connection, string tableName, out int Count, out List<string> ValueName, out List<string> ValueType)
        {
            string query = $"PRAGMA table_info({tableName})";
            List<string> valueName = new List<string>();
            List<string> valueType = new List<string>();
            int count = 0;

            if (!(connection.State == System.Data.ConnectionState.Open))
            {
                connection.Open();
            }

            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                count++;
                valueName.Add(reader.GetValue(1).ToString());
                valueType.Add(reader.GetValue(2).ToString());
            }

            Count = count;
            ValueName = valueName;
            ValueType = valueType;
        }

        private void Tables_Info(SQLiteConnection connection, out int Count, out List<string> ValueName)
        {
            string query = "SELECT name FROM sqlite_master WHERE type='table';";
            List<string> valueName = new List<string>();
            int count = 0;

            connection.Open();
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    count++;
                    valueName.Add(reader.GetValue(0).ToString());
                }
            }

            Count = count;
            ValueName = valueName;
        }

    }

}

