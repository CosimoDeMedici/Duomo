using System;


namespace Duomo.Common.Lib.Excel.Regression
{
    public class Statistics
    {
        public double MultipleR { get; set; }
        public double RSquare { get; set; }
        public double AdjustedRSquare { get; set; }
        public double StandardError { get; set; }
        public int Observations { get; set; }


        public Statistics() { }

        public Statistics(double multipleR, double rSquare, double adjustedRSquare, double standardError, int observations)
        {
            this.MultipleR = multipleR;
            this.RSquare = rSquare;
            this.AdjustedRSquare = adjustedRSquare;
            this.StandardError = standardError;
            this.Observations = observations;
        }
    }
}
