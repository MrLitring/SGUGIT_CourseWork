using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode
{
    /// <summary>
    /// Класс для работы с уровнем декомпозицией первого уровня
    /// 
    /// <para>Имеет следуюшие значения:
    /// (r) responce - отклик;
    /// (a) alpha - альфа;
    /// Где последнее значение, всегда прогноз
    /// </para>
    /// 
    /// <para>Проводит следующие основные операции
    /// -   заполнить значениями;
    /// -   рассчитать значения;
    /// -   заполнять точки;
    /// </para>
    /// </summary>
    public class DataTableCalculation
    {
        public DataTable dtable;
        public List<double> E; 
        public List<double> L;
        public List<double> LE;
        public List<string> LEs;



        public struct ColumnTable
        {
            public List<PointColumn> pointColumns;
            public List<double> responces;
            public List<double> alphas;

            public void init()
            {
                pointColumns = new List<PointColumn>();
                alphas = new List<double>();
                responces = new List<double>();
            }
        }

        public ColumnTable columnMinus;
        public ColumnTable columnNull;
        public ColumnTable columnPlus;



        public  DataTableCalculation()
        {
            dtable = GeneralData.dataTable;

            columnNull = new ColumnTable();
            columnPlus = new ColumnTable();
            columnMinus = new ColumnTable();

            columnNull.init();
            columnPlus.init();
            columnMinus.init();
        }
        public DataTableCalculation(DataTable dataTable) : this()
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
            columnNull = ColumnTable_MainCalculate(columnNull);

            if (isFullCalculation == true) 
            {
                for(int i = 0; i < columnNull.pointColumns.Count; i++)
                {
                    columnMinus.pointColumns.Add(columnNull.pointColumns[i] - GeneralData.assureValue);
                    columnPlus.pointColumns.Add(columnNull.pointColumns[i] + GeneralData.assureValue);
                }

                columnPlus = ColumnTable_MainCalculate(columnPlus);
                columnMinus = ColumnTable_MainCalculate(columnMinus);

                int n = columnNull.responces.Count();
                E = new List<double>();
                L = new List<double>();
                for(int i = 0; i < n; i++)
                {
                    E.Add(Math.Abs(columnPlus.responces[i] - columnMinus.responces[i]));
                    L.Add(Math.Abs(columnNull.responces[i] - columnNull.responces[0]));
                }
                //E.Add(Math.Abs(columnPlus.predicates[0] - columnMinus.predicates[0]));
                //L.Add(Math.Abs(columnNull.predicates[0] - columnNull.responces[0]));

                LE = new List<double>();
                LEs = new List<string>();

                for(int i = 0; i < n; i++)
                {
                    if (L[i] <= E[i])
                    {
                        LE.Add(1);
                        LEs.Add("Не изменяется");
                    }
                    else
                    {
                        LE.Add(0);
                        LEs.Add("изменяется");
                    }
                }

            }

        }



        private ColumnTable ColumnTable_MainCalculate(ColumnTable columnTable)
        {
            columnTable.responces = Responce_Calculation(columnTable.pointColumns);
            columnTable.alphas = Alphas_Calculation(columnTable.responces, columnTable.pointColumns);

            columnTable.responces.Add(Predicate_Calculation(columnTable.responces.ToArray()));
            columnTable.alphas.Add(Predicate_Calculation(columnTable.alphas.ToArray()));

            return columnTable;
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
