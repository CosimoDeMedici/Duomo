using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Duomo.Common.Lib.IO.Serialization;


namespace TG.CaseStudy
{
    public static class Construction
    {
        public static void SubMain()
        {
            //Construction.DetermineRetainedCustomerCounts();
            //Construction.CreateHistogramOfCustomerTransactionCounts();
            //Construction.DeteremineAllStateTransitions();
            //Construction.DetermineCustomersSpanningAllMonths();
            //Construction.TransactionNumberAndAmountByDateAndBusiness();
            //Construction.CalculateAverage();
            
            //Construction.RemoveOutlierTransactions();
            //Construction.GetCountByDayFinal();
            //Construction.ReDateData();
            Construction.HowManyCustomerIDsCrossBusinesses();
            //Construction.GetCountByDay();
            //Construction.RecoverDayInformationToDate();
            //Construction.DetermineBlocksWithinDates();
            //Construction.DetermineNForEachDate();
            //Construction.DetermineAverageTransactionSizes();
            //Construction.DetermineAmountPercentileStatisticsByBusiness();
            //Construction.DetermineAmountForEachDateForEachBusiness();
            //Construction.DetermineNForEachDateForEachBusiness();

            //Construction.ConvertCsvToTransactionsBinary();

            //Construction.VerifyBusinessRepetition();
            //Construction.SerializeRedatedTransactions();
            //Construction.DeserializeTransactions();
            //Construction.SerializeTransactions();
            //Construction.DetermineAnySubPennyAmounts();
            //Construction.ExamineTransactionAmounts();
            //Construction.TryParseAllTransactionAmounts();
            //Construction.CustomerIDFrequencies2();
            //Construction.CustomerIDFrequencies();
            //Construction.TryParseAllCustomerIDs();
            //Construction.GetAllCustomerIDs();
            //Construction.GetAllBusinessNames();
            //Construction.TryParseAllDates();
            //Construction.DetermineContentsOfColumns5And6();
            //Construction.CountCommas();
            //Construction.DetermineCharacterNumbers();
            //Construction.DetermineLineCount();
        }

        public static void DetermineRetainedCustomerCounts()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<DateTime> dates = TransactionDate.GetAllActualDatesInOrder(); // Oldest to newest.
            List<string> businessNames = Business.GetAllBusinessNames();

            Dictionary<DateTime, Dictionary<string, HashSet<int>>> customerIDsByBusinessAndMonth = new Dictionary<DateTime, Dictionary<string, HashSet<int>>>();
            foreach (DateTime month in dates)
            {
                List<Transaction> monthTransactions = Queries.GetTransactionsForYearAndMonthOfDate(allTransactions, month);

                Dictionary<string, HashSet<int>> customerIDsByBusiness = new Dictionary<string, HashSet<int>>();
                customerIDsByBusinessAndMonth.Add(month, customerIDsByBusiness);

                foreach (string business in businessNames)
                {
                    List<Transaction> businessTransactions = Queries.GetTransactionsForBusiness(monthTransactions, business);

                    List<int> customerIDs = Queries.GetCustomerIDs(businessTransactions);
                    
                    HashSet<int> customerIdHash = new HashSet<int>(customerIDs);
                    customerIDsByBusiness.Add(business, customerIdHash);
                }
            }

            Dictionary<DateTime, Dictionary<string, int>> retainedCustomerIDsByBusinessAndMonth = new Dictionary<DateTime, Dictionary<string, int>>();

            dates.Reverse(); // Newest to oldest.

            int newestMonthIndex = 0;
            DateTime newestMonth = dates[newestMonthIndex];
            Dictionary<string, HashSet<int>> continousCustomerIDsByBusiness = customerIDsByBusinessAndMonth[newestMonth];

            for (int iDate = newestMonthIndex; iDate < dates.Count; iDate++)
            {
                DateTime priorMonth = dates[iDate];
                Dictionary<string, HashSet<int>> priorMonthCustomerIDsByBusiness = customerIDsByBusinessAndMonth[priorMonth];

                Dictionary<string, int> retainedCustomerIDsByBusiness = new Dictionary<string, int>();
                retainedCustomerIDsByBusinessAndMonth.Add(priorMonth, retainedCustomerIDsByBusiness);

                foreach (string businessName in businessNames)
                {
                    HashSet<int> continuousCustomerIDs = continousCustomerIDsByBusiness[businessName];
                    HashSet<int> priorMonthCustomerIDs = priorMonthCustomerIDsByBusiness[businessName];

                    List<int> customerIDsToRemove = new List<int>();
                    foreach (int newestMonthCustomerID in continuousCustomerIDs)
                    {
                        if (!priorMonthCustomerIDs.Contains(newestMonthCustomerID))
                        {
                            customerIDsToRemove.Add(newestMonthCustomerID);
                        }
                    }

                    foreach (int customerIDToRemove in customerIDsToRemove)
                    {
                        continuousCustomerIDs.Remove(customerIDToRemove);
                    }

                    int count = continuousCustomerIDs.Count;
                    retainedCustomerIDsByBusiness.Add(businessName, count);
                }
            }

            List<string> outputLines = new List<string>();

            StringBuilder builder = new StringBuilder();
            foreach (string businessName in businessNames)
            {
                builder.Append(String.Format(@",{0}", businessName));
            }

            string headerRow = builder.ToString();
            outputLines.Add(headerRow);

