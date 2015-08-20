using System;
using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public class Worksheet
    {
        public XL.Worksheet XlWorksheet { get; set; }
        public Workbook Workbook
        {
            get
            {
                XL.Workbook xlWkbk = this.XlWorksheet.Parent;

                Workbook retValue = new Workbook(xlWkbk);
                return retValue;
            }
        }
        public Excel Application
        {
            get
            {
                XL.Application xlApp = this.XlWorksheet.Application;

                Excel retValue = new Excel(xlApp);
                return retValue;
            }
        }
        public string Name
        {
            get
            {
                string retValue = this.XlWorksheet.Name;
                return retValue;
            }
            set
            {
                this.XlWorksheet.Name = value;
            }
        }


        public Worksheet() { }

        public Worksheet(Workbook wkbk)
        {
            Worksheet ws = wkbk.GetNewWorksheet();

            this.XlWorksheet = ws.XlWorksheet;
        }

        public Worksheet(Workbook wkbk, string name)
        {
            Worksheet ws = wkbk.GetNewWorksheet();

            this.XlWorksheet = ws.XlWorksheet;

            this.Name = name;
        }

        public Worksheet(XL.Worksheet ws)
        {
            this.XlWorksheet = ws;
        }

        public Range GetA1Range()
        {
            XL.Range rng = this.XlWorksheet.Cells[1, 1];

            Range retValue = new Range(rng);
            return retValue;
        }

        public Range GetUsedRange()
        {
            XL.Range usedRng = this.XlWorksheet.UsedRange;

            Range retValue = new Range(usedRng);
            return retValue;
        }

        public Range GetRange(string address)
        {
            Range retValue = new Range(this, address);
            return retValue;
        }

        public Range GetRange(Range upperLeft, RangeSize size)
        {
            Range retValue = this.GetRange(upperLeft, size.Rows, size.Columns);
            return retValue;
        }

        public Range GetRange(Range upperLeft, int numberOfRows, int numberOfColumns)
        {
            XL.Range xlEndRng = this.XlWorksheet.Cells[upperLeft.Row + numberOfRows - 1, upperLeft.Column + numberOfColumns - 1];

            XL.Range xlRng = this.XlWorksheet.Range[upperLeft.XlRange, xlEndRng];
            
            Range retValue = new Range(xlRng);
            return retValue;
        }

        public Range GetRange(Range upperLeft, Range lowerRight)
        {
            XL.Range xlRng = this.XlWorksheet.Range[upperLeft.XlRange, lowerRight.XlRange];

            Range retValue = new Range(xlRng);
            return retValue;
        }

        public Range GetEntireWorksheetRange()
        {
            XL.Range entireWsRng = this.XlWorksheet.Cells.EntireRow.EntireColumn;

            Range retValue = new Range(entireWsRng);
            return retValue;
        }

        public void Hide()
        {
            this.XlWorksheet.Visible = XL.XlSheetVisibility.xlSheetHidden;
        }

        public void Show()
        {
            this.XlWorksheet.Visible = XL.XlSheetVisibility.xlSheetVisible;
        }

        public void HideVery()
        {
            this.XlWorksheet.Visible = XL.XlSheetVisibility.xlSheetVeryHidden;
        }
    }
}
