using System;
using System.Collections.Generic;

using Duomo.Common.Lib.IO.Serialization;
using Duomo.Common.Lib.Xml;


namespace Duomo.Common.Lib.Database
{
    [Serializable]
    public class DatabaseInterface
    {
        #region Static

        public static Dictionary<string, DatabaseInterface> LoadDatabaseInterfaces(string fileRootedPath)
        {
            DatabaseInterfacesListXmlType xmlInterfaces = XmlSerializer<DatabaseInterfacesListXmlType>.DeserializeFromRootedPath(fileRootedPath);

            Dictionary<string, DatabaseInterface> retValue = new Dictionary<string, DatabaseInterface>();
            foreach (DatabaseInterfaceXmlType xmlInterface in xmlInterfaces.DatabaseInterfaceDefinition)
            {
                DatabaseInterface curInterface = new DatabaseInterface(xmlInterface);
                retValue.Add(curInterface.DatabaseID, curInterface);
            }

            return retValue;
        }

        #endregion


        public string DatabaseID { get; set; }
        public string ServerAddress { get; set; }
        public int Port { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseType { get; set; } // TODO, split this off by inheritance hierarchy.


        public DatabaseInterface() { }

        public DatabaseInterface(string databaseID, string serverAddress, int port, string databaseName, string databaseType)
        {
            this.DatabaseID = databaseID;
            this.ServerAddress = serverAddress;
            this.Port = port;
            this.DatabaseName = databaseName;
            this.DatabaseType = databaseType;
        }

        public DatabaseInterface(DatabaseInterfaceXmlType xmlType)
        {
            this.DatabaseID = xmlType.DatabaseID;
            this.ServerAddress = xmlType.ServerAddress;
            this.Port = xmlType.Port;
            this.DatabaseName = xmlType.DatabaseName;
            this.DatabaseType = xmlType.DatabaseType;
        }

        public DatabaseInterfaceXmlType GetXmlType()
        {
            DatabaseInterfaceXmlType retValue = new DatabaseInterfaceXmlType();

            retValue.DatabaseID = this.DatabaseID;
            retValue.ServerAddress = this.ServerAddress;
            retValue.Port = this.Port;
            retValue.DatabaseName = this.DatabaseName;
            retValue.DatabaseType = this.DatabaseType;

            return retValue;
        }
    }
}