            foreach (DateTime month in retainedCustomerIDsByBusinessAndMonth.Keys)
            {
                Dictionary<string, int> retainedCustomerIDsByBusiness = retainedCustomerIDsByBusinessAndMonth[month];

                builder.Clear();
                builder.Append(String.Format(@"{0:yyyy-MM-dd}", month));
                foreach (string businessName in businessNames)
                {
                    int count = retainedCustomerIDsByBusiness[businessName];

                    builder.Append(String.Format(@",{0}", count));
                }

                string line = builder.ToString();
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Retained Customer Counts.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void CreateHistogramOfCustomerTransactionCounts()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<string> businessNames = Business.GetAllBusinessNames();

            Dictionary<string, Dictionary<int, int>> customerCountsByTransactionNumberAndBusinessName = new Dictionary<string, Dictionary<int, int>>();
            foreach (string businessName in businessNames)
            {
                List<Transaction> transactions = Queries.GetTransactionsForBusiness(allTransactions, businessName);

                Dictionary<int, int> countsForBusiness = new Dictionary<int, int>();
                customerCountsByTransactionNumberAndBusinessName.Add(businessName, countsForBusiness);

                Dictionary<int, List<Transaction>> transactionsByCustomerID = Queries.TransformToTransactionsByCustomerID(transactions);
                foreach (int customerID in transactionsByCustomerID.Keys)
                {
                    List<Transaction> transactionsForCustomerID = transactionsByCustomerID[customerID];

                    int count = transactionsForCustomerID.Count;
                    if (countsForBusiness.ContainsKey(count))
                    {
                        countsForBusiness[count]++;
                    }
                    else
                    {
                        countsForBusiness.Add(count, 1);
                    }
                }
            }

            List<int> countsPresent = Queries.GetAllCounts(customerCountsByTransactionNumberAndBusinessName);
            List<int> allCounts = new List<int>();
            int minCount = countsPresent[0];
            int maxCount = countsPresent[countsPresent.Count - 1];
            for (int i = minCount; i <= maxCount; i++)
            {
                allCounts.Add(i);
            }

            List<string> outputLines = new List<string>();

            string headerLine = String.Format(@",{0},{1},{2}", businessNames[0], businessNames[1], businessNames[2]);
            outputLines.Add(headerLine);

            Dictionary<int, StringBuilder> builders = new Dictionary<int,StringBuilder>();
            foreach(int count in allCounts)
            {
                builders.Add(count, new StringBuilder(String.Format(@"{0}", count)));
            }

            foreach (string businessName in businessNames)
            {
                Dictionary<int, int> countsForBusiness = customerCountsByTransactionNumberAndBusinessName[businessName];

                foreach (int count in allCounts)
                {
                    int countForBusiness = 0;
                    if (countsForBusiness.ContainsKey(count))
                    {
                        countForBusiness = countsForBusiness[count];
                    }

                    StringBuilder builder = builders[count];
                    builder.Append(String.Format(@",{0}", countForBusiness));
                }
            }

            foreach (int count in builders.Keys)
            {
                StringBuilder builder = builders[count];

                string line = builder.ToString();
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Histogram of Counts.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void DetermineAverageTransactionSizes()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<string> businessNames = Business.GetAllBusinessNames();

            Dictionary<string, double> averages = new Dictionary<string, double>();
            foreach (string businessName in businessNames)
            {
                List<Transaction> transactions = Queries.GetTransactionsForBusiness(allTransactions, businessName);

                double average = Queries.Average(transactions);
                averages.Add(businessName, average);
            }
        }

        public static void DetermineAmountPercentileStatisticsByBusiness()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<string> businessNames = Business.GetAllBusinessNames();

            Dictionary<string, Tuple<double, double, double, double, double, double, double>> data = new Dictionary<string, Tuple<double, double, double, double, double, double, double>>();
            foreach (string businessName in businessNames)
            {
                List<Transaction> businessTransactions = Queries.GetTransactionsForBusiness(allTransactions, businessName);

                Tuple<double, double, double, double, double, double, double> stats = Queries.GetStatistics(businessTransactions);
                data.Add(businessName, stats);
            }

            List<string> outputLines = new List<string>();

            string headerLine = @"Business Name,Min,Pct01,Pct25,Pct50,Pct75,Pct99,Max";
            outputLines.Add(headerLine);

            foreach (string businessName in data.Keys)
            {
                Tuple<double, double, double, double, double, double, double> stats = data[businessName];

                string line = String.Format(@"{0},{1},{2},{3},{4},{5},{6},{7}", businessName, stats.Item1, stats.Item2, stats.Item3, stats.Item4, stats.Item5, stats.Item6, stats.Item7);
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Business Amount Stats.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void DetermineTransactionsByCustomerIDsWithTwoTransactions()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<int> customerIDs = Queries.GetCustomerIDs(allTransactions);

            Dictionary<int, int> transactionCountsByCustomerIDs = new Dictionary<int, int>();
            //foreach(
        }

        public static void DetermineAllStateTransitions()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<int> customerIDs = Queries.GetCustomerIDs(allTransactions);

            int[,] transitions = new int[Business.NUMBER_OF_BUSINESSES, Business.NUMBER_OF_BUSINESSES]; // Rows are start, columns are end.

            List<string> businessNames = Business.GetAllBusinessNames();
            Dictionary<string, int> matrixIndexByBusinessName = new Dictionary<string, int>();
            int businessCount = 0;
            foreach (string businessName in businessNames)
            {
                matrixIndexByBusinessName.Add(businessName, businessCount);

                businessCount++;
            }

            int count = 0;
            DateTime now = DateTime.Now;

            Dictionary<int, List<Transaction>> transactionsByCustomerID = Queries.TransformToTransactionsByCustomerID(allTransactions);

            TransactionDateComparer dateComparer = new TransactionDateComparer();
            foreach (int customerID in customerIDs)
            {
                List<Transaction> customerTransactions = transactionsByCustomerID[customerID];

                if (1 < customerTransactions.Count)
                {
                    customerTransactions.Sort(dateComparer); // Oldest to newest.

                    for (int iTransaction = 1; iTransaction < customerTransactions.Count; iTransaction++) // Start at 1.
                    {
                        Transaction priorTransaction = customerTransactions[iTransaction - 1];
                        Transaction curTransaction = customerTransactions[iTransaction];

                        string priorBusinessName = priorTransaction.BusinessName;
                        string curBusinessName = curTransaction.BusinessName;

                        if (priorBusinessName != curBusinessName)
                        {
                            int priorBusinessIndex = matrixIndexByBusinessName[priorBusinessName];
                            int curBusinessIndex = matrixIndexByBusinessName[curBusinessName];

                            transitions[priorBusinessIndex, curBusinessIndex]++;
                        }
                    }
                }

                count++;
            }
            TimeSpan elapsedTime = DateTime.Now - now;

            List<string> outputLines = new List<string>();

            StringBuilder builder = new StringBuilder();

            foreach (string businessName in businessNames)
            {
                builder.Append(String.Format(@",{0}", businessName));
            }
            string headerRow = builder.ToString();
            outputLines.Add(headerRow);

            foreach (string sourceBusinessName in businessNames)
            {
                builder.Clear();

                builder.Append(sourceBusinessName);

                int sourceBusinessIndex = matrixIndexByBusinessName[sourceBusinessName];
                foreach (string destinationBusinessName in businessNames)
                {
                    int destinationBusinessIndex = matrixIndexByBusinessName[destinationBusinessName];

                    int transitionCount = transitions[sourceBusinessIndex, destinationBusinessIndex];
                    builder.Append(String.Format(@",{0}", transitionCount));
                }

                string line = builder.ToString();
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\All State Transitions.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void DetermineCustomersSpanningAllMonths()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<int> customerIDs = Queries.GetCustomerIDs(allTransactions);

            List<int> customerIDsSpanningAllMonths = new List<int>();
            int count = 0;
            DateTime now = DateTime.Now;

            Dictionary<int, List<Transaction>> transactionsByCustomerID = Queries.TransformToTransactionsByCustomerID(allTransactions);

            foreach (int customerID in customerIDs)
            {
                List<Transaction> customerTransactions = transactionsByCustomerID[customerID];

                Tuple<DateTime, DateTime> minMaxDates = Queries.GetMinMaxDates(customerTransactions);

                int octoberMonthIndex = 10;
                int marchMonthIndex = 3;
                if (octoberMonthIndex == minMaxDates.Item1.Month && marchMonthIndex == minMaxDates.Item2.Month)
                {
                    customerIDsSpanningAllMonths.Add(customerID);
                }

                count++;
            }
            TimeSpan elapsedTime = DateTime.Now - now;

            List<string> outputLines = new List<string>();
            foreach (int customerID in customerIDsSpanningAllMonths)
            {
                string line = customerID.ToString();

                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Customer IDs Spanning All Months.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        #region Transaction Count and Amount

        public static void TransactionNumberAndAmountByDateAndBusiness()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            Tuple<DateTime, DateTime> minMaxDate = Queries.GetMinMaxDates(allTransactions);

            List<DateTime> dates = new List<DateTime>();
            DateTime curDate = minMaxDate.Item1;
            while (curDate <= minMaxDate.Item2)
            {
                dates.Add(curDate);

                curDate = curDate.AddDays(1);
            }

            List<string> businessNames = Business.GetAllBusinessNames();
            businessNames.Sort(); // Alphabetical.

            Dictionary<DateTime, Dictionary<string, Tuple<int, double>>> data = new Dictionary<DateTime, Dictionary<string, Tuple<int, double>>>();
            foreach (DateTime date in dates)
            {
                Dictionary<string, Tuple<int, double>> dateData = new Dictionary<string, Tuple<int, double>>();
                data.Add(date, dateData);

                foreach (string businessName in businessNames)
                {
                    List<Transaction> transactions = Queries.GetTransactionsForDateAndBusiness(allTransactions, date, businessName);

                    int count = Queries.Count(transactions);
                    double sum = Queries.Sum(transactions);

                    Tuple<int, double> valueTuple = new Tuple<int, double>(count, sum);
                    dateData.Add(businessName, valueTuple);
                }
            }

            // Now write out data.
            List<string> outputLines = new List<string>();

            StringBuilder builder = new StringBuilder();

            builder.Append(@"Date");
            foreach (string businessName in businessNames)
            {
                builder.Append(String.Format(@",{0} Count,{0} Sum", businessName));
            }
            
            string headerRow = builder.ToString();
            outputLines.Add(headerRow);

            foreach (DateTime date in data.Keys)
            {
                builder.Clear();
                builder.Append(String.Format(@"{0:yyyy-MM-dd}", date));

                Dictionary<string, Tuple<int, double>> dateData = data[date];
                foreach (string businessName in dateData.Keys)
                {
                    Tuple<int, double> valueTuple = dateData[businessName];

                    builder.Append(String.Format(@",{0},{1}", valueTuple.Item1, valueTuple.Item2));
                }

                string line = builder.ToString();
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Counts and Sums by Date and Business.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void CalculateAverage()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            double average = Queries.Average(allTransactions);

            double stdev = Queries.StandardDeviation(allTransactions);
        }

        #endregion

        #region Re-Dating and Cleaning

        public static void RemoveOutlierTransactions()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.RedatedTransactionsBinary);
            List<Transaction> outlierTransactions = Construction.GetTransactions(FileRootedPaths.OutlierTransactions);

            List<Transaction> cleanedTransactions = new List<Transaction>();
            foreach (Transaction transaction in allTransactions)
            {
                bool clean = true;
                foreach (Transaction outlierTransaction in outlierTransactions)
                {
                    if (Transaction.TestEquality(transaction, outlierTransaction))
                    {
                        clean = false;
                        break;
                    }
                }

                if (clean)
                {
                    cleanedTransactions.Add(transaction);
                }
            }

            string cleanedTransactionsBinaryFileRootedPath = FileRootedPaths.CleanedTransactionsBinary;
            Construction.SerializeTransactions(cleanedTransactions, cleanedTransactionsBinaryFileRootedPath);

            string cleanedTransactionsCsvFileRootedPath = FileRootedPaths.CleanedData;
            Construction.SerializeTransactionsToCsv(cleanedTransactions, cleanedTransactionsCsvFileRootedPath);
        }

        public static void ReDateData()
        {
            List<DateTime> dates = Construction.GetDatesList();

            string redatedBinaryFileRootedPath = FileRootedPaths.RedatedDraftTransactionsBinary;
            List<Transaction> allDraftTransactions = Construction.DeserializeTransactions(redatedBinaryFileRootedPath);

            List<Transaction> allFinalTransactions = new List<Transaction>();

            DateTime priorDate = allDraftTransactions[0].Date;
            int finalDateCount = 0;
            foreach (Transaction curDraftTransaction in allDraftTransactions)
            {
                Transaction curFinalTransaction = new Transaction(curDraftTransaction);
                allFinalTransactions.Add(curFinalTransaction);

                if (curDraftTransaction.Date != priorDate)
                {
                    priorDate = curDraftTransaction.Date;
                    finalDateCount++;
                }

                curFinalTransaction.Date = dates[finalDateCount];
            }

            string redatedTransactionBinaryFileRootedPath = FileRootedPaths.RedatedTransactionsBinary;
            Construction.SerializeTransactions(allFinalTransactions, redatedTransactionBinaryFileRootedPath);

            string redatedTransactionsCsvFileRootedPath = FileRootedPaths.RedatedData;
            Construction.SerializeTransactionsToCsv(allFinalTransactions, redatedTransactionsCsvFileRootedPath);
        }

        public static List<DateTime> GetDatesList()
        {
            List<DateTime> retValue = new List<DateTime>();

            string datesListFileRootedPath = FileRootedPaths.DatesList;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(datesListFileRootedPath);

            foreach (string line in lines)
            {
                DateTime date = DateTime.Parse(line);

                retValue.Add(date);
            }

            return retValue;
        }

        public static void VerifyBusinessRepetition()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<Transaction> allTransactions = Construction.GetTransactions(rawDataFileRootedPath);

            string priorBusssinesName = String.Empty;
            string currentBusinessName = Business.SPROUTS;

            List<int> badCounts = new List<int>();

            int count = 1;
            foreach (Transaction transaction in allTransactions)
            {
                if (transaction.BusinessName != currentBusinessName)
                {
                    switch (currentBusinessName)
                    {
                        case Business.SPROUTS:
                            priorBusssinesName = Business.SPROUTS;
                            currentBusinessName = Business.TRADER_JOES;
                            break;

                        case Business.TRADER_JOES:
                            priorBusssinesName = Business.TRADER_JOES;
                            currentBusinessName = Business.WHOLE_FOODS;
                            break;

                        case Business.WHOLE_FOODS:
                            priorBusssinesName = Business.WHOLE_FOODS;
                            currentBusinessName = Business.SPROUTS;
                            break;
                    }

                    if (transaction.BusinessName != currentBusinessName)
                    {
                        badCounts.Add(count);
                        //throw new InvalidDataException();
                    }
                }

                count++;
            }

            List<string> outLines = new List<string>();
            foreach (int badCount in badCounts)
            {
                string line = badCount.ToString();

                outLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Bad line numbers.csv";
            TextFileSerializer.SerializeToRootedPath(outLines, outputFileRootedPath);
        }

        public static void HowManyCustomerIDsPerBusinesses()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            Dictionary<int, HashSet<string>> listOfBusinessesByCustomerID = new Dictionary<int, HashSet<string>>();
            foreach (Transaction transaction in allTransactions)
            {
                HashSet<string> businesses;
                if (listOfBusinessesByCustomerID.ContainsKey(transaction.CustomerID))
                {
                    businesses = listOfBusinessesByCustomerID[transaction.CustomerID];
                }
                else
                {
                    businesses = new HashSet<string>();
                    listOfBusinessesByCustomerID.Add(transaction.CustomerID, businesses);
                }

                businesses.Add(transaction.BusinessName);
            }
        }

