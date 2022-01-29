using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace com.workflowconcepts.customerjourney
{
    public class SqlServerClient
    {
        System.Data.SqlClient.SqlConnection _connection = null;

        string _Server = string.Empty;
        string _Instance = string.Empty;
        string _UserName = string.Empty;
        string _Password = string.Empty;
        bool _UseSQLServerAuthentication = false;
        string _Database = string.Empty;

        string _Details = string.Empty;

        object _lock = null;

        public string Server
        {
            get { return _Server; }
            set { _Server = value; }
        }

        public string Instance
        {
            get { return _Instance; }
            set { _Instance = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public bool UseSQLServerAuthentication
        {
            get { return _UseSQLServerAuthentication; }
            set { _UseSQLServerAuthentication = value; }
        }

        public string Database
        {
            get { return _Database; }
            set { _Database = value; }
        }

        public string Details
        {
            get { return _Details; }
        }

        public SqlServerClient()
        {
            _lock = new object();

            _Server = string.Empty;
            _Instance = string.Empty;
            _UserName = string.Empty;
            _Password = string.Empty;
            _UseSQLServerAuthentication = false;

            _Database = Constants.SystemDatabaseName;

            _Details = string.Empty;
        }

        public bool TestConnection()
        {
            Trace.TraceInformation("Enter.");

            bool result = false;

            try
            {
                if (Connect(false))
                {
                    _Details = string.Empty;

                    Trace.TraceInformation("Connect() returned true.");

                    result = true;

                    if (Disconnect())
                    {
                        Trace.TraceInformation("Disconnect() returned true.");
                    }
                    else
                    {
                        Trace.TraceWarning("Disconnect() returned false.");
                    }
                }
                else
                {
                    Trace.TraceWarning("Connect() returned false.");
                }

                return result;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                _Details = ex.Message;
                return false;
            }
        }

        public bool GetDispositionCodesForTeamCSQ(String Team, String CSQ, out System.Data.DataTable Table)
        {
            Trace.TraceInformation("Enter.");

            try
            {
                Table = null;

                if (!Connect(true))
                {
                    _Details = string.Empty;
                    Trace.TraceWarning("Connect() returned false.");
                    return false;
                }

                System.Data.SqlClient.SqlCommand _command = null;

                _command = new System.Data.SqlClient.SqlCommand();

                _command.Connection = _connection;
                _command.CommandTimeout = 5;
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = Constants.Procedure_GetDispositionCodesForTeamCSQ;

                _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@TeamName", Team));
                _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSQName", CSQ));

                System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                retval.Direction = System.Data.ParameterDirection.ReturnValue;

                _command.Parameters.Add(retval);

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(_command);

                System.Data.DataSet ds = new System.Data.DataSet();

                da.Fill(ds);

                da.Dispose();
                da = null;

                int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                Trace.TraceInformation("Return value:" + iCode);

                if (ds == null)
                {
                    Table = null;
                    Trace.TraceWarning("DataSet is null.");
                    _Details = "DataSet is null.";
                    return false;
                }

                if(ds.Tables.Count == 0)
                {
                    Table = null;
                    Trace.TraceWarning("DataSet contains no tables.");
                    _Details = "DataSet contains no tables.";
                    return false;
                }

                if (ds.Tables.Count > 1)
                {
                    Table = null;
                    Trace.TraceWarning("DataSet contains an unexpected number of tables.");
                    _Details = "DataSet contains an unexpected number of tables.";
                    return false;
                }

                Table = ds.Tables[0];

                ds.Dispose();
                ds = null;

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                Table = null;
                _Details = ex.Message;
                return false;
            }
        }

        public bool GetDispositionCodesByCodeID(String DispositionCode1, String DispositionCode2, string DispositionCode3, out System.Data.DataTable Table)
        {
            Trace.TraceInformation("Enter.");

            try
            {
                Table = null;

                if (!Connect(true))
                {
                    _Details = string.Empty;
                    Trace.TraceWarning("Connect() returned false.");
                    return false;
                }

                System.Data.SqlClient.SqlCommand _command = null;

                _command = new System.Data.SqlClient.SqlCommand();

                _command.Connection = _connection;
                _command.CommandTimeout = 5;
                _command.CommandType = System.Data.CommandType.StoredProcedure;
                _command.CommandText = Constants.Procedure_GetDispositionCodesByID;

                _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier1", DispositionCode1));
                _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier2", DispositionCode2));
                _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier3", DispositionCode3));

                System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                retval.Direction = System.Data.ParameterDirection.ReturnValue;

                _command.Parameters.Add(retval);

                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(_command);

                System.Data.DataSet ds = new System.Data.DataSet();

                da.Fill(ds);

                da.Dispose();
                da = null;

                int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                Trace.TraceInformation("Return value:" + iCode);

                if (ds == null)
                {
                    Table = null;
                    Trace.TraceWarning("DataSet is null.");
                    _Details = "DataSet is null.";
                    return false;
                }

                if (ds.Tables.Count == 0)
                {
                    Table = null;
                    Trace.TraceWarning("DataSet contains no tables.");
                    _Details = "DataSet contains no tables.";
                    return false;
                }

                if (ds.Tables.Count > 1)
                {
                    Table = null;
                    Trace.TraceWarning("DataSet contains an unexpected number of tables.");
                    _Details = "DataSet contains an unexpected number of tables.";
                    return false;
                }

                Table = ds.Tables[0];

                ds.Dispose();
                ds = null;

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                Table = null;
                _Details = ex.Message;
                return false;
            }
        }

        public bool AddAccount(String FirstName, String LastName, String Email, String Phone1, String Phone1Descriptor, String Phone2, String Phone2Descriptor, String Phone3, String Phone3Descriptor, String CIS, String Username)
        {
            Trace.TraceInformation("Enter.");

            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_AddAccount;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", FirstName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", LastName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", Email));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone1", Phone1));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone1Descriptor", Phone1Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone2", Phone2));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone2Descriptor", Phone2Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone3", Phone3));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone3Descriptor", Phone3Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIS", CIS));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", Username));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();
                    
                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

                    if(iCode == 0)
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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock(_lock)
        }

        public bool UpdateAccount(String ID, String FirstName, String LastName, String Email, String Phone1, String Phone1Descriptor, String Phone2, String Phone2Descriptor, String Phone3, String Phone3Descriptor, String CIS, String Username)
        {
            Trace.TraceInformation("Enter.");

            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_UpdateAccount;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", ID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", FirstName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", LastName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", Email));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone1", Phone1));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone1Descriptor", Phone1Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone2", Phone2));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone2Descriptor", Phone2Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone3", Phone3));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone3Descriptor", Phone3Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIS", CIS));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", Username));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock (_lock)
        }

        public bool AddUpdateAccount(String FirstName, String LastName, String Email, String Phone1, String Phone1Descriptor, String Phone2, String Phone2Descriptor, String Phone3, String Phone3Descriptor, String CIS, String Username, out int Code)
        {
            Trace.TraceInformation("Enter.");

            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Code = -1;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_AddUpdateAccount;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@FirstName", FirstName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastName", LastName));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Email", Email));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone1", Phone1));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone1Descriptor", Phone1Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone2", Phone2));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone2Descriptor", Phone2Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone3", Phone3));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Phone3Descriptor", Phone3Descriptor));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIS", CIS));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", Username));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

                    if (iCode >= 0)
                    {
                        Code = iCode;
                        return true;
                    }
                    else
                    {
                        Code = -1;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    Code = -1;
                    return false;
                }

            }//lock (_lock)
        }

        public bool DeleteAccount(String ID)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_DeleteAccount;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", ID));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock (_lock)
        }

        public bool AddJourneyEntry(String ID, String EntryDateTime, String AgentLoginID, String AgentTeam, String CSQ, String ContactType, String ContentURL, String Comments, String DispositionCodeTier1, String DispositionCodeTier2, String DispositionCodeTier3, String Username)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_AddJourneyEntry;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", ID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DateTime", EntryDateTime));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentLoginID", AgentLoginID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AgentTeam", AgentTeam));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CSQ", CSQ));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContactType", ContactType));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ContentURL", ContentURL));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Comments", Comments));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier1", DispositionCodeTier1));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier2", DispositionCodeTier2));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier3", DispositionCodeTier3));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", Username));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

                    if (iCode >= 0)
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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock(_lock)
        }

        public bool UpdateJourneyEntry(String ID, String Comments, String DispositionCodeTier1, String DispositionCodeTier2, String DispositionCodeTier3, String Username)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_UpdateJourneyEntry;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", ID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Comments", Comments));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier1", DispositionCodeTier1));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier2", DispositionCodeTier2));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DispositionCodeTier3", DispositionCodeTier3));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserName", Username));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();
                    
                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock(_lock)
        }

        public bool DeleteJourneyEntry(String ID)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_DeleteJourneyEntry;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", ID));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock(_lock)
        }

        public bool DeleteJourney(String AccountID)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_DeleteJourney;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", AccountID));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock(_lock)
        }

        public bool DeleteJourneyEntriesBefore(String AccountID, String EntryDateTime)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_DeleteJourneyEntriesBefore;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", AccountID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DateTime", EntryDateTime));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    _command.Dispose();
                    _command = null;

                    Trace.TraceInformation("NumberOfRowsAffected:" + iNumberOfRowsAffected + " Return value:" + iCode);

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
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock(_lock)
        }

        public bool FindAccountsByToken(String Token, out System.Data.DataTable Table)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        Table = null;
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_FindAccountsByToken;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Token", Token));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(_command);

                    System.Data.DataSet ds = new System.Data.DataSet();

                    da.Fill(ds);

                    da.Dispose();
                    da = null;

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    Trace.TraceInformation("Return value:" + iCode);

                    if (iCode == 0)
                    {
                        if (ds == null)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet is null.");
                            _Details = "DataSet is null.";
                            return false;
                        }

                        if (ds.Tables.Count == 0)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains no tables.");
                            _Details = "DataSet contains no tables.";
                            return false;
                        }

                        if (ds.Tables.Count > 1)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains an unexpected number of tables.");
                            _Details = "DataSet contains an unexpected number of tables.";
                            return false;
                        }

                        Table = ds.Tables[0];

                        ds.Dispose();
                        ds = null;

                        return true;
                    }
                    else if (iCode == -99)
                    {
                        ds.Dispose();
                        ds = null;

                        _Details = "Too many records to return. Please narrow your search.";
                        Table = null;
                        return true;
                    }
                    else
                    {
                        ds.Dispose();
                        ds = null;
                        Table = null;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    Table = null;
                    return false;
                }

            }//lock(_lock)
        }

        public bool GetAccountByCIS(String CIS, out System.Data.DataTable Table)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        Table = null;
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_GetAccountByCIS;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIS", CIS));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(_command);

                    System.Data.DataSet ds = new System.Data.DataSet();

                    da.Fill(ds);

                    da.Dispose();
                    da = null;

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    Trace.TraceInformation("Return value:" + iCode);

                    if (iCode == 0)
                    {
                        if (ds == null)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet is null.");
                            _Details = "DataSet is null.";
                            return false;
                        }

                        if (ds.Tables.Count == 0)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains no tables.");
                            _Details = "DataSet contains no tables.";
                            return false;
                        }

                        if (ds.Tables.Count > 1)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains an unexpected number of tables.");
                            _Details = "DataSet contains an unexpected number of tables.";
                            return false;
                        }

                        Table = ds.Tables[0];

                        ds.Dispose();
                        ds = null;

                        return true;
                    }
                    else
                    {
                        ds.Dispose();
                        ds = null;
                        Table = null;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    Table = null;
                    return false;
                }

            }//lock (_lock)
        }

        public bool GetCustomerJourneyByAccountID(String AccountID, int NumberOfDays, out System.Data.DataTable Table)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        Table = null;
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_GetCustomerJourneyByAccountID;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@AccountID", AccountID));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NumOfDays", NumberOfDays));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(_command);

                    System.Data.DataSet ds = new System.Data.DataSet();

                    da.Fill(ds);

                    da.Dispose();
                    da = null;

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    Trace.TraceInformation("Return value:" + iCode);

                    if (iCode == 0)
                    {
                        if (ds == null)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet is null.");
                            _Details = "DataSet is null.";
                            return false;
                        }

                        if (ds.Tables.Count == 0)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains no tables.");
                            _Details = "DataSet contains no tables.";
                            return false;
                        }

                        if (ds.Tables.Count > 1)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains an unexpected number of tables.");
                            _Details = "DataSet contains an unexpected number of tables.";
                            return false;
                        }

                        Table = ds.Tables[0];

                        ds.Dispose();
                        ds = null;

                        return true;
                    }
                    else if (iCode == -99)
                    {
                        ds.Dispose();
                        ds = null;

                        _Details = "Too many records to return. Please narrow your search.";
                        Table = null;
                        return true;
                    }
                    else
                    {
                        ds.Dispose();
                        ds = null;
                        Table = null;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    Table = null;
                    return false;
                }

            }//lock (_lock)
        }

        public bool GetCustomerJourneyByCIS(String CIS, int NumberOfDays, out System.Data.DataTable Table)
        {
            lock (_lock)
            {
                try
                {
                    if (!Connect(true))
                    {
                        _Details = string.Empty;
                        Trace.TraceWarning("Connect() returned false.");
                        Table = null;
                        return false;
                    }

                    System.Data.SqlClient.SqlCommand _command = null;

                    _command = new System.Data.SqlClient.SqlCommand();

                    _command.Connection = _connection;
                    _command.CommandTimeout = 5;
                    _command.CommandType = System.Data.CommandType.StoredProcedure;
                    _command.CommandText = Constants.Procedure_GetCustomerJourneyByCIS;

                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CIS", CIS));
                    _command.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NumOfDays", NumberOfDays));

                    System.Data.SqlClient.SqlParameter retval = new System.Data.SqlClient.SqlParameter("@ReturnValue", System.Data.SqlDbType.Int);
                    retval.Direction = System.Data.ParameterDirection.ReturnValue;

                    _command.Parameters.Add(retval);

                    System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(_command);

                    System.Data.DataSet ds = new System.Data.DataSet();

                    da.Fill(ds);

                    da.Dispose();
                    da = null;

                    int iCode = (int)_command.Parameters["@ReturnValue"].Value;

                    Trace.TraceInformation("Return value:" + iCode);

                    if (iCode == 0)
                    {
                        if (ds == null)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet is null.");
                            _Details = "DataSet is null.";
                            return false;
                        }

                        if (ds.Tables.Count == 0)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains no tables.");
                            _Details = "DataSet contains no tables.";
                            return false;
                        }

                        if (ds.Tables.Count > 1)
                        {
                            Table = null;
                            Trace.TraceWarning("DataSet contains an unexpected number of tables.");
                            _Details = "DataSet contains an unexpected number of tables.";
                            return false;
                        }

                        Table = ds.Tables[0];

                        ds.Dispose();
                        ds = null;

                        return true;
                    }
                    else if (iCode == -99)
                    {
                        ds.Dispose();
                        ds = null;

                        _Details = "Too many records to return. Please narrow your search.";
                        Table = null;
                        return true;
                    }
                    else
                    {
                        ds.Dispose();
                        ds = null;
                        Table = null;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    Table = null;
                    return false;
                }

            }//lock (_lock)
        }

        public bool CreateDatabase()
        {
            lock (_lock)
            {
                try
                {
                    //if (!Connect(false))
                    //{
                    //    _Details = "Failed to connect to database server.";
                    //    Trace.TraceWarning("Connect() returned false.");
                    //    return false;
                    //}

                    //System.Data.SqlClient.SqlCommand _command = null;

                    //_command = new System.Data.SqlClient.SqlCommand();

                    //_command.Connection = _connection;
                    //_command.CommandTimeout = 5;
                    //_command.CommandText = "SELECT * FROM sys.sysdatabases where name=\'" + Constants.SystemDatabaseName + "\'";
                    //_command.CommandType = System.Data.CommandType.Text;

                    //if ((String)_command.ExecuteScalar() == Constants.SystemDatabaseName)
                    //{
                    //    Trace.TraceInformation("Database already exists in this server.");
                    //    _Details = "Database " + Constants.SystemDatabaseName + " already exists in this server\\instance.";

                    //    _command.Dispose();
                    //    _command = null;

                    //    Disconnect();

                    //    return false;
                    //}

                    ////_command.Connection.ChangeDatabase("master");
                    //_command.CommandText = Properties.Resources.CustomerJourney;
                    //_command.CommandType = System.Data.CommandType.Text;
                    //int iNumberOfRowsAffected = _command.ExecuteNonQuery();

                    //Trace.TraceInformation("iNumberOfRowsAffected:" + iNumberOfRowsAffected);

                    //_command.Dispose();
                    //_command = null;

                    //Disconnect();

                    return true;
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                    _Details = ex.Message;
                    return false;
                }

            }//lock (_lock)
        }

        private bool Connect(bool AddDefaultDatabase)
        {
            Trace.TraceInformation("Enter.");

            if (_connection != null)
            {
                try
                {
                    if (_connection.State != System.Data.ConnectionState.Broken && _connection.State != System.Data.ConnectionState.Closed)
                    {
                        Trace.TraceInformation("Connection to database is still open.");
                        return true;
                    }
                    else
                    {
                        Trace.TraceWarning("Connection to database is either broken or closed.");

                        _connection.InfoMessage -= _connection_InfoMessage;

                        if (Disconnect())
                        {
                            Trace.TraceInformation("Disconnect() returned true.");
                        }
                        else
                        {
                            Trace.TraceWarning("Disconnect() returned false.");

                            return false;
                        }

                    }

                }
                catch (Exception ex)
                {
                    Trace.TraceError("Outer Exception:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);

                    _connection = null;
                }
            }

            string ConnectionString = string.Empty;

            try
            {
                ConnectionString = BuildConnectionString(AddDefaultDatabase);

                if (ConnectionString == string.Empty)
                {
                    Trace.TraceWarning("ConnectionString is empty.");
                    _Details = "ConnectionString is empty.";

                    return false;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception getting connection string:" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                _Details = "Exception getting connection string.";
                return false;
            }

            try
            {
                _connection = new System.Data.SqlClient.SqlConnection(ConnectionString);

                _connection.Open();

                _connection.InfoMessage += _connection_InfoMessage;

                if (_connection.State != System.Data.ConnectionState.Broken && _connection.State != System.Data.ConnectionState.Closed)
                {
                    Trace.TraceInformation("Database connection successfully open.");
                    _Details = "Database connection successfully open.";
                    return true;
                }
                else
                {
                    Trace.TraceWarning("Database connection did not open.");
                    _Details = "Database connection did not open.";
                    _connection = null;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception :" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                _Details = "Exception opening database connection.";
                _connection = null;
                return false;
            }
        }

        private void _connection_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            Trace.TraceInformation("Connection InfoMessage:" + e.Message);
        }

        private bool Disconnect()
        {
            Trace.TraceInformation("Enter.");

            if (_connection == null)
            {
                Trace.TraceInformation("Connection to database was already closed.");
                return true;
            }

            try
            {
                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    Trace.TraceInformation("Connection is open; application will attempt to close it.");
                    _connection.Close();
                }
                else if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    Trace.TraceInformation("Connection is already closed.");
                }
                else
                {
                    Trace.TraceWarning("Database connection was busy.");
                    return false;
                }

                _connection.Dispose();
                _connection = null;

                Trace.TraceInformation("Database connection was closed.");

                return true;
            }
            catch (Exception ex)
            {
                Trace.TraceError("Exception :" + ex.Message + Environment.NewLine + "StackTrace:" + ex.StackTrace);
                _connection = null;
                return false;
            }
        }

        private string BuildConnectionString(bool AddDefaultDatabase)
        {
            Trace.TraceInformation("Enter.");

            string ConnectionString = string.Empty;

            if (_Server.Length == 0)
            {
                Trace.TraceError("ServerName is empty.");
                _Details = "Invalid server name.";
                return string.Empty;
            }

            ConnectionString = "Server=" + _Server;

            if (_Instance.Length != 0)
            {
                ConnectionString = ConnectionString + "\\" + _Instance;
            }

            ConnectionString = ConnectionString + ";";

            if (!_UseSQLServerAuthentication)
            {
                ConnectionString = ConnectionString + "Integrated Security=SSPI;";
            }
            else
            {
                if (_UserName.Length == 0)
                {
                    Trace.TraceError("UserName is empty.");
                    _Details = "Invalid user name.";
                    return string.Empty;
                }

                ConnectionString = ConnectionString + "User ID=" + _UserName + ";";

                if (_Password.Length == 0)
                {
                    Trace.TraceError("Password is empty.");
                    _Details = "Invalid password.";
                    return string.Empty;
                }

                ConnectionString = ConnectionString + "Password=" + _Password + ";";
            }

            if (AddDefaultDatabase)
            {
                ConnectionString = ConnectionString + "Database=" + Database + ";";
            }

            Trace.TraceInformation("Connection string: " + ConnectionString);

            return ConnectionString;
        }
    }
}
