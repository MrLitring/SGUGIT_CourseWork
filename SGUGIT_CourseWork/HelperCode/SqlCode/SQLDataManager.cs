using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Xml;

namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    /// <summary>
    /// Класс для управления данными в SQL
    /// Update
    /// Insert
    /// Delete
    /// 
    /// </summary>
    internal class SQLDataManager
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
        private List<string> parameterNames;
        private List<object> parameterValues;
        private string tableName;
        private string where;
        private object time;
        private int minCount;
        private const string nameParam = "value_";

        private SQLDataManager()
        {
            parameterNames = new List<string>();
            parameterValues = new List<object>();
            where = string.Empty;
            time = 0;
            minCount = 0;
        }
        public SQLDataManager(string tableName, SQLiteConnection connection) : this()
        {
            this.ExecutionCommand = executionNumber.None;
            this.tableName = tableName;
            this.connection = connection;

        }
        public SQLDataManager(string tableName, SQLiteConnection connection, executionNumber command) : this(tableName, connection)
        {
            this.ExecutionCommand = command;
        }



        //
        // Добавление значений
        //
        public void AddValue(string str_value, object obj_value)
        {
            parameterValues.Add(obj_value);
            parameterNames.Add(str_value);
        }
        public void AddValue(string[] str_values, object[] obj_values)
        {
            parameterValues.AddRange(obj_values);
            parameterNames.AddRange(str_values);
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
        public void Execute()
        {
            Execute(this.ExecutionCommand);
        }
        public void Execute(executionNumber numCommand)
        {
            if (numCommand != ExecutionCommand && ExecutionCommand != executionNumber.None) numCommand = ExecutionCommand;

            minCount = Math.Min(parameterNames.Count, parameterValues.Count);
            string query = SelectQuery(numCommand);

            SQLiteCommand command = new SQLiteCommand(query, connection);

            for (int i = 0; i < minCount; i++)
            {
                command.Parameters.AddWithValue(nameParam + i.ToString(), parameterValues[i]);
            }
            if (string.IsNullOrEmpty(where) == false)
                command.Parameters.AddWithValue(where, time);

            Console.WriteLine(query);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void ExecuteImageSave(string name, byte[] bytes)
        {
            string content = $"UPDATE {this.tableName} SET \"{name}\" = @Image;"; ;
            SQLiteCommand command = new SQLiteCommand(content, connection);

            command.Parameters.AddWithValue("@Image", bytes);

            command.ExecuteNonQuery();
            command.Dispose();
        }

        //
        // Yes, Palpatin
        //
        private string SelectQuery(executionNumber numberName)
        {
            switch (numberName)
            {
                case executionNumber.None:
                    return "";

                case executionNumber.Insert:
                    return Insert();

                case executionNumber.Delete:
                    return Delete();

                case executionNumber.Update:
                    return Update();
            }

            return "";
        }

        private string Insert()
        {
            string query = $"INSERT INTO {tableName} ";
            string columns = "";
            string values = "";

            for (int i = 0; i < minCount; i++)
            {
                columns += $"[{parameterNames[i]}]";
                values += "@" + nameParam + i.ToString();

                if (i < minCount - 1)
                {
                    columns += ",";
                    values += ",";
                }
            }
            return $"{query} ({columns}) VALUES ({values});";
        }

        private string Update()
        {
            string query = $"UPDATE [{tableName}] SET ";
            int min = minCount;

            for (int i = 0; i < min; i++)
            {
                query += $"[{parameterNames[i]}] = " + $"@{nameParam + i.ToString()}";

                if (i < min - 1)
                    query += ",";
            }

            if (string.IsNullOrEmpty(where) == false)
                query += $" WHERE {where} = @{where}";

            query += ";";

            return query;
        }

        private string Delete()
        {
            if (where != string.Empty)
                return $"DELETE FROM [{tableName}] WHERE [{where}] = {time}";

            return "";
        }

    }
}