        public static void HowManyCustomerIDsCrossBusinesses()
        {
            string redatedBinaryFileRootedPath = FileRootedPaths.TransactionsBinary;
            List<Transaction> allTransactions = Construction.DeserializeTransactions(redatedBinaryFileRootedPath);

            Dictionary<int, HashSet<string>> listOfBusinessesByCustomerID = new Dictionary<int, HashSet<string>>();
            foreach (Transaction transaction in allTransactions)
            {
                HashSet<string> businesses;
                if (listOfBusinessesByCustomerID.ContainsKey(transaction.CustomerID))
                {
                    businesses = listOfBusinessesByCustomerID[transaction.CustomerID];
                }
                else
                {
                    businesses = new HashSet<string>();
                    listOfBusinessesByCustomerID.Add(transaction.CustomerID, businesses);
                }

                businesses.Add(transaction.BusinessName);
            }

            Dictionary<int, int> businessCountByCustomerID = new Dictionary<int, int>();
            foreach (int customerID in listOfBusinessesByCustomerID.Keys)
            {
                HashSet<string> businesses = listOfBusinessesByCustomerID[customerID];

                int businessCount = businesses.Count;
                businessCountByCustomerID.Add(customerID, businessCount);
            }

            List<string> outputLines = new List<string>();
            foreach (int customerID in businessCountByCustomerID.Keys)
            {
                int businessCount = businessCountByCustomerID[customerID];

                string line = String.Format(@"{0},{1}", customerID, businessCount);
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Business Counts By Customer.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void GetCountByDayFinal()
        {
            string redatedBinaryFileRootedPath = FileRootedPaths.RedatedTransactionsBinary;
            string outputFileRootedPath = @"C:\temp\TG\Case Study\Transactions by Day Final.csv";

            Construction.GetCountByDay(redatedBinaryFileRootedPath, outputFileRootedPath);
        }

        public static void GetCountByDay()
        {
            string redatedBinaryFileRootedPath = FileRootedPaths.RedatedDraftTransactionsBinary;
            string outputFileRootedPath = @"C:\temp\TG\Case Study\Transactions by Day Draft.csv";

            Construction.GetCountByDay(redatedBinaryFileRootedPath, outputFileRootedPath);
        }

        public static void GetCountByDay(string redatedBinaryFileRootedPath, string outputFileRootedPath)
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(redatedBinaryFileRootedPath);

            List<DateTime> dates = Queries.GetDates(allTransactions);
            dates.Sort(); // Oldest to newest.

            Dictionary<DateTime, int> countByDay = new Dictionary<DateTime, int>();
            foreach (DateTime date in dates)
            {
                List<Transaction> transactions = Queries.GetTransactionsForDate(allTransactions, date);

                int count = transactions.Count;

                countByDay.Add(date, count);
            }

            // Now write out values.
            List<string> lines = new List<string>();
            foreach (DateTime date in countByDay.Keys)
            {
                int count = countByDay[date];

                string line = String.Format(@"{0:yyyy-MM-dd},{1}", date, count);
                lines.Add(line);
            }
            
            TextFileSerializer.SerializeToRootedPath(lines, outputFileRootedPath);
        }

        public static void RecoverDayInformationToDate()
        {
            List<Transaction> allInputTransactions = Construction.DeserializeTransactions(FileRootedPaths.TransactionsBinary);
            List<Transaction> allOutputTransactions = new List<Transaction>();

            string transitionBusinessName = Business.WHOLE_FOODS; // Whole foods marks the end of a date.

            DateTime priorMonthDate = allInputTransactions[0].Date;
            DateTime currentDayOfMonth = new DateTime(allInputTransactions[0].Date.Year, allInputTransactions[0].Date.Month, 1); // Assumes data is in order by transaction date. Should be 10/1/2011;
            string priorBusinessName = String.Empty;

            foreach (Transaction transaction in allInputTransactions)
            {
                if (transaction.BusinessName != priorBusinessName && priorBusinessName == transitionBusinessName)
                {
                    if (transaction.Date == priorMonthDate)
                    {
                        currentDayOfMonth = currentDayOfMonth.AddDays(1);
                    }
                    else
                    {
                        currentDayOfMonth = new DateTime(transaction.Date.Year, transaction.Date.Month, 1);
                        priorMonthDate = transaction.Date;
                    }
                }

                priorBusinessName = transaction.BusinessName;

                Transaction newDateTransaction = new Transaction(transaction);
                newDateTransaction.Date = currentDayOfMonth;

                allOutputTransactions.Add(newDateTransaction);
            }

            string outputFileRootedPath = FileRootedPaths.RedatedDraftData;
            Construction.SerializeTransactionsToCsv(allOutputTransactions, outputFileRootedPath);

            string binaryOutputFileRootedPath = FileRootedPaths.RedatedDraftTransactionsBinary;
            Construction.SerializeTransactions(allOutputTransactions, binaryOutputFileRootedPath);
        }

        public static void DetermineBlocksWithinDates()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.TransactionsBinary);

