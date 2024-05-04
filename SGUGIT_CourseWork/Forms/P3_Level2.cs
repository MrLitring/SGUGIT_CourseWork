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

namespace SGUGIT_CourseWork.Forms
{
    public partial class P3_Level2 : Form
    {
        FormHelperCode formHelperCode;


        private struct TableWork
        {
            public string name;
            public List<string> points;

            public void Init()
            {
                name = "";
                points = new List<string>();
            }
        }


        List<TableWork> tableWorks;




        public P3_Level2()
        {
            InitializeComponent();
            formHelperCode = new FormHelperCode();
            tableWorks = new List<TableWork>();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void P3_Level2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = GeneralData.imageSheme;

            string name = "block_";
            for(int i = 0; i < GeneralData.blockCount; i++)
            {
                comboBox1.Items.Add(name + i.ToString());
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
