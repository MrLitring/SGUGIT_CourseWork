using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode
{
    internal static class Message
    {
        public static void WarningMessage(string name, string type, string value)
        {
            MessageBox.Show(
                $"{type}(\"{name}\") :  Не является {value}",
                "Осторожно",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }
}
