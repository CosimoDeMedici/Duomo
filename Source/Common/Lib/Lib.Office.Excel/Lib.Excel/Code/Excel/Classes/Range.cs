using System;
using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public class Range
    {
        public XL.Range XlRange { get; set; }
        public Worksheet Worksheet
        {
            get
            {
                XL.Worksheet xlWs = this.XlRange.Parent;

                Worksheet retValue = new Worksheet(xlWs);
                return retValue;
            }
        }
        public Workbook Workbook
        {
            get
            {
                Worksheet ws = this.Worksheet;

                XL.Workbook xlWkbk = ws.XlWorksheet.Parent;

                Workbook retValue = new Workbook(xlWkbk);
                return retValue;
            }
        }
        public Excel Excel
        {
            get
            {
                XL.Application xlApp = this.XlRange.Application;

                Excel retValue = new Excel(xlApp);
                return retValue;
            }
        }
        public int Row
        {
            get
            {
                int retValue = this.XlRange.Row;
                return retValue;
            }
        }
        public int Column
        {
            get
            {
                int retValue = this.XlRange.Column;
                return retValue;
            }
        }
        public bool IsEmpty
        {
            get
            {
                bool retValue = null == this.XlRange.Value2;
                return retValue;
            }
        }
        public bool IsNumeric
        {
            get
            {
                bool retValue = this.XlRange.Application.WorksheetFunction.IsNumber(this.XlRange.Value);
                return retValue;
            }
        }
        public object Value
        {
            get
            {
                object retValue = this.XlRange.Value;
                return retValue;
            }
            set
            {
                this.XlRange.Value = value;
            }
        }
        public double ValueDouble
        {
            get
            {
                double retValue = this.XlRange.Value2;
                return retValue;
            }
            set
            {
                this.XlRange.Value2 = value;
            }
        }
        public int ValueInt
        {
            get
            {
                int retValue = this.XlRange.Value2;
                return retValue;
            }
            set
            {
                this.XlRange.Value2 = value;
            }
        }
        public string ValueString
        {
            get
            {
                string retValue = this.XlRange.Value2;
                return retValue;
            }
            set
            {
                this.XlRange.Value2 = value;
            }
        }
        public object[,] Values
        {
            get
            {
                object[,] retValue = this.XlRange.get_Value(XL.XlRangeValueDataType.xlRangeValueDefault);
                return retValue;
            }
            set
            {
                this.XlRange.Value = value;
            }
        }
        public Range EndDown
        {
            get
            {
                XL.Range xlRng = this.XlRange.get_End(XL.XlDirection.xlDown);

                Range retValue = new Range(xlRng);
                return retValue;
            }
        }
        public Range EndLeft
        {
            get
            {
                XL.Range xlRng = this.XlRange.get_End(XL.XlDirection.xlToLeft);

                Range retValue = new Range(xlRng);
                return retValue;
            }
        }
        public Range EndRight
        {
            get
            {
                XL.Range xlRng = this.XlRange.get_End(XL.XlDirection.xlToRight);

                Range retValue = new Range(xlRng);
                return retValue;
            }
        }
        public Range EndUp
        {
            get
            {
                XL.Range xlRng = this.XlRange.get_End(XL.XlDirection.xlUp);

                Range retValue = new Range(xlRng);
                return retValue;
            }
        }
        public string NumberFormat
        {
            get
            {
                string retValue = this.XlRange.NumberFormat;
                return retValue;
            }
            set
            {
                this.XlRange.NumberFormat = value;
            }
        }
        public string Formula
        {
            get
            {
                string retValue = this.XlRange.Formula;
                return retValue;
            }
            set
            {
                this.XlRange.Formula = value;
            }
        }


        public Range() { }

        public Range(Worksheet ws, string address)
        {
            this.XlRange = ws.XlWorksheet.get_Range(address);
        }

        public Range(XL.Range xlRange)
        {
            this.XlRange = xlRange;
        }

        public bool Intersects(Range range)
        {
            bool retValue = this.Intersects(range.XlRange);
            return retValue;
        }

        protected bool Intersects(XL.Range xlRange)
        {
            XL.Range intersection = this.GetIntersection(xlRange);

            bool retValue = null == intersection;
            return retValue;
        }

        public Range GetIntersection(Range range)
        {
            XL.Range intersection = this.GetIntersection(range.XlRange);

            Range retValue = new Range(intersection);
            return retValue;
        }

        protected XL.Range GetIntersection(XL.Range xlRange)
        {
            XL.Range retValue = this.XlRange.Application.Intersect(xlRange, this.XlRange);
            return retValue;
        }

        public Range GetOffset(int rows, int columns)
        {
            XL.Range xlOffsetRng = this.XlRange.get_Offset(rows, columns);

            Range retValue = new Range(xlOffsetRng);
            return retValue;
        }
    }
}
