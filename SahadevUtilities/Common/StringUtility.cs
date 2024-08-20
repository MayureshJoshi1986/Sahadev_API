using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace SahadevUtilities.Common
{
    /// <summary>
    /// This class consist of common method related to string operations
    /// </summary>
    public class StringUtility
    {
        static string _className = "SahadevUtilities.Common.StringUtility";
        

        #region ConvertNumbertoWords
        /// <summary>
        /// This method is used to convert number to words
        /// </summary>
        /// <param name="number">number</param>
        /// <returns>Returns string</returns>
        public static string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
                {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
                {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
        #endregion

        #region ConvertStringToCamelCase
        /// <summary>
        /// This method converts first letter of every word to capital case in string 
        /// </summary>
        /// <param name="str">Input string to convert first letter of every word in capital case</param>
        /// <returns>string with first letter of every word in capital case</returns>
        public static string ConvertStringToCamelCase(string str)
        {
            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            return UsaTextInfo.ToTitleCase(str);
        }
        #endregion        

        #region GenerateRandomNo
        /// <summary>
        /// This method is used to generate random no
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        #endregion

        #region RandomString
        /// <summary>
        /// this method is used generate random number based on length
        /// </summary>
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion
    }
}
