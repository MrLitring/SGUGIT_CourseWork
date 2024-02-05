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
using System.Security;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F0_MainLoad : Form
    {
        FormMoved formMoved;
        F1_Window formShower;
        private bool isOpenDB = false;

        public F0_MainLoad()
        {
            InitializeComponent();
            formMoved = new FormMoved(this);
            NewShoweGenerate();

            FormMove();
        }

        private void MenuStrip_File_Click(object sender, EventArgs e)
        {
            if ((sender is ToolStripItem) == false) GenerallCode.WarningMessage(
                sender.GetType().Name, 
                sender.ToString(), 
                "ToolStripItem");

            if ((sender as ToolStripItem).Name == "StripClose") Application.Exit();
            string named = (sender as ToolStripItem).Name;

            switch (named)
            {
                case "StripNewDataBase":
                    {
                        F2_NewDataBase f2_NewDataBase = new F2_NewDataBase();
                        f2_NewDataBase.ShowDialog();
                        break;
                    }
                case "StripOpenDataBase":
                    {
                        
                        break;
                    }

                default:
                    {
                        MessageBox.Show($"{sender.GetType().Name}(\"{named}\") - Не было назначено",
                            "Осторожно",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

                        break;
                    }
            }
        }

        private void MenuStrip_Windows_Click(object sender, EventArgs e)
        {
            if ((sender is ToolStripItem) == false) GenerallCode.WarningMessage(
                sender.GetType().Name,
                sender.ToString(),
                "ToolStripItem");

            string named = (sender as ToolStripItem).Name;
            switch (named)
            {
                case "StripOneWindow":
                    {

                        break;
                    }
                case "StripTwoWindow":
                    {

                        break;
                    }
                case "StripCloseAllWindow":
                    {
                        NewShoweGenerate();
                        break;
                    }
            }
        }


        private void NewShoweGenerate()
        {
            if (formShower != null)
            {
                panel1.Controls.Remove(formShower);
                formShower.Close();

                formShower = null;
                panel1.Update();
            }

            formShower = new F1_Window();
            formShower.TopLevel = false;
            formShower.Dock = DockStyle.Fill;

            panel1.Controls.Add(formShower);
            formShower.Show();
            panel1.Update();
        }



        private void FormMove()
        {
            formMoved.ControlAdd(panel1);
        }


    }
}
