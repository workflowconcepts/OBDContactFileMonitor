using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.workflowconcepts.applications.omnixpress
{
    public class SqlAccess
    {
     //   public ApplicationSettings settings = new ApplicationSettings("C:\\ProgramData\\Workflow Concepts\\OmniXpress Server 2\\ApplicationSettings.json");
        System.Data.SqlClient.SqlConnection _connection = null;

        private object _lock = null;
        private ApplicationSettings settings;
        public SqlAccess(ApplicationSettings applicationSettings) 
        {
            settings = applicationSettings;
        }

        private void _connection_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            Log.Instance.Debug("Connection InfoMessage:" + e.Message);
        }

        public bool ParseFileData(string fileData)
        {
            try
            {
                string[] records = fileData.Split('\n');
                Log.Instance.Debug("Found " + records.Length + " records.");
                for (int i = 1; i < records.Length; i++) // skip the first line (header info)
                {
                    Log.Instance.Debug("Record " + i);
                    string[] data = records[i].Split('|');
                    Log.Instance.Debug("Record " + i + " data split");
                    AddAccount(data[0], data[1], data[2], data[3], data[4], data[5], data[6]);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Debug("Error in ParseFileData:  " + ex.Message.ToString());
                return false;
            }
        }
        public bool AddAccount(String CampaignID, String LastName, String FirstName, String MiddleName, String PhoneNumber, String CommunicationPreference, String MedicationName)
        {
            Log.Instance.Debug("Enter.");
          //  lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        Log.Instance.Debug("Connect() returned false.");
                        return false;
                    }
                    Log.Instance.Debug("Attempt stored procedure");
                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = settings.SqlCommand;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CampaignID", CampaignID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", LastName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", FirstName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MiddleName", MiddleName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PhoneNumber", PhoneNumber));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CommunicationPreference", CommunicationPreference));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MedicationName", MedicationName));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;
                    //string sDescription = (string)_command.Parameters["@Description"].Value;

                    _command.Dispose();
                    _command = null;

                    Log.Instance.Debug("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

                    if (iCode == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Debug("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    return false;
                }

            }//lock(_lock)
        }

        private bool Connect(bool AddDefaultDatabase)
        {
            Log.Instance.Debug("Enter.");

            if (_connection != null)
            {
                try
                {
                    if (_connection.State != System.Data.ConnectionState.Broken && _connection.State != System.Data.ConnectionState.Closed)
                    {
                        Log.Instance.Debug("Connection to database is still open.");
                        return true;
                    }
                    else
                    {
                        Log.Instance.Debug("Connection to database is either broken or closed.");

                        if (Disconnect())
                        {
                            Log.Instance.Debug("Disconnect() returned true.");
                        }
                        else
                        {
                            Log.Instance.Debug("Disconnect() returned false.");

                            return false;
                        }

                    }

                }
                catch (Exception ex)
                {
                    Log.Instance.Debug("Outer Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                    _connection = null;
                }
            }

            string ConnectionString = string.Empty;

            try
            {
                if (settings.SqlServerHostIp == string.Empty)
                {
                    Log.Instance.Debug("ConnectionString data missing.");

                    return false;
                }
                ConnectionString = "Data Source=" + settings.SqlInstanceName + "\\" + settings.SqlServerHostIp + ";Initial Catalog="
                                        + settings.SqlDatabaseName + ";User id=" + settings.SqlUserName + ";Password=" + settings.SqlPassword;
                Log.Instance.Debug("Connection string: " + ConnectionString);
            }
            catch (Exception ex)
            {
                Log.Instance.Debug("Exception getting connection string:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                return false;
            }

            try
            {
                _connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

                _connection.Open();

                if (_connection.State != System.Data.ConnectionState.Broken && _connection.State != System.Data.ConnectionState.Closed)
                {
                    Log.Instance.Debug("Database connection successfully open.");
                    return true;
                }
                else
                {
                    Log.Instance.Debug("Database connection did not open.");
                    _connection = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.Instance.Debug("Exception :" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                _connection = null;
                return false;
            }
        }

        private bool Disconnect()
        {
            Log.Instance.Debug("Enter.");

            if (_connection == null)
            {
                Log.Instance.Debug("Connection to database was already closed.");
                return true;
            }

            try
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    Log.Instance.Debug("Connection is open; application will attempt to close it.");
                    _connection.Close();
                }
                else if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    Log.Instance.Debug("Connection is already closed.");
                }
                else
                {
                    Log.Instance.Debug("Database connection was busy.");
                    return false;
                }

                _connection.Dispose();
                _connection = null;

                Log.Instance.Debug("Database connection was closed.");

                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Debug("Exception :" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                _connection = null;
                return false;
            }
        }
    }
}
