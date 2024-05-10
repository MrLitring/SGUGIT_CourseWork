using SGUGIT_CourseWork.HelperCode;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using SGUGIT_CourseWork.HelperCode.Other;

namespace SGUGIT_CourseWork.Forms
{
    /// <summary>
    /// Клас, предоставляющие основные общие данные и функциональность для этих данных
    /// </summary>
    public partial class P2_Level2 : Form
    {
        private FormHelperCode formHelperCode;
        private DataTable dTable;
        List<DataTableCalculation> dataTables;
        string timeName = "Block_Other";
        string lastName = "";




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


            if (GeneralData.underBlockStorage_1.Count == 0)
            {
                string timeName2 = "Block_";
                DataTableStorage underBlock = new DataTableStorage(timeName, GeneralData.dataTable);
                GeneralData.underBlockStorage_1.Add(underBlock);


                for (int i = 0; i < GeneralData.blockCount; i++)
                {
                    string name = timeName2 + i.ToString();

                    underBlock = new DataTableStorage(name, GeneralData.dataTable);
                    GeneralData.underBlockStorage_1.Add(underBlock);
                    comboBox1.Items.Add(underBlock.Name);
                }

                for (int i = 0; i < dTable.Columns.Count; i++)
                    listBox1.Items.Add(dTable.Columns[i].ColumnName);
            }
            else
            {
                for (int i = 0; i < GeneralData.underBlockStorage_1.Count; i++)
                {
                    if (GeneralData.underBlockStorage_1[i].Name != timeName)
                    {
                        comboBox1.Items.Add(GeneralData.underBlockStorage_1[i].Name);
                        DataTable table = GeneralData.underBlockStorage_1[i].GetUnderBlock();
                        for (int i2 = 0; i2 < table.Columns.Count; i2++)
                            listBox2.Items.Add(table.Columns[i2].ColumnName);
                    }
                    else
                    {
                        for (int i2 = 0; i2 < GeneralData.underBlockStorage_1[i].GetUnderBlock().Columns.Count; i2++)
                            listBox1.Items.Add(GeneralData.underBlockStorage_1[i].GetUnderBlock().Columns[i2].ColumnName);
                    }

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


            DataTableStorage linkTable = DTS_Search(comboBox1.SelectedItem.ToString(), GeneralData.underBlockStorage_1);

            if (linkTable == null) return;
            SaveUnderBlock(listBox2, linkTable);

            linkTable = DTS_Search(timeName, GeneralData.underBlockStorage_1);
            if (linkTable == null) return;
            SaveUnderBlock(listBox1, linkTable);
            //DataTable linkTable = DTC_Search(comboBox1.SelectedItem.ToString()).currentDTable;
            //linkTable.Clear();
            //linkTable.Rows.Clear();
            //linkTable.Columns.Clear();
            //SaveBlock(listBox2, linkTable);

            //if (DTC_Search(timeName).currentDTable == null)
            //{
            //    linkTable = new DataTable();
            //}
            //linkTable = DTC_Search(timeName).currentDTable;
            //linkTable.Clear();
            //linkTable.Rows.Clear();
            //linkTable.Columns.Clear();

            //SaveBlock(listBox1, linkTable);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTableStorage dataTableStorage = DTS_Search(comboBox1.SelectedItem.ToString(), GeneralData.underBlockStorage_1);
            if (dataTableStorage == null) return;

            DataTable currentTable = dataTableStorage.GetUnderBlock();

            listBox2.Items.Clear();
            for (int i = 0; i < currentTable.Columns.Count; i++)
                listBox2.Items.Add(currentTable.Columns[i].ColumnName);




            //DataTableCalculation currentDTC = DTC_Search(comboBox1.SelectedItem.ToString());
            //currentDTable = currentDTC.currentDTable;
            //listBox2.Items.Clear();

            //for (int i = 0; i < currentDTable.Columns.Count; i++)
            //{
            //    listBox2.Items.Add(currentDTable.Columns[i].ColumnName);
            //}
        }


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



        private void ValueSwap(ListBox list1, ListBox list2, int index)
        {
            if (index == -1) return;

            list2.Items.Add(list1.Items[index].ToString());
            list1.Items.RemoveAt(index);
        }

        private void SaveUnderBlock(ListBox listBox, DataTableStorage tableStorage)
        {
            DataTable table = new DataTable();

            for (int i = 0; i < listBox.Items.Count; i++)
            {
                string colName = listBox.Items[i].ToString();
                if (tableStorage.isColumnExist(colName) == false)
                {
                    table.Columns.Add(colName);
                }
            }

            tableStorage.DataTableDeconstuction(table);
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

        private DataTableStorage DTS_Search(string name, List<DataTableStorage> DTstorage)
        {
            foreach (DataTableStorage elem in DTstorage)
            {
                if (elem.Name == name)
                    return elem;
            }

            return null;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < GeneralData.dataTables.Count; i++)
            {

            }
        }
    }
}
