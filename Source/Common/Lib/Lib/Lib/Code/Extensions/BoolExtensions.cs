using System;


namespace Duomo.Common.Lib
{
    public static class BoolExtensions
    {
        public static string ToY_N(this bool value)
        {
            string retValue = value.ToY_N(true);
            return retValue;
        }

        public static string ToY_N(this bool value, bool capitalize)
        {
            string retValue;
            if (value)
            {
                retValue = @"y";
            }
            else
            {
                retValue = @"n";
            }

            if (capitalize)
            {
                retValue = retValue.ToUpperInvariant();
            }

            return retValue;
        }
    }
}
