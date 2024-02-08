using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    public static class SqlMainData
    {
        public static SQLiteConnection SQLConnection = null;

        public static string SelectAll(string tableName)
        {
            return $"SELECT * FROM {tableName} ";
        }
    }
}
