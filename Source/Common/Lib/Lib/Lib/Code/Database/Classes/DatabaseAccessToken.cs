using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Database
{
    [Serializable]
    public class DatabaseAccessToken
    {
        #region Static

        public static DatabaseAccessToken GetToken(DatabaseAuthentication authentication, string databaseInterfacesFileRootedPath)
        {
            Dictionary<string, DatabaseInterface> interfaces = DatabaseInterface.LoadDatabaseInterfaces(databaseInterfacesFileRootedPath);

            DatabaseInterface curInterface = interfaces[authentication.DatabaseID];

            DatabaseAccessToken retValue = new DatabaseAccessToken(curInterface, authentication);
            return retValue;
        }

        #endregion


        public string DatabaseID { get; set; }
        public DatabaseInterface Interface { get; set; }
        public DatabaseAuthentication Authentication { get; set; }


        public DatabaseAccessToken(DatabaseInterface databaseInterface, DatabaseAuthentication databaseAuthentication)
        {
            if (databaseInterface.DatabaseID != databaseAuthentication.DatabaseID)
            {
                throw new ArgumentException(String.Format(@"Database ID mismatch between interface '{0}' and authentication '{1}'.", databaseInterface.DatabaseID, databaseAuthentication.DatabaseID));
            }

            this.DatabaseID = databaseInterface.DatabaseID;
            this.Interface = databaseInterface;
            this.Authentication = databaseAuthentication;
        }
    }
}
