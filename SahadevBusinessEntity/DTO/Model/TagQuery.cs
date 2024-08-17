using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// TagQuery table
    /// </summary>
    public class TagQuery
    {
        /// <summary>
        /// TagQueryID
        /// </summary>
        [Key]
        public int TagQueryID { get; set; }

        /// <summary>
        /// TagID
        /// </summary>
        public int TagID { get; set; }

        /// <summary>
        /// PlatformID
        /// </summary>
        public int PlatformID { get; set; }

        /// <summary>
        /// Query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// TypeOfQuery
        /// </summary>
        public string TypeOfQuery { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

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
