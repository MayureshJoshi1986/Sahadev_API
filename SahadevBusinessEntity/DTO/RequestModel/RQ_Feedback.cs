/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_Event                                                                 *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is request model for feedback detail                                *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 22-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for feedback
    /// </summary>
    public class RQ_Feedback
    {
        /// <summary>
        /// event_id
        /// </summary>
        public int event_id { get; set; }

        /// <summary>
        /// user_id
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// platform_id
        /// </summary>
        public int platform_id { get; set; }

        /// <summary>
        /// type_id
        /// </summary>
        public int type_id { get; set; }

        /// <summary>
        /// record_id
        /// </summary>
        public int record_id { get; set; }

        /// <summary>
        /// screen_name 
        /// </summary>
        public string screen_name { get; set; }

        /// <summary>
        /// description
        /// </summary>
        public string description { get; set; }

    }
}
