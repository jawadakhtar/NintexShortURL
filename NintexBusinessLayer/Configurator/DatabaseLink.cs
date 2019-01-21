using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.BusinessLayer.Configurator
{
    public class DatabaseLink
    {
        private static DatabaseLink c_Self = null;
        private static object locker = new object();
        public static string ConnectionStringKey { get; private set; }

        public static void Initialize(string aConnectionStringKey)
        {
            ConnectionStringKey = aConnectionStringKey;
        }

        private DatabaseLink()
        {
        }

        public static DatabaseLink Instance()
        {
            if (String.IsNullOrEmpty(ConnectionStringKey) == true)
                throw new ApplicationException("Database Link in not Initialized. Call the Initialize first.");

            if (c_Self == null)
                c_Self = new DatabaseLink();
            return c_Self;
        }

        public string GetDatabaseConnectionString()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            if (connectionStringsSection == null || connectionStringsSection.ConnectionStrings[ConnectionStringKey] == null)
                throw new ApplicationException("The ConnectionString Entities in Application Configuration is not defined.");

            string connStr = connectionStringsSection.ConnectionStrings[ConnectionStringKey].ConnectionString;
            return connStr;
        }

        public void GetDatabaseConnectionParameters(System.Configuration.Configuration aConfiguration, out string aServerIP, out string aDatabaseName, out string aUserID, out string aPassword)
        {
            var config = aConfiguration;
            var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");

            aServerIP = "";
            aDatabaseName = "";
            aUserID = "";
            aPassword = "";

            //for (int i = 0; i < connectionStringsSection.ConnectionStrings.Count; i++)
            {
                if (connectionStringsSection == null || connectionStringsSection.ConnectionStrings[ConnectionStringKey] == null)
                    throw new ApplicationException("The ConnectionString Entities in Application Configuration is not defined.");

                string connStr = connectionStringsSection.ConnectionStrings[ConnectionStringKey].ConnectionString;
                Nintex.DataLayer.DBConfiguration.GetConnectionStringBuilder(connStr, out aServerIP, out aDatabaseName, out aUserID, out aPassword);
                //System.Data.EntityClient.EntityConnectionStringBuilder entityConnectionBuilder = new System.Data.EntityClient.EntityConnectionStringBuilder(connStr);
                //System.Data.SqlClient.SqlConnectionStringBuilder connBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder(entityConnectionBuilder.ProviderConnectionString);
            }
        }

        public void ConfigureDatabase(System.Configuration.Configuration aConfiguration, string aServerIP, string aDatabaseName, string aUserID, string aPassword)
        {
            //string connectionString = "data source=" + aServerIP + ";initial catalog=" + aDatabaseName + ";user id=" + aUserID + ";password=" + aPassword + ";MultipleActiveResultSets=True;App=EntityFramework";
            //DataAccessLayer.DBConfiguration.ConnectionString = connectionString;
            Nintex.DataLayer.DBConfiguration.Instance().Initialize(aServerIP, aDatabaseName, aUserID, aPassword);

            //var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var connectionStringsSection = (ConnectionStringsSection)aConfiguration.GetSection("connectionStrings");
            //for(int i = 0; i < connectionStringsSection.ConnectionStrings.Count ; i++)
            if (connectionStringsSection.ConnectionStrings[ConnectionStringKey] != null)
            {
                connectionStringsSection.ConnectionStrings[ConnectionStringKey].ConnectionString = Nintex.DataLayer.DBConfiguration.Instance().GetConnection().ConnectionString;
            }
            else
            {
                throw new ApplicationException("The ConnectionString "+ ConnectionStringKey + " is missing from the Configuration.");
            }

            aConfiguration.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
