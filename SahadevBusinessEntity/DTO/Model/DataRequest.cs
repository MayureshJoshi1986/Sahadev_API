using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{

    /// <summary>
    /// Created On :  18-Aug-2024
    /// Created By: Saroj Laddha
    /// </summary>
    public class DataRequest
    {
        public int DataRequestID {  get; set; }
        public int UserID { get; set; }
        public int EventID  { get; set; }

        public int PlatformID { get; set; }

        public int StatusID  { get; set; }

        public string FilterJson { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get;set; }


    }
}
