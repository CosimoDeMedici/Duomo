using System;
using System.Collections.Generic;


namespace TG.CaseStudy
{
    [Serializable]
    public class Transaction
    {
        #region Static

        public static bool TestIdentifierEquality(Transaction a, Transaction b)
        {
            bool retValue = false; // Assume failure.

            if (a.Date != b.Date)
            {
                return retValue;
            }

            if (a.BusinessName != b.BusinessName)
            {
                return retValue;
            }

            if (a.CustomerID != b.CustomerID)
            {
                return retValue;
            }

            retValue = true;
            return retValue;
        }

        public static bool TestEquality(Transaction a, Transaction b)
        {
            bool retValue = false; // Assume failure.

            if (!Transaction.TestIdentifierEquality(a, b))
            {
                return retValue;
            }

            if (a.Amount != b.Amount)
            {
                return retValue;
            }

            retValue = true;
            return retValue;
        }


        public static string FormatCsvRow(Transaction transaction)
        {
            string dateToken = String.Format(@"{0:MM/dd/yyyy}", transaction.Date);
            string businessNameToken = transaction.BusinessName;
            string customerIDToken = transaction.CustomerID.ToString();
            string amountToken = transaction.Amount.ToString();

            string retValue = String.Format(@"{0}{1}{2}{1}{3}{1}{4}", dateToken, @",", businessNameToken, customerIDToken, amountToken);
            return retValue;
        }

        #endregion


        public DateTime Date { get; set; }
        public string BusinessName { get; set; }
        public int CustomerID { get; set; }
        public double Amount { get; set; }


        public Transaction() { }

        public Transaction(DateTime date, string businessName, int customerID, double amount)
        {
            this.Date = date;
            this.BusinessName = businessName;
            this.CustomerID = customerID;
            this.Amount = amount;
        }

        public Transaction(Transaction other)
        {
            this.Date = other.Date;
            this.BusinessName = other.BusinessName;
            this.CustomerID = other.CustomerID;
            this.Amount = other.Amount;
        }
    }


    public class TransactionDateComparer : IComparer<Transaction>
    {
        #region IComparer<Transaction> Members

        public int Compare(Transaction x, Transaction y)
        {
            int retValue = x.Date.CompareTo(y.Date);
            return retValue;
        }

        #endregion
    }


    public class TransactionAmountComparer : IComparer<Transaction>
    {
        #region IComparer<Transaction> Members

        public int Compare(Transaction x, Transaction y)
        {
            int retValue = x.Amount.CompareTo(y.Amount);
            return retValue;
        }

        #endregion
    }
}
