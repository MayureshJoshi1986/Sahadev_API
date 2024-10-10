using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// Client table
    /// </summary>
    public class Client
    {
        /// <summary>
        /// ClientID
        /// </summary>
        [Key]
        public int ClientID { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// RegisteredName
        /// </summary>
        public string RegisteredName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// BSEListed
        /// </summary>
        public bool BSEListed { get; set; }

        /// <summary>
        /// NSEListed
        /// </summary>
        public bool NSEListed { get; set; }

        /// <summary>
        /// CoreTagID 
        /// </summary>
        public bool CoreTagID { get; set; }

        /// <summary>
        /// ActivationFrom
        /// </summary>
        public DateTime ActivationFrom { get; set; }

        /// <summary>
        /// ValidUntil
        /// </summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// </summary>
        /// </summary>
        public int SupportUserID { get; set; }

        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// ModifiedAt
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

    }
}
