/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  Event`                                                                  *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Event                                              *
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
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public int EventTypeID { get; set; }
        public int ClientID { get; set; }
        public string RefArticleURL { get; set; }
        public string Keywords { get; set; }
        public string Query {  get; set; }
        public int Platform1 { get; set; }
        public int Platform2 { get; set; }
        public int Platform3 { get; set; }
        public int Platform4 { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusID { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }
        public List<DataRequest> DataRequests { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifieddOn { get; set; }

    }
}
