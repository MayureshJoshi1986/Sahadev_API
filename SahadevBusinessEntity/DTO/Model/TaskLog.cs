using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class TaskLog
    {
        /// <summary>
        /// TLID
        /// </summary>
        [Key]
        public int TLID { get; set; }

        /// <summary>
        /// TagID
        /// </summary>
        public int TaskID { get; set; }

        /// <summary>
        /// PlatformID
        /// </summary>
        public int FromStatusID { get; set; }

        /// <summary>
        /// ToStatusID
        /// </summary>
        public int ToStatusID { get; set; }

        /// <summary>
        /// StartTime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// EndTime
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// ModifiedAt
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }
}
