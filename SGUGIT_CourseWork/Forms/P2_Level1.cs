﻿using Microsoft.Office.Core;
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
            for(int rows = 0; rows < dTable.Rows.Count; rows++)
            {
                PointColumn point = new PointColumn();
                for (int cols = 1; cols < dTable.Columns.Count; cols++)
                {
                    point.PointAdd(Convert.ToDouble(dTable.Rows[rows][cols]));
                }
                points.Add(point);
            }

            DataTableWork tableWork = new DataTableWork(dTable, points, dataView);
            DataTableWork tableWorkPlus = new DataTableWork(dTable, points, dataView);
            DataTableWork tableWorkMinus = new DataTableWork(dTable, points, dataView);

            tableWorkPlus.AddValue(GeneralData.assureValue);
            tableWorkMinus.AddValue(-GeneralData.assureValue);

            //tableWorkPlus.DataGridFill();

            tableWork.Calculation();
            tableWorkPlus.Calculation();
            tableWorkMinus.Calculation();

            int roundaValue = 4;
            tableWork.ColumnAdd("M+", tableWorkPlus.Responce, roundaValue);
            tableWork.ColumnAdd("M-", tableWorkMinus.Responce, roundaValue);
            tableWork.ColumnAdd("M", tableWork.Responce, roundaValue);

            tableWork.ColumnAdd("A+", tableWorkPlus.Alphas);
            tableWork.ColumnAdd("A-", tableWorkMinus.Alphas);
            tableWork.ColumnAdd("A", tableWork.Alphas);

            List<double> predicats = new List<double>();
            predicats.Add(tableWorkPlus.Predicates[0]);
            predicats.Add(tableWork.Predicates[0]);
            predicats.Add(tableWorkMinus.Predicates[0]);
            predicats.Add(tableWorkPlus.Predicates[1]);
            predicats.Add(tableWork.Predicates[1]);
            predicats.Add(tableWorkMinus.Predicates[1]);

            tableWork.RowAdd(predicats, roundaValue);

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

            for (int i = 0; i < 10; i++)
            {
                chart1.Series[0].Points.AddXY(tableWork.Responce[i], tableWork.Alphas[i]);
                chart1.Series[1].Points.AddXY(tableWorkMinus.Responce[i], tableWorkMinus.Alphas[i]);
                chart1.Series[2].Points.AddXY(tableWorkPlus.Responce[i], tableWorkPlus.Alphas[i]);
            }
        }
    }
}
