/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- BookMark                                                                 *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- BookMark request model                                                   *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 18-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// BookMark class
    /// </summary>
    public class BookMark
    {
        /// <summary>
        /// Action
        /// </summary>
        [JsonPropertyName("action")]
        public string Action { get; set; }

        /// <summary>
        /// BookMarkID
        /// </summary>
        [JsonPropertyName("bookmark_id")]
        public int BookMarkID{ get; set; }

        /// <summary>
        /// EventID
        /// </summary
        [JsonPropertyName("event_id")]
        public int EventID { get; set; }


        /// <summary>
        /// UserID
        /// </summary
        [JsonPropertyName("user_id")]
        public int UserID { get; set; }


        /// <summary>
        /// PlateformID
        /// </summary
        [JsonPropertyName("platform_id")]
        public int PlateformID { get; set; }


        /// <summary>
        /// RecordID
        /// </summary
        [JsonPropertyName("record_id")]
        public int RecordID { get; set; }

    }
}
