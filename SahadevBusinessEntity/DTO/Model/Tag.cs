using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// Tag table
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// TagID
        /// </summary>
        [Key]
        public int TagID { get; set; }

        /// <summary>
        /// IGTagID
        /// </summary>
        public int? IGTagID { get; set; }

        /// <summary>
        /// TagName
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// TagDescription
        /// </summary>
        public string TagDescription { get; set; }

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

        public List<Event> Events { get; set; }

        public List<TagQuery> TagQuery { get; set; }



    }
}
