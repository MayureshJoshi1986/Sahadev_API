using Serilog;
using System;
using System.Globalization;

namespace SahadevUtilities.Common
{
    /// <summary>
    /// This class consist of common method related to datetime
    /// </summary>
    public class DateTimeUtility
    {
        static string _className = "SahadevUtilities.Common.DateTimeUtility";

        #region GetTodayISTDateTime
        /// <summary>
        /// Get Today IST DateTime
        /// </summary>
        /// <param name="date">input date to compare</param>
        /// <returns>returns DateTime of IST</returns>
        public static DateTime GetTodayISTDateTime()
        {
            DateTime dtTodayIST = new DateTime();
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            try
            {
                dtTodayIST = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetTodayISTDateTime");
            }
            return dtTodayIST;
        }
        #endregion

        #region GetRelativeDateValue
        /// <summary>
        /// This method returns difference between the given two date-time
        /// It returns in format of eg:- Yesterday,10 minutes ago,Few minutes ago etc
        /// </summary>
        /// <param name="date">First date to be compare</param>
        /// <param name="comparedTo">Date compare with</param>
        /// <returns>String with difference in date times</returns>
        public static string GetRelativeDateValue(DateTime date, DateTime comparedTo)
        {
            try
            {
                // Get differences  
                TimeSpan diff = comparedTo.Subtract(date);
                // Retrun formated date message
                string retunDate = string.Empty;


                if (diff.Days >= 7)                                             // Check for more then 7 days
                {
                    retunDate = string.Concat("On ", date.ToString("MMMM d, yyyy"));
                }
                else if (comparedTo.Day - date.Day == 1)                        // Check for last day
                {
                    retunDate = "Yesterday";
                }
                else if (diff.Days >= 1)                                        // For few days
                {
                    retunDate = string.Concat(diff.Days, " days ago");
                }
                else if (diff.Hours >= 2)                                       // For hours ago
                {
                    retunDate = string.Concat(diff.Hours, " hours ago");
                }
                else if (diff.TotalMinutes >= 60)                               // For an hour
                {
                    retunDate = "More than an hour ago";
                }
                else if (diff.Minutes >= 5)                                     // For more then 5 minutes
                {
                    retunDate = string.Concat(diff.Minutes, " minutes ago");
                }
                else if (diff.Minutes >= 1)                                     // For few minutes
                {
                    retunDate = "Few minutes ago";
                }
                else
                {
                    retunDate = "Less than a minute ago";                       // For few seconds 
                }
                return retunDate;
            }
            catch (ArgumentOutOfRangeException exOutofRange)
            {
                Log.Error(exOutofRange, _className, "GetRelativeDateValue - ArgumentOutOfRangeException");
                throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetRelativeDateValue");
                throw;
            }
        }
        #endregion

