using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Data.EntityClient;
//using System.Data.Metadata.Edm;

namespace Nintex.DataLayer
{
    /// <summary>
    /// A Class to configure the connection string 
    /// </summary>
    public class DBConfiguration
    {
        private static DBConfiguration c_Self = null;
        string m_ServerIP; string m_DatabaseName; string m_UserID; string m_Password;
        
        private DBConfiguration()
        {
            
        }
        public static DBConfiguration Instance()
        {
            if (c_Self == null)
                c_Self = new DBConfiguration();
            return c_Self;
        }

        public void Initialize(string aServerIP, string aDatabaseName, string aUserID, string aPassword)
        {
            m_DatabaseName = aDatabaseName;
            m_Password = aPassword;
            m_ServerIP = aServerIP;
            m_UserID = aUserID;
        }

        public static bool GetConnectionStringBuilder(string aConnectionString, out string aServerIP, out string aDatabaseName, out string aUserID, out string aPassword)
        {
            SqlConnectionStringBuilder connBuilder = null;
            aServerIP = "";
            aDatabaseName = "";
            aUserID = "";
            aPassword = "";

            try
            {
                System.Data.EntityClient.EntityConnectionStringBuilder entityConnectionBuilder = null;

                //first assuming that provided connection string is of Entity Framework
                try
                {
                    entityConnectionBuilder = new System.Data.EntityClient.EntityConnectionStringBuilder(aConnectionString);
                }
                catch(Exception ex)
                {
                    //ingnoring the exception
                }

                string sqlConnectionString = entityConnectionBuilder != null ? entityConnectionBuilder.ProviderConnectionString
                    : aConnectionString;
                
                connBuilder = new System.Data.SqlClient.SqlConnectionStringBuilder(sqlConnectionString);

                aServerIP = connBuilder.DataSource;
                aDatabaseName = connBuilder.InitialCatalog;
                aUserID = connBuilder.UserID;
                aPassword = connBuilder.Password;

                return true;
            }
            catch(Exception ex)
            {
                //ignoring the exception. since may be the provided connection string is invalid
                return false;
            }
        }

        public DbConnection GetConnection()
        {
            // Specify the provider name, server and database.
            string providerName = "System.Data.SqlClient";
            
            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder(); 

            // Set the properties for the data source.
            sqlBuilder.DataSource = m_ServerIP;
            sqlBuilder.InitialCatalog = m_DatabaseName;
            sqlBuilder.IntegratedSecurity = string.IsNullOrEmpty(m_UserID);
            sqlBuilder.UserID = m_UserID;
            sqlBuilder.Password = m_Password;

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder =
                new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.
            entityBuilder.Metadata = @"res://*/NintexDataModel.csdl|res://*/NintexDataModel.ssdl|res://*/NintexDataModel.msl";
            Console.WriteLine(entityBuilder.ToString());

            //using (EntityConnection conn =
            //    new EntityConnection(entityBuilder.ToString()))
            //{
            //    conn.Open();
            //    Console.WriteLine("Just testing the connection.");
            //    conn.Close();
            //}

            return new EntityConnection(entityBuilder.ToString());
        }
    }
}
