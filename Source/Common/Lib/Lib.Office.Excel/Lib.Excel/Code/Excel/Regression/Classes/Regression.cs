using System;

using XL = Microsoft.Office.Interop.Excel;


namespace Duomo.Common.Lib.Excel.Regression
{
    public class Regression
    {
        #region Static

        public static Regression Run(Excel app, Workbook wkbk, Range xRng, Range yRng)
        {
            Worksheet outWs = wkbk.GetNewWorksheet();
            Range outRng = outWs.GetA1Range();

            XL.Workbook atvbaen = app.XlApplication.Workbooks.Open(@"C:\Program Files\Microsoft Office 15\root\office15\Library\Analysis\ATPVBAEN.XLAM");
            atvbaen.RunAutoMacros(XL.XlRunAutoMacro.xlAutoOpen);

            app.XlApplication.Run(@"Regress", yRng.XlRange, xRng.XlRange, Missing.Value, Missing.Value, Missing.Value, outRng.XlRange);

            Range usedRng = outWs.GetUsedRange();

            object[,] values = usedRng.Values;

            Regression retValue = Regression.ParseValues(values);
            return retValue;
        }

        private static Regression ParseValues(object[,] values)
        {
            Regression retValue = new Regression();

            retValue.Statistics = Regression.ParseStatistics(values);
            retValue.Anova = Regression.ParseAnovaStatistics(values);
            retValue.Coefficients = Regression.ParseCoefficientStatistics(values);

            return retValue;
        }

        private static Statistics ParseStatistics(object[,] values)
        {
            Statistics retValue = new Statistics();
            retValue.MultipleR = Convert.ToDouble(values[4, 2]);
            retValue.RSquare = Convert.ToDouble(values[5, 2]);
            retValue.AdjustedRSquare = Convert.ToDouble(values[6, 2]);
            retValue.StandardError = Convert.ToDouble(values[7, 2]);
            retValue.Observations = Convert.ToInt32(values[8, 2]);

            return retValue;
        }

        public static AnovaStatistics ParseAnovaStatistics(object[,] values)
        {
            AnovaStatistics retValue = new AnovaStatistics();

            object[,] anovaValues = Regression.GetAnovaValues(values);

            AnovaRowType[] anovaRowTypes = AnovaRow.GetAnovaRowTypes();

            int counter = 0;
            foreach (AnovaRowType rowType in anovaRowTypes)
            {
                AnovaRow row = new AnovaRow(rowType);
                retValue[row.Type] = row;

                row.DegreesOfFreedom = Convert.ToInt32(anovaValues[counter, 1]);
                row.SumOfSquares = Convert.ToDouble(anovaValues[counter, 2]);
                row.MeanSquare = Convert.ToDouble(anovaValues[counter, 3]);
                row.F = Convert.ToDouble(anovaValues[counter, 4]);
                row.FSignificance = Convert.ToDouble(anovaValues[counter, 5]);

                counter++;
            }

            return retValue;
        }

        private static CoefficentStatistics ParseCoefficientStatistics(object[,] values)
        {
            CoefficentStatistics retValue = new CoefficentStatistics();

            object[,] coefficientValues = Regression.GetCoefficientValues(values);

            CoefficentStatisticRow[] rows = new CoefficentStatisticRow[]
            {
                new CoefficentStatisticRow(),
                new CoefficentStatisticRow()
            };

            int numRows = 2;
            for (int iRow = 0; iRow < numRows; iRow++)
            {
                CoefficentStatisticRow curRow = rows[iRow];

                curRow.Coefficient = Convert.ToDouble(coefficientValues[iRow, 1]);
                curRow.StandardError = Convert.ToDouble(coefficientValues[iRow, 2]);
                curRow.TStatistic = Convert.ToDouble(coefficientValues[iRow, 3]);
                curRow.PValue = Convert.ToDouble(coefficientValues[iRow, 4]);
                curRow.Lower95Pct = Convert.ToDouble(coefficientValues[iRow, 5]);
                curRow.Upper95Pct = Convert.ToDouble(coefficientValues[iRow, 6]);
            }

            retValue.Intercept = rows[0];
            retValue.XVariable = rows[1];

            return retValue;
        }

        private static object[,] GetAnovaValues(object[,] values)
        {
            int numRows = 3;
            int numCols = 6;
            int rowOffset = 11;

            object[,] retValue = new object[numRows, numCols];

            for (int iRow = 0; iRow < numRows; iRow++)
            {
                for (int iCol = 0; iCol < numCols; iCol++)
                {
                    retValue[iRow, iCol] = values[iRow + rowOffset + 1, iCol + 1];
                }
            }

            return retValue;
        }

        private static object[,] GetCoefficientValues(object[,] values)
        {
            int numRows = 2;
            int numCols = 7;
            int rowOffset = 16;

            object[,] retValue = new object[numRows, numCols];

            for (int iRow = 0; iRow < numRows; iRow++)
            {
                for (int iCol = 0; iCol < numCols; iCol++)
                {
                    retValue[iRow, iCol] = values[iRow + rowOffset + 1, iCol + 1];
                }
            }

            return retValue;
        }

        #endregion


        public Statistics Statistics { get; set; }
        public AnovaStatistics Anova { get; set; }
        public CoefficentStatistics Coefficients { get; set; }


        public Regression() { }

        public Regression(Statistics statistics, AnovaStatistics anova, CoefficentStatistics coefficients)
        {
            this.Statistics = statistics;
            this.Anova = anova;
            this.Coefficients = coefficients;
        }
    }
}
