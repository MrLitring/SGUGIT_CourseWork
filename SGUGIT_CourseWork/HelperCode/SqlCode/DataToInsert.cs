using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    public class DataToInsert
    {
        private SQLiteConnection connection = GeneralData.MainConnection;
        private string tableName;
        private string[] columnNames;
        private object[] values;

        public DataToInsert(string tableName, string[] columnNames, object[] values)
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.values = values;
        }

        public DataToInsert(string tableName)
        {
            this.tableName = tableName;
        }


        public void SetConnection(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public void UpdateColumnNames(string[] columnNames)
        {
            this.columnNames = columnNames;
        }

        public void UpdateValues(object[] values)
        {
            this.values = values;
        }

        public void ExecuteInsert()
        {
            string content = InsertQuery();
            SQLiteCommand command = new SQLiteCommand(content, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        private string InsertQuery()
        {
            string query = $"INSERT INTO {tableName}";
            int n = values.Count();

            if (columnNames != null)
            {
                if (columnNames.Count() >= 0)
                {
                    query += "(";
                    if (values.Count() < columnNames.Count()) n = values.Count();

                    for (int i = 0; i < n; i++)
                    {
                        query += columnNames[i];
                        if (i < n - 1) query += ",";
                    }

                    query += ") ";
                }
            }

            query += " VALUES(" ;

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
