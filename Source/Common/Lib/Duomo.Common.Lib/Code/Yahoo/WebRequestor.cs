using System;
using System.IO;
using System.Net;


namespace Duomo.Common.Lib.Yahoo
{
    public static class WebDataRequester
    {
        public static string GetStringResponse(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.UseDefaultCredentials = true;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string statusDescription = response.StatusDescription;

            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);

            string retValue = reader.ReadToEnd();

            reader.Close();
            stream.Close();
            response.Close();

            return retValue;
        }
    }
}
