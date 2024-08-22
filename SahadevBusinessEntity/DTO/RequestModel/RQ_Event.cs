/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_Event                                                                 *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is request model for event detail                                   *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 17-Aug-2024                                                              *
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
    /// RQ_Event
    /// </summary>
    public class RQ_Event
    {
        /// <summary>
        /// EventID
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// ClientID
        /// </summary>
        [JsonPropertyName("client_id")]
        public int ClientID { get; set; }

        /// <summary>
        /// EventTypeID
        /// </summary>
        [JsonPropertyName("type_id")]
        public int EventTypeID { get; set; }

        /// <summary>
        /// EventName
        /// </summary>
        [JsonPropertyName("name")]
        public string EventName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// RefArticleURL
        /// </summary>
        [JsonPropertyName("URL")]
        public string RefArticleURL { get; set; }

        /// <summary>
        /// Keywords 
        /// </summary>
        [JsonPropertyName("keywords")]
        public string Keywords { get; set; }

        /// <summary>
        /// Query
        /// </summary>
        [JsonPropertyName("query")]
        public string Query { get; set; }


        /// <summary>
        /// Platform1 
        /// </summary>
        [JsonPropertyName("platform1")]
        public int Platform1 { get; set; }

        /// <summary>
        /// Platform2
        /// </summary>
        [JsonPropertyName("platform2")]
        public int Platform2 { get; set; }


        /// <summary>
        /// Platform3 
        /// </summary>
        [JsonPropertyName("platform3")]
        public int Platform3 { get; set; }

        /// <summary>
        /// Platform4
        /// </summary>
        [JsonPropertyName("platform4")]
        public int Platform4 { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// StatusID
        /// </summary>
        [JsonPropertyName("status_id")]
        public int StatusID { get; set; }

    }
}
