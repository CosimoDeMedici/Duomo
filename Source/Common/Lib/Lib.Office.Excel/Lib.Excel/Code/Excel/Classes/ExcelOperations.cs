using System;
using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public static class ExcelOperations
    {
        public static void DisplayAlerts(Excel app, bool value)
        {
            app.DisplayAlerts = value;
        }

        public static void FreezePanes(Excel app, bool value)
        {
            app.FreezePanes = value;
        }

        public static void FreezePanes(Excel app, Range range)
        {
            range.XlRange.Worksheet.Select();
            range.XlRange.Select();

            ExcelOperations.FreezePanes(app, true);
        }

        public static void ScreenUpdating(Excel app, bool value)
        {
            app.ScreenUpdating = value;
        }

        public static void ShowGridlines(Excel app, Worksheet ws, bool value)
        {
            ((XL._Worksheet)ws.XlWorksheet).Activate();

            app.XlApplication.ActiveWindow.DisplayGridlines = value;
        }
    }
}
