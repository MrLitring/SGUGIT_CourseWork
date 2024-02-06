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
    public partial class F2_NewDataBase : Form
    {
        FormMoved formMoved;

        public F2_NewDataBase()
        {
            InitializeComponent();
            formMoved = new FormMoved(this);
            formMoved.ControlAdd(this);
        }
    }
}
