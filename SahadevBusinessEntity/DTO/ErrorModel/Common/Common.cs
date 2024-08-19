/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- Common                                                                   *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is Common class which contan all common error message               *
 *                     related to different                                                     *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 19-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Error.Common
{
    /// <summary>
    /// Common Class
    /// </summary>
    public class Common
    {
        /// <summary>
        /// This is Event class which contain all error message related to Event 
        /// </summary>
        public class Event
        {
            #region Event
            /// <summary>
            /// SDCOM001: Something went wrong. Please try again.
            /// </summary>
            public const string SDCOM001 = "SDCOM001: Something went wrong. Please try again.";

            /// <summary>
            /// SDCOM002: Failed to add {0} detail. Please try again.
            /// </summary>
            public const string SDCOM002 = "SDCOM002: Failed to add {0} detail. Please try again.";

            /// <summary>
            /// {0} detail added successfully.
            /// </summary>
            public const string SDCOM003 = "{0} detail added successfully.";

            /// <summary>
            /// {0} detail added successfully.
            /// </summary>
            public const string SDCOM004 = "{0} detail updated successfully.";
            #endregion
        }

    }
}
