using System;


namespace Duomo.Common.Lib.Excel.Regression
{
    public class CoefficentStatisticRow
    {
        public double Coefficient { get; set; }
        public double StandardError { get; set; }
        public double TStatistic { get; set; }
        public double PValue { get; set; }
        public double Lower95Pct { get; set; }
        public double Upper95Pct { get; set; }


        public CoefficentStatisticRow() { }

        public CoefficentStatisticRow(double coefficient, double standardError, double tStatistic, double pValue, double lower95Pct, double upper95Pct)
        {
            this.Coefficient = coefficient;
            this.StandardError = standardError;
            this.TStatistic = tStatistic;
            this.PValue = pValue;
            this.Lower95Pct = lower95Pct;
            this.Upper95Pct = upper95Pct;
        }
    }
}
