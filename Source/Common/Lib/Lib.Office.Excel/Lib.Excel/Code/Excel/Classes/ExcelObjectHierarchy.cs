using System;


namespace Duomo.Common.Lib.Excel
{
    public class ExcelObjectHierarchy
    {
        public Excel Application { get; set; }
        public Workbook Workbook { get; set; }
        public Worksheet Worksheet { get; set; }
        public Range Range { get; set; }


        public ExcelObjectHierarchy() { }
    }
}
