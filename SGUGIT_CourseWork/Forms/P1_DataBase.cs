using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using SGUGIT_CourseWork.HelperCode.SqlCode;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P1_DataBase : Form
    {
        public P1_DataBase()
        {
            InitializeComponent();
            dTable = new DataTable();
        }

        private DataTable dTable;
        private List<DataToSave> commandChanges = new List<DataToSave>();


        private void P1_DataBase_Load(object sender, EventArgs e)
        {
            //panel1.Width = 200;
            DataTable_Clear();
            DataTable_SetData();
        }

        private void DataTable_SetData()
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"{HelperCode.SqlCode.MainData.SelectAll("GenerallData")} order by 1",
                HelperCode.SqlCode.MainData.SQLConnection);
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
        
        //
        //
        //
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable_Clear();
            DataTable_SetData();

        }
        
        //
        //  Сохранение
        //
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //DataToCreateTable dataToCreateTable = new DataToCreateTable
                //( HelperCode.SqlCode.SQLConnection ,"Xed", new string[] { "asd"}, new string[] { "Integer" });

            //dataToCreateTable.ExecuteCreate();

            //if (commandChanges.Count == 0) return;

            //foreach (DataToSave elem in commandChanges) 
                //elem.ExecuteSave();

            //commandChanges.Clear();
            //await Task.Run(() => { GC.Collect(); });
        }

        //
        // Изменение ячейки приводит к изменению и добавление к сохранению
        //
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataToSave dataToSave = new DataToSave
                (
                "GenerallData",
                dataGridView1.Columns[e.ColumnIndex].HeaderText,
                int.Parse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()),
                1
                );

            commandChanges.Add(dataToSave);
        }
    }
}
