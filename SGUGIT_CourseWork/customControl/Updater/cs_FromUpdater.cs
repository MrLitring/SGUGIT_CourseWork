using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.customControl
{
    internal class cs_FromUpdater : Component
    {
        public Form Form { get; set; }

        public bool FullUpdate
        {
            get
            {
                return false;
            }
            set
            {
                FullFormUpdate();
            }
        }
        public bool MainSettingsUpdate
        {
            get
            {
                return false;
            }
            set
            {
                MainSettingUpdate();
            }
        }


        private Size localeSize;

        private void FullFormUpdate()
        {
            if (Form == null) return;

            MainSettingUpdate();
        }

        private void MainSettingUpdate()
        {
            if (Form == null) return;

            localeSize = Form.Size;

            Form.FormBorderStyle = FormBorderStyle.None;
            Form.StartPosition = FormStartPosition.CenterScreen;

            Form.Font = new System.Drawing.Font
                (
                ConfigureSettings.FontName, 
                ConfigureSettings.FontSize(1),
                FontStyle.Regular
                );

            ColorUpdate();
            Form.Size = localeSize;
        }

        public void ColorUpdate()
        {

            Form.BackColor = ConfigureSettings.Colors(2);
            Form.ForeColor = ConfigureSettings.Colors(7);
        }
    }
}
