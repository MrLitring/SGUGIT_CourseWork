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



        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source = {formHelperCode.FIleBrowser("SQLite файл (*.db)|*.db")}; Version = 3;");
            connection.Open();

            SQLDataTable table = new SQLDataTable(connection, GeneralData.TableName_Second);
            table.AddColumnName("rea", SQLDataTable.ValueType.real);
            table.AddColumnName("int", SQLDataTable.ValueType.integer);
            table.AddColumnName("text", SQLDataTable.ValueType.text);
            table.AddColumnName("blob", SQLDataTable.ValueType.blob);
            table.Execute();

            connection.Close();

        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), textBox1.Text + ".db");
            CreateDataBase(path);
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
                    new SQLiteConnection(GeneralData.GenerateConnection_string(textBox3.Text)),
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
            SQLiteConnection connection = new SQLiteConnection(GeneralData.GenerateConnection_string(fullPath));
            connection.Open();


            SQLDataTable table_first = Table_First(connection);
            table_first.Execute();
            SQLDataTable table_second = Table_Second(connection);
            table_second.Execute();

            SQLiteConnection old = new SQLiteConnection(GeneralData.GenerateConnection_string(textBox3.Text));
            DataLoad(old, connection, table_first.MinCount);

            SQLData data = new SQLData(GeneralData.TableName_Second, connection, SQLData.executionNumber.Update);
            data.AddValue(GeneralData.smoothValue);
            data.AddName("A");
            data.Execute();
            data = new SQLData(GeneralData.TableName_Second, connection, SQLData.executionNumber.Update);
            data.AddValue(GeneralData.assureValue);
            data.AddName("E");
            data.Execute();
            data = new SQLData(GeneralData.TableName_Second, connection, SQLData.executionNumber.Update);
            data.AddValue(GeneralData.blockCount);
            data.AddName("BlockCount");
            data.Execute();


            connection.Close();
            this.Close();
        }

        private SQLDataTable Table_First(SQLiteConnection connection)
        {
            SQLDataTable table = new SQLDataTable(connection, GeneralData.TableName_First);
            List<string> names;
            List<string> values;
            string name = "Данные";
            if (comboBox1.SelectedIndex >= 1)
                name = comboBox1.Items[comboBox1.SelectedIndex].ToString();

            SQLiteConnection oldCon = new SQLiteConnection(GeneralData.GenerateConnection_string(textBox3.Text));
            Table_Columns(oldCon, name, out _, out names, out values);
            table.AddColumnName(names, values);

            return table;
        }

        private SQLDataTable Table_Second(SQLiteConnection connection)
        {
            SQLDataTable table = new SQLDataTable(connection, GeneralData.TableName_Second);
            table.AddColumnName("A", SQLDataTable.ValueType.real);
            table.AddColumnName("E", SQLDataTable.ValueType.real);
            table.AddColumnName("BlockCount", SQLDataTable.ValueType.integer);
            table.AddColumnName("Image", SQLDataTable.ValueType.blob);

            

            return table;
        }

        private void DataLoad(SQLiteConnection oldConnection, SQLiteConnection newConnection, int min)
        {
            oldConnection.Open();

            string oldQuery = $"Select * from Данные";
            if (comboBox1.SelectedIndex >= 1) oldQuery = $"Select * from {comboBox1.SelectedItem}";
            SQLiteCommand command = new SQLiteCommand(oldQuery, oldConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<SQLData> datas = new List<SQLData>();

            if(reader.HasRows)
            {

                while(reader.Read())
                {
                    SQLData data = new SQLData(
                        GeneralData.TableName_First, 
                        newConnection,
                        SQLData.executionNumber.Insert);

                    for(int i = 0; i < min; i++)
                    {
                        data.AddValue(Convert.ToDouble(reader.GetValue(i)));
                        data.AddName(reader.GetName(i));
                    }
                    datas.Add(data);
                }
            }


            foreach (SQLData data in datas)
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

