using SGUGIT_CourseWork.Additional_Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SGUGIT_CourseWork.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F0_MainLoad : Form
    {
        public F0_MainLoad()
        {
            InitializeComponent();
            MenuStrip_Clicked();
            FormMove();
        }

        private void MenuStrip_Clicked()
        {
            Strip_Close.Click += (s, e) =>
            {
                F2_DataBase form1 = new F2_DataBase();
                panel1.Controls.Clear();
                form1.TopLevel = false;

                panel1.Controls.Add(form1);
                form1.Dock = DockStyle.Fill;
                form1.Update();
                form1.Show();
            };
        }

        private void FormMove()
        {
            FormMoved formMoved = new FormMoved(this);
            //formMoved.ControlAdd(header1);
        }
    }
}
