 using SGUGIT_CourseWork.HelperCode;
using SGUGIT_CourseWork.HelperCode.SqlCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P1_DataBase : Form
    {
        private bool isFirstStart = true; // Первый ли старт ?
        private List<SQLData> commandChanges = new List<SQLData>();
        // [A] [E] [B] [image]
        private DataTable dTable;
        private Point cellFocus;
        private const string formName = "База данных";
        private double currentValue;



        public P1_DataBase()
        {
            InitializeComponent();
        }

        private void P1_DataBase_Load(object sender, EventArgs e)
        {
            GeneralData.DataUpdate();
            DataLoad();

            textBox1.TextChanged += TextBox_ValueChange;
            textBox2.TextChanged += TextBox_ValueChange;
            textBox3.TextChanged += TextBox_ValueChange;

            if (dataGridView1.Rows.Count > 0 && dataGridView1.Columns.Count > 0)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                dataGridView1.FirstDisplayedScrollingColumnIndex = 0;
            }

            isFirstStart = false;
        }

        //
        // Установление данных
        //

        private void DataLoad()
        {
            dTable = GeneralData.dataTable;

            for (int col = 0; col < dTable.Rows.Count; col++)
            {
                string colName = dTable.Columns[col].ColumnName; 
                dataGridView1.Columns.Add(colName, colName);

                dataGridView1.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dTable.Rows.Count; row++)
            {
                dataGridView1.Rows.Add(dTable.Rows[row].ItemArray);
            }

            textBox1.Text = GeneralData.smoothValue.ToString();
            textBox2.Text = GeneralData.assureValue.ToString();
            textBox3.Text = GeneralData.blockCount.ToString();

            pictureBox1.Image = GeneralData.imageSheme;
        }

        //
        // Кнопки 
        //
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonNewCycle_Click(object sender, EventArgs e)
        {
            if (commandChanges.Count != 0)
            {
                DialogResult result = MessageBox.Show("У вас есть несохранённые данные, сохранить эти данные?", "", MessageBoxButtons.YesNo);
                if ((result == DialogResult.Yes) == false)
                    return;

                Save();
            }

            if (dTable.Rows.Count <= 0) return;


            List<PointColumn> points = new List<PointColumn>();

            for (int i = 0; i < dTable.Columns.Count; i++)
            {
                PointColumn pointColumn = new PointColumn(dTable.Rows[dTable.Rows.Count - 1][0]);
                pointColumn.ColumnName = dTable.Columns[i].ColumnName;
                for (int j = 0; j < dTable.Rows.Count; j++)
                {
                    pointColumn.PointAdd(dTable.Rows[j][i]);
                }
                points.Add(pointColumn);
            }

            SQLData dataToInsert = new SQLData(GeneralData.TableName_First, GeneralData.MainConnection, SQLData.executionNumber.Insert);

            object[] newPoints = new object[points.Count];
            newPoints[0] = points[0].Time;
            for (int i = 1; i < points.Count; i++)
            {
                newPoints[i] = points[i].NewH();
            }
            string[] names = new string[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                names[i] = points[i].ColumnName;
            }


            dataToInsert.AddValue(newPoints);
            dataToInsert.AddName(names);
            dataToInsert.Execute();

            DataGridFocus(0, 0);
            dataGridView1.Focus();
        }

        private void buttonDeleteCycle_Click(object sender, EventArgs e)
        {
            if (cellFocus.X >= 0 && cellFocus.X < dTable.Rows.Count)
            {
                SQLData data = new SQLData(GeneralData.TableName_First, GeneralData.MainConnection, SQLData.executionNumber.Delete);
                data.AddWhere("Эпоха", dTable.Rows[cellFocus.X][0]);
                data.Execute(SQLData.executionNumber.Delete);

                dataGridView1.CurrentCell = dataGridView1.Rows[cellFocus.X].Cells[cellFocus.Y];
                dataGridView1.Focus();
            }
        }

        private void tool_ImageSet_Click(object sender, EventArgs e)
        {
            string imagePath = FIleBrowser("png (*.png)|*.png");
            pictureBox1.Image = new Bitmap(imagePath);

            SQLData dataToSave = new SQLData(GeneralData.TableName_Second, GeneralData.MainConnection, SQLData.executionNumber.Update);
            dataToSave.ExecuteImageSave("Image", File.ReadAllBytes(imagePath));
        }

        private void toolUpdate_Click(object sender, EventArgs e)
        {
            isFirstStart = true;
            GeneralData.DataUpdate();
            DataClear();
            DataLoad();
            isFirstStart = false;
        }

        //
        // События
        //


        private void TextBox_ValueChange(object sender, EventArgs e)
        {
            if (isFirstStart == true) return;

            string senderName = (sender as TextBox).Name;
            SQLData dataSave = new SQLData(GeneralData.TableName_Second, GeneralData.MainConnection);

            switch (senderName)
            {
                case "textBox1":
                    {
                        dataSave.AddName("A");
                        break;
                    }
                case "textBox2":
                    {
                        dataSave.AddName("E");
                        break;
                    }
                case "textBox3":
                    {
                        dataSave.AddName("A");
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            dataSave.AddValue((sender as TextBox).Text);
            commandChanges.Add(dataSave);

            LabelText_Save();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                currentValue = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
            catch { }
            cellFocus = new Point(e.RowIndex, e.ColumnIndex);
        }

        private void Cell_ValueChange(object sender, DataGridViewCellEventArgs e)
        {
            string name = $"cell_{e.ColumnIndex}_{e.RowIndex}";
            SQLData data = data = new SQLData(GeneralData.TableName_First, GeneralData.MainConnection, SQLData.executionNumber.Update);

            double value = Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            if (value == currentValue) return;
            if (value == Convert.ToDouble(dTable.Rows[e.RowIndex][e.ColumnIndex])) return;


            data.AddName($"{dataGridView1.Columns[e.ColumnIndex].HeaderText}");
            data.AddValue(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            data.AddWhere(dataGridView1.Columns[0].HeaderText, Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));
            data.Name = "";

            commandChanges.Add(data);
            LabelText_Save();
        }

        //
        //
        //
        private void DataGridFocus(int column, int row)
        {
            //dataGridView1.CurrentCell = dataGridView1.Rows[row].Cells[column];

        }

        private void DataClear()
        {
            dTable.Columns.Clear();
            dTable.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void Save()
        {
            if (commandChanges.Count == 0)
                return;

            foreach (SQLData elem in commandChanges)
                elem.Execute();

            commandChanges.Clear();
            LabelText_UnSave();

            GeneralData.DataUpdate();
        }

        private void LabelText_Save()
        {
            if (toolStripLabelSave.Text.EndsWith("*") == false)
            {
                toolStripLabelSave.Text = formName + "*" + commandChanges.Count.ToString();
            }
        }

        private void LabelText_UnSave()
        {
            toolStripLabelSave.Text = formName;
        }

        private string FIleBrowser(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = filter
            };

            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                return openFileDialog.FileName; ;
            }
            else
                return null;

        }

        


    }

}
