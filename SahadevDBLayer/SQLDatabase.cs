/*  Copy right      :- This source file is property of Millicent Technologies.                  *
 *  --------------------------------------------------------------------------------------------*
 *  Class Name      :- DataBase                                                                 *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This class performs all the database manipulation related activities     *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- MS                                                                       *                                                                 
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 18/April/2014                                                            *
 *  --------------------------------------------------------------------------------------------* 
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          * 
 //**********************************************************************************************/

#region Used Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
#endregion


namespace TB.DBLayer
{
    /// <summary>
    /// This class performs all the database manipulation related activities
    /// It performs DML activities like Insertion,Updation,Selection etc.
    /// </summary>
    public class SQLDatabase
    {
        #region Variables

        private SqlConnection connection;
        private SqlDataReader reader;
        private SqlDataAdapter adapter;
        private SqlCommand command;

        private int returnValue = 0;
        private DataTable returnDataTable = null;
        private object returnObject = null;
        private DataSet returnDataSet = null;

        #endregion

        #region Properties

        #region NeverThrowException
        /// <summary>
        /// Checks whether error will pass to the method calling it
        /// </summary>
        public Boolean NeverThrowException
        {
            get;
            set;
        }
        #endregion

        #region ErrorText
        /// <summary>
        /// Contains error message details for calling object
        /// </summary>
        public String ErrorText
        {
            get;
            set;
        }
        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SQLDatabase()
        {

        }

