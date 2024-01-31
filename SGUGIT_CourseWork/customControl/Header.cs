using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SGUGIT_CourseWork.customControl
{
    public class Header : Control
    {
        private Form form = null;
        [Category("Form")]
        public Form Form { get { return form; } set { form = value; } }

        public Button ButtonClose { get; set; }
        public Button ButtonMin { get; set; }
        public Button ButtonMax { get; set; }

        private bool isMax = false;
        private bool isMin = false;

        public Header()
        {
            Initilization();
        }

        private void Initilization()
        {
            ButtonClose = new Button();
            ButtonMin = new Button();
            ButtonMax = new Button();

            this.Size = new Size(0, 25);
            this.MinimumSize = new Size(0, 25);
            this.MaximumSize = new Size(0, 25);
            this.Dock = DockStyle.Top;

            ButtonClose.Text = "X";
            ButtonClose.FlatAppearance.MouseOverBackColor = Color.Red;
            ButtonClose.MouseClick += (s, e) => { if (form != null) form.Close(); };

            ButtonMax.FlatAppearance.MouseOverBackColor = Color.Gray;
            ButtonMax.Text = "\u25AD";
            ButtonMax.MouseClick += (s, e) =>
            {
                if (isMax == true)
                {
                    if (form != null)
                    {

                        form.WindowState = FormWindowState.Normal;
                        this.Size = new Size(0, 20);
                        isMax = false;
                    }
                }
                else
                {
                    if (form != null)
                    {
                        form.WindowState = FormWindowState.Maximized;
                        this.Size = new Size(0, 30);
                        isMax = true;
                    }

                    ButtonInit(ButtonMin);
                    ButtonInit(ButtonMax);
                    ButtonInit(ButtonClose);
                }
            };

            ButtonMin.FlatAppearance.MouseOverBackColor = Color.Gray;
            ButtonMin.Text = "-";
            ButtonMin.MouseClick += (s, e) =>
            {
                if (isMin == true)
                {
                    if (form != null)
                    {
                        isMin = false;
                        form.WindowState = FormWindowState.Normal;
                    }
                }
                else
                {
                    if (form != null)
                    {
                        isMin = true;
                        form.WindowState = FormWindowState.Minimized;
                    }
                }
            };

            this.MouseDoubleClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (form != null)
                    {
                        if (isMax == true)
                        {
                            
                            form.WindowState = FormWindowState.Normal;
                            this.Size = new Size(0, 20);
                            isMax = false;
                        }
                        else
                        {
                            
                            form.WindowState = FormWindowState.Maximized;
                            this.Size = new Size(0, 30);
                            isMax = true;
                        }

                        ButtonInit(ButtonMin);
                        ButtonInit(ButtonMax);
                        ButtonInit(ButtonClose);
                    }
                    
                }
            };

            ButtonInit(ButtonMin);
            ButtonInit(ButtonMax);
            ButtonInit(ButtonClose);

            this.Controls.Add(ButtonMin);
            this.Controls.Add(ButtonMax);
            this.Controls.Add(ButtonClose);
        }

        private void ButtonInit(Button button)
        {
            button.BackColor = ConfigureSettings.Colors(4);
            if (Size.Height == 30) button.Font = new Font(Font.Name, 14, FontStyle.Regular);
            else button.Font = new Font(Font.Name, 8, FontStyle.Regular);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Size = new Size(Height + 15, Height);
            button.Dock = DockStyle.Right;
            button.Enabled = true;
            button.Visible = true;
            button.Show();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            Rectangle rectangle = new Rectangle(0, 0, Width, Height);
            g.DrawRectangle(new Pen(new SolidBrush(BackColor)), rectangle);

            this.Update();
        }


    }
}
