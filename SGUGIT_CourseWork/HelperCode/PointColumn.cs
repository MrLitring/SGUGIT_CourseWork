using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode
{
    public  class PointColumn
    {
        private List<double> points;
        public List<double> Points { get {  return points; } }
        public int RoundValue = 4;
        public int Time = 0;


        public PointColumn()
        {
            points = new List<double>();
            this.RoundValue = 4;
        }
        public PointColumn(PointColumn other) : this()
        {
            RoundValue = other.RoundValue;
            Time = other.Time;

            for (int i = 0; i < other.Points.Count; i++)
            {
                points.Add(other.Points[i]);
            }
        }
        public PointColumn(int RoundValue, object Time) : this()
        {
            this.RoundValue = RoundValue;
            this.Time = Convert.ToInt32(Time) + 1;
        }
        

        public void PointAdd(object point)
        {
            points.Add(Convert.ToDouble(point));
        }

        public void print()
        {
            foreach (var point in points)
            {
                Debug.Write(point + " ");
            }
        }

        public double NewH()
        {
            return Math.Round(points[points.Count - 1] + Svi(), RoundValue);
        }

        public int arrayCount()
        {
            return points.Count;
        }

        public double Sum(double pow = 1)
        {
            double summ = 0;

            foreach(double elem in points)
            {
                summ += Math.Pow(elem, pow);
            }

            return summ;
        }

        private double Max()
        {
            double max = Math.Round(points[1] - points[0], RoundValue);
            for(int i = 2; i < points.Count; i++)
            {
                max = Math.Max(max,  Math.Round(points[i] - points[i-1], RoundValue));
            }

            return max;
        }

        private double Svi()
        {
            double dmax = Max();
            Random random = new Random();
            double svi = random.Next((int)dmax);
            svi = svi+random.Next((int)(dmax%1000));
            svi = svi - dmax / 2;

            return Math.Round(svi, RoundValue);
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

    }
}
