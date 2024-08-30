/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  Feedback                                                                *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Feedback Entity Model for the Feedback                                  *
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
    public  class Feedback
    {
        public int FeedbackID { get; set; }
        public int EventID  { get; set; }
        public int UserID { get; set; }
        public int PlatformID  { get; set; }
        public int FTID { get; set; }
        public int RecordID { get; set; }
        public string ScreenName {  get; set; }
        public string Description {  get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
