using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode
{
    internal class FormOpenCode
    {
        public static void OpenForm(Form currentForm, Form chilldForm, Control controlShow)
        {
            if (currentForm != null)
                currentForm.Close();

            currentForm = chilldForm;
            currentForm.TopLevel = false;
            currentForm.Size = controlShow.Size;
            currentForm.BringToFront();
            currentForm.Dock = DockStyle.Fill;

            controlShow.Controls.Clear();
            controlShow.Controls.Add(currentForm);
            currentForm.Show();
        }

        public static void OpenForm(Form chilldForm, Control controlShow)
        {
            if (controlShow.Controls.Count != 0)
            {
                (controlShow.Controls[0] as Form).Close();
                controlShow.Controls.Clear();
            }

            chilldForm.TopLevel = false;
            chilldForm.Size = controlShow.Size;
            chilldForm.BringToFront();
            chilldForm.Dock = DockStyle.Fill;
            chilldForm.FormBorderStyle = FormBorderStyle.None;

            controlShow.Controls.Add(chilldForm);
            chilldForm.Show();
        }
    }
}
