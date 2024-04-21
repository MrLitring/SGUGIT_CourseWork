using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode
{
    public class PointColumn
    {
        private string columnName;
        private List<double> points;
        public int Time = 0;


        public List<double> Points { get { return points; } }
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }



        public PointColumn()
        {
            columnName = null;
            points = new List<double>();
        }
        public PointColumn(PointColumn other) : this()
        {
            Time = other.Time;

            for (int i = 0; i < other.Points.Count; i++)
            {
                points.Add(other.Points[i]);
            }
        }
        public PointColumn(object Time) : this()
        {
            this.Time = Convert.ToInt32(Time) + 1;
        }


        public void PointAdd(object point)
        {
            points.Add(Convert.ToDouble(point));
        }

        public double NewH()
        {
            return points[points.Count - 1] + Svi();
        }

        public double Sum(double pow = 1)
        {
            double summ = 0;

            foreach (double elem in points)
            {
                summ += Math.Pow(elem, pow);
            }

            return summ;
        }



        public static PointColumn operator *(PointColumn firstPoint, PointColumn secondPoint)
        {
            PointColumn point = new PointColumn();
            for (int i = 0; i < firstPoint.Points.Count; i++)
                point.PointAdd(firstPoint.Points[i] * secondPoint.Points[i]);

            return point;
        }

        public static PointColumn operator +(PointColumn firstPoint, double value)
        {
            PointColumn point = new PointColumn();
            for (int i = 0; i < firstPoint.points.Count; i++)
                point.PointAdd(firstPoint.points[i] + value);

            return point;
        }

        public static PointColumn operator -(PointColumn firstPoint, double value)
        {
            PointColumn point = new PointColumn();
            for (int i = 0; i < firstPoint.points.Count; i++)
                point.PointAdd(firstPoint.points[i] - value);

            return point;
        }



        private double Max()
        {
            double max = points[1] - points[0];
            for (int i = 2; i < points.Count; i++)
            {
                max = Math.Max(max, points[i] - points[i - 1]);
            }

            return max;
        }

        private double Svi()
        {
            double dmax = Max();
            Random random = new Random();
            double svi = random.Next((int)dmax);
            svi = svi + random.Next((int)(dmax % 1000));
            svi = svi - dmax / 2;

            return svi;
        }


    }
}
