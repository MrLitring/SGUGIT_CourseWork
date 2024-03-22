using System.Data.SQLite;


namespace SGUGIT_CourseWork.HelperCode.SqlCode
{
    public class DataToSave
    {
        public string Name = null;

        private SQLiteConnection connection = GeneralData.MainConnection;
        private string tableName;
        private string columnName;
        private object value;
        private int time = -1;

        public DataToSave(string tableName, string columnName)
        {
            this.tableName = tableName;
            this.columnName = columnName;
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
            string content = UpdateQuery();
            SQLiteCommand command = new SQLiteCommand(content, connection);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void ExecuteSave(byte[] bytes)
        {
            string content = UpdateQuery();
            SQLiteCommand command = new SQLiteCommand(content, connection);
            if (Name != null) command.Parameters.AddWithValue("@Image", bytes);

            command.ExecuteNonQuery();
            command.Dispose();
        }

        private string UpdateQuery()
        {
            if(Name.StartsWith("Image"))
            {
                return $"UPDATE {this.tableName} SET \"{this.columnName}\" = @Image;";
            }

            if(time >= 0)
                return $"UPDATE {this.tableName} SET \"{this.columnName}\" = {this.value} WHERE Эпоха = {this.time};";
            else
                return $"UPDATE {this.tableName} SET \"{this.columnName}\" = {this.value};";
        }
    }
}
