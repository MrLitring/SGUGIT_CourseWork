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
        private int round = -1;

                return sum;
            }
        }


        public string Name;
        public DataTable currentDTable;
        public List<double> E;
        public List<double> L;
        public List<double> LE;
        public List<string> LEs;
        public int count
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < LE.Count; i++)
                    if (LE[i] == 0) sum++;

                return sum;
            }
        }



        public struct ColumnTable
        {
            public List<PointColumn> pointColumns;
            public List<double> responces;
            public List<double> alphas;
            public List<double> Alphas(int rounds)
            {
                int round = rounds;
                List<double> vs = new List<double>();
                for (int i = 0; i < alphas.Count; i++)
                {
                    double s = alphas[i];
                    double a = Math.Pow(10, round);
                    s = (int)(s * a) / a;
                    vs.Add(s);
                }

                return vs;
            }

            public void init()
            {
                pointColumns = new List<PointColumn>();
                alphas = new List<double>();
                responces = new List<double>();
            }

            public int Min(List<double> doubles)
            {
                int min = 0;
                for (int a = 0; a < doubles.Count; a++)
                {
                    if (doubles[a] != 0)
                    {
                        min = doubles[a].ToString().Length;
                        break;
                    }
                }

                for (int i = 0; i < doubles.Count; i++)
                {
                    while ((doubles[i] % 1) * Math.Pow(10, min) >= 1)
                    {
                        min--;
                    }
                }

                return min;
            }
        }

        public ColumnTable columnMinus;
        public ColumnTable columnNull;
        public ColumnTable columnPlus;




        public DataTableCalculation()
        {
            Name = "table";
            currentDTable = GeneralData.dataTable;

            columnNull = new ColumnTable();
            columnPlus = new ColumnTable();
            columnMinus = new ColumnTable();

            columnNull.init();
            columnPlus.init();
            columnMinus.init();
        }
        public DataTableCalculation(DataTable dataTable, int round = 0) : this()
        {
            this.currentDTable = dataTable;
        }



        public void ColumnFill(bool isRowRead = true)
        {
            int colCount = currentDTable.Columns.Count;
            int rowCount = currentDTable.Rows.Count;

            if (isRowRead == false)
            {
                colCount = currentDTable.Rows.Count;
                rowCount = currentDTable.Columns.Count;
            }
            for (int col = 0; col < colCount; col++)
            {
                PointColumn point = new PointColumn();
                for (int row = 0; row < rowCount; row++)
                {
                    if (isRowRead)
                        point.PointAdd(currentDTable.Rows[row][col]);
                    else
                    {
                        point.PointAdd(currentDTable.Rows[col][row]);
                    }
                }

                columnNull.pointColumns.Add(point);
            }


        }

        public void Calculation(bool isFullCalculation = true)
        {
            columnNull = ColumnTable_MainCalculate(columnNull);

            if (isFullCalculation == true)
            {
                for (int i = 0; i < columnNull.pointColumns.Count; i++)
                {
                    columnMinus.pointColumns.Add(columnNull.pointColumns[i] - GeneralData.assureValue);
                    columnPlus.pointColumns.Add(columnNull.pointColumns[i] + GeneralData.assureValue);
                }

                columnPlus = ColumnTable_MainCalculate(columnPlus);
                columnMinus = ColumnTable_MainCalculate(columnMinus);

                int n = columnNull.responces.Count();
                E = new List<double>();
                L = new List<double>();
                for (int i = 0; i < n; i++)
                {
                    E.Add(Math.Abs(columnPlus.responces[i] - columnMinus.responces[i]));
                    L.Add(Math.Abs(columnNull.responces[i] - columnNull.responces[0]));
                }

                LE = new List<double>();
                LEs = new List<string>();

                for (int i = 0; i < n; i++)
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

        public List<bool> Flags()
        {
            List<bool> result = new List<bool>();
            foreach (double d in LE)
            {
                if (d == 1)
                {
                    result.Add(true);
                }
                else
                {
                    result.Add(false);
                }

            }

            return result;
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
            List<double> responce = new List<double>();
            for (int i = 0; i < pointColumn.Count; i++)
            {
                responce.Add(Math.Sqrt(pointColumn[i].Sum(2)));
            }
            return responce;
        }

        private List<double> Alphas_Calculation(List<double> responce, List<PointColumn> pointColumnsNull)
        {
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
            double avSum = AvarageSumm(doubles);
            double[] list = new double[doubles.Count()];

            list[0] = smooth * doubles[0] + (1 - smooth) * avSum;

            for (int i = 1; i < doubles.Count(); i++)
            {
                list[i] = smooth * doubles[i] + (1 - smooth) * list[i - 1];
            }
            double respon = smooth * AvarageSumm(list) + (1 - smooth) * list[list.Count() - 1];

            return respon;
        }

        private double AvarageSumm(double[] doubles)
        {
            return doubles.Sum() / doubles.Length;
        }

    }
}
