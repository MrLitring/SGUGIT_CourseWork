using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.Additional_Commands
{
    public static class SQLquery
    {
        public static string CreateTable(string tableName, string[] columns, string[] types)
        {
            string text = $"Create table {tableName} (";
            for (int i = 0; i < columns.Count() && i < types.Count(); i++)
            {
                text += $"{columns[i]} {types[i].ToUpper()}";
            }
            text += ");";
            return text;
        }


        public static string TableInfoAdd(string tableName, string[] values, string[] columns = null)
        {
            string text = $"INSERT INTO {tableName} (";
            for (int i = 0; i < columns.Count(); i++)
            {
                text += columns[i];
                if (i < columns.Count() - 1) text += ", ";
            }
            text += ") VALUES (";
            for (int i = 0; i < columns.Count(); i++)
            {
                text += values[i];
                if (i < values.Count() - 1) text += ", ";
            }
            text += ");";
            return text;
        }

        public static string TableInfoUpdate(string tableName, string[] columnNames, string[] values)
        {
            return "";
        }

        public static string TableInfoUpdate(string tableName, string where, string[] columnNames, string[] values)
        {
            return "";
        }
    }
}
