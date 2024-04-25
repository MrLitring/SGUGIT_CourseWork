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


        private struct ColumnTable
        {
            public List<PointColumn> pointColumns;
            public List<double> alphas;
            public List<double> responces;
            public double[] predicates;


            public void init()
            {
                pointColumns = new List<PointColumn>();
                alphas = new List<double>();
                responces = new List<double>();
                predicates = new double[2];
            }
        }

        ColumnTable columnMinus;
        ColumnTable columnNull;
        ColumnTable columnPlus;

        public DataGridView lastDataGridView;



        public DataTableWork()
        {
            dtable = GeneralData.dataTable;

            lastDataGridView = new DataGridView();
            columnNull = new ColumnTable();
            columnPlus = new ColumnTable();
            columnMinus = new ColumnTable();

            columnNull.init();
            columnPlus.init();
            columnMinus.init();
        }
        public DataTableWork(DataTable dataTable) : this()
        {
            this.dtable = dataTable;
        }


        public void ColumnFill(bool isRowRead = true)
        {
            if (dtable == null) dtable = GeneralData.dataTable;

            int colCount = dtable.Columns.Count;
            int rowCount = dtable.Rows.Count;

            if (isRowRead == false)
            {
                colCount = dtable.Rows.Count;
                rowCount = dtable.Columns.Count;
            }
            for (int col = 0; col < colCount; col++)
            {
                PointColumn point = new PointColumn();
                for (int row = 0; row < rowCount; row++)
                {
                    if (isRowRead)
                        point.PointAdd(dtable.Rows[row][col]);
                    else
                    {
                        point.PointAdd(dtable.Rows[col][row]);
                    }
                }
                if (isRowRead == false)
                    point.Points.RemoveAt(0);

                columnNull.pointColumns.Add(point);
            }

            
        }

        public void Calculation(bool isFullCalculation = true)
        {
            columnNull.responces = Responce_Calculation(columnNull.pointColumns);
            columnNull.alphas = Alphas_Calculation(columnNull.responces, columnNull.pointColumns);
            columnNull.predicates[0] = Predicate_Calculation(columnNull.responces.ToArray());
            columnNull.predicates[1] = Predicate_Calculation(columnNull.alphas.ToArray());

            if (isFullCalculation == true) 
            {
                for(int i = 0; i < columnNull.pointColumns.Count; i++)
                {
                    columnMinus.pointColumns.Add(columnNull.pointColumns[i] - GeneralData.assureValue);
                    columnPlus.pointColumns.Add(columnNull.pointColumns[i] + GeneralData.assureValue);
                }

                columnMinus.responces = Responce_Calculation(columnMinus.pointColumns);
                columnMinus.alphas = Alphas_Calculation(columnMinus.responces, columnMinus.pointColumns);

                columnPlus.responces = Responce_Calculation(columnMinus.pointColumns);
                columnPlus.alphas = Alphas_Calculation(columnMinus.responces, columnMinus.pointColumns);

                columnMinus.predicates[0] = Predicate_Calculation(columnNull.responces.ToArray());
                columnMinus.predicates[1] = Predicate_Calculation(columnNull.alphas.ToArray());

                columnPlus.predicates[0] = Predicate_Calculation(columnNull.responces.ToArray());
                columnPlus.predicates[1] = Predicate_Calculation(columnNull.alphas.ToArray());
            }

        }

        public void OutFill(DataGridView dataGridView, int roundValue = 7)
        {
            ColumnAdd(dataGridView, "M-", columnMinus.responces, roundValue);
            ColumnAdd(dataGridView, "M", columnNull.responces, roundValue);
            ColumnAdd(dataGridView, "M+", columnPlus.responces, roundValue);
            ColumnAdd(dataGridView, "A-", columnMinus.alphas, roundValue);
            ColumnAdd(dataGridView, "A", columnNull.alphas, roundValue);
            ColumnAdd(dataGridView, "A+", columnPlus.alphas, roundValue);
        }

        private void ColumnAdd(DataGridView dataGridView,string name, List<double> list, int roundValue = 7)
        {
            dataGridView.Columns.Add(name, name);
            RowAdd(dataGridView, list, roundValue);
        }

        private void RowAdd(DataGridView dataGridView, List<double> list, int roundValue = 7)
        {
            for (int i = dataGridView.Rows.Count; i < list.Count; i++)
            {
                dataGridView.Rows.Add();
            }

            for(int i = 0; i < list.Count; i++)
            {
                dataGridView.Rows[i].Cells[dataGridView.Columns.Count -1].Value = Math.Round(list[i], roundValue);
            }
        }



        private List<double> Responce_Calculation(List<PointColumn> pointColumn)
        {
            //// Формула = "Корень(СУММКВ(array[i]))"
            List<double> responce = new List<double>();
            for (int i = 0; i < pointColumn.Count; i++)
            {
                responce.Add(Math.Sqrt(pointColumn[i].Sum(2)));
            }
            return responce;
        }

        private List<double> Alphas_Calculation(List<double> responce, List<PointColumn> pointColumnsNull)
        {
            //// формула = "ГРАДУСЫ(ACOS(СУММПРОИЗВ(array[0],array[1])/(M[0]*M[i])))"
            List<double> list = new List<double>();
            List<double> M = responce;

            for (int i = 0; i < pointColumnsNull.Count; i++)
            {
                list.Add((pointColumnsNull[0] * pointColumnsNull[i]).Sum() / (M[0] * M[i]));
                if (list[i] >= 1) list[i] = 1;

                list[i] = Math.Acos(list[i]);
                list[i] = list[i] * 180 / Math.PI;
            }

            return list;
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
