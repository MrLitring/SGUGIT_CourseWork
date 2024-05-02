using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    internal class SQLDataTable
    {
        private string tableName;
        private SQLiteConnection connection;
        private List<string> columnNames;
        private List<ValueType> columnTypes;



        public enum ValueType
        {
            integer,
            text,
            blob,
            real,
            numeric
        }
        public int MinCount { get { return Math.Min(columnNames.Count, columnTypes.Count); } }



        private SQLDataTable() 
        {
            this.tableName = string.Empty;
            this.columnNames = new List<string>();
            this.columnTypes = new List<ValueType>();
        }
        public SQLDataTable(SQLiteConnection connect, string name) : this()
        {
            this.tableName = name;
            this.connection = connect;
        }



        public void Execute()
        {
            if(GeneralData.MainConnection.State == System.Data.ConnectionState.Closed) { GeneralData.MainConnection.Open(); }

            string query = CreateQuery();
            SQLiteCommand cmd = new SQLiteCommand(query, connection);
            cmd.ExecuteNonQuery();
        }


        public void AddColumnName(string name, ValueType type)
        {
            this.columnNames.Add(name);
            this.columnTypes.Add(type);
        }
        public void AddColumnName(List<string> name, List<string> type)
        {
            //this.columnNames.AddRange(name);
            for (int i = 0; i < type.Count; i++)
            {
                this.columnNames.Add(name[i]);

                if (type[i].ToUpper() == ValueType.integer.ToString().ToUpper())
                    columnTypes.Add(ValueType.integer);

                else if (type[i].ToUpper() == ValueType.real.ToString().ToUpper())
                    columnTypes.Add(ValueType.real);

                else if (type[i].ToUpper() == ValueType.text.ToString().ToUpper())
                    columnTypes.Add(ValueType.text);

                else if (type[i].ToUpper() == ValueType.blob.ToString().ToUpper())
                    columnTypes.Add(ValueType.blob);

                else if (type[i].ToUpper() == ValueType.numeric.ToString().ToUpper())
                    columnTypes.Add(ValueType.numeric);

            }
        }




        private string CreateQuery()
        {
            string query = $"CREATE TABLE {tableName} (";
            int min = Math.Min(columnNames.Count(), columnTypes.Count());

            for (int i = 0; i < min; i++)
            {
                query += $"\'{columnNames[i]}\'" + " " + columnTypes[i].ToString().ToUpper();

                if ((i < columnNames.Count() - 1) && (i < columnTypes.Count() - 1))
                    query += ",\n";
            }
            query += ");";

            return query ;
        }
    }
}
