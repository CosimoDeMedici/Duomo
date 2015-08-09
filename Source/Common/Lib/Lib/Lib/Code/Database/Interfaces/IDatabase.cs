using System;
using System.Data;


namespace Duomo.Common.Lib.Database
{
    public interface IDatabase
    {
        void Open();
        void Close();
        ConnectionState State { get; }
        IDbConnection Connection { get; }
        
        
        IDbCommand CreateCommand();
    }
}
