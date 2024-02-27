using SGUGIT_CourseWork.HelperCode;
using System;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F1_Window : Form
    {
        public bool isShowSecondForm = false;
        public Form currentForm = null;

        public F1_Window()
        {
            InitializeComponent();
        }

        private void StripMenu_Space_Click(object sender, EventArgs e)
        {
            switch((sender as ToolStripItem).Name)
            {
                case "StripDataBase":
                    {
                        HelperCode.FormOpenCode.OpenForm(currentForm, new P1_DataBase(), panel1);
                        break;
                    }
            }
        }

    }
}
