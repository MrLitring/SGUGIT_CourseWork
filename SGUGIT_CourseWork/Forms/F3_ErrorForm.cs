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
    public partial class F3_ErrorForm : Form
    {
        public enum ErrorList
        {
            notExistDataBase,
            mixDataBase
        }

        public F3_ErrorForm()
        {
            InitializeComponent();
            EventBus.onError += this.Errors;
        }

        private void Errors(int errornumber)
        {

        }
    }
}
