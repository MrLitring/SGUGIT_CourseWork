using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P2_Level4 : Form
    {
        ChartManager chartManager;


        public P2_Level4()
        {
            InitializeComponent();
        }

        private void P2_Level4_Load(object sender, EventArgs e)
        {
            chartManager = new ChartManager(chart1, checkedListBox1);

            comboBox1.Items.Add("Main");

            for (int i = 0; i < GeneralData.dataTables.Count; i++)
            {
                DataTableCalculation DTCtable = GeneralData.dataTables[i];
                comboBox1.Items.Add(DTCtable.Name);
                
            }

            for (int i = 0; i < GeneralData.dataTables2.Count; i++)
            {
                comboBox1.Items.Add(GeneralData.dataTables[i].Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;
            chartManager.Clear(chart1);

            
            DataTableCalculation DTCtable = DTC_Search(comboBox1.SelectedItem.ToString());
            DataTable table;

            if (comboBox1.SelectedItem.ToString() == "Main")
                table = GeneralData.dataTable;
            else
                table = DTCtable.currentDTable;


            for (int col = 0; col < table.Columns.Count; col++)
            {
                string name = table.Columns[col].ColumnName;
                chartManager.Series_Add(name);
                for (int row = 0; row < table.Rows.Count; row++)
                {
                    chartManager.AddPointY(name, Convert.ToDouble(table.Rows[row][col]));
                }

            }
        }

        private DataTableCalculation DTC_Search(string name)
        {
            foreach (DataTableCalculation elem in GeneralData.dataTables)
            {
                if (elem.Name == name)
                    return elem;
            }
            foreach (DataTableCalculation elem in GeneralData.dataTables2)
            {
                if (elem.Name == name)
                    return elem;
            }

            return null;
        }
    }
}
