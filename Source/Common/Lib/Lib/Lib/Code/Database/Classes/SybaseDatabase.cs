using System;
using System.Data;
using System.Data.Odbc;


namespace Duomo.Common.Lib.Database
{
    public class SybaseDatabase : DatabaseBase
    {
        private static readonly DateTime MinDate = new DateTime(1753, 1, 1);


        public override DateTime MinDateValue
        {
            get
            {
                return SybaseDatabase.MinDate;
            }
        }


        public SybaseDatabase(string userName, string password, string connectionString) : base(String.Format(connectionString, userName, password)) { }

        public SybaseDatabase(DatabaseAccessToken accessToken) : base(accessToken)
        {
        }

        protected override string GetConnectionString(DatabaseAccessToken accessToken)
        {
            string connectionStringMask = @"Driver={{Adaptive Server Enterprise}};NA={0},{1};DB={2};UID={3};PWD={4};WorkArounds2=32768;TextSize=2147483647;ServerInitiatedTransactions=0";
            string retValue = String.Format(connectionStringMask,
                accessToken.Interface.ServerAddress,
                accessToken.Interface.Port,
                accessToken.Interface.DatabaseName,
                accessToken.Authentication.UserName,
                accessToken.Authentication.Password);

            return retValue;
        }

        public override IDbConnection ProvideConnection()
        {
            return new OdbcConnection();
        }
    }
}
