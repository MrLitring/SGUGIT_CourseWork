using Microsoft.Office.Core;
using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.UI;
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

            DataGridViewManager dataGridView = new DataGridViewManager(dataView);
            dataGridView.ColumnAdd("M-", work.columnMinus.responces, 4);
            dataGridView.ColumnAdd("M", work.columnMinus.responces, 4);
            dataGridView.ColumnAdd("M+", work.columnPlus.responces, 4);

            dataGridView.ColumnAdd("A-", work.columnMinus.responces, 4);
            dataGridView.ColumnAdd("A", work.columnMinus.alphas, 4);
            dataGridView.ColumnAdd("A+", work.columnPlus.responces, 4);

            dataGridView.ColumnAdd("E", work.E, 10);
            dataGridView.ColumnAdd("L", work.L, 10);
            dataGridView.ColumnAdd("L<=E", work.LE, 4);
            dataGridView.ColumnAdd("LEs", work.LEs, 10);

            ChartManager char_1 = new ChartManager(chart1, checkedListBox1,"Предельные значения функции S(t)");
            ChartManager char_2 = new ChartManager(chart2, checkedListBox2, "Прогнозные функции для М(t)", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line );
            char_2.isStartToZero = false;

            char_1.Series_Add("M");
            char_1.Series_Add("M-");
            char_1.Series_Add("M+");
            char_1.AddPointXY("M-", work.columnMinus.responces, work.columnMinus.alphas);
            char_1.AddPointXY("M", work.columnNull.responces, work.columnNull.alphas);
            char_1.AddPointXY("M+", work.columnPlus.responces, work.columnPlus.alphas);

            char_2.Series_Add("M");
            char_2.Series_Add("M-");
            char_2.Series_Add("M+");
            char_2.AddPointY("M-", work.columnMinus.responces);
            char_2.AddPointY("M", work.columnNull.responces);
            char_2.AddPointY("M+", work.columnPlus.responces);

        }
    }
}
