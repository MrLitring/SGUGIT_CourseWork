
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Additional_Commands
{
    public static class GenerallCode
    {

        public static void WarningMessage(string name, string type, string value)
        {
            MessageBox.Show(
                $"{type}(\"{name}\") :  Не является {value}",
                "Осторожно",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        public static void OpenForm(Form currentForm, Form chilldForm, Control controlShow)
        {
            if(currentForm != null)
                currentForm.Close();

            currentForm = chilldForm;
            currentForm.TopLevel = false;
            currentForm.Size = controlShow.Size;
            currentForm.BringToFront();
            currentForm.Dock = DockStyle.Fill;

            controlShow.Controls.Add(currentForm);
            currentForm.Show();
        }

    }
}
