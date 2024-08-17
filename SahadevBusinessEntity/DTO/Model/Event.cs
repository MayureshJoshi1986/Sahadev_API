using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// Event table
    /// </summary>
    public class Event
    {
        /// <summary>
        /// EventID
        /// </summary>
        public int EventID { get; set; }

        /// <summary>
        /// ClientID
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// EventTypeID
        /// </summary>
        public int EventTypeID { get; set; }

        /// <summary>
        /// EventName
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// RefArticleURL
        /// </summary>
        public string RefArticleURL { get; set; }

        /// <summary>
        /// Keywords 
        /// </summary>
        public string Keywords { get; set; }

        /// <summary>
        /// Query
        /// </summary>
        public string Query { get; set; }


        /// <summary>
        /// Platform1 
        /// </summary>
        public int Platform1 { get; set; }

        /// <summary>
        /// Platform2
        /// </summary>
        public int Platform2 { get; set; }


        /// <summary>
        /// Platform3 
        /// </summary>
        public int Platform3 { get; set; }

        /// <summary>
        /// Platform4
        /// </summary>
        public int Platform4 { get; set; }

        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// StatusID
        /// </summary>
        public int StatusID { get; set; }

        /// <summary>
        /// TagID
        /// </summary>
        public int TagID { get; set; }

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
