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
        public List<PointColumn> pointColumns;
        private DataTable dtable = new DataTable();
        private DataGridView dataGridView = new DataGridView();

        public DataTableWork()
        {
            dtable = GeneralData.DataTable;
            pointColumns = new List<PointColumn>();
        }
        public DataTableWork(DataTable dataTable) : this()
        {
            this.dtable = dataTable;
        }
        public DataTableWork(DataTable dataTable, List<PointColumn> pointColumns) : this(dataTable)
        {
            this.pointColumns = pointColumns;
        }

        public void DataGridFill(DataGridView gridView)
        {
            this.dataGridView = gridView;
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            //
            // Даём новые колонки и строки
            //
            for(int i = 0; i < pointColumns.Count; i++)
            {
                dataGridView.Columns.Add(i.ToString(), i.ToString());
            }
            for(int i = 0; i < pointColumns[0].Points.Count;i++)
            {
                dataGridView.Rows.Add();
            }

            //
            // Заполняем
            //
            for(int col = 0; col < pointColumns.Count; col++)
            {
                for (int row = 0; row < pointColumns[col].Points.Count; row++)
                {
                    dataGridView.Rows[row].Cells[col].Value = pointColumns[col].Points[row].ToString();
                }
            }
        }

        public void RowAdd(DataGridView dataGridView, List<double> list)
        {
            int min = Math.Min(dataGridView.Columns.Count, list.Count);
            for(int i = 0; i < min; i++)
            {
                dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[i].Value = list[i].ToString();
            }
        }

        public List<double> M()
        {
            List<double> list = new List<double>();
            
            for(int i = 0; i < pointColumns.Count; i++)
            {
                list.Add(Math.Round(Math.Sqrt(pointColumns[i].Sum(2)), 4));
            }


            return list;
        }

        public List<double> A() 
        {
            List<double> list = new List<double>();

            for (int i = 0; i < pointColumns.Count; i++)
            {
                list.Add(Math.Round(Math.Acos(pointColumns[i].Sum(2)), 4));
            }


            return list;
        }

        public void AddValue(double value)
        {
            for(int col = 0; col < pointColumns.Count;col++)
            {
                PointColumn point = new PointColumn();
                for (int row = 0; row < pointColumns[col].Points.Count;row++)
                {
                    point.PointAdd(pointColumns[col].Points[row] + value);
                }
                pointColumns[col] = point;
            }

        }


    }
}
