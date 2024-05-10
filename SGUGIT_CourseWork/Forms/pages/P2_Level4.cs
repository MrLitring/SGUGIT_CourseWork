using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.Other;
using SGUGIT_CourseWork.HelperCode.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P2_Level4 : Form
    {
        ChartManager chartManager;


        public P2_Level4()
        {
            InitializeComponent();
        }

        private void P2_Level4_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            chartManager = new ChartManager(chart1, checkedListBox1);

            comboBox1.Items.Add("Main");

            for (int i = 1; i < GeneralData.underBlockStorage_1.Count; i++)
            {
                comboBox1.Items.Add(GeneralData.underBlockStorage_1[i].Name);
            }
            for (int i = 0; i < GeneralData.underBlockStorage_2.Count; i++)
            {
                comboBox1.Items.Add(GeneralData.underBlockStorage_2[i].Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;
            chartManager.Clear();

            string name = comboBox1.SelectedItem.ToString();

            DataTable table;

            if (name == "Main")
            {
                table = GeneralData.dataTable.Clone();
                for(int row =0;  row < GeneralData.dataTable.Rows.Count; row++)
                    table.ImportRow(GeneralData.dataTable.Rows[row]);
                table.Columns.RemoveAt(0);
            }
            else
            {
                DataTableStorage tableStorage = StorageSearch(name, GeneralData.underBlockStorage_1);
                if (tableStorage == null) return;
                table = tableStorage.GetUnderBlock();
            }

            
            for (int col = 0; col < table.Columns.Count; col++)
            {
                string serName = table.Columns[col].ColumnName;
                chartManager.Series_Add(serName);
                for(int row = 0; row < GeneralData.dataTable.Rows.Count; row++)
                {
                    double X = Convert.ToDouble(GeneralData.dataTable.Rows[row][0]);
                    double Y = Convert.ToDouble(table.Rows[row][col]);

                    chartManager.AddPointXY(serName, X, Y);
                }
            }

            List<string> list = new List<string>();
            for(int row = 0; row < GeneralData.dataTable.Rows.Count;row++)
            {
                list.Add(GeneralData.dataTable.Rows[row][0].ToString());
            }
            chartManager.AnnotanionCreate(list);

        }


        private DataTableStorage StorageSearch(string name, List<DataTableStorage> storages)
        {
            foreach (DataTableStorage storage in storages)
            {
                if (storage.Name == name) return storage;
            }

            return null;
        }
    }
}
