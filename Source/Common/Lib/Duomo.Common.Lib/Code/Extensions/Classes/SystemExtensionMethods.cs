using System;
using System.IO;


namespace Duomo.Common.Lib
{
    public static class SystemExtensionMethods
    {
        public static Stream ToStream(this string str)
        {
            MemoryStream memStream = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(memStream))
            {
                writer.Write(str);
                writer.Flush();
            }

            memStream.Position = 0;

            return memStream;
        }
    }
}
