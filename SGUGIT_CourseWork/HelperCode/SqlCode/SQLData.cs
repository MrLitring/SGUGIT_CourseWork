using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    internal class SQLData
    {
        public string Name;

        private SQLiteConnection connection = GeneralData.MainConnection;
        private List<string> colNames;
        private List<object> colValues;
        private string tableName;
        private string where;
        private int time = -1;


        public SQLData(string tableName) 
        {
            this.tableName = tableName;

            colNames = new List<string>();
            colValues = new List<object>(); 
        }

        public SQLData(string tableName, string colName, object colValue)
        {
            this.tableName = tableName;
            colNames = new List<string>();
            colValues = new List<object>();

            this.colNames.Add(colName);
            this.colValues.Add(colValue);
        }
        public SQLData(string tableName, string[] colName, object[] colValue)
        {
            this.tableName = tableName;
            colNames = new List<string>();
            colValues = new List<object>();

            foreach(string elem in colName)
                this.colNames.Add((string)elem);
            this.colValues.Add(colValue);
        }

        public void AddValue(object value)
        {
            colValues.Add(value);
        }

        public void AddValue(object[] value)
        {
            foreach(object elem in value)
                colValues.Add(elem);
        }

        public void AddName(string value)
        {
            colNames.Add(value);
        }

        public void AddName(string[] value)
        {
            foreach (string elem in value)
                colValues.Add(elem);
        }

        public void AddWhere(string where, object time)
        {
            this.where = where;
            this.time = (int)time;
        }


        public void UpdateExecute()
        {
            Execute(Update());
        }

        public void InsertExecute()
        {

        }

        public void DeleteExecute()
        {

        }

        private void Execute(string query)
        {
            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }


        private string Update()
        {
            string query = "";
            if(where != null && where != "")
            {
                query = $"UPDATE {this.tableName} SET ";
                int min = Math.Min(colNames.Count, colValues.Count);
                for (int i = 0; i < min; i++)
                {
                    query += $"{colNames[i]} = {colValues[i]}";

                    if (i != min - 1)
                        query += ",";
                }
                query += $" WHERE {where} = {time};";
            }
            else
            {
                query = $"UPDATE {this.tableName} SET ";
                int min = Math.Min(colNames.Count, colValues.Count);
                for (int i = 0; i < min; i++)
                {
                    query += $"\'{colNames[i]}\' = {colValues[i]}";

                    if (i != min - 1)
                        query += ",";
                }
                query += $";";
            }

            return query;
        }
    }
}