            string transitionBusinessName = Business.WHOLE_FOODS;

            List<DateTime> dates = TransactionDate.GetAllDatesInOrder();

            Dictionary<DateTime, int> numTransitionsByDate = new Dictionary<DateTime,int>();
            foreach (DateTime sampleDate in dates)
            {
                int numTransitions = 0;

                string priorBusinessName = String.Empty;
                foreach (Transaction transaction in allTransactions)
                {
                    if (transaction.Date == sampleDate)
                    {
                        if (transaction.BusinessName != priorBusinessName && transaction.BusinessName == transitionBusinessName)
                        {
                            numTransitions++;
                        }

                        priorBusinessName = transaction.BusinessName;
                    }
                }

                numTransitionsByDate.Add(sampleDate, numTransitions);
            }

            // Now write out data.
            List<string> lines = new List<string>();
            foreach (DateTime date in numTransitionsByDate.Keys)
            {
                int numTransitions = numTransitionsByDate[date];

                string line = String.Format(@"{0:yyyy-MM-dd},{1}", date, numTransitions);
                lines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Transitions by Month.csv";
            TextFileSerializer.SerializeToRootedPath(lines, outputFileRootedPath);
        }

        public static void DetermineNForEachDate()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions();

            List<DateTime> dates = TransactionDate.GetAllDatesInOrder();

