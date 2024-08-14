using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SahadevUtilities.Common
{
    /// <summary>
    /// This class consist of common method related to DB
    /// </summary>
    public class DBUtility
    {
        static string _className = "SahadevUtilities.Common.DBUtility";
        #region GetSqlParameterList
        /// <summary>
        /// This method is used to convert dictionary to SQL parameters.
        /// </summary>
        /// <param name="dicParamList">Object of Dictionary</param>
        /// <returns>Returns array of SQL Parameter</returns>
        public static object[] GetSqlParameterList(Dictionary<string, string> dicParamList)
        {
            List<SqlParameter> lstSQLParam = new List<SqlParameter>();
            //object[] objReturnParam = null;
            try
            {
                foreach (KeyValuePair<string, string> pair in dicParamList)
                {
                    lstSQLParam.Add(new SqlParameter(pair.Key, string.IsNullOrEmpty(pair.Value) ? DBNull.Value.ToString() : pair.Value));
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetSqlParameterList");
            }
            return lstSQLParam.ToArray();
        }
        #endregion

        #region GetSqlParameterListWithOutputParam
        /// <summary>
        /// This method is used to convert dictionary to SQL parameters with output parameter.
        /// </summary>
        /// <param name="dicParamList">Object of Dictionary</param>
        /// <param name="objSqlParamOutput">Object of SQLParameter</param>
        /// <param name="outputParameterName">Output parameter name</param>
        /// <returns>Returns array of SQL Parameter</returns>
        public static object[] GetSqlParameterListWithOutputParam(Dictionary<string, string> dicParamList, out SqlParameter objSqlParamOutput, string outputParameterName = "")
        {
            List<SqlParameter> lstSQLParam = new List<SqlParameter>();
            objSqlParamOutput = new SqlParameter();
            //object[] objReturnParam = null;
            try
            {
                foreach (KeyValuePair<string, string> pair in dicParamList)
                {
                    lstSQLParam.Add(new SqlParameter(pair.Key, string.IsNullOrEmpty(pair.Value) ? DBNull.Value.ToString() : pair.Value));
                }

                if (!string.IsNullOrEmpty(outputParameterName))
                {
                    objSqlParamOutput = new SqlParameter(outputParameterName, System.Data.SqlDbType.NVarChar, 50);
                    objSqlParamOutput.Direction = System.Data.ParameterDirection.Output;
                    lstSQLParam.Add(objSqlParamOutput);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetSqlParameterListWithOutputParam");
            }
            return lstSQLParam.ToArray();
        }
        #endregion
    }
}
