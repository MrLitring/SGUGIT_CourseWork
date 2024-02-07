using System.Data.SQLite;


namespace SGUGIT_CourseWork.HelperCode.SqlClass
{
    public class DataToSave
    {
        private SQLiteConnection connection;
        public string TableName;
        public string ColumnName;
        public object Value;
        public int Time;

        public DataToSave(string tableName, string columnName, object value, int time)
        {
            this.connection = this.connection = HelperCode.SqlClass.MainData.SQLConnection;
            this.TableName = tableName;
            this.ColumnName = columnName;
            this.Value = value;
            this.Time = time;
        }

        public void ExecuteSave()
        {
            SQLiteCommand command = new SQLiteCommand(updateQuery(), connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        private string updateQuery()
        {
            return $"UPDATE {this.TableName} SET {this.ColumnName} = {this.Value} WHERE time = {this.Time};";
        }
    }
}
