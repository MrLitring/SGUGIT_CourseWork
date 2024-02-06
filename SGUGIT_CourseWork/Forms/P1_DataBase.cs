using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P1_DataBase : Form
    {
        private DataTable dTable;

        public P1_DataBase()
        {
            InitializeComponent();
            dTable = new DataTable();
        }

        private void P1_DataBase_Load(object sender, EventArgs e)
        {
            panel1.Width = 200;
            DataTable_Clear();
            DataTAble_SetData();
        }


        private void DataTAble_SetData()
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                "SELECT * FROM [Mama] order by 1",
                HelperCode.SqlCode.SQLConnection);
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

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable_Clear();
            DataTAble_SetData();

        }
    }
}
