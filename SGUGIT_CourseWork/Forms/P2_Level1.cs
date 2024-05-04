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
            FillData();
        }

        List<PointColumn> points = new List<PointColumn>();
        private void FillData()
        {
            DataTableCalculation work = new DataTableCalculation(GeneralData.dataTable);
            work.ColumnFill(false);
            work.Calculation();
            work.lastDataGridView = dataView;
            work.OutFill(dataView, 5);

            DataPresentationManager dataManager = new DataPresentationManager();
            dataManager.Chart_NewDesigner(
                chart1, 
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline);

            dataManager.AddDataToChart(chart1, work.columnNull.responces, work.columnNull.alphas, 0);
            dataManager.AddDataToChart(chart1, work.columnPlus.responces, work.columnPlus.alphas, 1);
            dataManager.AddDataToChart(chart1, work.columnMinus.responces, work.columnMinus.alphas, 2);
            dataManager.AddDataToChart(chart1, work.columnNull.predicates[0], work.columnNull.predicates[1], 0);
            dataManager.AddDataToChart(chart1, work.columnPlus.predicates[0], work.columnPlus.predicates[1], 1);
            dataManager.AddDataToChart(chart1, work.columnMinus.predicates[0], work.columnMinus.predicates[1], 2);


            dataManager.Chart_NewDesigner(
                chart2,
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line);

            dataManager.AddDataToChart(chart2, work.columnNull.alphas, 0, 1);
            dataManager.AddDataToChart(chart2, work.columnPlus.alphas, 1, 1);
            dataManager.AddDataToChart(chart2, work.columnMinus.alphas, 2, 1);
            dataManager.AddDataToChart(chart2, work.columnNull.predicates[1], 0);
            dataManager.AddDataToChart(chart2, work.columnPlus.predicates[1], 1);
            dataManager.AddDataToChart(chart2, work.columnMinus.predicates[1], 2);
            //chart1.Series.Clear();
            //chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_0"));
            //chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_1"));
            //chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Ser_2"));
            //chart1.Series[0].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            //chart1.Series[1].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            //chart1.Series[2].ChartType = (System.Windows.Forms.DataVisualization.Charting.SeriesChartType)4;
            //chart1.Series[0].BorderWidth = 1;
            //chart1.Series[1].BorderWidth = 1;
            //chart1.Series[2].BorderWidth = 1;


            for (int i = 0; i < work.columnNull.responces.Count; i++)
            {
                double respone = Math.Round(work.columnMinus.responces[i], 4);
                chart1.Series[0].Points.AddXY(respone, work.columnMinus.alphas[i]);
                chart2.Series[0].Points.AddXY(i, respone);

                respone = Math.Round(work.columnNull.responces[i], 4);
                chart1.Series[1].Points.AddXY(respone, work.columnNull.alphas[i]);
                chart2.Series[1].Points.AddXY(i, respone);

                respone = Math.Round(work.columnPlus.responces[i], 4);
                chart1.Series[2].Points.AddXY(respone, work.columnPlus.alphas[i]);
                chart2.Series[2].Points.AddXY(i, respone);  
            }

            int j = chart2.Series.Count + 1;
            //chart2.Series[0].Points.AddXY(j, work.columnMinus.predicates[0]);
            //chart2.Series[1].Points.AddXY(j, work.columnNull.predicates[0]);
            //chart2.Series[2].Points.AddXY(j, work.columnPlus.predicates[0]);

        }
    }
}
