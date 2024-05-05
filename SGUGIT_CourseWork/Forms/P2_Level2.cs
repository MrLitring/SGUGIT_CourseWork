using SGUGIT_CourseWork.HelperCode;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Reflection;

namespace SGUGIT_CourseWork.Forms
{
    /// <summary>
    /// Клас, предоставляющие основные общие данные и функциональность для этих данных
    /// </summary>
    public partial class P2_Level2 : Form
    {
        private FormHelperCode formHelperCode;
        private DataTable dTable;
        private DataTable currentDTable;
        List<DataTableCalculation> dataTables;

        HelperCode.UI.DataGridViewManager gridView;
        HelperCode.UI.ChartManager chartView1;
        HelperCode.UI.ChartManager chartView2;






        public P2_Level2()
        {
            InitializeComponent();
        }



        private void P3_Level2_Load(object sender, EventArgs e)
        {
            dTable = GeneralData.dataTable;
            formHelperCode = new FormHelperCode();
            dataTables = GeneralData.dataTables;

            gridView = new HelperCode.UI.DataGridViewManager(dataGridView1);
            chartView1 = new HelperCode.UI.ChartManager(chart1, checkedListBox1);
            chartView2 = new HelperCode.UI.ChartManager(chart2, checkedListBox2);


            pictureBox1.Image = GeneralData.imageSheme;

            string name = "block_";
            for(int i = 0; i < GeneralData.blockCount; i++)
            {
                comboBox1.Items.Add(name + i.ToString());
                comboBox2.Items.Add(name + i.ToString());

                DataTableCalculation calculation = new DataTableCalculation(new DataTable());
                calculation.Name = name + i.ToString();
                dataTables.Add(calculation);
            }

            for(int i = 1; i < dTable.Columns.Count; i++)
            {
                listBox1.Items.Add(dTable.Columns[i].ColumnName);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((sender as ListBox).Items.Count == 0 && comboBox1.SelectedIndex == -1) return;

            if((sender as ListBox).Name == "listBox1") 
                ValueSwap(listBox1, listBox2, listBox1.SelectedIndex);
            else
                ValueSwap(listBox2, listBox1, listBox2.SelectedIndex);


            
        }

        private void toolSaveData_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == -1) return;
            string name = comboBox1.SelectedItem.ToString();

            DataTable linkTable = DTC_Search(name).currentDTable;
            linkTable.Clear();
            linkTable.Rows.Clear();
            linkTable.Columns.Clear();

            List<int> strings = new List<int>();
            for (int i = 0; i < listBox2.Items.Count; i++)
            {
                strings.Add(Convert.ToInt32(listBox2.Items[i]));
            }
            strings.Sort();

            for(int item  = 0; item < strings.Count; item++)
            {
                string colName = strings[item].ToString();
                PointColumn column = FillColumnSearch(colName);
                if(ColumnSearch(colName, linkTable) == false)
                {
                    linkTable.Columns.Add(colName, typeof(double));
                    int lastCol = linkTable.Columns.Count - 1;

                    for(int row = linkTable.Rows.Count; row < column.GetPointList.Count; row++)
                    {
                        linkTable.Rows.Add();
                    }

                    for (int row = 0; row < column.GetPointList.Count; row++)
                    {
                        linkTable.Rows[row][lastCol] = column.GetPointList[row];
                    }
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTableCalculation currentDTC = DTC_Search(comboBox1.SelectedItem.ToString());
            currentDTable = currentDTC.currentDTable;
            textBox1.Text = currentDTC.Name;
            listBox2.Items.Clear();
            
            for(int i = 0; i < currentDTable.Columns.Count; i++)
            {
                listBox2.Items.Add(currentDTable.Columns[i].ColumnName);
            }
        }



        private void ValueSwap(ListBox list1, ListBox list2, int index)
        {
            if (index == -1) return;

            list2.Items.Add(list1.Items[index].ToString());
            list1.Items.RemoveAt(index);
        }

        private PointColumn FillColumnSearch(string colName)
        {
            PointColumn column = new PointColumn();

            for(int col = 0; col < dTable.Columns.Count; col++)
            {
                if (dTable.Columns[col].ColumnName == colName)
                {
                    column.ColumnName = colName;
                    for(int row = 0; row < dTable.Rows.Count; row++)
                    {
                        column.PointAdd(dTable.Rows[row][col]);
                    }
                    break;
                }
            }

            return column;
        }

        private bool ColumnSearch(string colName, DataTable dataTable)
        {
            for (int col = 0; col < dataTable.Columns.Count; col++)
            {
                if (dataTable.Columns[col].ColumnName == colName)
                {
                    return true;
                }
            }

            return false;
        }


        string lastName = "";
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;


            string name = comboBox2.SelectedItem.ToString();

            if (lastName != name)
            {
                DataTableCalculation table = DTC_Search(name);
                if (table == null) return;

                gridView.Clear();
                chartView1.Clear();
                chartView2.Clear();

                table.ColumnFill(false);
                table.Calculation(true);

                DataView(table);
                lastName = name;
            }
            
        }

        private void DataView(DataTableCalculation table)
        {
            gridView.Clear();
            chartView1.Clear();
            chartView2.Clear();            

            gridView.ColumnAdd("M-", table.columnMinus.responces, 4);
            gridView.ColumnAdd("M", table.columnNull.responces, 4);
            gridView.ColumnAdd("M+", table.columnPlus.responces, 4);

            gridView.ColumnAdd("A-", table.columnMinus.alphas, 4);
            gridView.ColumnAdd("A", table.columnNull.alphas, 4);
            gridView.ColumnAdd("A+", table.columnPlus.alphas, 4);

            gridView.ColumnAdd("E", table.E, 10);
            gridView.ColumnAdd("L", table.L, 10);
            gridView.ColumnAdd("L<=E", table.LE, 4);
            gridView.ColumnAdd("LEs", table.LEs, 10);



            chartView1.Series_Add("M");
            chartView1.Series_Add("M-");
            chartView1.Series_Add("M+");
            chartView1.AddPointXY("M-", table.columnMinus.responces, table.columnMinus.alphas);
            chartView1.AddPointXY("M", table.columnNull.responces, table.columnNull.alphas);
            chartView1.AddPointXY("M+", table.columnPlus.responces, table.columnPlus.alphas);

            chartView2.Series_Add("M");
            chartView2.Series_Add("M-");
            chartView2.Series_Add("M+");
            chartView2.AddPointY("M-", table.columnMinus.responces);
            chartView2.AddPointY("M", table.columnNull.responces);
            chartView2.AddPointY("M+", table.columnPlus.responces);
        }


        private DataTableCalculation DTC_Search(string name)
        {
            foreach(DataTableCalculation elem in dataTables)
            {
                if(elem.Name == name)
                    return elem;
            }

            return null;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            

        }
    }
}
