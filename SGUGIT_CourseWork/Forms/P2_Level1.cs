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
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void P2_Level1_Load(object sender, EventArgs e)
        {

            FillData();
            M();


        }

        List<PointColumn> points = new List<PointColumn>();
        private void FillData()
        {
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                PointColumn point = new PointColumn();
                for(int j = 0; j < dTable.Columns.Count; j++)
                {
                    point.PointAdd(dTable.Rows[i][j]);
                }
                points.Add(point);
            }


            for (int i = 0;i < points.Count;i++)
            {
                string name = points[i].Points[0].ToString();
                DataView.Columns.Add(name, name);
            }
            for (int j = 0; j < points[0].Points.Count; j++)
            {
                DataView.Rows.Add();
            }


            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 1; j < points[i].Points.Count; j++)
                {
                    DataView.Rows[j-1].Cells[i].Value = points[i].Points[j];
                }
            }
        }

        private void M()
        {
            for (int i = 0; i < points.Count; i++)
            {
                double m = 0;

                for (int j = 1; j < points[i].Points.Count; j++)
                {
                    m += Math.Pow(points[i].Points[j], 2);

                }
                m = Math.Sqrt(m);
                m = Math.Round(m, 4);

                DataView.Rows[DataView.Rows.Count - 2].Cells[i].Value = m.ToString();
            }

            double a = 0;
            for (int i = 0;  i < points.Count; i++)
            {
                a += points[i].Summ(2);
                a = Math.Sqrt(a);
                a = Math.Round(a, 4);

                DataView.Rows[DataView.Rows.Count - 1].Cells[i].Value = a.ToString();
            }
        }
    }
}
