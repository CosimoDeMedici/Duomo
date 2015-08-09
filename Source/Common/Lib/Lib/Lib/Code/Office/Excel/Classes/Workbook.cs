//using System;
//using System.Collections.Generic;
//using System.IO;
//using XL = Microsoft.Office.Interop.Excel;


//namespace Duomo.Common.Lib.Excel
//{
//    public enum ExcelFileFormatEnumeration
//    {
//        XLSX,
//        XLSM,
//        XLS,
//        CSV,
//        Other
//    }

//    public enum ExcelWorkbookFileVersion
//    {
//        Excel2003,
//        Excel2007OrLater
//    }


//    public class Workbook
//    {
//        public XL.Workbook XlWorkbook { get; set; }
//        public string Name
//        {
//            get
//            {
//                return this.XlWorkbook.Name;
//            }
//        }
//        public string FileRootedPath
//        {
//            get
//            {
//                return this.XlWorkbook.FullName;
//            }
//        }
//        public ExcelFileFormatEnumeration FileFormat
//        {
//            get
//            {
//                ExcelFileFormatEnumeration retValue;

//                XL.XlFileFormat format = this.XlWorkbook.FileFormat;
//                switch (format)
//                {
//                    case XL.XlFileFormat.xlOpenXMLWorkbook:
//                        retValue = ExcelFileFormatEnumeration.XLSX;
//                        break;

//                    case XL.XlFileFormat.xlOpenXMLWorkbookMacroEnabled:
//                        retValue = ExcelFileFormatEnumeration.XLSM;
//                        break;

//                    case XL.XlFileFormat.xlExcel8:
//                        retValue = ExcelFileFormatEnumeration.XLS;
//                        break;

//                    case XL.XlFileFormat.xlCSV:
//                        retValue = ExcelFileFormatEnumeration.CSV;
//                        break;

//                    default:
//                        retValue = ExcelFileFormatEnumeration.Other;
//                        break;
//                }

//                return retValue;
//            }
//        }


//        public Workbook(XL.Workbook workbook)
//        {
//            this.XlWorkbook = workbook;
//        }

//        public void Save()
//        {
//            this.XlWorkbook.Save();
//        }

//        public void SaveAs(string fileRootedPath)
//        {
//            SaveAs(fileRootedPath, ExcelWorkbookFileVersion.Excel2007OrLater);
//        }

//        public void SaveAs(string fileRootedPath, ExcelWorkbookFileVersion workbookFileVersion)
//        {
//            SaveAs(fileRootedPath, workbookFileVersion, true);
//        }

//        public void SaveAs(string fileRootedPath, ExcelWorkbookFileVersion workbookFileVersion, bool overwrite)
//        {
//            if (overwrite && File.Exists(fileRootedPath))
//            {
//                File.Delete(fileRootedPath);
//            }

//            XL.XlFileFormat format;
//            switch (workbookFileVersion)
//            {
//                case ExcelWorkbookFileVersion.Excel2003:
//                    format = XL.XlFileFormat.xlExcel8;
//                    break;

//                case ExcelWorkbookFileVersion.Excel2007OrLater:
//                    format = XL.XlFileFormat.xlOpenXMLWorkbook;
//                    break;

//                default:
//                    throw new EnumerationValueUnhandledException(workbookFileVersion);
//            }

//            this.XlWorkbook.SaveAs(fileRootedPath, format,
//                Missing.Value, Missing.Value, Missing.Value, Missing.Value, XL.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
//        }

//        public void Close()
//        {
//            this.XlWorkbook.Close(false, Missing.Value, Missing.Value);
//        }
//    }
//}
