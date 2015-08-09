using System;
using System.IO;


namespace Duomo.Common.Lib
{
    public class SetDifferenceTextSerializer<T>
    {
        #region Static

        public static void SerializeToRootedPath(SetDifference<T> difference, string fileRootedPath)
        {
            using (StreamWriter writer = new StreamWriter(fileRootedPath))
            {
                writer.WriteLine(String.Format(@"Only in '{0}':", difference.Set1Name));
                writer.WriteLine();

                foreach (T value in difference.Set1Only)
                {
                    writer.WriteLine(value.ToString());
                }

                writer.WriteLine(String.Format(@"Only in '{0}':", difference.Set2Name));
                writer.WriteLine();

                foreach (T value in difference.Set2Only)
                {
                    writer.WriteLine(value.ToString());
                }
            }
        }

        #endregion
    }
}
