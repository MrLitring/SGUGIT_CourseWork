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

            if (isFullCalculation == true) 
            {
                for(int i = 0; i < columnNull.pointColumns.Count; i++)
                {
                    columnMinus.pointColumns.Add(columnNull.pointColumns[i] - GeneralData.assureValue);
                    columnPlus.pointColumns.Add(columnNull.pointColumns[i] + GeneralData.assureValue);
                }


                columnMinus.responces = Responce_Calculation(columnMinus.pointColumns);
                columnMinus.alphas = Alphas_Calculation(columnMinus.responces, columnMinus.pointColumns);
            }

        }

        public void OutFill(DataGridView dataGridView)
        {
            ColumnAdd(dataGridView, "M-", columnMinus.responces);
            ColumnAdd(dataGridView, "M", columnNull.responces);
            ColumnAdd(dataGridView, "M+", columnPlus.responces);
            ColumnAdd(dataGridView, "A-", columnMinus.alphas);
            ColumnAdd(dataGridView, "A", columnNull.alphas);
            ColumnAdd(dataGridView, "A+", columnPlus.alphas);
        }

        private void ColumnAdd(DataGridView dataGridView,string name, List<double> list)
        {
            dataGridView.Columns.Add(name, name);
            RowAdd(dataGridView, list);
        }

        private void RowAdd(DataGridView dataGridView, List<double> list)
        {
            for (int i = dataGridView.Rows.Count; i < list.Count; i++)
            {
                dataGridView.Rows.Add();
            }

            for(int i = 0; i < list.Count; i++)
            {
                dataGridView.Rows[i].Cells[dataGridView.Columns.Count - 1].Value = list[i];
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
