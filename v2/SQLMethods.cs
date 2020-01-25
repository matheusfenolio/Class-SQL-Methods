using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace SQLMethods //Change to your namespace project
{
    public static class SQLMethods
    {
        public static class ConnectionParameters
        {
            public static string Host { get; set; }
            public static string Port { get; set; }
            public static string Instance { get; set; }
            public static string User { get; set; }
            public static string Password { get; set; }
            public static string Timeout { get; set; }
            public static string Type { get; set; }
            public static string ConnectionStringModel { get; set; }
            public static string ConnectionString { get; set; }
        }

        private static dynamic connection;
        private static dynamic command;
        private static dynamic transaction;
        private static dynamic reader;


        public static void SetParameters()
        {
            try
            {
                string path = String.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "config.json");

                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    dynamic connectionParameters = JsonConvert.DeserializeObject<dynamic>(json);
                    ConnectionParameters.Host = connectionParameters.Host;
                    ConnectionParameters.Port = connectionParameters.Port;
                    ConnectionParameters.Instance = connectionParameters.Instance;
                    ConnectionParameters.User = connectionParameters.User;
                    ConnectionParameters.Password = connectionParameters.Password;
                    ConnectionParameters.Timeout = connectionParameters.Timeout;
                    ConnectionParameters.Type = connectionParameters.Type;
                    ConnectionParameters.ConnectionStringModel = connectionParameters.ConnectionStringModel;
                    ConnectionParameters.ConnectionString = connectionParameters.ConnectionString;
                }


                string defaultConnectionString = string.Empty;

                if (ConnectionParameters.Type == "sqlServer")
                {
                    connection = new SqlConnection();
                    command = new SqlCommand();

                    defaultConnectionString = "Data Source={0},{1};Initial Catalog={2};User id={3};Password={4};Connection Timeout={5};";
                }
                else if (ConnectionParameters.Type == "MySql")
                {
                    connection = new MySqlConnection();
                    command = new MySqlCommand();

                    defaultConnectionString = "Server={0};Port={1};Database={2};Uid={3};Pwd={4}";
                }
                else if (ConnectionParameters.Type == "Oracle")
                {
                    connection = new OracleConnection();
                    command = new OracleCommand();

                    defaultConnectionString = "Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST ={0})(PORT = {1})))(CONNECT_DATA =(SERVICE_NAME = {2})));User ID={3};Password={4};Unicode=True";
                }


                if (string.IsNullOrWhiteSpace(ConnectionParameters.ConnectionStringModel))
                {
                    ConnectionParameters.ConnectionStringModel = defaultConnectionString;
                }

                ConnectionParameters.ConnectionString = String.Format(ConnectionParameters.ConnectionStringModel,
                    ConnectionParameters.Host,
                    ConnectionParameters.Port,
                    ConnectionParameters.Instance,
                    ConnectionParameters.User,
                    ConnectionParameters.Password,
                    ConnectionParameters.Timeout);

                command.Connection = connection;
                connection.ConnectionString = ConnectionParameters.ConnectionString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public static void ExecQuery(List<string> sql)
        {
            try
            {
                SetParameters();
                connection.Open();
                transaction = connection.BeginTransaction("SQLMethods");
                command.Transaction = transaction;

                try
                {

                    foreach (String query in sql)
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                        throw new Exception(ex.Message, ex);
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message, ex2);
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static void ExecQuery(string sql)
        {
            try
            {
                SetParameters();
                connection.Open();
                transaction = connection.BeginTransaction("SQLMethods");
                command.Transaction = transaction;

                command.CommandText = sql;

                reader = command.ExecuteReader();

                if (reader != null)
                {
                    reader.Close();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message, ex);
                }
                catch (Exception ex2)
                {
                    throw new Exception(ex2.Message, ex2);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static DataTable GetDataTable(string sql)
        {
            try
            {
                SetParameters();
                connection.Open();
                transaction = connection.BeginTransaction("SQLMethods");
                command.Transaction = transaction;

                command.CommandText = sql;

                reader = command.ExecuteReader();

                DataTable dt = new DataTable();

                dt.Load(reader);

                if (reader != null)
                {
                    reader.Close();
                }

                transaction.Commit();

                return dt;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message, ex);
                }
                catch (Exception ex2)
                {
                    throw new Exception(ex2.Message, ex2);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public static object GetField(string sql, string field)
        {
            try
            {
                SetParameters();
                connection.Open();
                transaction = connection.BeginTransaction("SQLMethods");
                command.Transaction = transaction;

                command.CommandText = sql;

                reader = command.ExecuteReader();

                object returnValue = null;

                while (reader.Read())
                {
                    returnValue = reader[field];
                }

                if (reader != null)
                {
                    reader.Close();
                }

                transaction.Commit();

                return returnValue;
            }
            catch (Exception ex)
            {
                try
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message, ex);
                }
                catch (Exception ex2)
                {
                    throw new Exception(ex2.Message, ex2);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
