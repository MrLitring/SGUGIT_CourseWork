using Microsoft.Office.Core;
using SGUGIT_CourseWork.HelperCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P2_Level1 : Form
    {
        DataTable dTable = GeneralData.dataTable;  


        public P2_Level1()
        {
            InitializeComponent();
            EventBus.onDataBaseChange += Update;
        }

        private void Update()
        {
            dataView.Rows.Clear();
            dataView.Columns.Clear();
            dTable.Clear();
            dTable.Rows.Clear();
            dTable.Columns.Clear();
            dTable = GeneralData.dataTable;
            MessageBox.Show("Uwu");

            FillData();
        }

        private void P2_Level1_Load(object sender, EventArgs e)
        {
            EventBus.onDataBaseChange += Update;
            FillData();
        }

        List<PointColumn> points = new List<PointColumn>();
        private void FillData()
        {
            DataTableWork work = new DataTableWork(GeneralData.dataTable);
            work.ColumnFill(false);
            work.Calculation();
            work.lastDataGridView = dataView;
            work.OutFill(dataView, 5);


            chart1.Series.Clear();
            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_0"));
            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_1"));
            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_2"));
            chart1.Series[0].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart1.Series[1].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart1.Series[2].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            chart1.Series[0].BorderWidth = 1;
            chart1.Series[1].BorderWidth = 1;
            chart1.Series[2].BorderWidth = 1;


            chart2.Series.Clear();
            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_0"));
            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_1"));
            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_2"));
            chart2.Series[0].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)3;
            chart2.Series[1].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)3;
            chart2.Series[2].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)3;
            chart2.Series[0].BorderWidth = 1;
            chart2.Series[1].BorderWidth = 1;
            chart2.Series[2].BorderWidth = 1;

            for (int i = 0; i < work.columnNull.responces.Count; i++)
            {
                chart1.Series[0].Points.AddXY(work.columnMinus.responces[i], work.columnMinus.alphas[i]);
                chart1.Series[1].Points.AddXY(work.columnNull.responces[i], work.columnNull.alphas[i]);
                chart1.Series[2].Points.AddXY(work.columnPlus.responces[i], work.columnPlus.alphas[i]);


                if (i >= 1)
                {
                    chart2.Series[0].Points.AddXY(i, work.columnMinus.alphas[i]);
                    chart2.Series[1].Points.AddXY(i, work.columnNull.alphas[i]);
                    chart2.Series[2].Points.AddXY(i, work.columnPlus.alphas[i]);
                }
            }


        }
    }
}
