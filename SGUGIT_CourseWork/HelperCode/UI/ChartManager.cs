using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SGUGIT_CourseWork.HelperCode.UI
{
    /// <summary>
    /// Класс, для управления графиком. Дизайн и вывод данных
    /// </summary>
    internal class ChartManager
    {
        private Chart currentChart;
        private SeriesChartType type;
        private int widthBorder;
        private List<Series> seriesList;
        private CheckedListBox checkListBox;
        private ContextMenuStrip contextHelpMenu;
        private ContextMenuStrip chartHelpMenu;


        public int roundX;
        public int roundY;
        
        public bool isStartToZero = true;
        public string TitleText
        {
            get { return currentChart.Titles[0].Text; }
            set { currentChart.Titles[0].Text = value;  }
        }

         

        private ChartManager()
        {
            seriesList = new List<Series>();
            widthBorder = 1;
            roundX = 4;
            roundY = 7;
        }
        public ChartManager(Chart chart, SeriesChartType type = SeriesChartType.Spline) : this()
        {
            this.currentChart = chart;
            this.type = type;

            chartDesigner();
        }
        public ChartManager(Chart chart, CheckedListBox listBox, SeriesChartType type = SeriesChartType.Spline) : this()
        {
            this.currentChart = chart;
            this.checkListBox = listBox;
            this.type = type;

            chartDesigner();
            checkListBoxDesigner();
        }

        

        public void Clear(Chart chart)
        {
            chart.Series.Clear();
            seriesList.Clear();
            if (checkListBox != null) checkListBox.Items.Clear();
        }

        public void AxisSetTitle(string x, string y)
        {
            currentChart.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font(currentChart.ChartAreas[0].AxisX.TitleFont.Name, 14);
            currentChart.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font(currentChart.ChartAreas[0].AxisY.TitleFont.Name, 14);

            currentChart.ChartAreas[0].AxisX.Title = x;
            currentChart.ChartAreas[0].AxisY.Title = y;
        }

        public void Series_Add(string name)
        {
            Series series = new Series();
            series.Name = name;
            series.ChartType = type;
            series.BorderWidth = widthBorder;

            currentChart.Series.Add(series);

            if (checkListBox != null)
            {
                checkListBox.Items.Add(series.Name);
                checkListBox.SetItemChecked(checkListBox.Items.Count - 1, true);
            }
            
        }



        private void AddPointXY(Series serie, double X, double Y)
        {
            if (serie == null) return;

            X = Math.Round(X, roundX);
            Y = Math.Round(Y, roundY);
            serie.Points.AddXY(X, Y);
        }
        public void AddPointXY(string name, double X, double Y)
        {
            Series series = SerieSearch(name);

            AddPointXY(series, X, Y);
        }
        public void AddPointXY(string name, List<double> X, List<double> Y)
        {
            Series serie = SerieSearch(name);
            int min = Math.Min(X.Count, Y.Count);

            for (int i = 0; i < min; i++)
                AddPointXY(serie, X[i], Y[i]);
        }

        public void AddPointY(string name, double Y)
        {
            Series series = SerieSearch(name);
            
            AddPointXY(series, series.Points.Count + 1, Y);
        }
        public void AddPointY(string name, List<double> Y)
        {
            Series series = SerieSearch(name);

            for (int i = 0; i < Y.Count; i++)
                AddPointXY(series, i, Y[i]);
        }

        public Series SerieSearch(string name)
        {
            foreach (Series serie in currentChart.Series)
            { if (serie.Name == name) return serie; }

            return null;
        }
        public int SerieSearchIndex(string name, List<Series > series)
        {
            for (int i = 0; i < series.Count; i++)
                if (series[i].Name == name) return i;
            return -1;
        }



        private void chartDesigner()
        {
            if(currentChart.Titles.Count == 0) currentChart.Titles.Add(string.Empty);

            currentChart.Series.Clear();
            currentChart.Titles[0].Text = "";
            currentChart.Legends.Clear();

            currentChart.Titles[0].Font = new System.Drawing.Font(currentChart.Titles[0].Font.Name, 14);
            currentChart.ChartAreas[0].Axes[1].IsStartedFromZero = false;
        }

        private void checkListBoxDesigner()
        {
            checkListBox.MultiColumn = true;
            contextHelpMenu = new ContextMenuStrip();
            ToolStripMenuItem toolClear = new ToolStripMenuItem("Снять все выделения");
            toolClear.MouseUp += ToolClear_MouseUp;

            contextHelpMenu.Items.Add(toolClear);

            checkListBox.MouseUp += CheckListBox_MouseClick;
            checkListBox.ItemCheck += CheckListBox_ItemCheck;
            
        }

        private void ToolClear_MouseUp(object sender, MouseEventArgs e)
        {
            for(int i = 0;  i < checkListBox.Items.Count; i++)
            {
                checkListBox.SetItemChecked(i, false);
            }
        }

        private void CheckListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string itemName = checkListBox.Items[e.Index].ToString();
            bool isChecked = checkListBox.GetItemCheckState(e.Index) == CheckState.Checked;


            if (isChecked)
                currentChart.Series[itemName].Enabled = false;
            else
            {
                currentChart.Series[itemName].Enabled = true;
            }

            
        }

        private void CheckListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextHelpMenu.Show(checkListBox.PointToScreen(e.Location));
            }

        }
    }
}