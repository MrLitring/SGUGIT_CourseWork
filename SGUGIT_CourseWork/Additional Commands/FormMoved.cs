using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Additional_Commands
{
    public class FormMoved
    {
        private List<Control> controls;
        public List<Control> Controls { get { return controls; } }

        private Form form;

        private Point lastPosition;
        private bool mouseDown = false;

        public FormMoved(Form form) 
        {
            this.form = form;
            controls = new List<Control>();
        }

        public void ControlAdd(Control control)
        {
            controls.Add(control);
            MoveUpdate();
        }

        public void ControllRemove(Control control)
        { 
            controls.Remove(control);
            MoveUpdate();
        }

        private void MoveUpdate()
        {
            foreach (Control control in controls)
            {
                control.MouseDown -= Control_MouseDown;
                control.MouseMove -= Control_MouseMove;
                control.MouseUp -= Control_MouseUp;

                control.MouseDown += Control_MouseDown;
                control.MouseMove += Control_MouseMove;
                control.MouseUp += Control_MouseUp;

            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
                lastPosition = e.Location;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if(mouseDown == true)
            {
                form.Location = new Point(
                    (form.Location.X - lastPosition.X) + e.X,
                    (form.Location.Y - lastPosition.Y) + e.Y);

                form.Update();
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

    }
}
