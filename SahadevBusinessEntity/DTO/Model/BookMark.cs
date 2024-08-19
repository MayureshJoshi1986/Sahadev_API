using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{

    /// <summary>
    /// Created On :  18-Aug-2024
    /// Created By: Saroj Laddha
    /// </summary>
    public class BookMark
    {

        /// <summary>
        /// BookMarkID
        /// </summary>
        [Key]
        public int BookMarkID{ get; set; }

        /// <summary>
        /// EventID
        /// </summary
        public int EventID { get; set; }


        /// <summary>
        /// UserID
        /// </summary
        public int UserID { get; set; }


        /// <summary>
        /// PlateformID
        /// </summary
        public int PlateformID { get; set; }


        /// <summary>
        /// RecordID
        /// </summary
        public int RecordID { get; set; }


        /// <summary>
        /// CreatedAt
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// ModifiedAt
        /// </summary>
        public DateTime? ModifiedOn { get; set; }




    }
}
