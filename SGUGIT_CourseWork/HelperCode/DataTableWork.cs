using System;
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
        public List<PointColumn> pointColumnsNull;
        public List<PointColumn> pointColumnsMinus;
        public List<PointColumn> pointColumnsPlus;
        private List<double> responce; // отклик
        private List<double> alphas; // Альфа
        private double[] predicates;


        private struct ColumnTable
        {
            public List<PointColumn> PointColumn;
            public List<double> alphas;
            public List<double> responces;
            public double[] predicates;
        }

        ColumnTable columnMinus;
        ColumnTable columnNull;
        ColumnTable columnPlus;

        public DataGridView lastDataGridView;
        public List<PointColumn> PointColumns
        {
            get { return pointColumnsNull; }
        }
        public double[] Predicates { get { return predicates; } }
        public List<double> Responce { get { return responce; } } // отклик
        public List<double> Alphas { get { return alphas; } } // Альфа



        public DataTableWork()
        {
            dtable = GeneralData.dataTable;

            lastDataGridView = new DataGridView();
            pointColumnsNull = new List<PointColumn>();
            responce = new List<double>();
            alphas = new List<double>();

            predicates = new double[6];
        }
        public DataTableWork(DataTable dataTable) : this()
        {
            this.dtable = dataTable;
        }
        public DataTableWork(DataTable dataTable, List<PointColumn> pointColumns) : this(dataTable)
        {
            this.pointColumnsNull = new List<PointColumn>();
            foreach (PointColumn elem in pointColumns)
            {
                PointColumn point = new PointColumn(elem);
                this.pointColumnsNull.Add(point);
            }
        }
        public DataTableWork(DataTable dataTable, List<PointColumn> pointColumns, DataGridView dataGridView) : this(dataTable, pointColumns)
        {
            this.lastDataGridView = dataGridView;
        }


        public void ColumnFill(bool isRowRead = true)
        {
            if (dtable == null) dtable = GeneralData.dataTable;

            if (isRowRead)
            {
                for (int col = 0; col < dtable.Columns.Count; col++)
                {
                    PointColumn point = new PointColumn();
                    for (int row = 0; row < dtable.Rows.Count; row++)
                    {
                        point.PointAdd(dtable.Rows[row][col]);
                    }
                    pointColumnsNull.Add(point);
                }
            }
            else
            {
                for (int row = 0; row < dtable.Rows.Count; row++)
                {
                    PointColumn point = new PointColumn();
                    for (int col = 0; col < dtable.Columns.Count; col++)
                    {
                        point.PointAdd(dtable.Rows[row][col]);
                    }
                    pointColumnsNull.Add(point);
                }
            }
        }


        public void DataGridFill()
        {
            lastDataGridView.Rows.Clear();
            lastDataGridView.Columns.Clear();

            //
            // Даём новые колонки и строки
            //
            for (int i = 0; i < pointColumnsNull.Count; i++)
            {
                lastDataGridView.Columns.Add(i.ToString(), i.ToString());
            }
            for (int i = 0; i < pointColumnsNull[0].Points.Count; i++)
            {
                lastDataGridView.Rows.Add();
            }

            //
            // Заполняем
            //
            for (int col = 0; col < pointColumnsNull.Count; col++)
            {
                for (int row = 0; row < pointColumnsNull[col].Points.Count; row++)
                {
                    lastDataGridView.Rows[row].Cells[col].Value = pointColumnsNull[col].Points[row].ToString();
                }
            }
        }

        public void RowAdd(List<double> list, int roundValue = 10)
        {
            int min = Math.Min(lastDataGridView.Columns.Count, list.Count);
            lastDataGridView.Rows.Add();
            for (int i = 0; i < min; i++)
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
            for (int i = 0; i < pointColumnsNull.Count; i++)
                pointColumnsNull[i] += value;
        }

        public void Calculation()
        {
            for (int i = 0; i < pointColumnsNull.Count; i++)
            {
                pointColumnsMinus.Add(pointColumnsNull[i] - GeneralData.assureValue);
                pointColumnsPlus.Add(pointColumnsPlus[i] + GeneralData.assureValue);
            }
            Responce_Calculation();
            Alphas_Calculation();
            predicates[0] = Predicate_Calculation(responce.ToArray());
            predicates[1] = Predicate_Calculation(alphas.ToArray());
        }



        private void Responce_Calculation()
        {
            // Формула = "Корень(СУММКВ(array[i]))"
            responce = new List<double>();
            for (int i = 0; i < pointColumnsNull.Count; i++)
            {
                responce.Add(Math.Sqrt(pointColumnsNull[i].Sum(2)));
            }
        }

        private void Alphas_Calculation()
        {
            // формула = "ГРАДУСЫ(ACOS(СУММПРОИЗВ(array[0],array[1])/(M[0]*M[i])))"
            List<double> list = new List<double>();
            List<double> M = responce;

            for (int i = 0; i < pointColumnsNull.Count; i++)
            {
                list.Add((pointColumnsNull[0] * pointColumnsNull[i]).Sum() / (M[0] * M[i]));
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
                responceValues[i] = smooth * doubles[i] + (1 - smooth) * responceValues[i - 1];
            }
            double respon = smooth * AvarageSumm(responceValues) + (1 - smooth) * responceValues[responceValues.Length - 1];

            return respon;

        }

        private double AvarageSumm(double[] doubles)
        {
            double sum = 0;

            foreach (double elem in doubles)
                sum += elem;

            return sum / doubles.Length;
        }

    }
}
