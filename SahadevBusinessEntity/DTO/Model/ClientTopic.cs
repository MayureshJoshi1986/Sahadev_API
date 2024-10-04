using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// ClientTopic table
    /// </summary>
    public class ClientTopic
    {
        /// <summary>
        /// ClientTopicID
        /// </summary>
        [Key]
        public int ClientTopicID { get; set; }

        /// <summary>
        /// ClientID
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// TopicTypeID
        /// </summary>
        public int TopicTypeID { get; set; }

        /// <summary>
        /// TopicName
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// TopicDescription
        /// </summary>
        public string TopicDescription { get; set; }

        /// <summary>
        /// RefTopicID
        /// </summary>
        public int RefTopicID { get; set; }

        /// <summary>
        /// Status 
        /// </summary>
        public int Status { get; set; }


        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// CreatedOn
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// ModifiedOn
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        public Tag Tag { get; set; }




    }
}
