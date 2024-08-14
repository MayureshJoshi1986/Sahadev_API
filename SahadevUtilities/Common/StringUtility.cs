using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SahadevUtilities.Common
{
    /// <summary>
    /// This class consist of common method related to string operations
    /// </summary>
    public class StringUtility
    {
        static string _className = "SahadevUtilities.Common.StringUtility";
        // Html pattern to be removed
        const string HTML_TAG_PATTERN = "<.*?>";

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

        #region DatasetToCommaSeparatedString

        /// <summary>
        ///  This method converts dataset to comma seperated string. 
        ///  Dataset should contains only single row and single column
        /// </summary>
        /// <param name="dataset">DataSet</param>
        /// <returns>Comma seperated string</returns>
        public string DatasetToCommaSeparatedString(DataSet dataset)
        {
            string strCommaSeperated = string.Empty;
            try
            {
                // Generate comma separated string
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    strCommaSeperated = strCommaSeperated + "," + dr[0].ToString();
                }
                // Remove if starting with comma
                if (strCommaSeperated.Length > 0)
                {
                    strCommaSeperated = strCommaSeperated.Remove(0, 1);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "DatasetToCommaSeparatedString");
            }

            return strCommaSeperated;
        }

        #endregion

        #region StripHTML
        /// <summary>
        /// This method Strips Html tags from the input string and returns plain string(Without html tags)
        /// </summary>
        /// <param name="inputString">String to be stripped of HTML tags</param>
        /// <returns>Plain string</returns>
        public static string StripHTML(string inputString)
        {
            return Regex.Replace(inputString, HTML_TAG_PATTERN, string.Empty);
        }
        #endregion

        #region RemoveHTMLTags
        /// <summary>
        /// This method removes all HTML tag from string  
        /// </summary>
        /// <param name="value">String to be stripped of HTML tags</param>
        /// <returns>String without html tags</returns>
        public static string RemoveHTMLTags(string value)
        {
            // Return strippted string
            return (string.IsNullOrEmpty(value) ? string.Empty : System.Text.RegularExpressions.Regex.Replace(value, "<[^>]*>", string.Empty));
        }
        #endregion

        #region StripScript
        /// <summary>
        /// This method Strips script tags from the input string and returns plain string(With html tags)
        /// </summary>
        /// <param name="inputString">String to be stripped of HTML doc</param>
        /// <returns>html string</returns>
        public static string StripScript(string inputString)
        {
            return Regex.Replace(inputString, "<script.*?</script>", string.Empty, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
        #endregion

        #region RemoveScriptContent
        /// <summary>
        /// This method removes all script tag from string  
        /// </summary>
        /// <param name="value">String to be stripped of HTML doc</param>
        /// <returns>String with html tags</returns>
        public static string RemoveScriptContent(string value)
        {
            // Return stripped string
            return (string.IsNullOrEmpty(value) ? string.Empty : System.Text.RegularExpressions.Regex.Replace(value, "<script.*?</script>", string.Empty));
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

        #region ReplaceEnterWithBR
        /// <summary>
        /// This method replaces enter key with BR tag 
        /// </summary>
        /// <param name="str">Input string replce \r\n </param>
        public static string ReplaceEnterWithBR(string str)
        {
            return str.Replace("\r\n", "<br/>");
        }
        #endregion

        #region GetCountOfWordsWithSentence
        /// <summary>
        /// This method is used to get count of words in a sentence
        /// </summary>
        /// <param name="value">The value from which the sentence is to  be extracted</param>
        /// <param name="wordsCount">The count of words</param>
        /// <returns>string with  count of words</returns>
        public static string GetCountOfWordsWithSentence(string value, int wordsCount)
        {
            StringBuilder strB = new StringBuilder();
            try
            {
                //Removing carriage returns
                value = value.Replace("\r", " ");
                value = value.Replace("\n", " ");
                //Spliting by blank
                string[] arr = value.Trim().Split(' ');

                int count = 0;      //Used to count words
                foreach (string temp in arr)
                {
                    string str = temp.Trim();
                    if (str.Length == 0)    //Checking for blank values
                        continue;

                    strB.Append(str);

                    count++;
                    if (count == wordsCount)
                        break;

                    strB.Append(" ");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "GetCountOfWordsWithSentence");
            }

            return strB.ToString().Trim();
        }
        #endregion

        #region AppendAnchorTag
        /// <summary>
        /// This appends anchor tag to text/String containing http:// or https://
        /// </summary>
        /// <param name="text">String to append tag for links</param>
        /// <returns>anchor tagged string</returns> 
        public string AppendAnchorTag(string text)
        {
            string anchoredString = "";
            try
            {
                foreach (string str in text.Split(new string[] { " ", "\r\n" }, StringSplitOptions.None))
                {
                    if (str.Contains("http://") || str.Contains("https://"))
                        anchoredString += " <a target='_blank' href=" + str + ">" + str + "</a> ";
                    else
                        anchoredString += " " + str;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, _className, "AppendAnchorTag");
            }
            return anchoredString;
        }
        #endregion

        #region GetAlternateCharFromString
        /// <summary>
        /// Get Alternate Character From String
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>returns the resultant string</returns>                                                                                      
        public static string GetAlternateCharFromString(string input)
        {
            string sReturn = string.Empty;
            input = input.Replace("-", string.Empty);
            for (int i = 1; i <= input.Length; i++)
            {
                if (i % 2 == 0)
                    sReturn += input[i - 1];
            }
            return sReturn;
        }
        #endregion

        #region RemoveHyphenString
        /// <summary>
        /// Remove hyphen in the input string
        /// </summary>
        /// <param name="input">input string</param>
        /// <returns>returns the string without hypen</returns>
        public static string RemoveHyphenString(string input)
        {
            string sReturn = string.Empty;
            sReturn = input.Replace("-", string.Empty);
            return sReturn;
        }
        #endregion

        #region CreateHyphenString
        /// <summary>
        /// Create hyphen string in the input string 
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="splitsize">split size count to include hyphen</param>
        /// <returns>returns the string with hyphen</returns>
        public static string CreateHyphenString(string input, int splitsize)
        {
            string sReturn = string.Empty;
            input = input.Replace("-", string.Empty);
            for (int i = 1; i <= input.Length; i++)
            {
                if (i % splitsize == 0 && i != input.Length)
                    sReturn += (input[i - 1] + "-");
                else
                    sReturn += input[i - 1];
            }
            return sReturn;
        }
        #endregion

        #region ReverseString
        /// <summary>
        /// Reverse the given input string
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns>returns the reverse string</returns>
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        #endregion

        #region SplitString
        /// <summary>
        /// Split Key to get values
        /// </summary>
        /// <param name="decKey">decrypted key</param>
        /// <returns>returns split list string values</returns>
        public static List<string> SplitString(string decKey, List<int> lstKeyType)
        {
            List<string> lstKey = new List<string>();
            decKey = decKey.Replace("-", string.Empty);
            int curPos = 0;
            foreach (int i in lstKeyType)
            {
                lstKey.Add(decKey.Substring(curPos, i));
                curPos += i;
            }
            return lstKey;
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
