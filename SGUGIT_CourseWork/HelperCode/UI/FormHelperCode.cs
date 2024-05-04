using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode
{
    public class FormHelperCode
    {
        private Control currentControlOut;
        private Form currentForm;

        
        public FormHelperCode()
        {
            this.currentControlOut = null;
            this.currentForm = null;
        }


        public void ControlOut_Set(Control control)
        {
            this.currentControlOut = control;
        }

        public void PageLoad(Form form, DockStyle dockStyle = DockStyle.Fill)
        {
            if (this.currentControlOut == null) throw new Exception("Для отображения формы требуется Control, который сейчас  = null");

            currentControlOut.Controls.Clear();

            if(currentForm != null) currentForm.Close();
            currentForm = form;

            currentForm.TopLevel = false;
            currentForm.Size = currentControlOut.Size;
            currentForm.BringToFront();
            currentForm.Dock = dockStyle;

            currentControlOut.Controls.Add(currentForm);
            currentForm.Show();
        }

        public void CloseAllForms()
        {
            if(currentForm != null) 
                currentForm.Close();
            if(currentControlOut.Controls.Count > 0)
                currentControlOut.Controls.Clear();
        }

        public string FIleBrowser(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = filter
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName; ;
            }
            else
                return null;

        }

    }
}
