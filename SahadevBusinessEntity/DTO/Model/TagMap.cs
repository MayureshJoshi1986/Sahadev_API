using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// TagMap table
    /// </summary>
    public class TagMap
    {
        /// <summary>
        /// TagMapID
        /// </summary>
        [Key]
        public int TagMapID { get; set; }

        /// <summary>
        /// ClientTopicID
        /// </summary>
        public int ClientTopicID { get; set; }

        /// <summary>
        /// TagID
        /// </summary>
        public int TagID { get; set; }

        /// <summary>
        /// IsActive
        /// </summary>
        public int IsActive { get; set; }

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
