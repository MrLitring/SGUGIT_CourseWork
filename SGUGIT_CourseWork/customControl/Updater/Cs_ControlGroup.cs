using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.customControl.Updater
{
    internal class Cs_ControlGroup : Component
    {
        internal struct Items
        {
            public string Name;
            public List<Control> items {  get; set; }

            public int BackColor { get; set; }
            public int ForeColor { get; set; }

            public Items(string name)
            {
                items = new List<Control>();
                BackColor = 0;
                ForeColor = 0;

                Name = name;
            }

            public void ColorUpdate()
            {
                if(items.Count != 0)
                {
                    foreach(Control control in items)
                    {
                        control.BackColor = ConfigureSettings.Colors(BackColor);
                        control.BackColor = ConfigureSettings.Colors(ForeColor);
                    }
                }
            }
        }

        private List<Items> groups;
        public List<Items> Groups { get { return groups; } }

        private Form form = null;
        public Form Form
        {
            get { return form; }
            set
            {
                if (value != null)
                {
                    form = value;
                    ListUpdate();
                }
                else
                {
                    groups.Clear();
                    form = null;
                }

            }
        }

        public bool ColorUpdate { get { return false;  } set { ColorUpdater(); } }

        public Cs_ControlGroup()
        {
            groups = new List<Items>();
        }

        public void ListUpdate()
        {

        }

        private void ColorUpdater()
        {
            if(groups != null)
            {
                foreach(Items item in groups)
                {
                    item.ColorUpdate();
                }
            }
        }
    }
}
