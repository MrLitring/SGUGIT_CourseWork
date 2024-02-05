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
                GenerallCode.WarningMessage(
                sender.GetType().Name,
                sender.ToString(),
                "ToolStripItem");

            switch((sender as ToolStripItem).Name)
            {
                case "StripDataBase":
                    {
                        GenerallCode.OpenForm(currentForm, new P1_DataBase(), panel1);
                        break;
                    }
            }
        }

        private void FormOpen(Form form)
        {
            if(panel1.Controls.Count != 0)
                panel1.Controls.Remove(currentForm);

            currentForm = form;
            currentForm.TopLevel = false;
            currentForm.Dock = DockStyle.Fill;
            currentForm.Update();

            panel1.Controls.Add(currentForm);
            currentForm.BringToFront();
            currentForm.Show();
            panel1.Update();
        }
    }
}
