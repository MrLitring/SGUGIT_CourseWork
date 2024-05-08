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



        public string Name { get { return name; } }
        public DataTable DataTable { get { return dataTable; } }
        public List<DataTableStorage> DataTableStorages { get { return storages; } }


        public DataTableStorage(string name)
        {
            this.name = name;
            this.storages = new List<DataTableStorage>();
        }
        public DataTableStorage(string name, DataTable dataTable) : this(name)
        {
            this.name = name;
            this.dataTable = dataTable;
        }



        public void AddDataTable(DataTableStorage table)
        {
            storages.Add(table);
        }
    }
}
