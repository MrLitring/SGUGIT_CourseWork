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
    public partial class F0_MainLoad : Form
    {
        public F0_MainLoad()
        {
            InitializeComponent();
            FormMoveInit();

        }

        private void FormMoveInit()
        {
            FormMoved formMoved = new FormMoved(this);
            formMoved.ControlAdd(this);
            formMoved.ControlAdd(panel1);
        }

        private void CreateNew()
        {

        }

        private void HistoryLoad()
        {

        }

        private void HistorySave()
        {

        }
    }
}
