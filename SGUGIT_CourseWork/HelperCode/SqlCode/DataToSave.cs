using System.Data.SQLite;


namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    public class DataToSave
    {
        private SQLiteConnection connection;
        private string tableName;
        private string columnName;
        private object value;
        private int time;

        public DataToSave(string tableName, string columnName, object value, int time)
        {
            this.connection = this.connection = HelperCode.SqlCode.MainData.SQLConnection;
            this.tableName = tableName;
            this.columnName = columnName;
            this.value = value;
            this.time = time;
        }

        public void ExecuteSave()
        {
            SQLiteCommand command = new SQLiteCommand(UpdateQuery(), connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        private string UpdateQuery()
        {
            return $"UPDATE {this.tableName} SET {this.columnName} = {this.value} WHERE time = {this.time};";
        }
    }
}
