/***********************************************************************************************/
/*  Copy right issue :- This source file is property of Millicent Technologies.                 *
 *  --------------------------------------------------------------------------------------------*
 *  Class Name      :- GeneralUtility                                                           *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This class is used for Correcting date-formats,Checking links,
 *                     String manipulation etc.
 *                     Basically it contains all methods used for general purposes
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- MS                                                                       *                                                                 
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 10/Apr/2014                                                              *
 *  --------------------------------------------------------------------------------------------* 
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 /**********************************************************************************************/


using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace SahadevUtilities.Common
{
    /// <summary>
    /// This class is used to perform functionality like string manipulation,correcting date format,Managing cookie etc
    /// This class basically contains all methods that are used for general manipulation purposes.
    /// </summary>
    public class GeneralUtility
    {
        static string _className = "SahadevUtilities.Common.GeneralUtility";

        #region GetSettingKeyValue
        /// <summary>
        /// Get App Setting Key Value
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>returns string containing value </returns>
        public static string GetSettingKeyValue(string key)
        {
            string sReturn = string.Empty;
            try
            {
                if (ConfigurationManager.AppSettings[key] != null)
                {
                    sReturn = ConfigurationManager.AppSettings[key].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetSettingKeyValue");
            }
            return sReturn;
        }
        #endregion

        #region GetDistinctValue
        /// <summary>
        /// Extract distinct value for particular column in datatable
        /// </summary>
        /// <param name="dtSource">source table from which distinct values are to be selected</param>
        /// <param name="idColumnName">column name for whom the values are to be selected</param>
        /// <param name="condition">condition on which grounds it has to be selected</param>
        /// <returns>return distinct content values</returns>
        public static List<string> GetDistinctValue(DataTable dtSource, string idColumnName, string condition)
        {
            List<string> arUniqueId = new List<string>();
            try
            {
                foreach (DataRow dr in dtSource.Select(condition))
                {
                    if (dr[idColumnName] == DBNull.Value ||
                       arUniqueId.Equals(dr[idColumnName].ToString().Trim()))
                    {
                        continue;
                    }
                    arUniqueId.Add(dr[idColumnName].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetDistinctValue");
            }
            return arUniqueId;
        }
        #endregion

        #region GetDistinctValueForParticularColumn
        /// <summary>
        /// Extract distinct value for particular column in datatable
        /// </summary>
        /// <param name="dtSource">source table from which distinct values are to be selected</param>
        /// <param name="idColumnName">column name for whom the values are to be selected</param>
        /// <param name="condition">condition on which grounds it has to be selected</param>
        /// <returns>return distinct content values</returns>
        public static List<string> GetDistinctValueForParticularColumn(DataTable dtSource, string idColumnName, string condition)
        {
            List<string> arUniqueId = new List<string>();
            try
            {
                foreach (DataRow dr in dtSource.Select(condition))
                {
                    if (dr[idColumnName] == DBNull.Value ||
                       arUniqueId.Contains(dr[idColumnName].ToString().Trim()))
                    {
                        continue;
                    }
                    arUniqueId.Add(dr[idColumnName].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetDistinctValueForParticularColumn");
            }
            return arUniqueId;
        }
        #endregion

        #region HttpParam
        /// <summary>
        /// This method is used to HTTP parameter
        /// </summary>
        /// <param name="httpRequest">Object of HTTP Request</param>
        /// <returns>Returns dictionary of HTTP request parameter</returns>
        public static Dictionary<string, string> HttpParam(HttpRequest httpRequest)
        {
            Dictionary<string, string> dicHttpRequest = new Dictionary<string, string>();
            foreach (string key in httpRequest.Form.Keys)
            {
                dicHttpRequest.Add(key, httpRequest.Form[key].ToString());
            }
            return dicHttpRequest;
        }
        #endregion

        

        
    }
}
