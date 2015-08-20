using System;
using System.Collections.Generic;
using System.IO;
using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel
{
    public enum ExcelFileFormatEnumeration
    {
        XLSX,
        XLSM,
        XLS,
        CSV,
        Other
    }

    public enum ExcelWorkbookFileVersion
    {
        Excel2003,
        Excel2007OrLater
    }


    public class Workbook
    {
        public XL.Workbook XlWorkbook { get; set; }
        public Excel Application
        {
            get
            {
                XL.Application xlApp = this.XlWorkbook.Application;

                Excel retValue = new Excel(xlApp);
                return retValue;
            }
        }
        public string Name
        {
            get
            {
                string retValue = this.XlWorkbook.Name;
                return retValue;
            }
        }
        public string FileRootedPath
        {
            get
            {
                string retValue = this.XlWorkbook.FullName;
                return retValue;
            }
        }
        public ExcelFileFormatEnumeration FileFormat
        {
            get
            {
                ExcelFileFormatEnumeration retValue;

                XL.XlFileFormat format = this.XlWorkbook.FileFormat;
                switch (format)
                {
                    case XL.XlFileFormat.xlOpenXMLWorkbook:
                        retValue = ExcelFileFormatEnumeration.XLSX;
                        break;

                    case XL.XlFileFormat.xlOpenXMLWorkbookMacroEnabled:
                        retValue = ExcelFileFormatEnumeration.XLSM;
                        break;

                    case XL.XlFileFormat.xlExcel8:
                        retValue = ExcelFileFormatEnumeration.XLS;
                        break;

                    case XL.XlFileFormat.xlCSV:
                        retValue = ExcelFileFormatEnumeration.CSV;
                        break;

                    default:
                        retValue = ExcelFileFormatEnumeration.Other;
                        break;
                }

                return retValue;
            }
        }


        public Workbook() { }

        public Workbook(XL.Workbook workbook)
        {
            this.XlWorkbook = workbook;
        }

        public void Save()
        {
            this.XlWorkbook.Save();
        }

        public void SaveAs(string fileRootedPath)
        {
            SaveAs(fileRootedPath, ExcelWorkbookFileVersion.Excel2007OrLater);
        }

        public void SaveAs(string fileRootedPath, ExcelWorkbookFileVersion workbookFileVersion)
        {
            SaveAs(fileRootedPath, workbookFileVersion, true);
        }

        public void SaveAs(string fileRootedPath, ExcelWorkbookFileVersion workbookFileVersion, bool overwrite)
        {
            if (overwrite && File.Exists(fileRootedPath))
            {
                File.Delete(fileRootedPath);
            }

            XL.XlFileFormat format;
            switch (workbookFileVersion)
            {
                case ExcelWorkbookFileVersion.Excel2003:
                    format = XL.XlFileFormat.xlExcel8;
                    break;

                case ExcelWorkbookFileVersion.Excel2007OrLater:
                    format = XL.XlFileFormat.xlOpenXMLWorkbook;
                    break;

                default:
                    throw new EnumerationValueUnhandledException(workbookFileVersion);
            }

            this.XlWorkbook.SaveAs(fileRootedPath, format,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, XL.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
        }

        public void Close()
        {
            this.XlWorkbook.Close(false, Missing.Value, Missing.Value);
        }

        public Worksheet GetNewWorksheet()
        {
            XL.Worksheet xlWs = this.XlWorkbook.Worksheets.Add(Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            Worksheet ws = new Worksheet(xlWs);
            return ws;
        }

        public Worksheet GetWorksheet(string wsName)
        {
            XL.Worksheet xlWs = this.XlWorkbook.Worksheets[wsName];

            Worksheet retValue = new Worksheet(xlWs);
            return retValue;
        }

        public Worksheet GetWorksheet(int wsNumber)
        {
            XL.Worksheet xlWs = this.XlWorkbook.Worksheets[wsNumber + 1]; // Excel is index-1 based.

            Worksheet retValue = new Worksheet(xlWs);
            return retValue;
        }

        public bool HasWorksheet(string wsName)
        {
            bool retValue = false;

            foreach(XL.Worksheet curWs in this.XlWorkbook.Worksheets)
            {
                if (wsName == curWs.Name)
                {
                    retValue = true;
                    break;
                }
            }

            return retValue;
        }

        public void DeleteWorksheet(string wsName)
        {
            Worksheet wsToDelete = this.GetWorksheet(wsName);

            wsToDelete.XlWorksheet.Delete();
        }

        public void SetNamedRange(string name, Range range)
        {
            this.XlWorkbook.Names.Add(name, range.XlRange);
        }

        private XL.Name GetXlName(string name)
        {
            XL.Name retValue = null;

            foreach (XL.Name curXlName in this.XlWorkbook.Names)
            {
                if (name == curXlName.Name)
                {
                    retValue = curXlName;
                    break;
                }
            }

            return retValue;
        }

        public bool HasNamedRange(string name)
        {
            bool retValue = null == this.GetXlName(name);
            return retValue;
        }

        public XL.Range GetXlRangeByName(string name)
        {
            XL.Name xlName = this.GetXlName(name);

            XL.Range retValue = xlName.RefersToRange;
            return retValue;
        }

        public Range GetNamedRange(string name)
        {
            XL.Range xlRng = this.GetXlRangeByName(name);

            Range retValue = new Range(xlRng);
            return retValue;
        }

        public void RunAutoMacrosAutoOpen()
        {
            this.XlWorkbook.RunAutoMacros(XL.XlRunAutoMacro.xlAutoOpen);
        }
    }
}
