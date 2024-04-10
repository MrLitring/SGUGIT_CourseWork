﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode
{
    public class DataTableWork
    {
        /*
         * Таблица сохраняется как последнее использованная
         * 
         * Работа с таблицей:
         * заполнить значениями, рассчитать значения, заполнять точки
         * 
         * Расчёт следующих даннных:
         * отклик - responce, альфа - alpha , прогнозируемое значение - predicted(всегда последний в списке) 
         */

        private DataTable dtable;
        private List<PointColumn> pointColumns;
        private List<double> responce; // отклик
        private List<double> alphas; // Альфа
        private double[] predicates;


        public DataGridView lastDataGridView;
        public List<PointColumn> PointColumns
        {
            get { return pointColumns; }
        }
        public double[] Predicates {get { return predicates; } }
        public List<double> Responce { get { return responce; } } // отклик
        public List<double> Alphas { get { return alphas; } } // Альфа


        public DataTableWork()
        {
            dtable = GeneralData.DataTable;

            lastDataGridView = new DataGridView();
            pointColumns = new List<PointColumn>();
            responce = new List<double>();
            alphas = new List<double>();

            predicates = new double[2];
        }
        public DataTableWork(DataTable dataTable) : this()
        {
            this.dtable = dataTable;
        }
        public DataTableWork(DataTable dataTable, List<PointColumn> pointColumns) : this(dataTable)
        {
            this.pointColumns = new List<PointColumn>();
            foreach(PointColumn elem in pointColumns)
            {
                PointColumn point = new PointColumn(elem);
                this.pointColumns.Add(point);
            }
        }
        public DataTableWork(DataTable dataTable, List<PointColumn> pointColumns, DataGridView dataGridView) : this(dataTable, pointColumns)
        {
            this.lastDataGridView = dataGridView;
        }

        public void DataGridFill()
        {
            lastDataGridView.Rows.Clear();
            lastDataGridView.Columns.Clear();

            //
            // Даём новые колонки и строки
            //
            for(int i = 0; i < pointColumns.Count; i++)
            {
                lastDataGridView.Columns.Add(i.ToString(), i.ToString());
            }
            for(int i = 0; i < pointColumns[0].Points.Count;i++)
            {
                lastDataGridView.Rows.Add();
            }

            //
            // Заполняем
            //
            for(int col = 0; col < pointColumns.Count; col++)
            {
                for (int row = 0; row < pointColumns[col].Points.Count; row++)
                {
                    lastDataGridView.Rows[row].Cells[col].Value = pointColumns[col].Points[row].ToString();
                }
            }
        }

        public void RowAdd(List<double> list, int roundValue = 10)
        { 
            int min = Math.Min(lastDataGridView.Columns.Count, list.Count);
            lastDataGridView.Rows.Add();
            for(int i = 0; i < min; i++)
            {
                lastDataGridView.Rows[lastDataGridView.Rows.Count - 2].Cells[i].Value = Math.Round(list[i], roundValue).ToString();
            }
        }        

        public void ColumnAdd(string nameColumn, List<double> list, int roundValue = 10)
        {
            lastDataGridView.Columns.Add(nameColumn, nameColumn);

            for (int i = lastDataGridView.Rows.Count; i < list.Count + 1; i++)
                lastDataGridView.Rows.Add();

            for (int i = 0; i < list.Count; i++)
                lastDataGridView.Rows[i].Cells[lastDataGridView.Columns.Count - 1].Value = Math.Round(list[i], roundValue).ToString();
        }

        public void AddValue(double value)
        {
            for(int i = 0; i < pointColumns.Count; i++)
                pointColumns[i] += value;
        }

        public void Calculation()
        {
            Responce_Calculation();
            Alphas_Calculation();
            predicates[0] = Predicate_Calculation(responce.ToArray());
            predicates[1] = Predicate_Calculation(alphas.ToArray());
        }

        

        private void Responce_Calculation()
        {
            // Формула = "Корень(СУММКВ(array[i]))"
            responce = new List<double>();
            for (int i = 0; i < pointColumns.Count; i++)
            {
                responce.Add(Math.Sqrt(pointColumns[i].Sum(2)));
            }
        }

        private void Alphas_Calculation()
        {
            // формула = "ГРАДУСЫ(ACOS(СУММПРОИЗВ(array[0],array[1])/(M[0]*M[i])))"
            List<double> list = new List<double>();
            List<double> M = responce;

            for (int i = 0; i < pointColumns.Count; i++)
            {
                list.Add((pointColumns[0] * pointColumns[i]).Sum() / (M[0] * M[i]));
                if (list[i] >= 1) list[i] = 1;

                list[i] = Math.Acos(list[i]);
                list[i] = list[i] * 180 / Math.PI;
            }

            alphas = list;
        }

        private double Predicate_Calculation(double[] doubles)
        {
            double smooth = GeneralData.smoothValue;
            double[] responceValues = new double[doubles.Length];

            responceValues[0] = smooth * doubles[0] + (1 - smooth) * AvarageSumm(doubles.ToArray());
            for (int i = 1; i < responceValues.Length; i++)
            {
                responceValues[i] = smooth * doubles[i] + (1 - smooth) * responceValues[i-1];
            }
            double respon = smooth * AvarageSumm(responceValues) + (1 - smooth) * responceValues[responceValues.Length - 1];

            return respon;

        }

        private double AvarageSumm(double[] doubles)
        {
            double sum = 0;
            
            foreach(double elem in doubles)
                sum += elem;

            return sum / doubles.Length;
        }

    }
}
