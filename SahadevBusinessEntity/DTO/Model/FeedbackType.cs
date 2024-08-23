using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{

    /// <summary>
    /// FeedbackType table
    /// </summary>
    public class FeedbackType
    {
        /// <summary>
        /// FTID
        /// </summary>
        [Key]
        public int FTID { get; set; }

        /// <summary>
        /// FeedbackType
        /// </summary>
        public string FeedbackTypeName { get; set; }
    }
}
