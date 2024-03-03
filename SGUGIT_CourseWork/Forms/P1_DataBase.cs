using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.SqlCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P1_DataBase : Form
    {
        private bool isHasSaved = false;  // Есть ли сохранение?
        private bool isFirstStart = true;
        private List<DataToSave> commandChanges = new List<DataToSave>();
        private DataTable dTable;
        private int cellRowIndex;
        private int cellColumnIndex;


        public P1_DataBase()
        {
            InitializeComponent();
            dTable = new DataTable();

            DataTable_SetData();
            DataText_SetData();
            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            dataGridView1.FirstDisplayedScrollingColumnIndex = 0;

            isFirstStart = false;
            DataSaving(false);
        }

        //
        // Установление данных
        //
        #region
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
        #endregion
        //
        //  Сохранение данных
        //
        #region
        private void Save()
        {
            if (commandChanges.Count == 0)
            {
                return;
            }

            foreach (DataToSave elem in commandChanges)
                elem.ExecuteSave();

            commandChanges.Clear();
            

            if (EventBus.onDataBaseChange != null)
                EventBus.onDataBaseChange.Invoke();
        }

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

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Save();
            DataSaving(false);
        }
        #endregion

        //
        // Изменение данных
        //
        #region
        private void Cell_ValueChange(object sender, DataGridViewCellEventArgs e)
        {
            commandChanges.Add(new DataToSave(
                GeneralData.TableName_First,
                dataGridView1.Columns[e.ColumnIndex].HeaderText,
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value,
                cellRowIndex+1
                ));
            DataSaving(true);
        }

        private void TextBox_ValueChange(object sender, EventArgs e)
        {
            string tableName = GeneralData.TableName_Second;

            if ((sender as TextBox).Name == "textBox1")
            {
                commandChanges.Add(new DataToSave(tableName, "A", textBox1.Text));
                DataSaving(true);
            }
            if ((sender as TextBox).Name == "textBox2")
            {
                commandChanges.Add(new DataToSave(tableName, "E", textBox2.Text));
                DataSaving(true);
            }
            if ((sender as TextBox).Name == "textBox3")
            {
                commandChanges.Add(new DataToSave(tableName, "BlockCount", int.Parse(textBox3.Text)));
                DataSaving(true);
            }

        }
        #endregion
        //
        // Циклы измерений
        //
        #region 
        //
        // Новый цикл измерения
        //
        private void button1_Click(object sender, EventArgs e)
        {
            if (dTable.Rows.Count <= 0) return;

            List<PointColumn> points = new List<PointColumn>();

            for (int i = 0; i < dTable.Columns.Count; i++)
            {
                PointColumn pointColumn = new PointColumn(5, dTable.Rows[dTable.Rows.Count - 1][0]);
                for (int j = 0; j < dTable.Rows.Count; j++)
                {
                    pointColumn.PointAdd(dTable.Rows[j][i]);
                }
                points.Add(pointColumn);
            }

            DataToInsert dataToInsert = new DataToInsert(GeneralData.TableName_First);


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

        //
        // Удалить цикл измерения
        //
        private void button2_Click(object sender, EventArgs e)
        {
            Delete();
        }

        #endregion

        //
        // Какие-то методы
        //
        #region
        private void DataSaving(bool value)
        {
            if (isFirstStart == true) return;

            if(value == true)
            {
                if(toolStripLabelSave.Text.EndsWith("*") == false)
                {
                    toolStripLabelSave.Text += "*";
                }
            }

            if(value == false)
            {
                if (toolStripLabelSave.Text.EndsWith("*") == true)
                {
                    toolStripLabelSave.Text.Replace("*", "");
                }
            }

        }
        #endregion

        //
        // Какие-то события
        //
        #region
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cellRowIndex = e.RowIndex;
            cellColumnIndex = e.ColumnIndex;
        }
        #endregion
    }

}
