using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Principal;


namespace Duomo.Common.Lib
{
    public static class Utilities
    {
        public static bool ProcessIs64Bit
        {
            get
            {
                return Environment.Is64BitProcess;
            }
        }
        public static string PathEnvironmentVariable
        {
            get
            {
                return Environment.GetEnvironmentVariable(@"PATH");
            }
            set
            {
                Environment.SetEnvironmentVariable(@"PATH", value);
            }
        }
        public static string ExecutableAssemblyFolderRootedPath
        {
            get
            {
                string executableAssemblyFileRootedPath = Utilities.ExecutableAssemblyRootedPath;
                
                string retValue = Path.GetDirectoryName(executableAssemblyFileRootedPath);
                return retValue;
            }
        }
        public static string ExecutableAssemblyRootedPath
        {
            get
            {
                string retValue = Assembly.GetEntryAssembly().Location;
                return retValue;
            }
        }
        public static string ExecutableFolderRootedPath
        {
            get
            {
                return Path.GetDirectoryName(Utilities.ExecutableRootedPath);
            }
        }
        public static string ExecutableRootedPath
        {
            get
            {
                return Environment.GetCommandLineArgs()[0];
            }
        }
        public static string DuomoCommonLibAssemblyFolderRootedPath
        {
            get
            {
                string duomoCommonLibAssemblyFileRootedPath = Utilities.ExecutableAssemblyRootedPath;

                string retValue = Path.GetDirectoryName(duomoCommonLibAssemblyFileRootedPath);
                return retValue;
            }
        }
        public static string DuomoCommonLibAssemblyRootedPath
        {
            get
            {
                string retValue = Assembly.GetExecutingAssembly().Location;
                return retValue;
            }
        }
        public static string DocumentsFolderRootedPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
        }
        public static string AppDataRoamingFolderRootedPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
        }
        public static string TempFolderRootedPath
        {
            get
            {
                return @"C:\temp";
            }
        }
        public static string UserName
        {
            get
            {
                string retValue = WindowsIdentity.GetCurrent().Name;
                return retValue;
            }
        }
        public static string MachineName
        {
            get
            {
                string retValue = Environment.MachineName;
                return retValue;
            }
        }
        public static DateTime DefaultCobDate
        {
            get
            {
                DateTime today = DateTime.Today;

                DateTime retValue;
                if (DayOfWeek.Monday == today.DayOfWeek)
                {
                    retValue = today.AddDays(-3);
                }
                else
                {
                    retValue = today.AddDays(-1);
                }

                return retValue;
            }
        }


        public static string GetEnvironmentVariable(string variable)
        {
            string retValue = Environment.GetEnvironmentVariable(variable);
            return retValue;
        }

        public static void SetEnvironmentalVariable(string variable, string value)
        {
            Environment.SetEnvironmentVariable(variable, value);
        }

        public static DateTime DateTimeFromStringExactFormat(string date, string format)
        {
            DateTime retValue;
            if (!DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out retValue))
            {
                throw new ArgumentException(String.Format("The date string '{0}' was not recognized as having the format '{1}'.", date, format), @"date");
            }

            return retValue;
        }

        public static bool TryParseDateTimeFromStringExactFormat(string date, string format, out DateTime dateTime)
        {
            bool retValue = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
            return retValue;
        }
    }
}
