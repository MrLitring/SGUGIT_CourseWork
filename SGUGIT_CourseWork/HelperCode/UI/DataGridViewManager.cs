using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode.UI
{
    /// <summary>
    /// Класс, для управления таблицей. Дизайн и вывод данных
    /// </summary>
    internal class DataGridViewManager
    {
        private DataGridView dataGridView;
        private bool isColored;



        public int ColumnCount { get {  return dataGridView.Columns.Count; } }
        public int RowCount { get { return dataGridView.Rows.Count;} }



        private DataGridViewManager()
        {
            isColored = false;
        }
        public DataGridViewManager(DataGridView dataGridView, bool isColored = false) : this()
        {
            this.dataGridView = dataGridView;
            this.isColored = isColored;

            GridViewDesign();
        }



        public void Clear(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
        }

        public void Colorize(List<bool> bools, int row)
        {
            for(int col = 0; col < bools.Count; col++)
            {
                if (bools[col] == true)
                    dataGridView.Rows[row].Cells[col].Style.BackColor = Color.Green;
                else
                    dataGridView.Rows[row].Cells[col].Style.BackColor = Color.Red;
            }

        }
        public void ColorizeCol(List<bool> bools, int column)
        {
            for (int row = 0; row < bools.Count; row++)
            {
                if (bools[row] == true)
                    dataGridView.Rows[row].Cells[column].Style.BackColor = Color.Green;
                else
                    dataGridView.Rows[row].Cells[column].Style.BackColor = Color.Red;
            }

            

        }


        public void RowAdd(List<double> list, int roundValue = 7, int offset = 0)
        {
            for (int i = dataGridView.Rows.Count-1; i < list.Count; i++)
            {
                dataGridView.Rows.Add();
            }

            for (int i = offset; i < list.Count; i++)
            {
                dataGridView.Rows[i].Cells[dataGridView.Columns.Count - 1].Value = Math.Round(list[i], roundValue);
            }

        }

        public void RowAdd(List<string> list, int roundValue = 7, int offset = 0)
        {
            for (int i = dataGridView.Rows.Count; i < list.Count+1; i++)
            {
                dataGridView.Rows.Add();
            }

            for (int i = offset; i < list.Count; i++)
            {
                dataGridView.Rows[i].Cells[dataGridView.Columns.Count - 1].Value = list[i];
            }

        }

        public void ColumnAdd(string name, List<double> list, int roundValue = 7)
        {
            dataGridView.Columns.Add(name, name);
            RowAdd(list, roundValue);
        }
        public void ColumnAdd(string name, List<string> list, int roundValue = 7)
        {
            dataGridView.Columns.Add(name, name);
            RowAdd(list, roundValue);
        }

        private void GridViewDesign()
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
        }
    }
}
