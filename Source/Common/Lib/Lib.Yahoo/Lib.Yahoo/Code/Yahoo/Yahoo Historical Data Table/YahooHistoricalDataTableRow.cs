using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Yahoo
{
    [Serializable]
    public class YahooHistoricalDataTableRow
    {
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public long Volume { get; set; }
        public double AdjustedClose { get; set; }


        public void FillFromTokens(List<string> tokens)
        {
            int counter = 0;
            try
            {
                this.Date = Convert.ToDateTime(tokens[counter]);
                counter++;
                this.Open = Convert.ToDouble(tokens[counter]);
                counter++;
                this.High = Convert.ToDouble(tokens[counter]);
                counter++;
                this.Low = Convert.ToDouble(tokens[counter]);
                counter++;
                this.Close = Convert.ToDouble(tokens[counter]);
                counter++;
                this.Volume = Convert.ToInt64(tokens[counter]);
                counter++;
                this.AdjustedClose = Convert.ToDouble(tokens[counter]);
                counter++;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(String.Format("Problem with token {0}: '{1}'.", counter, tokens[counter]), ex);
            }
        }

        public override string ToString()
        {
            string retValue = String.Format("{0,8:yyyyMMdd},{1,7:0.00},{2,7:0.00},{3,7:0.00},{4,7:0.00},{5,10},{6,7:0.00}",
                this.Date,
                this.Open,
                this.High,
                this.Low,
                this.Close,
                this.Volume,
                this.AdjustedClose);

            return retValue;
        }
    }
}
