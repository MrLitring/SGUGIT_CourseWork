using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode.Other
{
    public class DataTableStorage
    {
        private string name;
        private DataTable dataTable;
        private List<DataTableStorage> storages;
        private List<string> columnNames;



        public string Name { get { return name; } }
        public DataTable DataTable { get { return dataTable; } }
        public List<DataTableStorage> DataTableStorages { get { return storages; } }
        public int ColumnCount { get { return columnNames.Count; } }



        public DataTableStorage(string name)
        {
            this.name = name;
            this.storages = new List<DataTableStorage>();
            this.columnNames = new List<string>();
        }
        public DataTableStorage(string name, DataTable dataTable) : this(name)
        {
            this.name = name;
            this.dataTable = dataTable;
        }



        public DataTable GetUnderBlock()
        {
            DataTable table = new DataTable();
            for (int i = 0; i < columnNames.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (columnNames[i] == dataTable.Columns[j].ColumnName)
                        table.Columns.Add(columnNames[i], dataTable.Columns[j].DataType);
                }
            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
                table.ImportRow(dataTable.Rows[row]);

            return table;
        }

        public void AddDataTable(DataTableStorage table)
        {
            storages.Add(table);
        }

        
        public void DataTableDeconstuction(DataTable dataTable)
        {
            columnNames.Clear();
            for(int col =0; col < dataTable.Columns.Count; col++)
                columnNames.Add(dataTable.Columns[col].ColumnName);

            Sort();
        }

        private void Sort()
        {
            columnNames.Sort((x, y) =>
            {
                int num1, num2;

                if (int.TryParse(x, out num1) && int.TryParse(y, out num2))
                {
                    return num1.CompareTo(num2);
                }
                else
                { return 0; }
            });
        }


    }
}
