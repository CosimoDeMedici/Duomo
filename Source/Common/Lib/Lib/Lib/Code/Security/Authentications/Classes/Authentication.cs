using System;

using Duomo.Common.Lib.Xml;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class Authentication : IAuthentication
    {
        #region IAuthentication Members

        public string UserName { get; set; }
        public string Password { get; set; }

        #endregion


        public Authentication() { }

        public Authentication(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public Authentication(AuthenticationXmlType xmlType)
        {
            this.UserName = xmlType.UserName;
            this.Password = xmlType.Password;
        }

        public AuthenticationXmlType GetXmlType()
        {
            AuthenticationXmlType retValue = new AuthenticationXmlType();
            retValue.UserName = this.UserName;
            retValue.Password = this.Password;

            return retValue;
        }
    }
}
