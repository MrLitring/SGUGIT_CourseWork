using SGUGIT_CourseWork.HelperCode;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

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
        string timeName = "Block_Other";




        public P2_Level2()
        {
            InitializeComponent();
        }



        private void P3_Level2_Load(object sender, EventArgs e)
        {
            dTable = GeneralData.dataTable;
            formHelperCode = new FormHelperCode();
            dataTables = GeneralData.dataTables;
            formHelperCode.ControlOut_Set(panel1);

            GeneralData.ImageUpdate();
            pictureBox1.Image = GeneralData.imageSheme;

            if (GeneralData.dataTables.Count <= 2)
            {
                string name = "block_";
                for (int i = 0; i < GeneralData.blockCount; i++)
                {
                    comboBox1.Items.Add(name + i.ToString());
                    comboBox2.Items.Add(name + i.ToString());

                    DataTableCalculation calculation = new DataTableCalculation(new DataTable());
                    calculation.Name = name + i.ToString();
                    dataTables.Add(calculation);
                }

                for (int i = 0; i < dTable.Columns.Count; i++)
                {
                    if(dTable.Columns[i].ColumnName != "Эпоха")
                    listBox1.Items.Add(dTable.Columns[i].ColumnName);
                }
            }
            else
            {
                for (int i = 0; i < dataTables.Count; i++)
                {
                    comboBox1.Items.Add(GeneralData.dataTables[i].Name);
                    if(GeneralData.dataTables[i].Name != timeName)
                        comboBox2.Items.Add(GeneralData.dataTables[i].Name);
                }
            }

            comboBox1.SelectedIndex = 0;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((sender as ListBox).Items.Count == 0 && comboBox1.SelectedIndex == -1) return;

            if ((sender as ListBox).Name == "listBox1")
                ValueSwap(listBox1, listBox2, listBox1.SelectedIndex);
            else
                ValueSwap(listBox2, listBox1, listBox2.SelectedIndex);

            DataTable linkTable = DTC_Search(comboBox1.SelectedItem.ToString()).currentDTable;
            linkTable.Clear();
            linkTable.Rows.Clear();
            linkTable.Columns.Clear();
            SaveBlock(listBox2, linkTable);

            if (DTC_Search(timeName).currentDTable == null)
            {
                linkTable = new DataTable();
            }
            linkTable = DTC_Search(timeName).currentDTable;
            linkTable.Clear();
            linkTable.Rows.Clear();
            linkTable.Columns.Clear();

            SaveBlock(listBox1, linkTable);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTableCalculation currentDTC = DTC_Search(comboBox1.SelectedItem.ToString());
            currentDTable = currentDTC.currentDTable;
            listBox2.Items.Clear();

            for (int i = 0; i < currentDTable.Columns.Count; i++)
            {
                listBox2.Items.Add(currentDTable.Columns[i].ColumnName);
            }
        }

        string lastName = "";
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;


            string name = comboBox2.SelectedItem.ToString();

            if (lastName != name && name != timeName)
            {
                DataTableCalculation table = DTC_Search(name);
                if (table == null) return;

                table.ColumnFill(false);
                table.Calculation(true);


                formHelperCode.PageLoad(new P2_Level1(table.currentDTable));
                lastName = name;
            }

        }



        private DataTable SaveBlock(ListBox listBox, DataTable table)
        {
            List<int> strings = new List<int>();
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                strings.Add(Convert.ToInt32(listBox.Items[i]));
            }
            strings.Sort();

            for (int item = 0; item < strings.Count; item++)
            {
                string colName = strings[item].ToString();
                PointColumn column = FillColumnSearch(colName);
                if (ColumnSearch(colName, table) == false)
                {
                    table.Columns.Add(colName, typeof(double));
                    int lastCol = table.Columns.Count - 1;

                    for (int row = table.Rows.Count; row < column.GetPointList.Count; row++)
                    {
                        table.Rows.Add();
                    }

                    for (int row = 0; row < column.GetPointList.Count; row++)
                    {
                        table.Rows[row][lastCol] = column.GetPointList[row];
                    }
                }
            }


            return table;
        }

        private DataTableCalculation DTC_Search(string name)
        {
            foreach (DataTableCalculation elem in dataTables)
            {
                if (elem.Name == name)
                    return elem;
            }

            return null;
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

            for (int col = 0; col < dTable.Columns.Count; col++)
            {
                if (dTable.Columns[col].ColumnName == colName)
                {
                    column.ColumnName = colName;
                    for (int row = 0; row < dTable.Rows.Count; row++)
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            for(int i =0; i < GeneralData.dataTables.Count; i++)
            {
                
            }
        }
    }
}
