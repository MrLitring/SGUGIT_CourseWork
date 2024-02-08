using SGUGIT_CourseWork.HelperCode.SqlCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
        }

        private void P1_DataBase_Load(object sender, EventArgs e)
        {
            DataTable_Clear();
            DataTable_SetData();

            DataText_SetClear();
            DataText_SetData();

            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            dataGridView1.FirstDisplayedScrollingColumnIndex = 0;

            if (this.toolStripLabel1.Text.EndsWith("*") == true)
                this.toolStripLabel1.Text = this.toolStripLabel1.Text.Replace("*", "");
            isHasSaved = false;
        }

        private void DataTable_SetData()
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"{SqlMainData.SelectAll("FirstData")} order by 1",
                SqlMainData.SQLConnection);
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

        private void DataTable_Clear()
        {
            dTable.Columns.Clear();
            dTable.Rows.Clear();

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
        }

        private void DataText_SetData()
        {
            SQLiteCommand command = new SQLiteCommand(SqlMainData.SelectAll("SecondData"), SqlMainData.SQLConnection);

            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader.GetDouble(0).ToString();
                    textBox2.Text = reader.GetDouble(1).ToString();
                    textBox3.Text = reader.GetInt32(2).ToString();
                }
            }
        }

        private void DataText_SetClear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        //
        // 
        //
        private void Button1_Click(object sender, EventArgs e)
        {
            DataTable_Clear();
            DataTable_SetData();
        }
        
        //
        //  Сохранение
        //
        private void Save()
        {
            if (commandChanges.Count == 0) return;

            foreach (DataToSave elem in commandChanges)
                elem.ExecuteSave();

            commandChanges.Clear();

            if (this.toolStripLabel1.Text.EndsWith("*") == true)
                this.toolStripLabel1.Text = this.toolStripLabel1.Text.Replace("*", "");
            isHasSaved = false;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Cell_ValueChange(object sender, DataGridViewCellEventArgs e)
        {
            DataToSave dataToSave = new DataToSave(
                "FirstData",
                dataGridView1.Columns[e.ColumnIndex].HeaderText,
                int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()),
                e.RowIndex );

            commandChanges.Add(dataToSave);

            if (toolStripLabel1.Text.EndsWith("*") == false)
                toolStripLabel1.Text += "*";
            isHasSaved = true;
        }

        private void TextBox_ValueChange(object sender, EventArgs e)
        {
            string tableName = "SecondData";
            DataToSave dataToSave = new DataToSave();
            if ((sender as TextBox).Name == "textBox1") 
                dataToSave.UpdateValues(tableName, "A", textBox1.Text);
            if ((sender as TextBox).Name == "textBox2") 
                dataToSave.UpdateValues(tableName, "E", textBox2.Text);
            if ((sender as TextBox).Name == "textBox3") 
                dataToSave.UpdateValues(tableName, "BlockCount", int.Parse(textBox3.Text));

            commandChanges.Add(dataToSave);

            if (toolStripLabel1.Text.EndsWith("*") == false)
                toolStripLabel1.Text += "*";
            isHasSaved = true;
        }
    }

}
