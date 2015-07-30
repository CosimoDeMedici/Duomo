using System;


namespace TG.CaseStudy
{
    public static class FileRootedPaths
    {
        public static string OutlierTransactions
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Raw Data\Outlier Transactions.csv";
                return retValue;
            }
        }

        public static string DatesList
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Raw Data\DateList.csv";
                return retValue;
            }
        }

        public static string CleanedTransactionsBinary
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Cleaned Transactions.dat";
                return retValue;
            }
        }

        public static string RedatedTransactionsBinary
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Re-Dated Transactions.dat";
                return retValue;
            }
        }

        public static string RedatedDraftTransactionsBinary
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Re-Dated Draft Transactions.dat";
                return retValue;
            }
        }

        public static string TransactionsBinary
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Transactions.dat";
                return retValue;
            }
        }

        public static string CleanedData
        {
            get
            {
                string retValue = @"C:\temp\TG\Case Study\Cleaned Raw Data.csv";
                return retValue;
            }
        }

        public static string RedatedData
        {
            get
            {
                string retValue = @"C:\temp\TG\Case Study\Re-Dated Raw Data.csv";
                return retValue;
            }
        }

        public static string RedatedDraftData
        {
            get
            {
                string retValue = @"C:\temp\TG\Case Study\Re-Dated Draft Raw Data.csv";
                return retValue;
            }
        }

        public static string RawData
        {
            get
            {
                string retValue = @"C:\Users\David\Dropbox\Job Search\Companies\Tiger Global\Case Study\Raw Data\Tiger Global Management - Case Study Data.csv";
                return retValue;
            }
        }
    }
}
