using System;
using System.Data;


namespace Duomo.Common.Lib.Database
{
    public abstract class DatabaseBase : IDatabase
    {
        #region IDatabase Members

        public void Open()
        {
            this.CreateConnection();

            if (ConnectionState.Open != this.State)
            {
                this.zConnection.Open();
            }
        }

        public void Close()
        {
            if (null != this.zConnection)
            {
                if (ConnectionState.Closed != this.zConnection.State)
                {
                    this.zConnection.Close();
                }

                this.zConnection = null;
            }
        }

        public ConnectionState State
        {
            get
            {
                this.CreateConnection();

                return this.zConnection.State;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                this.CreateConnection();

                return this.zConnection;
            }
        }

        public IDbCommand CreateCommand()
        {
            this.CreateConnection();

            IDbCommand retValue = this.zConnection.CreateCommand();
            return retValue;
        }

        #endregion


        protected string zConnectionString;
        protected IDbConnection zConnection;
        public bool IsConnected
        {
            get
            {
                bool retValue = false;
                if (null != this.zConnection)
                {
                    retValue = this.State == ConnectionState.Open;
                }

                return retValue;
            }
        }
        public virtual DateTime MinDateValue
        {
            get
            {
                DateTime retValue = DateTime.MinValue;
                return retValue;
            }
        }
        public virtual DateTime MaxDateValue
        {
            get
            {
                DateTime retValue = DateTime.MaxValue;
                return retValue;
            }
        }


        protected DatabaseBase(string connectionString)
        {
            this.zConnectionString = connectionString;
        }

        protected DatabaseBase(DatabaseAccessToken accessToken)
        {
            this.zConnectionString = this.GetConnectionString(accessToken);

            this.CreateConnection();

            this.Open();
        }

        protected abstract string GetConnectionString(DatabaseAccessToken accessToken);

        public abstract IDbConnection ProvideConnection();

        private void CreateConnection()
        {
            if (null == this.zConnection)
            {
                if (null == this.zConnectionString)
                {
                    throw new InvalidOperationException(@"No connection has been opened, and no connection string has been provided.");
                }

                this.zConnection = this.ProvideConnection();
                this.zConnection.ConnectionString = this.zConnectionString;
            }
        }
    }
}
