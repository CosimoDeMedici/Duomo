using System;


namespace Duomo.Common.Lib.Excel
{
    public class RangeSize
    {
        public int Rows { get; set; }
        public int Columns { get; set; }


        public RangeSize() { }

        public RangeSize(int rows, int columns)
        {
            this.Setup(rows, columns);
        }

        private void Setup(int rows, int columns)
        {
            this.Rows = rows;
            this.Columns = columns;
        }

        public RangeSize(object[,] data)
        {
            int rows = data.GetLength(0);
            int columns = data.GetLength(1);

            this.Setup(rows, columns);
        }
    }
}
