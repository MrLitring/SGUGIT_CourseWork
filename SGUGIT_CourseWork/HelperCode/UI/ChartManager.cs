using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        private List<string> annotations;
        private CheckedListBox currentCheckList;
        private ContextMenuStrip contextListMenu;
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
            annotations = new List<string>();
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
            this.currentCheckList = listBox;
            this.type = type;

            chartDesigner();
            checkListBoxDesigner();
        }

        

        public void Clear()
        {
            currentChart.Annotations.Clear();
            currentChart.Series.Clear();
            seriesList.Clear();
            if (currentCheckList != null) currentCheckList.Items.Clear();
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
            seriesList.Add(series);

            series = new Series();
            series.Name = name;
            series.ChartType = type;
            series.BorderWidth = widthBorder;
            currentChart.Series.Add(series);

            if (currentCheckList != null)
            {
                currentCheckList.Items.Add(series.Name);
                currentCheckList.SetItemChecked(currentCheckList.Items.Count - 1, false);
            }
            
        }



        public void AddPointXY(string name, double X, double Y)
        {
            X = Math.Round(X, roundX);
            Y = Math.Round(Y, roundY);
            Series series = seriesList[SerieSearch(name)];
            series.Points.AddXY(X, Y);

        }
        public void AddPointXY(string name, List<double> X, List<double> Y)
        {
            int min = Math.Min(X.Count, Y.Count);

            for (int i = 0; i < min; i++)
                AddPointXY(name, X[i], Y[i]);
        }

        public void AddPointY(string name, double Y)
        {
            Series series = seriesList[SerieSearch(name)];
            AddPointXY(name, series.Points.Count + 1, Y);
        }
        public void AddPointY(string name, List<double> Y)
        {
            for (int i = 0; i < Y.Count; i++)
                AddPointXY(name, i, Y[i]);
        }

        public int SerieSearch(string name)
        {
            for(int i =0; i < seriesList.Count; i++)
            {
                if (seriesList[i].Name == name) return i;
            }

            return -1;
        }

        public void AnnotanionCreate(List<string> text)
        {
            for(int i =0; i<text.Count;i++)
                annotations.Add(text[i]);


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
            currentCheckList.MultiColumn = true;
            contextListMenu = new ContextMenuStrip();
            ToolStripMenuItem toolClear = new ToolStripMenuItem("Снять все выделения");
            toolClear.MouseUp += ToolClear_MouseUp;

            contextListMenu.Items.Add(toolClear);

            currentCheckList.MouseUp += CheckListBox_MouseClick;
            currentCheckList.ItemCheck += CheckListBox_ItemCheck;
            
        }

        private void ToolClear_MouseUp(object sender, MouseEventArgs e)
        {
            for(int i = 0;  i < currentCheckList.Items.Count; i++)
            {
                currentCheckList.SetItemChecked(i, false);
            }
        }

        private void CheckListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string itemName = currentCheckList.Items[e.Index].ToString();
            bool isChecked = currentCheckList.GetItemCheckState(e.Index) == CheckState.Checked;

            
            if(isChecked == false)
                seriesShow(itemName);
            else
                seriesUnShow(itemName);

        }

        private void CheckListBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextListMenu.Show(currentCheckList.PointToScreen(e.Location));
            }

        }



        private void seriesShow(string name)
        {
            int index = SerieSearch(name);
            if (index == -1) return;


            Series targetSerie = currentChart.Series[name];

            for(int i = 0; i < seriesList[index].Points.Count; i++)
            {
                targetSerie.Points.Add(seriesList[index].Points[i]);
                //currentChart.Series[name].Points.Add(seriesList[index].Points[i]);
            }

            AnnotationShow();
        }

        private void seriesUnShow(string name)
        {
            currentChart.Series[name].Points.Clear();
            AnnotationShow();
        }

        private void AnnotationShow()
        {
            currentChart.Annotations.Clear();

            foreach (Series series in currentChart.Series)
            {
                for(int i = 0; i < series.Points.Count;i++)
                {
                    DataPoint point= series.Points[i];

                    TextAnnotation textAnnotation = new TextAnnotation();
                    textAnnotation.Text = annotations[i];
                    textAnnotation.AnchorDataPoint = point;
                    currentChart.Annotations.Add(textAnnotation);
                }
            }
        }
    }
}