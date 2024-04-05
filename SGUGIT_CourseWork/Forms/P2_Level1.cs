using SGUGIT_CourseWork.HelperCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P2_Level1 : Form
    {
        DataTable dTable = GeneralData.DataTable;  


        public P2_Level1()
        {
            InitializeComponent();
            EventBus.onDataBaseChange += Update;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Update()
        {

            DataView = new DataGridView();
            dTable.Clear();
            dTable.Rows.Clear();
            dTable.Columns.Clear();
            dTable = GeneralData.DataTable;
            MessageBox.Show("Uwu");

            FillData();
        }

        private void P2_Level1_Load(object sender, EventArgs e)
        {
            EventBus.onDataBaseChange += Update;
            FillData();


        }

        List<PointColumn> points = new List<PointColumn>();
        private void FillData()
        {
            

            for(int rows = 0; rows < dTable.Rows.Count; rows++)
            {
                PointColumn point = new PointColumn();
                for (int cols = 1; cols < dTable.Columns.Count; cols++)
                {
                    point.PointAdd(Convert.ToDouble(dTable.Rows[rows][cols]));
                }
                points.Add(point);
            }

            DataTableWork tableWork = new DataTableWork(dTable, points);
            DataTableWork tableWorkPlus = new DataTableWork(dTable, points);
            DataTableWork tableWorkMinus = new DataTableWork(dTable, points);

            tableWorkPlus.AddValue(GeneralData.assureValue);
            tableWorkMinus.AddValue(-GeneralData.assureValue);

            tableWork.Calculation();
            tableWorkPlus.Calculation();
            tableWorkMinus.Calculation();


            tableWork.ColumnAdd(DataView, "M-", tableWorkMinus.Responce);
            tableWork.ColumnAdd(DataView, "M", tableWork.Responce);
            tableWork.ColumnAdd(DataView, "M+", tableWorkPlus.Responce);

            tableWork.ColumnAdd(DataView, "A-", tableWorkMinus.Alphas);
            tableWork.ColumnAdd(DataView, "A", tableWork.Alphas);
            tableWork.ColumnAdd(DataView, "A+", tableWorkPlus.Alphas);


            for(int i = 0; i < 10; i++)
            {
                chart1.Series[0].Points.Clear();
                chart1.Series[0].Points.Add(tableWork.Responce[i], tableWork.Alphas[i]);
            }
        }
    }
}
