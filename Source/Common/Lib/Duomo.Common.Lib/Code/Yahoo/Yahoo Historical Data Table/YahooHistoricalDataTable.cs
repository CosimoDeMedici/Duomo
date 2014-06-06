using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Yahoo
{
    public class YahooHistoricalDataTable
    {
        public List<YahooHistoricalDataTableRow> Rows { get; protected set; }


        public YahooHistoricalDataTable()
        {
            Rows = new List<YahooHistoricalDataTableRow>();
        }

        public void ParseCsvString(string csvStr)
        {
            string[] lineSeparators = new string[] { "\n" };
            string[] tokenSeparators = new string[] { "," };

            List<string> lines = new List<string>(csvStr.Split(lineSeparators, StringSplitOptions.RemoveEmptyEntries));
            lines.RemoveAt(0); // Remove the header.

            int count = 0;
            foreach (string line in lines)
            {
                List<string> tokens = new List<string>(line.Split(tokenSeparators, StringSplitOptions.None));
                
                YahooHistoricalDataTableRow row = new YahooHistoricalDataTableRow();
                this.Rows.Add(row);

                row.FillFromTokens(tokens);
                
                count++;
            }
        }
    }
}
