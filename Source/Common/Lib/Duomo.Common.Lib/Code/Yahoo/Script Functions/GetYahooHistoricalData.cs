using System;


namespace Duomo.Common.Lib.Yahoo
{
    public static class GetYahooHistoricalData
    {
        public static YahooHistoricalDataTable Run(string ticker, DateTime startDate, DateTime endDate, Periodicity periodicity)
        {
            DataRequest request = new DataRequest();
            request.Ticker = "GM";
            request.StartDate = DateTime.Parse("11-18-2010");
            request.EndDate = DateTime.Parse("06-06-2014");
            request.Periodicity = Periodicity.Daily;

            string url = request.ToURL();

            string csvString = WebDataRequester.GetCsvResponse(url);

            YahooHistoricalDataTable retValue = new YahooHistoricalDataTable();
            retValue.ParseCsvString(csvString);

            return retValue;
        }
    }
}
