using System;
using System.Runtime.InteropServices;

using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public class Excel
    {
        public XL.Application XlApplication { get; protected set; }
        public bool DisplayAlerts
        {
            get
            {
                bool retValue = this.XlApplication.DisplayAlerts;
                return retValue;
            }
            set
            {
                this.XlApplication.DisplayAlerts = value;
            }
        }
        public bool FreezePanes
        {
            get
            {
                bool retValue = this.XlApplication.ActiveWindow.FreezePanes;
                return retValue;
            }
            set
            {
                this.XlApplication.ActiveWindow.FreezePanes = value;
            }
        }
        public bool Started
        {
            get
            {
                return null != this.XlApplication;
            }
        }
        public bool ScreenUpdating
        {
            get
            {
                bool retValue = this.XlApplication.ScreenUpdating;
                return retValue;
            }
            set
            {
                this.XlApplication.ScreenUpdating = value;
            }
        }
        public int ZoomPercent
        {
            get
            {
                int retValue = this.XlApplication.ActiveWindow.Zoom;
                return retValue;
            }
            set
            {
                this.XlApplication.ActiveWindow.Zoom = value;
            }
        }



        public Excel() { }

        public Excel(XL.Application xlApp)
        {
            this.XlApplication = xlApp;
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
            XL.Workbook xlWkbk = this.XlApplication.Workbooks.Add(Missing.Value);

            Workbook retValue = new Workbook(xlWkbk);
            return retValue;
        }

        public Workbook OpenWorkbook(string fileRootedPath)
        {
            XL.Workbook xlWkbk = this.XlApplication.Workbooks.Open(fileRootedPath);

            Workbook retValue = new Workbook(xlWkbk);
            return retValue;
        }

        public Workbook GetWorkbook(string name)
        {
            XL.Workbook xlWkbk = this.GetXlWorkbook(name);

            Workbook retValue = new Workbook(xlWkbk);
            return retValue;
        }

        private XL.Workbook GetXlWorkbook(string name)
        {
            XL.Workbook retValue = null;

            foreach (XL.Workbook curXlWkbk in this.XlApplication.Workbooks)
            {
                if (name == curXlWkbk.Name)
                {
                    retValue = curXlWkbk;
                    break;
                }
            }

            return retValue;
        }

        public void CloseWorkbook(string name)
        {
            XL.Workbook xlWkbk = this.GetXlWorkbook(name);

            xlWkbk.Close();
        }
    }
}
