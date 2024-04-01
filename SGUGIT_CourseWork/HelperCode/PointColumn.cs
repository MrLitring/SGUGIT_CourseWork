﻿using System;
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
        
        public PointColumn(int RoundValue, object Time) {
            points = new List<double>();
            this.RoundValue = RoundValue;
            this.Time = Convert.ToInt32(Time) + 1;
        }
        public PointColumn()
        {
            points = new List<double>();
            this.RoundValue = 4;
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

        public double Summ(double pow = 1)
        {
            double summ = 0;

            foreach(double elem in points)
            {
                summ += Math.Pow(elem, pow);
            }

            return summ;
        }

        public double Multiply(double pow = 1)
        {
            double mult = 1;

            foreach(double elem in points)
            {
                mult *= Math.Pow(elem, pow);
            }

            return mult;
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

        
    }
}
