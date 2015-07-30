using System;


namespace TG.CaseStudy
{
    public class PercentileStatisticSet
    {
        public double Max { get; set; }
        public double Pct99 { get; set; }
        public double Pct75 { get; set; }
        public double Pct50 { get; set; }
        public double Pct25 { get; set; }
        public double Pct01 { get; set; }
        public double Min { get; set; }


        public PercentileStatisticSet() { }

        public PercentileStatisticSet(double max, double pct99, double pct75, double pct50, double pct25, double pct01, double min)
        {
            this.Max = max;
            this.Pct99 = pct99;
            this.Pct75 = pct75;
            this.Pct50 = pct50;
            this.Pct25 = pct25;
            this.Pct01 = pct01;
            this.Min = min;
        }
    }
}
