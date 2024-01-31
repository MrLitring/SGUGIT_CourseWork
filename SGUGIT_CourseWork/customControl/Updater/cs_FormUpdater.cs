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
    internal class cs_FormUpdater : Component
    {
        public Form Form { get; set; }

        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public int BackColorNum { get; set; }
        public int ForeColorNum { get; set; }

        public bool onFormUpdate
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
        public bool onColorUpdate
        {
            get
            {
                return false;
            }
            set
            {
                ColorUpdate();
            }
        }

        private Size localeSize;


        public cs_FormUpdater()
        {
            BackColorNum = 1;
            ForeColorNum = 1;
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
                ConfigureSettings.FontSize(2),
                FontStyle.Regular
                );

            ColorUpdate();
            Form.Size = localeSize;
        }

        public void ColorUpdate()
        {
            if(Form == null) return;

            BackColor = ConfigureSettings.Colors(BackColorNum);
            ForeColor = ConfigureSettings.Colors(ForeColorNum);
            Form.BackColor = ConfigureSettings.Colors(BackColorNum);
            Form.ForeColor = ConfigureSettings.Colors(ForeColorNum);
        }
    }
}
