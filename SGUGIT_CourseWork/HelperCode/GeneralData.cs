using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        public static double smoothValue = 0; // Сглаженная велечина A
        public static double assureValue = 0.0008; // Точность измерений E
        public static int blockCount = 0; // Кол-во блоков

        public static SQLiteConnection MainConnection;
        public static string DataBasePath;

        private static DataTable dataTable = null;
        public static DataTable DataTable = new DataTable();
    }
}
