using System;


namespace Duomo.Common.Gunther.Lib
{
    public class DaysOfWeekGrid
    {
        #region Static

        public const char NOT_THIS_DAY = '_';


        public static int IndexOfDayOfWeek(DayOfWeek dayOfWeek)
        {
            int retValue = -1;
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    retValue = 0;
                    break;

                case DayOfWeek.Tuesday:
                    retValue = 1;
                    break;

                case DayOfWeek.Wednesday:
                    retValue = 2;
                    break;

                case DayOfWeek.Thursday:
                    retValue = 3;
                    break;

                case DayOfWeek.Friday:
                    retValue = 4;
                    break;

                case DayOfWeek.Saturday:
                    retValue = 5;
                    break;

                case DayOfWeek.Sunday:
                    retValue = 6;
                    break;
            }

            return retValue;
        }

        public static bool TrueOnDayOfWeek(DayOfWeek dayOfWeek, string daysOfWeekGrid)
        {
            int index = DaysOfWeekGrid.IndexOfDayOfWeek(dayOfWeek);

            bool retValue = DaysOfWeekGrid.NOT_THIS_DAY != daysOfWeekGrid[index];
            return retValue;
        }

        #endregion
    }
}
