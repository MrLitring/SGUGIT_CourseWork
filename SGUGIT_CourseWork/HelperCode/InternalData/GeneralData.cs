using SGUGIT_CourseWork.HelperCode.Other;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;

namespace SGUGIT_CourseWork.HelperCode
{
    /// <summary>
    /// Клас предоставляет общие данные и общие функции
    /// </summary>
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
        public static double assureValue = 0.0001; // Точность измерений E
        public static int blockCount = 1; // Кол-во блоков
        public static int WhiteRound = 10; // значения после запятой для белого шума

        public static SQLiteConnection MainConnection;
        public static string DataBasePath;
        public static string TempFilePath = "tmp/lastSession.txt";

        public static DataTable dataTable = new DataTable();
        public static Image imageSheme = null;

        public static List<DataTableCalculation> dataTables = new List<DataTableCalculation>();
        public static List<DataTableCalculation> dataTables2 = new List<DataTableCalculation>();
        public static List<DataTableStorage> underBlockStorage_1 = new List<DataTableStorage>();
        public static List<DataTableStorage> underBlockStorage_2 = new List<DataTableStorage>();



        public static string Generate_SQLConnection(string path)
        {
            return $"Data Source={path};Version=3;";
        }

        public static void DataFullUpdate()
        {
            dataTables.Clear();
            
            DataTableReload();
            ValueReload();
            ImageUpdate();
            DataTableCalculation calculation = new DataTableCalculation(dataTable);
            calculation.Name = "Main";
            dataTables.Add(calculation);
        }

        public static void FullRestart()
        {
            dataTables = new List<DataTableCalculation> ();
            dataTables2 = new List<DataTableCalculation> ();

            underBlockStorage_1 = new List<DataTableStorage>();
            underBlockStorage_2 = new List <DataTableStorage>();

            DataTableStorage underBlock = new DataTableStorage("Block_" + "Other");
            underBlockStorage_1.Add(underBlock);
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
