using System;
using System.Collections.Generic;


namespace TG.CaseStudy
{
    public class TransactionDate
    {
        public const string Oct11YYYYMMDD = @"20111031";
        public const string Nov11YYYYMMDD = @"20111130";
        public const string Dec11YYYYMMDD = @"20111231";
        public const string Jan12YYYYMMDD = @"20120131";
        public const string Feb12YYYYMMDD = @"20120229";
        public const string Mar12YYYYMMDD = @"20120331";


        #region Static

        public static readonly DateTime Oct11 = new DateTime(2011, 10, 31);
        public static readonly DateTime Nov11 = new DateTime(2011, 11, 30);
        public static readonly DateTime Dec11 = new DateTime(2011, 12, 31);
        public static readonly DateTime Jan11 = new DateTime(2012, 01, 31);
        public static readonly DateTime Feb11 = new DateTime(2012, 02, 29);
        public static readonly DateTime Mar11 = new DateTime(2012, 03, 31);

        // In-sample: 151 days - 18 days = 133.
        public static List<DateTime> GetDatesToRemoveForRegression()
        {
            List<DateTime> retValue = new List<DateTime>();

            retValue.Add(new DateTime(2013, 11, 1));
            retValue.Add(new DateTime(2013, 11, 2));
            retValue.Add(new DateTime(2013, 11, 3));
            retValue.Add(new DateTime(2013, 11, 4));
            retValue.Add(new DateTime(2013, 11, 5));
            retValue.Add(new DateTime(2013, 11, 26));
            retValue.Add(new DateTime(2013, 11, 27));
            retValue.Add(new DateTime(2013, 11, 28));

            retValue.Add(new DateTime(2013, 12, 1));
            retValue.Add(new DateTime(2013, 12, 2));
            retValue.Add(new DateTime(2013, 12, 3));
            retValue.Add(new DateTime(2013, 12, 4));
            retValue.Add(new DateTime(2013, 12, 5));
            retValue.Add(new DateTime(2013, 12, 6));
            retValue.Add(new DateTime(2013, 12, 13));
            retValue.Add(new DateTime(2013, 12, 23));
            retValue.Add(new DateTime(2013, 12, 24));
            retValue.Add(new DateTime(2013, 12, 25));

            retValue.Add(new DateTime(2014, 3, 17));

            return retValue;
        }

        public static List<DateTime> GetAllDatesInOrder()
        {
            List<DateTime> retValue = new List<DateTime>();

            retValue.Add(TransactionDate.Oct11);
            retValue.Add(TransactionDate.Nov11);
            retValue.Add(TransactionDate.Dec11);
            retValue.Add(TransactionDate.Jan11);
            retValue.Add(TransactionDate.Feb11);
            retValue.Add(TransactionDate.Mar11);

            return retValue;
        }

        public static List<DateTime> GetAllActualDatesInOrder()
        {
            List<DateTime> retValue = new List<DateTime>();

            retValue.Add(new DateTime(2013, 10, 31));
            retValue.Add(new DateTime(2013, 11, 30));
            retValue.Add(new DateTime(2013, 12, 31));
            retValue.Add(new DateTime(2014, 01, 31));
            retValue.Add(new DateTime(2014, 02, 28));
            retValue.Add(new DateTime(2014, 03, 31));

            return retValue;
        }

        public static List<DateTime> GetInSampleDatesInOrder()
        {
            List<DateTime> retValue = new List<DateTime>();

            retValue.Add(TransactionDate.Oct11);
            retValue.Add(TransactionDate.Nov11);
            retValue.Add(TransactionDate.Dec11);
            retValue.Add(TransactionDate.Jan11);
            retValue.Add(TransactionDate.Feb11);
            //retValue.Add(TransactionDate.Mar11); // Remove this final date to avoid data-mining.

            return retValue;
        }

        public static DateTime GetOutOfSampleDate()
        {
            DateTime retValue = TransactionDate.Mar11;
            return retValue;
        }

        public static List<DateTime> GetOutOfSampleDates()
        {
            List<DateTime> retValue = new List<DateTime>();

            DateTime outOfSampleDate = TransactionDate.GetOutOfSampleDate();
            retValue.Add(outOfSampleDate);

            return retValue;
        }

        #endregion


        public DateTime Date { get; set; }


        public TransactionDate() { }

        public TransactionDate(DateTime date)
        {
            this.Date = date;
        }
    }
}
