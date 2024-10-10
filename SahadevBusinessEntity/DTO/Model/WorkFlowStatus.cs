using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class WorkFlowStatus
    {
       
        /// <summary>
        /// TagID
        /// </summary>
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
        /// ToStatusID
        /// </summary>
        public int ToStatusID { get; set; }

        /// <summary>
        /// FromStatusID
        /// </summary>
        public int FromStatusID { get; set; }
    }
}
