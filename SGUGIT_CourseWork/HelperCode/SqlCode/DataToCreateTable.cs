using System.Data.SQLite;
using System.Linq;


namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    public class DataToCreateTable
    {
        private SQLiteConnection connection = HelperCode.SqlCode.SqlMainData.SQLConnection;
        private string tableName;
        private string[] columnNames;
        private string[] columTypes;

        public DataToCreateTable(string tableName, string[] columnNames, string[] columTypes) 
        {
            this.tableName = tableName;
            this.columnNames = columnNames;
            this.columTypes = columTypes;
        }

        public void SetConnection(SQLiteConnection connection)
        {
            this.connection = connection;
        }

        public void ExecuteCreate()
        {
            SQLiteCommand command = new SQLiteCommand(createQuery(), connection);
            command.ExecuteNonQuery();
        }

        private string createQuery()
        {
            string query = $"CREATE TABLE {tableName} (";

            for(int i = 0; (i < columnNames.Count()) &&( i < columTypes.Count()); i++)
            {
                query += columnNames[i] + " " + columTypes[i].ToUpper();

                if ((i < columnNames.Count() - 1) && (i < columTypes.Count() - 1))
                    query += ",\n";
            }
            query += ")" ;

            return query;
        }

    }
}
