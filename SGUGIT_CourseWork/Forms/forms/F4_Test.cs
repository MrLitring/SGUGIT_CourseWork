using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGUGIT_CourseWork.HelperCode.UI;
using SGUGIT_CourseWork.HelperCode;
using System.Data.SQLite;

namespace SGUGIT_CourseWork.Forms.forms
{
    public partial class F4_Test : Form
    {
        private DataTable dTable;
        private ChartManager chartManager;
        private DataGridViewManager viewManager;
        private FormHelperCode formHelperCode;



        public F4_Test()
        {
            InitializeComponent();
        }

        private void F4_Test_Load(object sender, EventArgs e)
        {
            formHelperCode = new FormHelperCode();
            formHelperCode.ControlOut_Set(panel2);
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1) return;

            DataLoad(comboBox1.SelectedItem.ToString());
            DataTable dataTable = dTable.Clone();
            for (int row = 0; row < dTable.Rows.Count; row++)
            {
                dataTable.ImportRow(dTable.Rows[row]);
            }

            dataTable.Columns.RemoveAt(0);
            DataTableCalculation work = new DataTableCalculation(dataTable);
            work.ColumnFill(false);
            work.Calculation();

            if(comboBox1.SelectedItem.ToString() == "Скачок")
                formHelperCode.PageLoad(new P2_Level1(work.currentDTable, 0));
            else
                formHelperCode.PageLoad(new P2_Level1(work.currentDTable, -1));

        }

        private void DataLoad(string name)
        {
            dTable = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"Select * from [{name}] order by 1",
                GeneralData.MainConnection);
            adapter.Fill(dTable);
        }
    }
}
