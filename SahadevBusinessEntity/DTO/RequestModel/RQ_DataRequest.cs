/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DataRequest                                                           *
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
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for DataRequest
    /// </summary>
    public class RQ_DataRequest
    {
        //public int DataRequestID {  get; set; }
        [JsonPropertyName("user_id")]
        public int UserID { get; set; }

        [JsonPropertyName("event_id")]
        public int EventID { get; set; }

        [JsonPropertyName("platform_id")]
        public int PlatformID { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusID { get; set; }

        [JsonPropertyName("filter_json")]
        public string FilterJson { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }
    }
}
