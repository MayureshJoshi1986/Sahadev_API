using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// Feedback table
    /// </summary>
    public class Feedback
    {
        /// <summary>
        /// FeedbackID
        /// </summary>
        [Key]
        public int FeedbackID { get; set; }

        /// <summary>
        /// EventID
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// PlatformID
        /// </summary>
        public int PlatformID { get; set; }

        /// <summary>
        /// FTID
        /// </summary>
        public int FTID { get; set; }

        /// <summary>
        /// RecordID
        /// </summary>
        public int RecordID { get; set; }

        /// <summary>
        /// ScreenName 
        /// </summary>
        public string ScreenName { get; set; }

        /// <summary>
        /// FeedbackDescription
        /// </summary>
        public string FeedbackDescription { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// ModifiedOn
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

    }
}
