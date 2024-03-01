using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.SqlCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P1_DataBase : Form
    {
        public bool isHasSaved = false;

        private List<DataToSave> commandChanges = new List<DataToSave>();
        private DataTable dTable;


        public P1_DataBase()
        {
            InitializeComponent();
            dTable = new DataTable();

            DataTable_SetData();
            DataText_SetData();
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            dataGridView1.FirstDisplayedScrollingColumnIndex = 0;
        }

        private void DataTable_SetData()
        {
            dTable.Columns.Clear();
            dTable.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"Select * from {GeneralData.TableName_First} order by 1",
                GeneralData.MainConnection);
            adapter.Fill(dTable);

            for (int col = 0; col < dTable.Columns.Count; col++)
            {
                string ColName = dTable.Columns[col].ColumnName;
                dataGridView1.Columns.Add(ColName, ColName);

                dataGridView1.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dTable.Rows.Count; row++)
            {
                dataGridView1.Rows.Add(dTable.Rows[row].ItemArray);
            }
        }

        private void DataText_SetData()
        {
            SQLiteCommand command = new SQLiteCommand($"Select * From {GeneralData.TableName_Second}", GeneralData.MainConnection);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                if (reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader.GetDouble(0).ToString();
                    textBox2.Text = reader.GetDouble(1).ToString();
                    textBox3.Text = reader.GetInt32(2).ToString();
                }
            }
        }

        //
        //  Сохранение
        //
        #region
        private void DataChange()
        {
            if (commandChanges.Count > 0)
            {
                if (toolStripLabel1.Text.EndsWith("*") == false)
                    toolStripLabel1.Text += "*";
            }
            else
            {
                if (toolStripLabel1.Text.EndsWith("*") == true)
                    toolStripLabel1.Text.Remove(toolStripLabel1.Text.Length - 1);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (commandChanges.Count == 0)
            {
                DataChange();
                return;
            }

            foreach (DataToSave elem in commandChanges)
                elem.ExecuteSave();

            commandChanges.Clear();
            DataChange();

            if (EventBus.onDataBaseChange != null)
                EventBus.onDataBaseChange.Invoke();
        }

        private void Cell_ValueChange(object sender, DataGridViewCellEventArgs e)
        {
            DataToSave dataToSave = new DataToSave(
                GeneralData.TableName_First,
                dataGridView1.Columns[e.ColumnIndex].HeaderText,
                int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()),
                e.RowIndex);

            commandChanges.Add(dataToSave);
            DataChange();
        }

        private void TextBox_ValueChange(object sender, EventArgs e)
        {
            string tableName = GeneralData.TableName_Second;
            bool isSave = false;

            DataToSave dataToSave = new DataToSave();
            if ((sender as TextBox).Name == "textBox1")
            {
                dataToSave.UpdateValues(tableName, "A", textBox1.Text);
                isSave = true;
            }
            if ((sender as TextBox).Name == "textBox2")
            {
                dataToSave.UpdateValues(tableName, "E", textBox2.Text);
                isSave = true;
            }
            if ((sender as TextBox).Name == "textBox3")
            {
                dataToSave.UpdateValues(tableName, "BlockCount", int.Parse(textBox3.Text));
                isSave = true;
            }

            if (isSave == true)
            {
                commandChanges.Add(dataToSave);
                DataChange();
            }
        }
        #endregion
        //
        // Циклы измерений
        //
        #region 
        // Новый цикл измерения
        private void button1_Click(object sender, EventArgs e)
        {
            if (dTable.Rows.Count <= 0) return;

            List<PointColumn> points = new List<PointColumn>();

            for (int i = 0; i < dTable.Columns.Count; i++)
            {
                HelperCode.PointColumn pointColumn = new PointColumn(5, dTable.Rows[dTable.Rows.Count - 1][0]);
                for (int j = 0; j < dTable.Rows.Count; j++)
                {
                    pointColumn.PointAdd(dTable.Rows[j][i]);
                }
                points.Add(pointColumn);
            }

            HelperCode.SqlCode.DataToInsert dataToInsert = new DataToInsert(GeneralData.TableName_First);


            object[] newPoints = new object[points.Count];
            newPoints[0] = points[0].Time;
            for (int i = 1; i < points.Count; i++)
            {
                newPoints[i] = points[i].NewH();
            }

            dataToInsert.UpdateValues(newPoints);
            dataToInsert.ExecuteInsert();

            DataTable_SetData();
            dataGridView1.CurrentCell = dataGridView1.Rows[dTable.Rows.Count].Cells[cellColumnIndex];
        }

        // Удалить цикл измерения
        private int cellRowIndex;
        private int cellColumnIndex;
        private void button2_Click(object sender, EventArgs e)
        {
            Delete();
        }
        #endregion

        private void Delete()
        {
            if (cellRowIndex >= 0 && cellRowIndex < dTable.Rows.Count)
            {
                SQLiteConnection connection = GeneralData.MainConnection;

                string content = $"DELETE FROM {GeneralData.TableName_First} WHERE Эпоха = {dTable.Rows[cellRowIndex][0]}";
                SQLiteCommand command = new SQLiteCommand(content, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                
                DataTable_SetData();
                dataGridView1.CurrentCell = dataGridView1.Rows[cellRowIndex].Cells[cellColumnIndex];
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cellRowIndex = e.RowIndex;
           cellColumnIndex = e.ColumnIndex;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar == 46)
            {
                Debug.Write(((int)e.KeyChar).ToString());
                Delete();
            }
        }

    }

}
