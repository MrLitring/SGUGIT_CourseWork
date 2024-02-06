using System.Linq;
using System.Data.SQLite;

namespace SGUGIT_CourseWork.HelperCode
{
    internal class SqlCode
    {
        public static SQLiteConnection SQLConnection;
        public static string dataBasePath;


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

        public static string SelectAll(string tableName)
        {
            return "Select * From " + tableName;
        }

        public static string Update(string tableName, int value, string column, int Where)
        {
            return $"Update {tableName} ";
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

        public static string TableUpdate(string tableName, string ColumnName, int value, int Where)
        {
            return $"Update {tableName} set {ColumnName} = {value} WHERE time = {Where};";
        }

    }
}
