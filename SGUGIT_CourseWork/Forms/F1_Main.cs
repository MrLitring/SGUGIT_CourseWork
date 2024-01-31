using SGUGIT_CourseWork.Additional_Commands;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F1_Main : Form
    {
        public F1_Main()
        {
            InitializeComponent();
            FormMove();
        }

        private void FormMove()
        {
            FormMoved formMoved = new FormMoved(this);
            formMoved.ControlAdd(this);
        }
    }
}
