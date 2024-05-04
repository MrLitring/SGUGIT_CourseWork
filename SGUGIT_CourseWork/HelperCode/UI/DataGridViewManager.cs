using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        //



        private DataGridViewManager()
        {
            isColored = false;
        }
        public DataGridViewManager(DataGridView dataGridView, bool isColored = false) : this()
        {
            this.dataGridView = dataGridView;
            this.isColored = isColored;

            
        }



        public void RowAdd(List<double> list, int roundValue = 7, int offset = 0)
        {
            for (int i = dataGridView.Rows.Count; i < list.Count; i++)
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
            for (int i = dataGridView.Rows.Count; i < list.Count; i++)
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


    }
}
