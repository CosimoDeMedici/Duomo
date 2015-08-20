using System;


namespace Duomo.Common.Lib.Excel.Regression
{
    public class CoefficentStatistics
    {
        public CoefficentStatisticRow Intercept { get; set; }
        public CoefficentStatisticRow XVariable { get; set; }


        public CoefficentStatistics() { }

        public CoefficentStatistics(CoefficentStatisticRow intercept, CoefficentStatisticRow xVariable)
        {
            this.Intercept = intercept;
            this.XVariable = xVariable;
        }
    }
}
