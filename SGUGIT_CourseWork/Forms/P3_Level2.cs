using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.SqlCode;

namespace SGUGIT_CourseWork.Forms
{
    /// <summary>
    /// Клас, предоставляющие основные общие данные и функциональность для этих данных
    /// </summary>
    public partial class P3_Level2 : Form
    {
        private FormHelperCode formHelperCode;
        private DataTable dTable;
        List<PointColumn> Rows;


        public P3_Level2()
        {
            InitializeComponent();
            formHelperCode = new FormHelperCode();
            Rows = new List<PointColumn>();
        }



        private void P3_Level2_Load(object sender, EventArgs e)
        {
            dTable = GeneralData.dataTable;


            pictureBox1.Image = GeneralData.imageSheme;

            string name = "block_";
            for(int i = 0; i < GeneralData.blockCount; i++)
            {
                comboBox1.Items.Add(name + i.ToString());
            }

            for(int i = 0; i < dTable.Columns.Count; i++)
            {
                listBox1.Items.Add(dTable.Columns[i].ColumnName);
            }

        }


        private void ValueSwap(ListBox list1, ListBox list2, int index)
        {
            if (index == -1) return;

            list2.Items.Add(list1.Items[index].ToString());
            list1.Items.RemoveAt(index);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((sender as ListBox).Items.Count == 0) return;

            if((sender as ListBox).Name == "listBox1") 
                ValueSwap(listBox1, listBox2, listBox1.SelectedIndex);
            else
                ValueSwap(listBox2, listBox1, listBox2.SelectedIndex);


            
        }
    }
}
