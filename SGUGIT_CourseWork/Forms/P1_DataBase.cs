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
using System.Security.Cryptography;

namespace SGUGIT_CourseWork.Forms
{
    public partial class P1_DataBase : Form
    {
        private bool isHasSaved = false;  // Есть ли сохранение?
        private bool isFirstStart = true; // Первый ли старт ?
        private List<SQLData> commandChanges = new List<SQLData>();
        // [A] [E] [B] [image]
        private int[] indexSave = new int[4];
        private DataTable dTable;
        private Point cellFocus;
        private const string formName = "База данных";


        public P1_DataBase()
        {
            InitializeComponent();
            dTable = new DataTable();

            DataTable_SetData();
            DataText_SetData();
            DataImage_SetData();
            textBox1.TextChanged += TextBox_ValueChange;
            textBox2.TextChanged += TextBox_ValueChange;
            textBox3.TextChanged += TextBox_ValueChange;

            dataGridView1.FirstDisplayedScrollingRowIndex = 0;
            dataGridView1.FirstDisplayedScrollingColumnIndex = 0;

            isFirstStart = false;

        }

        //
        // Установление данных
        //
        #region
        private void DataTable_SetData()
        {
            dTable.Columns.Clear();
            dTable.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"Select * from {GeneralData.TableName_First} order by 1",
                GeneralData.MainConnection);
            adapter.Fill(dTable);

            for (int col = 0; col < dTable.Columns.Count; col++)
            {
                string ColName = dTable.Columns[col].ColumnName;
                dataGridView1.Columns.Add(ColName, ColName);

                dataGridView1.Columns[col].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dTable.Rows.Count; row++)
            {
                dataGridView1.Rows.Add(dTable.Rows[row].ItemArray);
            }
        }

        private void DataText_SetData()
        {
            SQLiteCommand command = new SQLiteCommand($"Select * From {GeneralData.TableName_Second}", GeneralData.MainConnection);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();

                if (reader.HasRows)
                {
                    reader.Read();
                    textBox1.Text = reader.GetDouble(0).ToString();
                    textBox2.Text = reader.GetDouble(1).ToString();
                    textBox3.Text = reader.GetInt32(2).ToString();
                }
            }
        }

        private void DataImage_SetData()
        {
            pictureBox1.Image = null;
            string query = $"SELECT Image FROM {GeneralData.TableName_Second};";
            

            SQLiteCommand command = new SQLiteCommand (query , GeneralData.MainConnection);
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        byte[] bytes = (byte[])reader["Image"];

                        MemoryStream ms = new MemoryStream(bytes);
                        
                        using (ms = new MemoryStream(bytes))
                        {
                             pictureBox1.Image = Image.FromStream(ms);
                        }
                    }
                }
            }



        }

        #endregion

        //
        //  Сохранение данных
        //
        #region
        private void Save()
        {
            if (commandChanges.Count == 0)
            {
                return;
            }

            foreach (SQLData elem in commandChanges)
            {
                elem.Execute(SQLData.executionNumber.Update);
                LabelText_UnSave();
            }
             
            commandChanges.Clear();
            if(EventBus.onDataBaseChange != null)
            {
                EventBus.onDataBaseChange.Invoke();
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        #endregion

        //
        // Изменение данных
        //
        #region
        private void LabelText_Save()
        {
            if (toolStripLabelSave.Text.EndsWith("*") == false)
            {
                toolStripLabelSave.Text = formName + "*" + commandChanges.Count.ToString();
                isHasSaved = true;
            }
        }

        private void LabelText_UnSave()
        {
            isHasSaved = false;
            toolStripLabelSave.Text = formName;
        }

        private void Cell_ValueChange(object sender, DataGridViewCellEventArgs e)
        {
            SQLData data = new SQLData(GeneralData.TableName_First, GeneralData.MainConnection);
            data.AddName($"\'{dataGridView1.Columns[e.ColumnIndex].HeaderText}\'");
            data.AddValue(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            data.AddWhere(dataGridView1.Columns[0].HeaderText , Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value));

            commandChanges.Add(data);
            LabelText_Save();
        }

        private void TextBox_ValueChange(object sender, EventArgs e)
        {
            if (isFirstStart == true) return;

            string senderName = (sender as TextBox).Name;
            SQLData dataSave = new SQLData(GeneralData.TableName_Second, GeneralData.MainConnection);

            switch(senderName)
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

        #endregion

        //
        // Циклы измерений
        //
        #region 
        private void buttonNewCycle_Click(object sender, EventArgs e)
        {
            if(isHasSaved == true)
            {
                DialogResult result = MessageBox.Show("У вас есть несохранённые данные, сохранить эти данные?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Save();
                }
                else
                    return;
            }

            if (dTable.Rows.Count <= 0) return;


            List<PointColumn> points = new List<PointColumn>();

            for (int i = 0; i < dTable.Columns.Count; i++)
            {
                PointColumn pointColumn = new PointColumn(5, dTable.Rows[dTable.Rows.Count - 1][0]);
                for (int j = 0; j < dTable.Rows.Count; j++)
                {
                    pointColumn.PointAdd(dTable.Rows[j][i]);
                }
                points.Add(pointColumn);
            }

            DataToInsert dataToInsert = new DataToInsert(GeneralData.TableName_First);


            object[] newPoints = new object[points.Count];
            newPoints[0] = points[0].Time;
            for (int i = 1; i < points.Count; i++)
            {
                newPoints[i] = points[i].NewH();
            }

            dataToInsert.UpdateValues(newPoints);
            dataToInsert.ExecuteInsert();

            DataTable_SetData();
            dataGridView1.CurrentCell = dataGridView1.Rows[dTable.Rows.Count].Cells[cellFocus.Y];
            dataGridView1.Focus();
        }

        private void buttonDeleteCycle_Click(object sender, EventArgs e)
        {
            if (cellFocus.X >= 0 && cellFocus.X < dTable.Rows.Count)
            {
                SQLData data = new SQLData(GeneralData.TableName_First, GeneralData.MainConnection, SQLData.executionNumber.Delete);
                data.AddWhere("Эпоха", dTable.Rows[cellFocus.X][0]);
                data.Execute(SQLData.executionNumber.Delete);
                //commandChanges.Add(data);
                
                DataTable_SetData();
                dataGridView1.CurrentCell = dataGridView1.Rows[cellFocus.X].Cells[cellFocus.Y];
                dataGridView1.Focus();
            }
        }

        #endregion

        //
        // Какие-то события
        //
        #region
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            cellFocus = new Point(e.RowIndex, e.ColumnIndex);
        }


        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tool_ImageSet_Click(object sender, EventArgs e)
        {
            string imagePath = FIleBrowser("png (*.png)|*.png");
            pictureBox1.Image = new Bitmap(imagePath);

            DataToSave dataToSave = new DataToSave
                (GeneralData.TableName_Second, "Image");
            dataToSave.Name = "Image";
            dataToSave.ExecuteSave(File.ReadAllBytes(imagePath));
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
