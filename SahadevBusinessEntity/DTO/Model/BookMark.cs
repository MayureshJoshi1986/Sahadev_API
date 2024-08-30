/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- BookMark                                                                 *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- BookMark request model                                                   *
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
    public  class BookMark
    {
        public int BookMarkID { get; set; }
        public int EventID { get; set; }
        public int UserID { get; set; }
        public int PlatformID { get; set; }
        public int RecordID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
