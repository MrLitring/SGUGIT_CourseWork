using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode.SqlClass
{
    public class DataToInsert
    {
        private SQLiteConnection connection;
        private string tableName;
        private string[] columnNames;
        private int[] values;

        public DataToInsert(string tableName, string[] columnNames, int[] values)
        {
            this.connection = HelperCode.SqlClass.MainData.SQLConnection;
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.values = values;
        }

        public void UpdateColumnNames(string[] columnNames)
        {
            this.columnNames = columnNames;
        }

        public void UpdateValues(int[] values)
        {
            this.values = values;
        }

        public void ExecuteInsert()
        {
            SQLiteCommand command = new SQLiteCommand(insertQuery(), connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        private string insertQuery()
        {
            string query = $"INSERT INTO {tableName} (";

            int n = columnNames.Count();
            if (values.Count() < columnNames.Count()) n = values.Count();

            for (int i = 0; i < n; i++)
            {
                query += columnNames[i];
                if(i < n - 1) query += ",";
            }

            query += ") VALUES(" ;

            for (int i = 0; i < n; i++)
            {
                query += values[i];
                if (i < n - 1) query += ",";
            }
            query += ");";

            return query;
        }

    }
}
