using System;
using System.Collections.Generic;
using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public static class WorkbookOperations
    {
        public static void RemoveSheet1Sheet2Sheet3(Workbook wkbk)
        {
            wkbk.DeleteWorksheet(@"Sheet1");
            wkbk.DeleteWorksheet(@"Sheet2");
            wkbk.DeleteWorksheet(@"Sheet3");
        }

        public static void ReorderWorksheets(Workbook wkbk, List<string> orderedWorksheetNames)
        {
            List<string> reversedOnPurpose = new List<string>(orderedWorksheetNames);
            reversedOnPurpose.Reverse();

            foreach (string wsName in reversedOnPurpose)
            {
                Worksheet ws = wkbk.GetWorksheet(wsName);

                XL.Worksheet xlWs = ws.XlWorksheet;

                xlWs.Move(wkbk.XlWorkbook.Sheets[1]);
            }
        }

        public static void HideWorksheet(Workbook wkbk, string name)
        {
            Worksheet ws = wkbk.GetWorksheet(name);

            ws.Hide();
        }
    }
}
