using System;
using System.Runtime.InteropServices;

using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public class Excel
    {
        public XL.Application XlApplication { get; protected set; }
        public bool Started
        {
            get
            {
                return null != this.XlApplication;
            }
        }


        public void Start()
        {
            Start(true);
        }

        public void Start(bool visible)
        {
            Start(visible, false);
        }

        public void Start(bool visible, bool forceNew)
        {
            if (forceNew)
            {
                Stop();
            }

            if (null == this.XlApplication)
            {
                this.XlApplication = new XL.Application();
                this.XlApplication.Visible = visible;
            }
        }

        public void Stop()
        {
            if (null != this.XlApplication)
            {
                int numWorkbooksToClose = this.XlApplication.Workbooks.Count;
                for (int iWorkbook = 0; iWorkbook < numWorkbooksToClose; iWorkbook++)
                {
                    this.XlApplication.Workbooks[iWorkbook + 1].Close(false, Missing.Value, Missing.Value);
                }

                this.XlApplication.Quit();

                try
                {
                    Marshal.FinalReleaseComObject(this.XlApplication);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                finally
                {
                    this.XlApplication = null;
                }
            }
        }

        public Workbook GetNewWorkbook()
        {
            throw new NotImplementedException();
        }
    }
}
