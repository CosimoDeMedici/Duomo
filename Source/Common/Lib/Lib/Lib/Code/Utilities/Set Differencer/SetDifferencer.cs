using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib
{
    public static class SetDifferencer<T>
    {
        public static SetDifference<T> Calculate(List<T> set1, List<T> set2)
        {
            SetDifference<T> retValue = SetDifferencer<T>.Calculate(@"Set1", set1, @"Set2", set2);
            return retValue;
        }

        public static SetDifference<T> Calculate(string set1Name, List<T> set1, string set2Name, List<T> set2)
        {
            SetDifference<T> retValue = new SetDifference<T>(set1Name, set2Name);

            HashSet<T> set1Hashes = new HashSet<T>(set1);
            set1Hashes.ExceptWith(set2);
            retValue.Set1Only.AddRange(set1Hashes);

            HashSet<T> set2Hashes = new HashSet<T>(set2);
            set2Hashes.ExceptWith(set1);
            retValue.Set2Only.AddRange(set2Hashes);

            return retValue;
        }
    }
}