        #region GetFormattedDate
        /// <summary>
        /// This method formats input datetime with respect to todays day
        /// Returns date time compared to today's day
        /// </summary>
        /// <param name="dateToFormat">Date to format</param>
        /// <returns>String formated date</returns>
        public static string GetFormattedDate(DateTime dateToFormat)
        {
            string result = "";

            if (dateToFormat == null)
            {
                // Set empty string
                result = string.Empty;
            }
            else
            {
                //Get the difference
                TimeSpan diff = DateTime.Now - dateToFormat;
                //Compare date for today                 
                if (Convert.ToDateTime(dateToFormat.ToShortDateString()) == Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                {
                    // Set for today
                    result = "Today @" + dateToFormat.ToString("hh:mm:ss tt");
                }
                else if ((diff.TotalDays > 1) && (diff.TotalDays <= 2))
                {
                    // Set for yesterday 
                    result = "Yesterday @" + dateToFormat.ToString("hh:mm:ss tt");
                }
                else
                {
                    // Set formated date
                    result = dateToFormat.ToString("dd/MM/yyyy hh:mm:ss tt");
                }
            }

            return result;
        }
        #endregion

        #region ConvertStringtoStartDatetime
        /// <summary>
        /// This method is used to convert string to datetime
        /// </summary>
        /// <param name="dateTime">DateTime in String format</param>
        /// <param name="dtResult">Result in DateTime format</param>
        /// <returns>Returns true converted successfully else returns false</returns>
        public static bool ConvertStringtoStartDatetime(string dateTime, out DateTime dtResult)
        {

            bool bReturn = false;
            dtResult = new DateTime();
            try
            {
                DateTime outDtResult = new DateTime();
                bReturn = DateTime.TryParse(dateTime.Trim(), out outDtResult);
                if (bReturn)
                {
                    dtResult = outDtResult;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "ConvertStringtoStartDatetime");
            }
            return bReturn;
        }
        #endregion

        #region ConvertStringtoStartMMDDYYYY
        /// <summary>
        /// This method is used to convert string to datetime in MM/dd/yyyy HH:mm:ss format
        /// </summary>
        /// <param name="dateTime">DateTime in String format</param>
        /// <param name="sResult">Result in String format</param>
        /// <returns>Returns true converted successfully else returns false</returns>
        public static bool ConvertStringtoStartMMDDYYYY(string dateTime, out string sResult)
        {

            bool bReturn = false;
            sResult = string.Empty;
            try
            {
                DateTime outDtResult = new DateTime();
                bReturn = DateTime.TryParse(dateTime.Trim(), out outDtResult);
                if (bReturn)
                {
                    sResult = outDtResult.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "ConvertStringtoStartMMDDYYYY");
            }
            return bReturn;
        }
        #endregion

        #region ConvertStringtoEndDatetime
        /// <summary>
        /// This method is used to convert string to datetime
        /// </summary>
        /// <param name="dateTime">DateTime in String format</param>
        /// <param name="dtResult">Result in DateTime format</param>
        /// <returns>Returns true converted successfully else returns false</returns>
        public static bool ConvertStringtoEndDatetime(string dateTime, out DateTime dtResult)
        {

            bool bReturn = false;
            dtResult = new DateTime();
            try
            {
                DateTime outDtResult = new DateTime();
                bReturn = DateTime.TryParse(dateTime.Trim(), out outDtResult);
                if (bReturn)
                {
                    System.TimeSpan duration = new System.TimeSpan(0, 23, 59, 59);
                    dtResult = outDtResult.Add(duration);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "ConvertStringtoEndDatetime");
            }
            return bReturn;
        }
        #endregion

        #region ConvertStringtoEndMMDDYYYY
        /// <summary>
        /// This method is used to convert string to datetime in MM/dd/yyyy HH:mm:ss format
        /// </summary>
        /// <param name="dateTime">DateTime in String format</param>
        /// <param name="sResult">Result in String format</param>
        /// <returns>Returns true converted successfully else returns false</returns>
        public static bool ConvertStringtoEndMMDDYYYY(string dateTime, out string sResult)
        {

            bool bReturn = false;
            sResult = string.Empty;
            try
            {
                DateTime outDtResult = new DateTime();
                bReturn = DateTime.TryParse(dateTime.Trim(), out outDtResult);
                if (bReturn)
                {
                    System.TimeSpan duration = new System.TimeSpan(0, 23, 59, 59);
                    outDtResult = outDtResult.Add(duration);
                    sResult = outDtResult.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "ConvertStringtoEndMMDDYYYY");
            }
            return bReturn;
        }
        #endregion

        #region GetTotalMilliSecond
        /// <summary>
        /// This method is used to get total millisecond from 1970 to till now.
        /// </summary>
        /// <returns>Returns milliseconds</returns>
        public static string GetTotalMilliSecond()
        {
            var st = new DateTime(1970, 1, 1);
            TimeSpan t = (DateTime.Now - st);
            return ((Int64)(t.TotalMilliseconds + 0.5)).ToString();
        }
        #endregion

        #region GenerateUniqueIdMilliSeconds
        /// <summary>
        /// Generate Unique Id with TotalMilliSeconds format
        /// </summary>
        /// <param name="uniquePrefix">prefix key string</param>
        /// <returns>returns string containing key with hyphen</returns>
        public static string GenerateUniqueIdMilliSeconds(string uniquePrefix)
        {
            return uniquePrefix + "-" + GetTotalMilliSecond();
        }
        #endregion

        #region GenerateUniqueIdDateTime
        /// <summary>
        /// Generate Unique Id with IST DateTime yyyyMMddHHmmssfff format
        /// </summary>
        /// <param name="uniquePrefix">prefix key string</param>
        /// <returns>returns string containing key with hyphen</returns>
        public static string GenerateUniqueIdDateTime(string uniquePrefix)
        {
            string sReturn = uniquePrefix + "-" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            try
            {
                sReturn = uniquePrefix + "-" + GetTodayISTDateTime().ToString("yyyyMMddHHmmssfff");
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GenerateUniqueIdDateTime");
            }
            return sReturn;

        }
        #endregion

        #region ValidateTodayDateTime
        /// <summary>
        /// Compare date with todays date in MMddyy format
        /// </summary>
        /// <param name="date">input date to compare</param>
        /// <returns>returns true if successful, false if failed</returns>
        public static bool ValidateTodayDateTime(string date)
        {
            bool bReturn = false;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            try
            {

                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                string todayDate = indianTime.ToString("MMddyy");
                if (date == todayDate)
                    bReturn = true;

            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "ValidateTodayDateTime");
            }
            return bReturn;
        }
        #endregion

        #region GetTodayDateTimeMMddYY
        /// <summary>
        /// Get date with todays date in MMddyy format
        /// </summary>
        /// <returns>returns string containing date</returns>
        public static string GetTodayDateTimeMMddYY()
        {
            string sReturn = "010101";
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            try
            {

                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                sReturn = indianTime.ToString("MMddyy");
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetTodayDateTimeMMddYY");
            }
            return sReturn;
        }
        #endregion
    }
}
