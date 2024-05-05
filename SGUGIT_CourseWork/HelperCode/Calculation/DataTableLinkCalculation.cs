using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGUGIT_CourseWork.HelperCode.Calculation
{
    /// <summary>
    /// Класс, вычисляющий дельту для 3 уровня
    /// </summary>
    internal class DataTableLinkCalculation
    {
        public DataTable currentDTable;
        public List<PointColumn> points;
        public List<PointColumn> linkPoints;
        public List<PointColumn> deltaPoints;
        public List<List<bool>> flags;



        private DataTableLinkCalculation()
        {
            this.currentDTable = new DataTable();
            this.points = new List<PointColumn>();
            this.linkPoints = new List<PointColumn>();
            this.deltaPoints = new List<PointColumn>(); 
            this.flags = new List<List<bool>>();
        }
        public DataTableLinkCalculation(DataTable dataTable) : this() 
        {
            this.currentDTable = dataTable;

            Calculate();
        }



        private void Calculate()
        {
            ColumnFill();
            CalculateLinks();
            CalculateDelta();
        }

        private void ColumnFill()
        {
            for (int col = 0; col < currentDTable.Columns.Count; col++)
            {
                PointColumn point = new PointColumn();
                point.ColumnName = currentDTable.Columns[col].ColumnName;

                for(int row = 0; row < currentDTable.Rows.Count; row++)
                {
                    point.PointAdd(Convert.ToDouble(currentDTable.Rows[row][col]));
                }

                points.Add(point);
            }
        }

        private void CalculateLinks()
        {
            for (int col = 0; col < points.Count-1; col++)
            {
                for (int col2 = col + 1; col2 < points.Count; col2++)
                {
                    PointColumn point = points[col] - points[col2];
                    point.ConvertToAbs();


                    point.ColumnName = points[col].ColumnName + " - " + points[col2].ColumnName;
                    linkPoints.Add(point);
                }
            }
        }

        private void CalculateDelta()
        {
            for(int col = 0; col < linkPoints.Count; col++)
            {
                PointColumn point = new PointColumn();
                point.ColumnName = linkPoints[col].ColumnName;

                for(int row = 0; row <  linkPoints[col].GetPointList.Count; row++)
                {
                    point.PointAdd(linkPoints[col].GetPointList[row] - linkPoints[col].GetPointList[0]);
                }

                point.ConvertToAbs();
                deltaPoints.Add(point);
            }
        }

        public List<bool> Checker(int row)
        {
            List<bool> flas = new List<bool>();
            for (int col = 0; col < deltaPoints.Count; col++) 
            {
                if (deltaPoints[col].GetPointList[row] <= GeneralData.assureValue)
                    flas.Add(true);
                else 
                    flas.Add(false);
            }

            return flas;
        }
    }
}
