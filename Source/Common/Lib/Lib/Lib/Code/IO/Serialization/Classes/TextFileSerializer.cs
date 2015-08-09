using System;
using System.Collections.Generic;
using System.IO;


namespace Duomo.Common.Lib.IO.Serialization
{
    public class TextFileSerializer
    {
        #region Static

        public static void SerializeToRootedPath(string line, string fileRootedPath)
        {
            using (StreamWriter fileWriter = new StreamWriter(fileRootedPath))
            {
                fileWriter.Write(line);
            }
        }

        public static void SerializeToRootedPath(IEnumerable<string> lines, string fileRootedPath)
        {
            using (StreamWriter fileWriter = new StreamWriter(fileRootedPath))
            {
                foreach (string line in lines)
                {
                    fileWriter.WriteLine(line);
                }
            }
        }

        public static List<string> DeserializeFromRootedPath(string fileRootedPath)
        {
            if (!File.Exists(fileRootedPath))
            {
                throw new FileNotFoundException(String.Format("Text file '{0}' not found.", fileRootedPath));
            }

            List<string> retValue = new List<string>();
            using (StreamReader fileReader = new StreamReader(fileRootedPath))
            {
                while (!fileReader.EndOfStream)
                {
                    retValue.Add(fileReader.ReadLine());
                }
            }

            return retValue;
        }

        #endregion


        public void SerializeToRootedPathInstance(string line, string fileRootedPath)
        {
            TextFileSerializer.SerializeToRootedPath(line, fileRootedPath);
        }

        public void SerializeToRootedPathInstance(IEnumerable<string> lines, string fileRootedPath)
        {
            TextFileSerializer.SerializeToRootedPath(lines, fileRootedPath);
        }

        public List<string> DeserializeFromRootedPathInstance(string fileRootedPath)
        {
            List<string> retValue = TextFileSerializer.DeserializeFromRootedPath(fileRootedPath);
            return retValue;
        }
    }
}
