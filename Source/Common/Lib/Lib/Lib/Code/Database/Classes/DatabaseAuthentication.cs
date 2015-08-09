using System;

using Duomo.Common.Lib;
using Duomo.Common.Lib.Xml;


namespace Duomo.Common.Lib.Database
{
    [Serializable]
    public class DatabaseAuthentication : Authentication
    {
        public string DatabaseID { get; set; }


        public DatabaseAuthentication() : base() { }

        public DatabaseAuthentication(string databaseID, string userName, string password)
            : base(userName, password)
        {
            this.DatabaseID = databaseID;
        }

        public DatabaseAuthentication(DatabaseAuthenticationXmlType xmlType)
            : base(xmlType.UserName, xmlType.Password)
        {
            this.DatabaseID = xmlType.DatabaseID;
        }

        public new DatabaseAuthenticationXmlType GetXmlType()
        {
            DatabaseAuthenticationXmlType retValue = new DatabaseAuthenticationXmlType();

            retValue.DatabaseID = this.DatabaseID;
            retValue.UserName = this.UserName;
            retValue.Password = this.Password;

            return retValue;
        }
    }
}
