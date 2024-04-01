using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    internal class SQLData
    {
        public enum executionNumber
        {
            None = 0,
            Update = 1,
            Insert = 2,
            Delete = 3,
        }


        public string Name;
        public executionNumber ExecutionCommand; // Первостепенная важность


        private SQLiteConnection connection;
        private List<string> colNames;
        private List<object> colValues;
        private string tableName;
        private string where;
        private object time;

        private SQLData() 
        {
            colNames = new List<string>();
            colValues = new List<object>();
            where = string.Empty;
            time = 0;
        }
        public SQLData(string tableName, SQLiteConnection connection) : this()
        {
            this.ExecutionCommand = executionNumber.None;
            this.tableName = tableName;
            this.connection = connection;

        }
        public SQLData(string tableName, SQLiteConnection connection, executionNumber command) : this(tableName, connection)
        {
            this.ExecutionCommand = command;
        }

        //
        // Добавление Значений
        //
        public void AddValue(object value)
        {
            colValues.Add(value);
        }

        public void AddValue(object[] value)
        {
            foreach (object elem in value)
                colValues.Add(elem);
        }

        //
        // Добавление Колонок
        //
        public void AddName(string value)
        {
            colNames.Add(value);
        }

        public void AddName(string[] value)
        {
            foreach (string elem in value)
                colValues.Add(elem);
        }

        //
        // Добавить эпоху
        //
        public void AddWhere(string where, object time)
        {
            this.where = where;
            this.time = time;
        }

        //
        // Execution order 66 
        //
        public void Execute(executionNumber numCommand)
        {
            if(numCommand != ExecutionCommand && ExecutionCommand != executionNumber.None) numCommand = ExecutionCommand;

            string query = "";

            switch (numCommand)
            {
                case executionNumber.None:
                    return;

                case executionNumber.Update: 
                    {
                        query = Update();
                        break;
                    }

                case executionNumber.Insert:
                    {
                        query = Insert();
                        break;
                    }

                case executionNumber.Delete:
                    {
                        query = Delete();
                        break;
                    }

            }

            SQLiteCommand command = new SQLiteCommand(query, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        //
        // Yes, Palpatin
        //
        private string Insert()
        {
            string[] query = new string[3];

            int min = Math.Min(colNames.Count, colValues.Count);
            if (min == 0) return "";
            
            query[0] = $"INSERT INTO [{tableName}] ";
            query[1] += " (";
            query[2] += " (";
            for (int i = 0; i < min; i++)
            {
                query[1] += colNames[i];
                query[2] += colValues[i];

                if(i < min - 1)
                {
                    query[1] += ",";
                    query[2] += ",";
                }
            }
            query[1] += ") ";
            query[2] += ") ";

            return query[0] + query[1] + query[2] + ";";
        }

        private string Update()
        {
            string query = $"UPDATE [{tableName}] SET ";

            int min = Math.Min(colNames.Count, colValues.Count);
            if (min == 0) return "";

            for (int i = 0; i < min; i++)
            {
                query += $"{colNames[i]} = {colValues[i]}";

                if (i != min - 1)
                    query += ", ";
            }

            if (where != string.Empty)
                query += $" WHERE {where} = {time}";

            return query + ";";
        }

        private string Delete()
        {
            if (where != string.Empty)
                return $"DELETE FROM [{tableName}] WHERE [{where}] = {time}";

            return "";
        }

    }
}
