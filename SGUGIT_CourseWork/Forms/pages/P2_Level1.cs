using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P2_Level1 : Form
    {
        DataTable dTable;
        FormHelperCode formHelp;
        bool isUseWhiteNoise;
        bool isNewWhiteNise;


        public P2_Level1()
        {
            InitializeComponent();
            formHelp = new FormHelperCode();
            isUseWhiteNoise = false;
        }
        public P2_Level1(DataTable dataTable, bool isUseWhiteNoise, bool isNewWN = false) : this ()
        {
            dTable = dataTable;
            this.isUseWhiteNoise = isUseWhiteNoise;
            this.isNewWhiteNise = isNewWN;
        }



        private void P2_Level1_Load(object sender, EventArgs e)
        {
            if(dTable == null) 
            { 
                dTable = GeneralData.dataTable; 
            }

            if(dTable.Columns.Count < 2) 
            {
                FormHelperCode.MessageError(GeneralTextData.Error, GeneralTextData.Error_MinimumTwoPoints);
                return;
            }
            
            FillData();
        }

        List<PointColumn> points = new List<PointColumn>();
        private void FillData()
        {
            DataGridViewManager dataGridView = new DataGridViewManager(dataView);
            ChartManager char_1 = new ChartManager(chart1, checkedListBox1, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline);
            ChartManager char_2 = new ChartManager(chart2, checkedListBox2, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line);

            char_1.TitleText = "График отклика";
            char_2.TitleText = "Фазовая траектория";
            char_1.AxisSetTitle(GeneralTextData.Assure.ToString(), GeneralTextData.Alpha.ToString());
            char_2.AxisSetTitle("t", GeneralTextData.Assure.ToString());
            char_2.AxisSetTitle("t", GeneralTextData.Assure.ToString());
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
            if (isNewWhiteNise == true)
            {
                int min = Math.Min(work.columnNull.Min(work.columnNull.alphas), work.columnMinus.Min(work.columnMinus.alphas));
                min = Math.Min(min, work.columnPlus.Min(work.columnPlus.alphas));
                GeneralData.WhiteRound = min;
                    }
            List<string> strings = new List<string>();
            for (int i = 0; i < dTable.Rows.Count && i < GeneralData.dataTable.Rows.Count; i++)
                strings.Add(GeneralData.dataTable.Rows[i][0].ToString());
            strings.Add("Прогноз");

            char_1.AnnotanionCreate(strings);
            char_2.AnnotanionCreate(strings);
            dataGridView.ColumnAdd("№", strings);
            dataGridView.ColumnAdd("M-", work.columnMinus.responces, 4);
            dataGridView.ColumnAdd("M", work.columnNull.responces, 4);
            dataGridView.ColumnAdd("M+", work.columnPlus.responces, 4);

            if(isUseWhiteNoise)
            {
                dataGridView.ColumnAdd("A-", work.columnMinus.Alphas(GeneralData.WhiteRound));
                dataGridView.ColumnAdd("A", work.columnNull.Alphas(GeneralData.WhiteRound));
                dataGridView.ColumnAdd("A+", work.columnPlus.Alphas(GeneralData.WhiteRound));
                dataGridView.ColumnAdd("A-", work.columnMinus.alphas);
                dataGridView.ColumnAdd("A", work.columnNull.alphas);
                dataGridView.ColumnAdd("A+", work.columnPlus.alphas);
            }
            else
            {
                dataGridView.ColumnAdd("A-", work.columnMinus.alphas, 5);
                dataGridView.ColumnAdd("A", work.columnNull.alphas, 5);
                dataGridView.ColumnAdd("A+", work.columnPlus.alphas, 5);
            }
            

            dataGridView.ColumnAdd("E", work.E, 10);
            dataGridView.ColumnAdd("L", work.L, 10);
            //dataGridView.ColumnAdd("L<=E", work.LE, 4);
            dataGridView.ColumnAdd("Оценка", work.LEs, 10);

            dataGridView.ColorizeCol(work.Flags(), dataGridView.ColumnCount - 1);
            

            char_1.Series_Add("M");
            char_1.Series_Add("M-");
            char_1.Series_Add("M+");
            if(isUseWhiteNoise)
            {
                char_1.AddPointXY("M-", work.columnMinus.responces, work.columnMinus.Alphas(GeneralData.WhiteRound));
                char_1.AddPointXY("M", work.columnNull.responces, work.columnNull.Alphas(GeneralData.WhiteRound));
                char_1.AddPointXY("M+", work.columnPlus.responces, work.columnPlus.Alphas(GeneralData.WhiteRound));
            }
            else
            {
                char_1.AddPointXY("M-", work.columnMinus.responces, work.columnMinus.alphas);
                char_1.AddPointXY("M", work.columnNull.responces, work.columnNull.alphas);
                char_1.AddPointXY("M+", work.columnPlus.responces, work.columnPlus.alphas);
            }

            char_2.Series_Add("M");
            char_2.Series_Add("M-");
            char_2.Series_Add("M+");
            char_2.AddPointY("M-", work.columnMinus.responces);
            char_2.AddPointY("M", work.columnNull.responces);
            char_2.AddPointY("M+", work.columnPlus.responces);

            
            if (work.count > 0 && isUseWhiteNoise == false)
                FormHelperCode.MessageInfo(GeneralTextData.Warning, GeneralTextData.Warning_BlockUnstable);
        }
    }
}
