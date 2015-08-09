using System;


namespace Duomo.Common.Lib.Yahoo
{
    public class DataRequest
    {
        #region Static

        private static string PeriodicityToUrlChar(Periodicity periodicity)
        {
            switch (periodicity)
            {
                case Duomo.Common.Lib.Yahoo.Periodicity.Daily:
                    return "d";

                default:
                    throw new ArgumentException("Unrecognized periodicity.", "periodicity");
            }
        }

        #endregion


        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Ticker { get; set; }
        public Periodicity Periodicity { get; set; }


        public string ToURL()
        {
            string httpMaskStr = @"http://{0}";
            string maskStr = @"ichart.finance.yahoo.com/table.csv?s={0}&d={1}&e={2}&f={3}&g={4}&a={5}&b={6}&c={7}&ignore=.csv";

            string periodicityStr = DataRequest.PeriodicityToUrlChar(this.Periodicity);

            string retValue = String.Format(
                httpMaskStr,
                String.Format(
                maskStr,
                Ticker,
                EndDate.Month - 1,
                EndDate.Day,
                EndDate.Year,
                periodicityStr,
                StartDate.Month - 1,
                StartDate.Day,
                StartDate.Year));

            return retValue;
        }
    }
}
