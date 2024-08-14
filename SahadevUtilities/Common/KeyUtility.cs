using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using SahadevUtilities.Encryption;


namespace SahadevUtilities.Common
{
    public class KeyUtility
    {
        public enum DeviceShortNameOS
        {
            A, W
        }

        #region GenerateLicenseKey
        /// <summary>
        /// Generate Product Key
        /// </summary>
        /// <param name="deviceOS">device OS Either A/W</param>
        /// <param name="smSrNo">count no of current pendrive</param>
        /// <param name="dbSerialNo">database auto generated serial no</param>
        /// <param name="macId">mac id of machine</param>
        /// <returns> return the generated product key</returns>
        public static string GenerateLicenseKey(string deviceOS, string userCode, string smSerialNo, string productCode, string macId, string userProductCode)
        {
            string sReturn = string.Empty;
            string date = DateTimeUtility.GetTodayDateTimeMMddYY();
            macId = StringUtility.GetAlternateCharFromString(macId);
            sReturn = StringUtility.ReverseString(StringUtility.CreateHyphenString(deviceOS + userCode + smSerialNo + productCode + macId.Substring(0, 7) + userProductCode + date + "LIC", 8));
            return sReturn;
        }
        #endregion

        #region GenerateServiceReplyKey
        /// <summary>
        /// Generate Service Reply Key
        /// </summary>
        /// <param name="svcKeyword">UBL|EPR</param>
        /// <param name="userProductCode">User Product Code</param>
        /// <param name="smSerialNo">Storage Media Serial No</param>
        /// <param name="macId">mac id of machine</param>
        /// <param name="userCode">User Code</param>
        /// <param name="deviceOS">device OS Either A/W</param>
        /// <param name="date">Date Passed</param>
        /// <returns> return the generated reply service key</returns>
        public static string GenerateServiceReplyKey(string svcKeyword, string userProductCode, string smSerialNo, string macId, string userCode, string deviceOS, string date)
        {
            string sReturn = string.Empty;
            macId = StringUtility.GetAlternateCharFromString(macId);
            sReturn = StringUtility.ReverseString(StringUtility.CreateHyphenString(svcKeyword + userProductCode + smSerialNo.Substring(smSerialNo.Length - 5) + macId.Substring(0, 4) + userCode + deviceOS + date, 7));
            return sReturn;
        }
        #endregion

        #region GenerateHardwareKey
        /// <summary>
        /// Generate Hardware Key
        /// </summary>
        /// <param name="deviceOS">device OS Either A/W</param>
        /// <param name="userProductCode">User Product Code</param>
        /// <param name="smSerialNo">Storage Media Serial No</param>
        /// <param name="macId">machine id of user</param>
        /// <param name="installationMedia">installation media code</param>
        /// <returns>return the generated Product Registration Device Key</returns>
        public static string GenerateProductRegistrationDeviceKey(string deviceOS, string userProductCode, string smSerialNo, string macId, string installationMedia)
        {
            string sReturn = string.Empty;
            if (Enum.IsDefined(typeof(DeviceShortNameOS), deviceOS))
            {
                //if (DeviceShortNameOS.W.ToString() == deviceOS.ToUpper())
                //    macId = GetAlternateCharFromString(macId);
                string date = DateTime.Now.ToString("MMddyy");
                sReturn = StringUtility.CreateHyphenString(deviceOS + userProductCode + date + "DEVK" + smSerialNo + macId + installationMedia, 13);
            }
            return sReturn;
        }
        #endregion

        #region GenerateServiceRequestKey
        /// <summary>
        /// Generate Hardware Key
        /// </summary>
        /// <param name="deviceOS">device OS Either A/W</param>
        /// <param name="userProductCode">User Product Code</param>
        /// <param name="smSerialNo">Storage Media Serial No</param>
        /// <param name="macId">machine id of user</param>
        /// <param name="installationMedia">installation media code</param>
        /// <returns>return the generated Product Registration Device Key</returns>
        public static string GenerateServiceRequestKey(string productCode, string errorCode, string deviceOS, string userProductCode, string macId)
        {
            string sReturn = string.Empty;
            if (Enum.IsDefined(typeof(DeviceShortNameOS), deviceOS))
            {

                macId = StringUtility.GetAlternateCharFromString(macId);
                if (DeviceShortNameOS.W.ToString() == deviceOS.ToUpper())
                    macId = macId.Substring(0, 8);
                string date = DateTime.Now.ToString("MMddyy");
                sReturn = StringUtility.CreateHyphenString("SR" + productCode + errorCode + deviceOS + userProductCode + macId, 5);
            }
            return sReturn;
        }
        #endregion

