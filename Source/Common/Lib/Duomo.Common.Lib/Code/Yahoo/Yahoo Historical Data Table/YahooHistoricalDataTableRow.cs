using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Yahoo
{
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
    }
}
