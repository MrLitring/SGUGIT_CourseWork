﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode.SqlClass
{
    public static class MainData
    {
        public static SQLiteConnection SQLConnection = null;
        public static string dataBasePath;


        public static string SelectAll(string tableName)
        {
            return $"SELECT * FROM {tableName} ";
        }
    }
}