            Dictionary<DateTime, int> countsByDate = new Dictionary<DateTime, int>();
            foreach (DateTime date in dates)
            {
                List<Transaction> transactions = Queries.GetTransactionsForDate(allTransactions, date);

                int count = transactions.Count;
                
                countsByDate.Add(date, count);
            }

            List<string> lines = new List<string>();
            foreach (DateTime date in countsByDate.Keys)
            {
                int count = countsByDate[date];

                string line = String.Format(@"{0:yyyy-MM-dd},{1}", date, count);
                lines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\CountsByDate.csv";
            TextFileSerializer.SerializeToRootedPath(lines, outputFileRootedPath);
        }

        public static void DetermineAmountForEachDateForEachBusiness()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<DateTime> dates = TransactionDate.GetAllActualDatesInOrder();
            List<string> businessNames = Business.GetAllBusinessNames();

            object[,] counts = new object[dates.Count, businessNames.Count];

            int dateCount = 0;
            foreach (DateTime date in dates)
            {
                DateTime startDate = new DateTime(date.Year, date.Month, 1);
                DateTime endDate = date.AddDays(1);

                List<Transaction> dateRangeTransactions = Queries.GetTransactionsForDateRange(allTransactions, startDate, endDate);

                int businessCount = 0;
                foreach (string businessName in businessNames)
                {
                    List<Transaction> businessTransactions = Queries.GetTransactionsForBusiness(dateRangeTransactions, businessName);

                    double sum = Queries.Sum(businessTransactions);

                    counts[dateCount, businessCount] = sum;

                    businessCount++;
                }

                dateCount++;
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\AmountsByDateAndBusiness.csv";
            Construction.WriteOutData(counts, dates, businessNames, outputFileRootedPath);
        }

        public static void DetermineNForEachDateForEachBusiness()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            List<DateTime> dates = TransactionDate.GetAllActualDatesInOrder();
            List<string> businessNames = Business.GetAllBusinessNames();

            object[,] counts = new object[dates.Count, businessNames.Count];

            int dateCount = 0;
            foreach (DateTime date in dates)
            {
                DateTime startDate = new DateTime(date.Year, date.Month, 1);
                DateTime endDate = date.AddDays(1);

                List<Transaction> dateRangeTransactions = Queries.GetTransactionsForDateRange(allTransactions, startDate, endDate);

                int businessCount = 0;
                foreach (string businessName in businessNames)
                {
                    List<Transaction> transactions = Queries.GetTransactionsForBusiness(dateRangeTransactions, businessName);

                    int count = transactions.Count;

                    counts[dateCount, businessCount] = count;

                    businessCount++;
                }

                dateCount++;
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\CountsByDateAndBusiness.csv";
            Construction.WriteOutData(counts, dates, businessNames, outputFileRootedPath);
        }

