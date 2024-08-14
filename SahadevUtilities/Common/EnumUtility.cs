/***********************************************************************************************/
/*  Copy right issue :- This source file is property of Millicent Technologies.                 *
 *  --------------------------------------------------------------------------------------------*
 *  Class Name      :- EnumUtility                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This class is used for Enum Supported Value Util of the system           *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- MS                                                                       *                                                                 
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 12/July/2014                                                             *
 *  --------------------------------------------------------------------------------------------* 
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 /**********************************************************************************************/
#region Used Namespace
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
#endregion

namespace SahadevUtilities.Common
{
    /// <summary>
    /// This class consist of common method related to ENUM
    /// </summary>
    public class EnumUtility
    {
        #region stringValueOf
        /// <summary>
        /// This method is used to get the description attribute value of enum
        /// </summary>
        /// <param name="value">The enum value for which the description attribute has to retrieved</param>
        /// <returns>The description attribute value</returns>
        public static string stringValueOf(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        #endregion

        #region enumValueOf
        /// <summary>
        ///  To get name of value of type Enumeration
        /// </summary>
        /// <param name="value">the string value to look for</param>
        /// <param name="enumType">Type of Enum in which it has to find</param>
        /// <returns>object returning name</returns>
        public static object enumValueOf(string value, Type enumType)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (stringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return Enum.Parse(enumType, name);
                }
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }
        #endregion

    }
}
