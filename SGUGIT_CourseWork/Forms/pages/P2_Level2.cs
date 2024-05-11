using SGUGIT_CourseWork.HelperCode;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using SGUGIT_CourseWork.HelperCode.Other;
using System.Xml.Serialization;

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
        const string blockName = "Block_";
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


            if (GeneralData.underBlockStorage_1.Count == 1)
            {
                FirstLoad();
            }
            else
            {
                SecondsLoad();
            }

            comboBox1.SelectedIndex = 0;
        }

        private void FirstLoad()
        {
            DataTableStorage underBlock;


            for (int i = 0; i < GeneralData.blockCount; i++)
            {
                string name = blockName + i.ToString();

                underBlock = new DataTableStorage(name, GeneralData.dataTable);
                GeneralData.underBlockStorage_1.Add(underBlock);
                comboBox1.Items.Add(underBlock.Name);
            }

            for (int i = 0; i < dTable.Columns.Count; i++)
                if(dTable.Columns[i].ColumnName != "Эпоха")
                listBox1.Items.Add(dTable.Columns[i].ColumnName);
        }

        private void SecondsLoad()
        {
            DataTable dataTable = GeneralData.underBlockStorage_1[0].GetUnderBlock();
            for (int i = 0; i < GeneralData.underBlockStorage_1[0].GetUnderBlock().Columns.Count; i++)
                listBox1.Items.Add(GeneralData.underBlockStorage_1[0].GetUnderBlock().Columns[i].ColumnName);

            for (int i = 1; i < GeneralData.underBlockStorage_1.Count; i++)
            {
                if (GeneralData.underBlockStorage_1[i].Name != blockName + "Other")
                {
                    comboBox1.Items.Add(GeneralData.underBlockStorage_1[i].Name);
                    DataTable table = GeneralData.underBlockStorage_1[i].GetUnderBlock();
                    for (int i2 = 0; i2 < table.Columns.Count; i2++)
                        listBox2.Items.Add(table.Columns[i2].ColumnName);
                }

            }
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

            linkTable = DTS_Search(blockName + "Other", GeneralData.underBlockStorage_1);
            if (linkTable == null) return;
            SaveUnderBlock(listBox1, linkTable);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTableStorage dataTableStorage = DTS_Search(comboBox1.SelectedItem.ToString(), GeneralData.underBlockStorage_1);
            if (dataTableStorage == null) return;

            DataTable currentTable = dataTableStorage.GetUnderBlock();

            listBox2.Items.Clear();
            for (int i = 0; i < currentTable.Columns.Count; i++)
                listBox2.Items.Add(currentTable.Columns[i].ColumnName);
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null) return;
            

            string name = comboBox2.SelectedItem.ToString();
            

            if (lastName != name)
            {
                DataTableStorage DT = DTS_Search(name, GeneralData.underBlockStorage_1);
                DataTable currentDateTable = DT.GetUnderBlock();
                DataTableCalculation currentTable = new DataTableCalculation(currentDateTable);
                if (currentTable == null) return;

                currentTable.ColumnFill(false);
                currentTable.Calculation(true);


                formHelperCode.PageLoad(new P2_Level1(currentTable.currentDTable));
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
                table.Columns.Add(colName);
            }

            tableStorage.DataTableDeconstuction(table);
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
            if (tabControl1.SelectedIndex == 1)
            {
                bool isNext = true;

                for(int i = 1; i < GeneralData.underBlockStorage_1.Count-1; i++)
                {
                    if (GeneralData.underBlockStorage_1[i].ColumnCount != GeneralData.underBlockStorage_1[i+1].ColumnCount)
                    {
                        isNext = false;
                        break;
                    }
                }

                if(isNext == false)
                {
                    FormHelperCode.MessageError(GeneralTextData.Error, GeneralTextData.Error_SystemCount);
                    tabControl1.SelectedIndex = 0;
                }

                comboBox2.Items.Clear();
                for (int i = 0; i < GeneralData.underBlockStorage_1.Count; i++)
                {
                    if (GeneralData.underBlockStorage_1[i].Name != blockName + "Other")
                    {
                        comboBox2.Items.Add(GeneralData.underBlockStorage_1[i].Name);
                    }

                }
            }
        }
    }
}
