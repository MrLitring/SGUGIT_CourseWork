using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode
{
    public static class GeneralData
    {
        public const string TableName_First =  "FirstData";
        public const string TableName_Second = "SecondData";


        public static SQLiteConnection MainConnection;
        public static string DataBasePath;

    }
}
