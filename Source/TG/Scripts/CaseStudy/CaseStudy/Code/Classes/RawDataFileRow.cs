using System;


namespace TG.CaseStudy
{
    public class RawDataFileRow
    {
        public const int TransactionDateColumnIndex = 0;
        public const int BusinessNameColumnIndex = 1;
        public const int CustomerIDColumnIndex = 2;
        public const int TransactionAmountColumnIndex = 3;


        public string TransactionDate { get; set; }
        public string BusinessName { get; set; }
        public string CustomerID { get; set; }
        public string TransactionAmount { get; set; }


        public RawDataFileRow() { }

        public RawDataFileRow(string transactionDate, string businessName, string customerID, string transactionAmount)
        {
            this.TransactionDate = transactionDate;
            this.BusinessName = businessName;
            this.CustomerID = customerID;
            this.TransactionAmount = transactionAmount;
        }
    }
}
