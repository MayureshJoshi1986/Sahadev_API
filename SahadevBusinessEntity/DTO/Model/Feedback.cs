using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// Feedback table
    /// </summary>
    public class Feedback
    {
        ///// <summary>
        ///// FeedbackID
        ///// </summary>
        //[Key]
        //public int FeedbackID { get; set; }

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
