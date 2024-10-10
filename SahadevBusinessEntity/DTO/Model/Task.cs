using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class Task
    {
        /// <summary>
        /// TaskID
        /// </summary>
        [Key]
        public int TaskID { get; set; }

        /// <summary>
        /// TTID
        /// </summary>
        public int TTID { get; set; }

        /// <summary>
        /// RefID
        /// </summary>
        public int RefID { get; set; }

        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// AssignedTo
        /// </summary>
        public int AssignedTo { get; set; }

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
