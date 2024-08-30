/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- DataRequest                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- DataRequest request model                                                *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 30-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
     public  class DataRequest
    {
        public int DataRequestID { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }
        public int PlatformID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FilterJson { get; set; }
        public int StatusID { get; set; }

        public Event Event { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
