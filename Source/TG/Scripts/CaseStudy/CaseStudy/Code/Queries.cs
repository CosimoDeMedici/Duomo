using System;
using System.Collections.Generic;
using System.Linq;


namespace TG.CaseStudy
{
    public static class Queries
    {
        public static List<Transaction> GetTransactionsForDate(List<Transaction> transactions, DateTime date)
        {
            List<Transaction> retValue =
                (from element in transactions
                 where element.Date == date
                 select element).ToList();

            return retValue;
        }

        public static List<Transaction> GetTransactionsForDateRange(List<Transaction> transactions, DateTime startDate, DateTime endDate)
        {
            List<Transaction> retValue =
                (from element in transactions
                 where element.Date >= startDate
                 && element.Date < endDate
                 select element).ToList();

            return retValue;
        }

        public static List<Transaction> GetTransactionsForBusiness(List<Transaction> transactions, string businessName)
        {
            List<Transaction> retValue =
                (from element in transactions
                 where element.BusinessName == businessName
                 select element).ToList();

            return retValue;
        }

        public static List<Transaction> GetTransactionsForDateAndBusiness(List<Transaction> transactions, DateTime date, string businessName)
        {
            List<Transaction> retValue =
                (from element in transactions
                 where element.Date == date
                 && element.BusinessName == businessName
                 select element).ToList();

            return retValue;
        }

        public static List<Transaction> GetTransactionsForCustomerID(List<Transaction> transactions, int customerID)
        {
            List<Transaction> retValue =
                (from element in transactions
                 where element.CustomerID == customerID
                 select element).ToList();

            return retValue;
        }

        public static int Count(List<Transaction> transactions)
        {
            int retValue = transactions.Count;
            return retValue;
        }

        public static double Sum(List<Transaction> transactions)
        {
            double retValue = transactions.Sum((x) => x.Amount);
            return retValue;
        }

        public static double Average(List<Transaction> transactions)
        {
            double retValue = transactions.Average((x) => x.Amount);
            return retValue;
        }

        public static double Variance(List<Transaction> transactions)
        {
            double average = Queries.Average(transactions);

            double sum = 0;
            foreach (Transaction transaction in transactions)
            {
                double residual = transaction.Amount - average;
                double residualSquared = residual * residual;

                sum += residualSquared;
            }

            int divisor = transactions.Count; // Use population STDEV.

            double retValue = sum / divisor;
            return retValue;
        }

        public static double StandardDeviation(List<Transaction> transactions)
        {
            double variance = Queries.Variance(transactions);

            double retValue = Math.Pow(variance, 0.5);
            return retValue;
        }

        public static List<DateTime> GetDates(List<Transaction> transactions)
        {
            List<DateTime> retValue =
                (from element in transactions
                 select element.Date).Distinct().ToList();

            return retValue;
        }

        public static List<Transaction> GetTransactionsForYearAndMonthOfDate(List<Transaction> transactions, DateTime date)
        {
            List<Transaction> retValue =
                (from element in transactions
                 where element.Date.Year == date.Year
                 && element.Date.Month == date.Month
                 select element).ToList();

            return retValue;
        }

        public static Tuple<DateTime, DateTime> GetMinMaxDates(List<Transaction> transactions)
        {
            DateTime minDate =
                (from element in transactions
                 select element.Date).Min();

            DateTime maxDate =
                (from element in transactions
                 select element.Date).Max();

            Tuple<DateTime, DateTime> retValue = new Tuple<DateTime, DateTime>(minDate, maxDate);
            return retValue;
        }

        public static List<int> GetCustomerIDs(List<Transaction> transactions)
        {
            List<int> retValue =
                (from element in transactions
                 select element.CustomerID).Distinct().ToList();

            retValue.Sort(); // Least to greatest.
            return retValue;
        }

        public static Dictionary<int, List<Transaction>> TransformToTransactionsByCustomerID(List<Transaction> transactions)
        {
            Dictionary<int, List<Transaction>> retValue = new Dictionary<int, List<Transaction>>();

            foreach (Transaction transaction in transactions)
            {
                List<Transaction> curTransactionsList;
                if (retValue.ContainsKey(transaction.CustomerID))
                {
                    curTransactionsList = retValue[transaction.CustomerID];
                }
                else
                {
                    curTransactionsList = new List<Transaction>();
                    retValue.Add(transaction.CustomerID, curTransactionsList);
                }

                curTransactionsList.Add(transaction);
            }

            return retValue;
        }

        public static List<int> GetAllCounts(Dictionary<string, Dictionary<int, int>> data)
        {
            List<int> retValue =
                (from element in data.Values
                 from element2 in element.Keys
                 select element2).Distinct().ToList();

            retValue.Sort();

            return retValue;
        }

        public static Tuple<double, double, double, double, double, double, double> GetStatistics(List<Transaction> transactions)
        {
            List<Transaction> temp = new List<Transaction>(transactions);
            temp.Sort(new TransactionAmountComparer()); // Amount, least to greatest.

            int count = temp.Count;

            int minIndex = 0;
            int pct01Index = (int)Math.Round((double)count / (double)100 * 1, 0);
            int pct25Index = (int)Math.Round((double)count / (double)100 * 25, 0);
            int pct50Index = (int)Math.Round((double)count / (double)100 * 50, 0);
            int pct75Index = (int)Math.Round((double)count / (double)100 * 75, 0);
            int pct99Index = (int)Math.Round((double)count / (double)100 * 99, 0);
            int maxIndex = count - 1;

            double min = temp[minIndex].Amount;
            double pct01 = temp[pct01Index].Amount;
            double pct25 = temp[pct25Index].Amount;
            double pct50 = temp[pct50Index].Amount;
            double pct75 = temp[pct75Index].Amount;
            double pct99 = temp[pct99Index].Amount;
            double max = temp[maxIndex].Amount;

            Tuple<double, double, double, double, double, double, double> retValue = new Tuple<double, double, double, double, double, double, double>(min, pct01, pct25, pct50, pct75, pct99, max);
            return retValue;
        }
    }
}
