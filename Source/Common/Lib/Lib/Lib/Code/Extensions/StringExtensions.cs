using System;
using System.IO;


namespace Duomo.Common.Lib
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string str)
        {
            MemoryStream memStream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(memStream))
            {
                writer.Write(str);
                writer.Flush();
            }

            memStream.Position = 0;

            return memStream;
        }

        public static DateTime ToDateTimeFromYYYYMMDD(this string yyyymmdd)
        {
            DateTime retValue;
            if (null == yyyymmdd)
            {
                retValue = DateTime.MinValue;
            }
            else
            {
                if (8 != yyyymmdd.Length)
                {
                    retValue = DateTime.MinValue;
                }
                else
                {
                    retValue = Utilities.DateTimeFromStringExactFormat(yyyymmdd, @"yyyyMMdd");
                }
            }

            return retValue;
        }

        public static DateTime ToDateTimeFromFormat(this string date, string format)
        {
            DateTime retValue = Utilities.DateTimeFromStringExactFormat(date, format);
            return retValue;
        }

        /// <summary>
        /// Converts the strings "Yes", "yes", "Y" or "y" to true, and the same for no and false.
        /// </summary>
        public static bool TryToBoolFromYesNo(this string yesOrNo, out bool trueOrFalse)
        {
            string loweredYesOrNo = yesOrNo.ToLowerInvariant();

            bool retValue = true;
            trueOrFalse = false;
            switch (loweredYesOrNo)
            {
                case @"yes":
                case @"y":
                    trueOrFalse = true;
                    break;

                case @"no":
                case @"n":
                    trueOrFalse = false;
                    break;

                default:
                    retValue = false;
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// Converts the strings "Yes", "yes", "Y" or "y" to true, and the same for no and false.
        /// </summary>
        public static bool ToBoolFromYesNo(this string yesOrNo)
        {
            bool retValue;
            if (!yesOrNo.TryToBoolFromYesNo(out retValue))
            {
                throw new ArgumentException(String.Format(@"Unable to parse value '{0}' to a boolean.", yesOrNo));
            }

            return retValue;
        }

        /// <summary>
        /// Converts the strings "Yes", "yes", "Y" or "y", "True" or "true", "T" or "t" to true, otherwise false.
        /// </summary>
        public static bool ToBool(this string yesOrNo)
        {
            string loweredYesOrNo = yesOrNo.ToLowerInvariant();

            bool retValue;
            switch (loweredYesOrNo)
            {
                case @"yes":
                case @"y":
                case @"true":
                case @"t":
                    retValue = true;
                    break;

                default:
                    retValue = false;
                    break;
            }

            return retValue;
        }
    }
}
