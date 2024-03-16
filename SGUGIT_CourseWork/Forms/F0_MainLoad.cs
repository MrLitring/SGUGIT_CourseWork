using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.Forms;
using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Linq;

namespace SGUGIT_CourseWork.Forms
{
    public partial class F0_MainLoad : Form
    {
        public F0_MainLoad()
        {
            InitializeComponent();
            GeneralData.MainConnection = new SQLiteConnection();
            SetActive(false);
        }

        private void SetActive(bool isActive = false)
        {
            MenuWorkBench.Enabled = isActive;
            string sql = "";
            if (GeneralData.DataBasePath != null)
            {
                sql = GeneralData.DataBasePath.Split('\\')
                [GeneralData.DataBasePath.Split('\\').Count() - 1];
                toolStripStatusLabel1.Text = sql;
            }
        }

        //
        //
        //
        private bool OpenDBFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "База данных (*.db)|*.db|Все файлы (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                GeneralData.MainConnection =
                    new SQLiteConnection("Data Source=" + openFileDialog.FileName + ";Version = 3;");
                GeneralData.MainConnection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = GeneralData.MainConnection;

                GeneralData.DataBasePath = openFileDialog.FileName;
                SetActive(true);
                return true;
            }
            else
            {
                SetActive(false);
                return false;
            }

            }

        //
        // Action MenuStrip onClick
        //
        #region

        private void MenuStrip_File_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripItem).Name == "StripClose") Application.Exit();
            string named = (sender as ToolStripItem).Name;

            switch (named)
            {
                case "StripNewDataBase":
                    {
                        F2_NewDataBase f2_NewDataBase = new F2_NewDataBase();
                        f2_NewDataBase.ShowDialog();
                        break;
                    }
                case "StripOpenDataBase":
                    {
                        OpenDBFile();
                        break;
                    }

                default:
                    {
                        MessageBox.Show($"{sender.GetType().Name}(\"{named}\") - Не было назначено",
                            "Осторожно",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);

                        break;
                    }
            }
        }

        private void MenuStrip_WorkBench_Click(object sender, EventArgs e)
        {

            switch((sender as ToolStripMenuItem).Name)
            {
                case "StripEditDataBase":
                    {
                        HelperCode.FormOpenCode.OpenForm(new P1_DataBase(), panel1);
                        break;
                    }
            }
        }

        private void MenuStrip_Windows_Click(object sender, EventArgs e)
        {
            string named = (sender as ToolStripMenuItem).Name;
            switch (named)
            {
                case "StripNewWindow":
                    {
                        F1_Window form = new F1_Window();
                        form.Show();
                        form.FormBorderStyle = FormBorderStyle.SizableToolWindow;

                        break;
                    }
                case "StripTwoWindow":
                    {

                        break;
                    }
                case "StripCloseAllWindow":
                    {

                        break;
                    }
            }
        }

        #endregion
    }
}
