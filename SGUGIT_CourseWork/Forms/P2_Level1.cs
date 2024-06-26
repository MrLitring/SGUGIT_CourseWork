﻿using Microsoft.Office.Core;
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
        DataTable dTable;



        public P2_Level1()
        {
            InitializeComponent();
        }
        public P2_Level1(DataTable dataTable) : this ()
        {
            dTable = dataTable;
        }



        private void P2_Level1_Load(object sender, EventArgs e)
        {
            if(dTable == null) 
            { 
                dTable = GeneralData.dataTable; 
            }
            FillData();
        }

        List<PointColumn> points = new List<PointColumn>();
        private void FillData()
        {
            DataGridViewManager dataGridView = new DataGridViewManager(dataView);
            ChartManager char_1 = new ChartManager(chart1, checkedListBox1);
            ChartManager char_2 = new ChartManager(chart2, checkedListBox2, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line);

            char_1.TitleText = "Предельные значения функции S(t)";
            char_2.TitleText = "Прогнозные функции для М(t)";
            char_2.isStartToZero = false;

            DataTable table = dTable.Clone();
            for (int row = 0; row < dTable.Rows.Count; row++)
            {
                table.ImportRow(dTable.Rows[row]);
            }
            table.Columns.RemoveAt(0);
            DataTableCalculation work = new DataTableCalculation(table);

            work.ColumnFill(false);
            work.Calculation();

            
            dataGridView.ColumnAdd("M-", work.columnMinus.responces, 4);
            dataGridView.ColumnAdd("M", work.columnNull.responces, 4);
            dataGridView.ColumnAdd("M+", work.columnPlus.responces, 4);

            dataGridView.ColumnAdd("A-", work.columnMinus.alphas, 4);
            dataGridView.ColumnAdd("A", work.columnNull.alphas, 4);
            dataGridView.ColumnAdd("A+", work.columnPlus.alphas, 4);

            dataGridView.ColumnAdd("E", work.E, 10);
            dataGridView.ColumnAdd("L", work.L, 10);
            dataGridView.ColumnAdd("L<=E", work.LE, 4);
            dataGridView.ColumnAdd("LEs", work.LEs, 10);

            

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
