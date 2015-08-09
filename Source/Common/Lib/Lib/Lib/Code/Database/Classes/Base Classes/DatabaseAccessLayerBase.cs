using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;


namespace Duomo.Common.Lib.Database
{
    public class DatabaseAccessLayerBase
    {
        #region Static

        protected static string CleanQueryStatement(string query)
        {
            string regExPattern = "\r\n";

            string retValue = Regex.Replace(query, regExPattern, @" ");
            return retValue;
        }

        public static string CleanStringValue(string value)
        {
            string retValue = value.Replace(@"'", @"''");
            return retValue;
        }

        #endregion


        protected SybaseDatabase zSybaseDatabase;


        public DatabaseAccessLayerBase(DatabaseAccessToken accessToken)
        {
            this.zSybaseDatabase = new SybaseDatabase(accessToken);
        }

        public IDbCommand CreateCommand()
        {
            IDbCommand retValue = this.zSybaseDatabase.CreateCommand();
            return retValue;
        }

        #region Example

        public bool ExistsByIdInTable(string tableName, int ID)
        {
            string query =
@"
select
    count(*) as 'Count'
from {0}
    where ID = {1}
";

            string formattedQuery = String.Format(query, tableName, ID);
            string cleanedQuery = DatabaseAccessLayerBase.CleanQueryStatement(formattedQuery);

            IDbCommand cmd = this.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = cleanedQuery;

            IDataReader reader = null;
            int count = 0;
            try
            {
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[@"Count"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Failed to execute query:\n{0}", cmd.CommandText), ex);
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                }
            }

            bool retValue = 0 != count;
            return retValue;
        }

        #endregion
    }
}
