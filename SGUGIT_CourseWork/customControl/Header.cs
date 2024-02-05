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

        public Button buttonClose { get; set; }
        public Button ButtonClose { get { return buttonClose; } }

        public Button buttonMin { get; set; }
        public Button ButtonMin { get { return buttonMin; } }

        private Button buttonMax { get; set; }
        public Button ButtonMax { get { return buttonMax; } }

        private Panel panel{ get; set; }
        public Panel Panel{ get { return panel; } }

        private MenuStrip menuStrip {  get; set; }
        public MenuStrip MenuStrip { get { return menuStrip; } }

        private bool isMax = false;
        private bool isMin = false;

        public Header()
        {
            Initilization();
        }

        private void Initilization()
        {
            buttonClose = new Button();
            buttonMin = new Button();
            buttonMax = new Button();
            menuStrip = new MenuStrip();
            panel = new Panel();

            this.Size = new Size(0, 25);
            this.MinimumSize = new Size(0, 25);
            this.Dock = DockStyle.Top;

            buttonClose.Text = "X";
            buttonClose.FlatAppearance.MouseOverBackColor = Color.Red;
            buttonClose.MouseClick += (s, e) => { if (form != null) form.Close(); };

            buttonMax.FlatAppearance.MouseOverBackColor = Color.Gray;
            buttonMax.Text = "\u25AD";
            buttonMax.MouseClick += (s, e) =>
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

                    ButtonInit(buttonMin);
                    ButtonInit(buttonMax);
                    ButtonInit(buttonClose);
                }
            };

            buttonMin.FlatAppearance.MouseOverBackColor = Color.Gray;
            buttonMin.Text = "-";
            buttonMin.MouseClick += (s, e) =>
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

            menuStrip.MouseDoubleClick += (s, e) =>
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

                        ButtonInit(buttonMin);
                        ButtonInit(buttonMax);
                        ButtonInit(buttonClose);
                    }
                    
                }
            };

            menuStrip.Dock = DockStyle.Top;
            menuStrip.Enabled = true;
            menuStrip.Visible = true;
            menuStrip.Show();

            panel.Dock = DockStyle.Fill;
            panel.Enabled = true;
            panel.Visible = true;
            panel.Show();

            ButtonInit(buttonMin);
            ButtonInit(buttonMax);
            ButtonInit(buttonClose);


            panel.Controls.Add(menuStrip);
            this.Controls.Add(buttonMin);
            this.Controls.Add(buttonMax);
            this.Controls.Add(buttonClose);
            this.Controls.Add(panel);
           
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
