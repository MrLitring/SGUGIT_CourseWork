using SGUGIT_CourseWork.HelperCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            if ((sender is ToolStripItem) == false) 
                HelperCode.Message.WarningMessage(
                sender.GetType().Name,
                sender.ToString(),
                "ToolStripItem");

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
