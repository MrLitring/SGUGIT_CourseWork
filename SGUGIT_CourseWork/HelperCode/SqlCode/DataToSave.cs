using System.Data.SQLite;


namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    public class DataToSave
    {
        private SQLiteConnection connection = HelperCode.SqlCode.SqlMainData.SQLConnection;
        private string tableName;
        private string columnName;
        private object value;
        private int time = -1;

        public DataToSave()
        {

        }

        public DataToSave(string tableName, string columnName, object value)
        {
            this.tableName = tableName;
            this.columnName = columnName;
            this.value = value;
        }

        public DataToSave(string tableName, string columnName, object value, int time)
        {
            this.tableName = tableName;
            this.columnName = columnName;
            this.value = value;
            this.time = time;
        }

        public void UpdateValues(string tableName, string columnName, object value)
        {
            this.tableName = tableName;
            this.columnName = columnName;
            this.value = value;
        }

        public void UpdateValues(string tableName, string columnName, object value, int time)
        {
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
            if(time >= 0)
                return $"UPDATE {this.tableName} SET {this.columnName} = {this.value} WHERE time = {this.time};";
            else
                return $"UPDATE {this.tableName} SET {this.columnName} = {this.value};";
        }
    }
}