        /// <summary>
        /// This is a constructor which sets whether the exception is to be throwed or not
        /// </summary>
        /// <param name="neverThrowException">If True then pass error to calling object ,If False for do not throw error</param>
        protected SQLDatabase(Boolean neverThrowException)
        {
            try
            {
                NeverThrowException = neverThrowException;
            }
            catch (ConfigurationException exConfig)
            {
                throw exConfig;
            }
            catch (Exception ex)
            {
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Methods

        #region Get ConnectionString
        /// <summary>
        /// This method gets the connectionstring from web.config connectionStrings section 
        /// with setting name as 'SqlConnectionString'
        /// </summary>
        /// <returns>Connection string</returns>
        private string GetConnectionString()
        {
            string connectionString = string.Empty;
            try
            {
                    connectionString = ConfigurationManager.ConnectionStrings["TEST_SqlConnectionString"].ConnectionString;
            }
            catch (Exception ex)
            {
                connectionString = string.Empty;
                throw ex;
            }
            return connectionString;
        }
        #endregion

        #region ExecNonQuery

        /// <summary>
        /// This methods performs the ExecuteNonQuery  command which is used for performing DML operation on database
        /// </summary>       
        /// <param name="spName">Storedprocedure name</param>
        /// <returns>Returns the number of rows affected</returns>   
        public int ExecNonQuery(string spName)
        {
            try
            {
                ErrorText = string.Empty;
                returnValue = ExecNonQuery(spName, null);
            }
            catch (Exception ex)
            {
                returnValue = 0;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// This methods performs the ExecuteNonQuery  command which is used for performing DML operation on database
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Returns the number of rows affected</returns>
        public int ExecNonQuery(string spName, string parameterName, string parameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(parameterName, parameterValue);
                returnValue = ExecNonQuery(spName, parameters);

            }
            catch (Exception ex)
            {
                returnValue = 0;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }

            return returnValue;

        }

        /// <summary>
        /// This methods performs the ExecuteNonQuery  command which is used for performing DML operation on database
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="firstParameterName">Name of the first parameter</param>
        /// <param name="firstParameterValue">Value of the first parameter</param>
        /// <param name="secondParameterName">Name of the second parameter</param>
        /// <param name="secondParameterValue">Value of the second parameter</param>
        /// <returns>Returns the number of rows affected</returns>
        public int ExecNonQuery(string spName, string firstParameterName, string firstParameterValue, string secondParameterName, string secondParameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(firstParameterName, firstParameterValue);
                parameters.Add(secondParameterName, secondParameterValue);
                returnValue = ExecNonQuery(spName, parameters);
            }
            catch (Exception ex)
            {
                returnValue = 0;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnValue;
        }

        /// <summary>
        /// This methods performs the ExecuteNonQuery  command which is used for performing DML operation on database
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="parameters">Dictionary of type string containing parameter name and value as a keypair</param>
        /// <returns>Returns the number of rows affected</returns>
        public int ExecNonQuery(string spName, Dictionary<string, string> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandText = spName;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, string> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                        returnValue = 1;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// This methods performs the ExecuteNonQuery  command which is used for performing DML operation on database
        /// </summary>
        /// <param name="execText">ExecQuery Text</param>
        /// <returns>Returns the number of rows affected</returns>
        public int ExecNonQueryText(string execQueryText)
        {
            try
            {
                ErrorText = string.Empty;
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandText = execQueryText;
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandTimeout = 0;
                        connection.Open();
                        command.ExecuteNonQuery();
                        returnValue = 1;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// This methods performs the ExecuteNonQuery  command which is used for performing DML operation on database
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>
        /// <param name="parameters">Dictionary of type string object containing parameter name and value as a keypair</param>
        /// <returns>Returns the number of rows affected</returns>
        public int ExecNonQueryTVP(string spName, Dictionary<string, object> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandText = spName;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, object> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                        returnValue = 1;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                returnValue = 0;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }

            return returnValue;
        }

        #endregion

        #region ExecReader

        /// <summary>
        /// This method performs the ExecuteReader  command. 
        /// </summary>
        /// <param name="spName">Storeprocedure name</param>      
        /// <returns>Returns result of the query as a datatable </returns>
        public DataTable ExecReader(string spName)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataTable = ExecReader(spName, null);
            }
            catch (Exception ex)
            {
                returnDataTable = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataTable;
        }

        /// <summary>
        /// This method performs the ExecuteReader  command. 
        /// </summary>
        /// <param name="spName">Storeprocedure name</param>      
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Returns result of the query as a datatable </returns>
        public DataTable ExecReader(string spName, string parameterName, string parameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(parameterName, parameterValue);
                returnDataTable = ExecReader(spName, parameters);
            }
            catch (Exception ex)
            {
                returnDataTable = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataTable;
        }

        /// <summary>
        /// This method performs the ExecuteReader  command. 
        /// </summary>
        /// <param name="spName">Storeprocedure name</param>      
        /// <param name="firstParameterName">Name of the first parameter</param>
        /// <param name="firstParameterValue">Value of the first parameter</param>
        /// <param name="secondParameterName">Name of the second parameter</param>
        /// <param name="secondParameterValue">Value of the second parameter</param>
        /// <returns>Returns result of the query as a datatable </returns>
        public DataTable ExecReader(string spName, string firstParameterName, string firstParameterValue, string secondParameterName, string secondParameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(firstParameterName, firstParameterValue);
                parameters.Add(secondParameterName, secondParameterValue);
                returnDataTable = ExecReader(spName, parameters);
            }
            catch (Exception ex)
            {
                returnDataTable = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataTable;
        }

        /// <summary>
        /// This method performs the ExecuteReader  command. 
        /// </summary>
        /// <param name="spName">Storeprocedure name</param>      
        /// <param name="parameters">Dictionary of type string containing parameter name and value as a keypair</param>
        /// <returns>Returns result of the query as a datatable </returns>
        public DataTable ExecReader(string spName, Dictionary<string, string> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataTable = new DataTable();
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.CommandText = spName;
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, string> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        connection.Open();
                        reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                        returnDataTable.Load(reader);
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                returnDataTable = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataTable;
        }

        #endregion

        #region ExecScalar

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>    
        /// <returns>Returns the first column of the first row in the result set returned by the query</returns>
        public object ExecScalar(string spName)
        {
            try
            {
                ErrorText = string.Empty;
                returnObject = ExecScalar(spName, null);
            }
            catch (Exception ex)
            {
                returnObject = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnObject;
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>    
        /// <param name="parameterName">Parameter name </param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Returns the first column of the first row in the result set returned by the query</returns>
        public object ExecScalar(string spName, string parameterName, string parameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(parameterName, parameterValue);
                returnObject = ExecScalar(spName, parameters);
            }
            catch (Exception ex)
            {
                returnObject = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnObject;
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>    
        /// <param name="firstParameterName">Name of the first parameter</param>
        /// <param name="firstParameterValue">Value of the first parameter</param>
        /// <param name="secondParameterName">Name of the second parameter</param>
        /// <param name="secondParameterValue">Value of the second parameter</param>
        /// <returns>Returns the first column of the first row in the result set returned by the query</returns>
        public object ExecScalar(string spName, string firstParameterName, string firstParameterValue, string secondParameterName, string secondParameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(firstParameterName, firstParameterValue);
                parameters.Add(secondParameterName, secondParameterValue);
                returnObject = ExecScalar(spName, parameters);
            }
            catch (Exception ex)
            {
                returnObject = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnObject;
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>    
        /// <param name="parameters">Dictionary of type string containing parameter name and value as a keypair</param>
        /// <returns>Returns the first column of the first row in the result set returned by the query</returns>
        public object ExecScalar(string spName, Dictionary<string, string> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, string> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        connection.Open();
                        returnObject = command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                returnObject = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnObject;
        }

        /// <summary>
        /// Executes the query and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>    
        /// <param name="parameters">Dictionary of type string containing parameter name and value as a keypair</param>
        /// <returns>Returns the first column of the first row in the result set returned by the query</returns>
        public object ExecScalarTVP(string spName, Dictionary<string, object> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, object> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        connection.Open();
                        returnObject = command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                returnObject = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnObject;
        }

        #endregion

        #region ExecDataSet

        /// <summary>
        /// This method result of the query as a dataset containing one or more tables
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>     
        /// <returns>Result of the query as a dataset</returns>
        public DataSet ExecDataSet(string spName)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataSet = new DataSet();
                returnDataSet = ExecDataSet(spName, null);
            }
            catch (Exception ex)
            {
                returnDataSet = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataSet;
        }

        /// <summary>
        /// This method result of the query as a dataset containing one or more tables
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>     
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        /// <returns>Result of the query as a dataset</returns>
        public DataSet ExecDataSet(string spName, string parameterName, string parameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataSet = new DataSet();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(parameterName, parameterValue);

                returnDataSet = ExecDataSet(spName, parameters);
            }
            catch (Exception ex)
            {
                returnDataSet = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataSet;
        }

        /// <summary>
        /// This method result of the query as a dataset containing one or more tables
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>  
        /// <param name="firstParameterName">Name of the first parameter</param>
        /// <param name="firstParameterValue">Value of the first parameter</param>
        /// <param name="secondParameterName">Name of the second parameter</param>
        /// <param name="secondParameterValue">Value of the second parameter</param>
        /// <returns>Result of the query as a dataset</returns>
        public DataSet ExecDataSet(string spName, string firstParameterName, string firstParameterValue, string secondParameterName, string secondParameterValue)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataSet = new DataSet();
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                parameters.Add(firstParameterName, firstParameterValue);
                parameters.Add(secondParameterName, secondParameterValue);

                returnDataSet = ExecDataSet(spName, parameters);
            }
            catch (Exception ex)
            {
                returnDataSet = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataSet;
        }

        /// <summary>
        /// This method result of the query as a dataset containing one or more tables
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>  
        /// <param name="parameters">Dictionary of type string containing parameter name and value as a keypair</param>
        /// <returns>Result of the query as a dataset</returns>
        public DataSet ExecDataSet(string spName, Dictionary<string, string> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataSet = new DataSet();
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, string> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        using (adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(returnDataSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnDataSet = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataSet;
        }

        /// <summary>
        /// This method result of the query as a dataset containing one or more tables
        /// </summary>
        /// <param name="spName">Storedprocedure name</param>  
        /// <param name="parameters">Dictionary of type string containing parameter name and value as a keypair</param>
        /// <returns>Result of the query as a dataset</returns>
        public DataSet ExecDataSetTVP(string spName, Dictionary<string, object> parameters)
        {
            try
            {
                ErrorText = string.Empty;
                returnDataSet = new DataSet();
                using (connection = new SqlConnection(GetConnectionString()))
                {
                    using (command = new SqlCommand(string.Empty, connection))
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;
                        command.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, object> parameter in parameters)
                            {
                                command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                            }
                        }
                        using (adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(returnDataSet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                returnDataSet = null;
                ErrorText = ex.Message;
                if (!NeverThrowException)
                {
                    throw ex;
                }
            }
            return returnDataSet;
        }

        #endregion
        #endregion
    }
}
