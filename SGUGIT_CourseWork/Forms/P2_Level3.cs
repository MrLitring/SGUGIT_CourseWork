using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.UI;
using SGUGIT_CourseWork.HelperCode.Calculation;
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
    public partial class P2_Level3 : Form
    {
        DataGridViewManager gridView;
        ChartManager chartView1;
        ChartManager chartView2;


        public P2_Level3()
        {
            InitializeComponent();
        }

        private void P2_Level3_Load(object sender, EventArgs e)
        {
            gridView = new DataGridViewManager(dataGridView1);
            //chartView1 = new ChartManager(chart1, checkedListBox1);
            //chartView2 = new ChartManager(chart2, checkedListBox2);


            DataTable a = GeneralData.dataTable;
            a.Columns.RemoveAt(0);
            DataTableLinkCalculation d = new DataTableLinkCalculation(a);


            for (int col = 0; col < d.deltaPoints.Count; col++)
            {
                gridView.ColumnAdd(d.deltaPoints[col].ColumnName, d.deltaPoints[col].GetPointList, 4);
            }

            for (int row = 0;  row < d.deltaPoints[0].GetPointList.Count; row++)
            {
                gridView.Colorize(d.Checker(row), row);
            }
        }
    }
}
