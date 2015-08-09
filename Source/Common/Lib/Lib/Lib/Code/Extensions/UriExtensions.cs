using System;
using System.Text;


namespace Duomo.Common.Lib
{
    public static class UriExtensions
    {
        public static string GetLeaf(this Uri uri)
        {
            string[] segments = uri.Segments;
            int segmentsLength = segments.Length;

            string retValue = String.Empty;
            if (0 < segmentsLength)
            {
                retValue = segments[segmentsLength - 1];
            }

            return retValue;
        }

        public static string GetParentName(this Uri uri)
        {
            string[] segments = uri.Segments;
            int segmentsLength = segments.Length;

            string retValue = String.Empty;
            if (1 < segmentsLength)
            {
                retValue = segments[segmentsLength - 2];
            }

            return retValue;
        }

        public static string GetParentUriString(this Uri uri)
        {
            StringBuilder builder = new StringBuilder();

            // Append the scheme, i.e. "http", "ftp", etc.
            builder.Append(uri.Scheme);

            // Append the scheme separator "://".
            builder.Append(@"://");

            // Append the host name.
            builder.Append(uri.Host);

            // Append each segment except the last one. That is the leaf segment which we want to ignore.
            string[] segments = uri.Segments;
            int segmentsLength = segments.Length - 1;
            for (int iSegment = 0; iSegment < segmentsLength; iSegment++)
            {
                string curSegment = segments[iSegment];
                builder.Append(curSegment);
            }

            string retValue = builder.ToString();
            return retValue;
        }

        public static string AppendSegment(this Uri uri, string segment)
        {
            string retValue = uri.ToString() + segment;
            return retValue;
        }
    }
}
