using System;


namespace Duomo.Common.Lib.Excel.Regression
{
    public enum AnovaRowType
    {
        Regression,
        Residual,
        Total
    }


    public class AnovaRow
    {
        #region Static

        public static AnovaRowType[] GetAnovaRowTypes()
        {
            AnovaRowType[] retValue = new AnovaRowType[3]
            {
                AnovaRowType.Regression,
                AnovaRowType.Residual,
                AnovaRowType.Total
            };

            return retValue;
        }

        #endregion


        public AnovaRowType Type { get; set; }
        public int DegreesOfFreedom { get; set; }
        public double SumOfSquares { get; set; } // TODO ?
        public double MeanSquare { get; set; } // TODO ?
        public double F { get; set; } // TODO ?
        public double FSignificance { get; set; }


        public AnovaRow() { }

        public AnovaRow(AnovaRowType type)
        {
            this.Type = type;
        }
    }
}
