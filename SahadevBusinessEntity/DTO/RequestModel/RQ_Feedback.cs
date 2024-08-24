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
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for feedback
    /// </summary>
    public class RQ_Feedback
    {
        /// <summary>
        /// EventID
        /// </summary>
        [JsonPropertyName("event_id")]
        public int EventID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        [JsonPropertyName("user_id")]
        public int UserID { get; set; }

        /// <summary>
        /// PlatformID
        /// </summary>
        [JsonPropertyName("platform_id")]
        public int PlatformID { get; set; }

        /// <summary>
        /// FTID
        /// </summary>
        [JsonPropertyName("type_id")]
        public int FTID { get; set; }

        /// <summary>
        /// RecordID
        /// </summary>
        [JsonPropertyName("record_id")]
        public int RecordID { get; set; }

        /// <summary>
        /// ScreenName 
        /// </summary>
        [JsonPropertyName("screen_name")]
        public string ScreenName { get; set; }

        /// <summary>
        /// FeedbackDescription
        /// </summary>
        [JsonPropertyName("description")]
        public string FeedbackDescription { get; set; }

    }
}
