using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SGUGIT_CourseWork.HelperCode
{
    internal class DataPresentationManager
    {



        public DataPresentationManager() { }



        public void DataGridView_Designer(DataGridView dataGridView)
        {

        }

        public void Chart_NewDesigner(Chart chart, SeriesChartType type)
        {
            chart.Series.Clear();
            chart.Series.Add(new Series("Ser_0"));
            chart.Series.Add(new Series("Ser_1"));
            chart.Series.Add(new Series("Ser_2"));

            chart.Series[0].ChartType = type;
            chart.Series[1].ChartType = type;
            chart.Series[2].ChartType = type;

            chart.Series[0].BorderWidth = 1;
            chart.Series[1].BorderWidth = 1;
            chart.Series[2].BorderWidth = 1;
        }

        public void AddDataToChart(Chart chart, List<double> X, List<double> Y, int serieNum, int offset = 0)
        {
            for (int i = offset; i < X.Count; i++)
            {
                chart.Series[serieNum].Points.AddXY(X[i], Y[i]);
            }
        }
        public void AddDataToChart(Chart chart, double X, double Y, int serieNum)
        {
            chart.Series[serieNum].Points.AddXY(X, Y);
        }
        public void AddDataToChart(Chart chart, List<double> X, int serieNum, int offset = 0)
        {
            for (int i = offset; i < X.Count; i++)
                chart.Series[serieNum].Points.AddXY(i, X[i]);
        }
        public void AddDataToChart(Chart chart, double X, int serieNum)
        {
            chart.Series[serieNum].Points.AddXY(chart.Series.Count + 1, X);
        }

    }
}