        public static void WriteOutData(object[,] data, List<DateTime> dates, List<string> businessNames, string fileRootedPath)
        {
            // Now write out results.
            List<string> lines = new List<string>();

            StringBuilder builder = new StringBuilder();
            foreach (string businessName in businessNames)
            {
                builder.Append(String.Format(@",{0}", businessName));
            }

            string headerRow = builder.ToString();
            lines.Add(headerRow);

            for (int iDate = 0; iDate < data.GetLength(0); iDate++)
            {
                builder.Clear();

                DateTime date = dates[iDate];
                builder.Append(String.Format(@"{0:yyyy-MM-dd}", date));

                for (int iBusiness = 0; iBusiness < data.GetLength(1); iBusiness++)
                {
                    object value = data[iDate, iBusiness];

                    builder.Append(String.Format(@",{0}", value));
                }

                string line = builder.ToString();
                lines.Add(line);
            }

            TextFileSerializer.SerializeToRootedPath(lines, fileRootedPath);
        }

        public static List<Transaction> DeserializeTransactions()
        {
            string transactionsBinaryFileRootedPath = FileRootedPaths.TransactionsBinary;

            List<Transaction> retValue = Construction.DeserializeTransactions(transactionsBinaryFileRootedPath);
            return retValue;
        }

        public static List<Transaction> DeserializeTransactions(string transactionsBinaryFileRootedPath)
        {
            List<Transaction> retValue = BinarySerializer<List<Transaction>>.DeserializatFromRootedPath(transactionsBinaryFileRootedPath);
            return retValue;
        }

        public static void SerializeRedatedTransactions()
        {
            string redatedFileRootedPath = FileRootedPaths.RedatedDraftData;

            List<Transaction> transactions = Construction.GetTransactions(redatedFileRootedPath);

            string redatedBinaryFileRootedPath = FileRootedPaths.RedatedDraftTransactionsBinary;

            BinarySerializer<List<Transaction>>.SerializeToRootedPath(transactions, redatedBinaryFileRootedPath);
        }

        public static void SerializeTransactions(List<Transaction> transactions, string transactionsBinaryFileRootedPath)
        {
            BinarySerializer<List<Transaction>>.SerializeToRootedPath(transactions, transactionsBinaryFileRootedPath);
        }

        public static void SerializeTransactions(List<Transaction> transactions)
        {
            string transactionsBinaryFileRootedPath = FileRootedPaths.TransactionsBinary;

            BinarySerializer<List<Transaction>>.SerializeToRootedPath(transactions, transactionsBinaryFileRootedPath);
        }

        public static void SerializeTransactions(string transactionsBinaryFileRootedPath)
        {
            List<Transaction> transactions = Construction.GetTransactions();

            BinarySerializer<List<Transaction>>.SerializeToRootedPath(transactions, transactionsBinaryFileRootedPath);
        }

        public static void SerializeTransactionsToCsv(List<Transaction> transactions, string fileRootedPath)
        {
            List<string> lines = new List<string>();
            foreach (Transaction transaction in transactions)
            {
                string line = Transaction.FormatCsvRow(transaction);
                lines.Add(line);
            }

            TextFileSerializer.SerializeToRootedPath(lines, fileRootedPath);
        }

        public static List<Transaction> GetTransactions()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;

            List<Transaction> retValue = Construction.GetTransactions(rawDataFileRootedPath);
            return retValue;
        }

        public static List<Transaction> GetTransactions(string rawCsvDataFileRootedPath)
        {
            List<RawDataFileRow> rawData = Construction.GetRawDataRows(rawCsvDataFileRootedPath);

            List<Transaction> retValue = Construction.GetTransactions(rawData);
            return retValue;
        }

        public static List<Transaction> GetTransactions(List<RawDataFileRow> rawData)
        {
            List<Transaction> retValue = new List<Transaction>();

            foreach (RawDataFileRow rawDataRow in rawData)
            {
                DateTime date = DateTime.Parse(rawDataRow.TransactionDate);
                string businessName = rawDataRow.BusinessName;
                int customerID = Int32.Parse(rawDataRow.CustomerID);
                double amount = Double.Parse(rawDataRow.TransactionAmount);

                Transaction transaction = new Transaction(date, businessName, customerID, amount);
                retValue.Add(transaction);
            }

            return retValue;
        }

        public static List<RawDataFileRow> GetRawDataRows()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;

