using SGUGIT_CourseWork.HelperCode;
using System;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F1_Window : Form
    {
        public bool isShowSecondForm = false;
        public Form ParentForm = null;
        public Form currentForm = null;

        public F1_Window()
        {
            InitializeComponent();
        }
        public F1_Window(Form parrentForm) : this()
        {
            ParentForm = parrentForm;
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
