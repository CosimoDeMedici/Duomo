using System;
using System.IO;


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

        public static DateTime DateTimeFromStringExact(string dateString, string format)
        {
            DateTime retValue;
            if (!DateTime.TryParseExact(dateString, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out retValue))
            {
                throw new ArgumentException(String.Format("The date string '{0}' was not recognized as having the format '{1}'.", dateString, format), "dateString");
            }

            return retValue;
        }
    }
}