            List<RawDataFileRow> retValue = Construction.GetRawDataRows(rawDataFileRootedPath);
            return retValue;
        }

        public static List<RawDataFileRow> GetRawDataRows(string rawCsvDataFileRootedPath)
        {
            List<RawDataFileRow> retValue = new List<RawDataFileRow>();

            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawCsvDataFileRootedPath);
            lines.RemoveAt(0);

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string transactionDateToken = tokens[RawDataFileRow.TransactionDateColumnIndex].Trim();
                string businessNameToken = tokens[RawDataFileRow.BusinessNameColumnIndex].Trim();
                string customerIDToken = tokens[RawDataFileRow.CustomerIDColumnIndex].Trim();
                string transactionAmountToken = tokens[RawDataFileRow.TransactionAmountColumnIndex].Trim();

                RawDataFileRow rawRow = new RawDataFileRow(transactionDateToken, businessNameToken, customerIDToken, transactionAmountToken);
                retValue.Add(rawRow);
            }

            return retValue;
        }

        #endregion

        #region Exploration

        public static void DetermineAnySubPennyAmounts()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);
            lines.RemoveAt(0);

            List<double> transactionAmounts = new List<double>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string transactionAmountToken = tokens[RawDataFileRow.TransactionAmountColumnIndex].Trim();

                double transactionAmount = Double.Parse(transactionAmountToken);
                transactionAmounts.Add(transactionAmount);
            }

            List<double> subPennyAmounts = new List<double>();
            foreach (double amount in transactionAmounts)
            {
                double roundedAmount = Math.Round(amount, 2);

                if (roundedAmount != amount)
                {
                    subPennyAmounts.Add(amount);
                }
            }
        }

        public static void ExamineTransactionAmounts()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);
            lines.RemoveAt(0);

            List<double> transactionAmounts = new List<double>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string transactionAmountToken = tokens[RawDataFileRow.TransactionAmountColumnIndex].Trim();

                double transactionAmount = Double.Parse(transactionAmountToken);
                transactionAmounts.Add(transactionAmount);
            }

            transactionAmounts.Sort(); // Least to greatest.

            List<string> outputLines = new List<string>();

            double minTransactionAmount = transactionAmounts[0];
            double maxTransactionAmount = transactionAmounts[transactionAmounts.Count - 1];

            string maxMinLine = String.Format(@"Max: {0}, Min: {1}", maxTransactionAmount, minTransactionAmount);
            outputLines.Add(maxMinLine);

            int pct99Index = (int)Math.Round(((double)transactionAmounts.Count) * 99 / 100);
            double pct99Amount = transactionAmounts[pct99Index];

            int pct01Index = (int)Math.Round(((double)transactionAmounts.Count) * 1 / 100);
            double pct01Amount = transactionAmounts[pct01Index];

            string pct99Pct01Line = String.Format(@"Pct99: {0}, Pct01: {1}", pct99Amount, pct01Amount);
            outputLines.Add(pct99Pct01Line);

            int pct75Index = (int)Math.Round(((double)transactionAmounts.Count) * 75 / 100);
            double pct75Amount = transactionAmounts[pct75Index];

            int pct25Index = (int)Math.Round(((double)transactionAmounts.Count) * 25 / 100);
            double pct25Amount = transactionAmounts[pct25Index];

            string pct75Pct25Line = String.Format(@"Pct75: {0}, Pct25: {1}", pct75Amount, pct25Amount);
            outputLines.Add(pct75Pct25Line);

            int pct50Index = (int)Math.Round(((double)transactionAmounts.Count) * 50 / 100);
            double pct50Amount = transactionAmounts[pct50Index];

            string pct50Line = String.Format(@"Pct50: {0}", pct50Amount);
            outputLines.Add(pct50Line);

            string outputFileRootedPath = @"C:\temp\TG\Case Study\Transaction Amounts Summary.txt";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void TryParseAllTransactionAmounts()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            List<double> transactionAmounts = new List<double>();
            List<string> problemLines = new List<string>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string transactionAmountToken = tokens[RawDataFileRow.TransactionAmountColumnIndex].Trim();

                double transactionAmount;
                if (Double.TryParse(transactionAmountToken, out transactionAmount))
                {
                    transactionAmounts.Add(transactionAmount);
                }
                else
                {
                    problemLines.Add(line);
                }
            }

            string transactionAmountProblemLinesOutputFileRootedPath = @"C:\temp\TG\Case Study\Transaction Amount Problem Lines.csv";
            TextFileSerializer.SerializeToRootedPath(problemLines, transactionAmountProblemLinesOutputFileRootedPath);
        }

        public static void CustomerIDFrequencies2()
        {
            List<Transaction> allTransactions = Construction.DeserializeTransactions(FileRootedPaths.CleanedTransactionsBinary);

            Dictionary<int, int> transactionCountsByCustomerID = new Dictionary<int, int>();
            foreach (Transaction transaction in allTransactions)
            {
                if (transactionCountsByCustomerID.ContainsKey(transaction.CustomerID))
                {
                    transactionCountsByCustomerID[transaction.CustomerID]++;
                }
                else
                {
                    transactionCountsByCustomerID.Add(transaction.CustomerID, 1);
                }
            }

            int customerCount = 0;
            int desiredCount = 1;
            foreach (int customerID in transactionCountsByCustomerID.Keys)
            {
                int transactionCount = transactionCountsByCustomerID[customerID];

                if (desiredCount == transactionCount)
                {
                    customerCount++;
                }
            }
        }

        public static void CustomerIDFrequencies()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);
            lines.RemoveAt(0);

            Dictionary<int, int> customerIDFrequencies = new Dictionary<int, int>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string customerIDToken = tokens[RawDataFileRow.CustomerIDColumnIndex].Trim();

                int customerID = Int32.Parse(customerIDToken); // No TryParse required, already checked that all can be parsed to an Int32.

                if (customerIDFrequencies.ContainsKey(customerID))
                {
                    customerIDFrequencies[customerID]++;
                }
                else
                {
                    customerIDFrequencies.Add(customerID, 1);
                }
            }

            List<string> outputLines = new List<string>();
            foreach (int customerID in customerIDFrequencies.Keys)
            {
                int count = customerIDFrequencies[customerID];

                string line = String.Format(@"{0},{1}", customerID, count);
                outputLines.Add(line);
            }

            string dateLinesOutputFileRootedPath = @"C:\temp\TG\Case Study\Customer ID Frequencies.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, dateLinesOutputFileRootedPath);
        }

        public static void TryParseAllCustomerIDs()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            HashSet<int> customerIDs = new HashSet<int>();
            List<string> problemLines = new List<string>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string customerIDToken = tokens[RawDataFileRow.CustomerIDColumnIndex].Trim();

                int customerID;
                if (Int32.TryParse(customerIDToken, out customerID))
                {
                    customerIDs.Add(customerID);
                }
                else
                {
                    problemLines.Add(line);
                }
            }

            string dateProblemLinesOutputFileRootedPath = @"C:\temp\TG\Case Study\Customer ID Problem Lines.csv";
            TextFileSerializer.SerializeToRootedPath(problemLines, dateProblemLinesOutputFileRootedPath);
        }

        public static void GetAllCustomerIDs()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            HashSet<string> customerIDs = new HashSet<string>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string customerID = tokens[RawDataFileRow.CustomerIDColumnIndex].Trim();
                customerIDs.Add(String.Format(@"'{0}", customerID));
            }

            List<string> customerIDLines = new List<string>(customerIDs);
            customerIDLines.Sort(); // Alphabetical.

            string businessNamesOutputFileRootedPath = @"C:\temp\TG\Case Study\All Customer IDs.csv";
            TextFileSerializer.SerializeToRootedPath(customerIDLines, businessNamesOutputFileRootedPath);
        }

        public static void GetAllBusinessNames()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            HashSet<string> businessNames = new HashSet<string>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string businessName = tokens[RawDataFileRow.BusinessNameColumnIndex].Trim();
                businessNames.Add(businessName);
            }

            List<string> businessNameLines = new List<string>(businessNames);

            string businessNamesOutputFileRootedPath = @"C:\temp\TG\Case Study\All Business Names.csv";
            TextFileSerializer.SerializeToRootedPath(businessNameLines, businessNamesOutputFileRootedPath);
        }

        public static void TryParseAllDates()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            HashSet<DateTime> dates = new HashSet<DateTime>();
            List<string> problemLines = new List<string>();

            char[] separators = new char[] { ',' };
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators);

                string transactionDateToken = tokens[RawDataFileRow.TransactionDateColumnIndex].Trim();

                DateTime result;
                if (DateTime.TryParse(transactionDateToken, out result))
                {
                    dates.Add(result);
                }
                else
                {
                    problemLines.Add(line);
                }
            }

            List<string> dateLines = new List<string>();
            foreach (DateTime date in dates)
            {
                string line = String.Format(@"{0:yyyyMMdd}", date);
                dateLines.Add(line);
            }

            string dateLinesOutputFileRootedPath = @"C:\temp\TG\Case Study\All Dates.csv";
            TextFileSerializer.SerializeToRootedPath(dateLines, dateLinesOutputFileRootedPath);

            string dateProblemLinesOutputFileRootedPath = @"C:\temp\TG\Case Study\Date Problem Lines.csv";
            TextFileSerializer.SerializeToRootedPath(problemLines, dateProblemLinesOutputFileRootedPath);
        }

        public static void DetermineContentsOfColumns5And6()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            char[] separators = new char[] { ',' };
            HashSet<string> column5 = new HashSet<string>();
            HashSet<string> column6 = new HashSet<string>();
            foreach (string line in lines)
            {
                string[] tokens = line.Split(separators, StringSplitOptions.None);

                string column5Token = tokens[4].Trim();
                if(String.Empty != column5Token)
                {
                    column5.Add(column5Token);
                }

                string column6Token = tokens[5].Trim();
                if(String.Empty != column6Token)
                {
                    column6.Add(column6Token);
                }
            }

            List<string> column5List = new List<string>(column5);
            List<string> column6List = new List<string>(column6);

            string column5OutputFileRootedPath = @"C:\temp\TG\Case Study\Column5.csv";
            TextFileSerializer.SerializeToRootedPath(column5List, column5OutputFileRootedPath);

            string column6OutputFileRootedPath = @"C:\temp\TG\Case Study\Column6.csv";
            TextFileSerializer.SerializeToRootedPath(column6List, column6OutputFileRootedPath);
        }

        public static void CountCommas()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            Dictionary<int, int> countsOfLinesByCommaCount = new Dictionary<int, int>();
            foreach (string line in lines)
            {
                int numberOfCommasInLine = 0;
                foreach (char character in line)
                {
                    if (',' == character)
                    {
                        numberOfCommasInLine++;
                    }
                }

                if (countsOfLinesByCommaCount.ContainsKey(numberOfCommasInLine))
                {
                    countsOfLinesByCommaCount[numberOfCommasInLine]++;
                }
                else
                {
                    countsOfLinesByCommaCount.Add(numberOfCommasInLine, 1);
                }
            }

            List<string> outputLines = new List<string>();
            foreach (int commaCount in countsOfLinesByCommaCount.Keys)
            {
                int count = countsOfLinesByCommaCount[commaCount];

                string line = String.Format(@"{0},{1}", commaCount, count);
                outputLines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\CountOfLinesByCommaCount.csv";
            TextFileSerializer.SerializeToRootedPath(outputLines, outputFileRootedPath);
        }

        public static void DetermineCharacterNumbers()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;

            string wholeFileContents;
            using (StreamReader reader = new StreamReader(rawDataFileRootedPath))
            {
                wholeFileContents = reader.ReadToEnd();
            }

            Dictionary<char, int> characterCounts = new Dictionary<char, int>();
            foreach (char character in wholeFileContents)
            {
                if (characterCounts.ContainsKey(character))
                {
                    characterCounts[character]++;
                }
                else
                {
                    characterCounts.Add(character, 1);
                }
            }
            
            List<char> characters = new List<char>(characterCounts.Keys);
            characters.Sort(); // Smallest to largest.

            List<string> lines = new List<string>();
            foreach (char character in characters)
            {
                int count = characterCounts[character];

                string line = String.Format(@"{0},{1}", character, count);
                lines.Add(line);
            }

            string outputFileRootedPath = @"C:\temp\TG\Case Study\WholeFileCharacterCount.csv";
            TextFileSerializer.SerializeToRootedPath(lines, outputFileRootedPath);
        }

        /// <summary>
        /// Determines the number of lines in the file.
        /// </summary>
        /// <remarks>99196 lines, with 1 header line.</remarks>
        public static void DetermineLineCount()
        {
            string rawDataFileRootedPath = FileRootedPaths.RawData;
            List<string> lines = TextFileSerializer.DeserializeFromRootedPath(rawDataFileRootedPath);

            int numberOfLines = lines.Count;
        }

        #endregion

        #region File Conversion

        public static void ConvertTransactionsBinaryToCsv()
        {
            string transactionsBinaryFileRootedPath = FileRootedPaths.TransactionsBinary;
            string csvFileRootedPath = FileRootedPaths.RawData;

            Construction.ConvertTransactionsBinaryToCsv(transactionsBinaryFileRootedPath, csvFileRootedPath);
        }

        public static void ConvertTransactionsBinaryToCsv(string transactionsBinaryFileRootedPath, string csvFileRootedPath)
        {
            List<Transaction> transactions = Construction.DeserializeTransactions(transactionsBinaryFileRootedPath);

            Construction.SerializeTransactionsToCsv(transactions, csvFileRootedPath);
        }

        public static void ConvertCsvToTransactionsBinary()
        {
            string csvFileRootedPath = FileRootedPaths.RawData;
            string transactionsBinaryFileRootedPath = FileRootedPaths.TransactionsBinary;

            Construction.ConvertCsvToTransactionsBinary(csvFileRootedPath, transactionsBinaryFileRootedPath);
        }

        public static void ConvertCsvToTransactionsBinary(string csvFileRootedPath, string transactionsBinaryFileRootedPath)
        {
            List<Transaction> transactions = Construction.GetTransactions(csvFileRootedPath);

            Construction.SerializeTransactions(transactions, transactionsBinaryFileRootedPath);
        }

        #endregion
    }
}