        #region GenerateProductKey
        /// <summary>
        /// Generate Product Key
        /// </summary>
        /// <param name="deviceOS">device OS Either A/W</param>
        /// <param name="smSrNo">count no of current pendrive</param>
        /// <param name="dbSerialNo">database auto generated serial no</param>
        /// <param name="macId">mac id of machine</param>
        /// <returns> return the generated product key</returns>
        public static string GenerateProductKey(string deviceOS, string smSrNo, string dbSerialNo, string macId)
        {
            string sReturn = string.Empty;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            string date = indianTime.ToString("MMddyy");
            macId = StringUtility.GetAlternateCharFromString(macId);
            sReturn = StringUtility.CreateHyphenString(deviceOS + dbSerialNo + macId.Substring(0, 4) + date + smSrNo, 4);
            return sReturn;
        }
        #endregion

        #region GenerateErrorReplyKey
        //// <summary>
        /// Generate Error Reply Key
        /// </summary>
        /// <param name="productEndDate">product allocated end date</param>
        /// /// <param name="macId">mac id of machine</param>
        /// <param name="productId">product key of product</param>
        /// <param name="customerId">customer key registered id of customer</param>
        /// <param name="allocationId">allocation key of allocation</param>
        /// <param name="smSerialNo">storage media serial no</param>
        /// <returns> return the generated Error Reply key</returns>
        public static string GenerateErrorReplyKey(string productEndDate, string macId, string productId, string allocationId, string smSerialNo)
        {
            string sReturn = string.Empty;
            string date = (DateTimeUtility.GetTodayISTDateTime()).ToString("MMddyy");
            macId = StringUtility.GetAlternateCharFromString(macId);
            sReturn = StringUtility.CreateHyphenString(productEndDate + date + macId.Substring(0, 4) + productId + allocationId + smSerialNo.Substring(smSerialNo.Length - 5) + "INS", 7);
            return sReturn;
        }
        #endregion

        #region GenerateReplyKeyContent
        //// <summary>
        /// Generate Error Reply Key
        /// </summary>
        /// <param name="productEndDate">product allocated end date</param>
        /// /// <param name="macId">mac id of machine</param>
        /// <param name="productId">product key of product</param>
        /// <param name="customerId">customer key registered id of customer</param>
        /// <param name="allocationId">allocation key of allocation</param>
        /// <param name="smSerialNo">storage media serial no</param>
        /// <returns> return the generated Error Reply key</returns>
        public static string GenerateReplyKeyContent(string deviceOS, string productEndDate, string macId, string dbSerialNo, string smSerialNo)
        {
            string sReturn = string.Empty;
            //string date = (GeneralUtility.GetTodayISTDateTime()).ToString("MMddyy");
            macId = StringUtility.GetAlternateCharFromString(macId);
            sReturn = StringUtility.CreateHyphenString(deviceOS + productEndDate + macId.Substring(0, 4) + dbSerialNo + string.Format("{0:00000}", smSerialNo.Substring(smSerialNo.Length - 5)) + "CD", 5);
            return sReturn;
        }
        #endregion

        #region GenerateHardwareKey
        /// <summary>
        /// Generate Hardware Key
        /// </summary>
        /// <param name="deviceOS">device OS Either A/W</param>
        /// <param name="macId">machine id of user</param>
        /// <param name="dbSerialNo">database auto generated serial no</param>
        /// <returns>return the generated hardware key</returns>
        public static string GenerateHardwareKey(string deviceOS, string macId, string dbSerialNo)
        {
            string sReturn = string.Empty;
            if (Enum.IsDefined(typeof(DeviceShortNameOS), deviceOS))
            {
                if (DeviceShortNameOS.W.ToString() == deviceOS.ToUpper())
                    macId = StringUtility.GetAlternateCharFromString(macId);
                string date = DateTime.Now.ToString("MMddyy");
                sReturn = StringUtility.CreateHyphenString(deviceOS + dbSerialNo + macId + date, 5);
            }
            return sReturn;
        }
        #endregion
    }
}
