using System;


namespace Duomo.Common.Lib.Yahoo
{
    public static class GetYahooHistoricalData
    {
        public static YahooHistoricalDataTable Run(string ticker, DateTime startDate, DateTime endDate, Periodicity periodicity)
        {
            DataRequest request = new DataRequest();
            request.Ticker = ticker;
            request.StartDate = startDate;
            request.EndDate = endDate;
            request.Periodicity = periodicity;

            string url = request.ToURL();

            string csvString = WebDataRequester.GetStringResponse(url);

            YahooHistoricalDataTable retValue = new YahooHistoricalDataTable();
            retValue.ParseCsvString(ticker, csvString);

            return retValue;
        }
    }
}
