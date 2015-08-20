using System;

using Duomo.Common.Lib;


namespace Duomo.Common.Lib.Excel.Regression
{
    public class AnovaStatistics
    {
        public AnovaRow Regression { get; set; }
        public AnovaRow Residual { get; set; }
        public AnovaRow Total { get; set; }
        public AnovaRow this[AnovaRowType type]
        {
            get
            {
                AnovaRow retValue;
                switch (type)
                {
                    case AnovaRowType.Regression:
                        retValue = this.Regression;
                        break;

                    case AnovaRowType.Residual:
                        retValue = this.Residual;
                        break;

                    case AnovaRowType.Total:
                        retValue = this.Total;
                        break;

                    default:
                        throw new EnumerationValueUnhandledException(type);
                }

                return retValue;
            }
            set
            {
                switch (type)
                {
                    case AnovaRowType.Regression:
                        this.Regression = value;
                        break;

                    case AnovaRowType.Residual:
                        this.Residual = value;
                        break;

                    case AnovaRowType.Total:
                        this.Total = value;
                        break;

                    default:
                        throw new EnumerationValueUnhandledException(type);
                }
            }
        }


        public AnovaStatistics()
        {
            this.Regression = new AnovaRow(AnovaRowType.Regression);
            this.Residual = new AnovaRow(AnovaRowType.Residual);
            this.Total = new AnovaRow(AnovaRowType.Total);
        }

        public AnovaStatistics(AnovaRow regression, AnovaRow residual, AnovaRow total)
        {
            this.Regression = regression;
            this.Residual = residual;
            this.Total = total;
        }
    }
}
