using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace SGUGIT_CourseWork.HelperCode
{
    public static class GeneralData
    {
        //
        // Название таблиц
        //
        public const string TableName_First =  "FirstData";
        public const string TableName_Second = "SecondData";

        //
        // Первичные параметры
        //
        public static double smoothValue = 0.9; // Сглаженная велечина A
        public static double assureValue = 0.0008; // Точность измерений E
        public static int blockCount = 0; // Кол-во блоков

        public static SQLiteConnection MainConnection;
        public static string DataBasePath;

        public static DataTable dataTable = new DataTable();
        public static Image imageSheme = null;

        public static void DataUpdate()
        {
            DataTableReload();
            ValueReload();
            ImageUpdate();
        }

        public static void DataTableReload()
        {
            dataTable = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(
                $"Select * from {TableName_First} order by 1",
                MainConnection);
            adapter.Fill(dataTable);
        }

        public static void ValueReload()
        {
            SQLiteDataReader reader = new SQLiteCommand(
                $"SELECT * FROM {TableName_Second}", 
                MainConnection).ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                smoothValue = reader.GetDouble(0);
                assureValue = reader.GetDouble(1);
                blockCount = reader.GetInt32(2);
            }
        }

        public static void ImageUpdate()
        {
            SQLiteDataReader reader = new SQLiteCommand(
                $"SELECT Image FROM {TableName_Second}",
                MainConnection).ExecuteReader();

            if(reader.HasRows)
            {
                reader.Read();
                if(!reader.IsDBNull(0))
                {
                    byte[] bytes = (byte[])reader["Image"];

                    MemoryStream ms = new MemoryStream(bytes);

                    using (ms = new MemoryStream(bytes))
                    {
                        imageSheme = Image.FromStream(ms);
                    }
                }
            }
        }
    }
}
