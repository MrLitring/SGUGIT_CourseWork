using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGUGIT_CourseWork.HelperCode
{
    public class DataTableWork
    {
        public List<PointColumn> pointColumns;
        private DataTable dtable = new DataTable();


        public DataTableWork(DataTable dataTable) 
        {
            this.dtable = dataTable;

            pointColumns = new List<PointColumn>();
            Console.WriteLine();
        }



    }
}
