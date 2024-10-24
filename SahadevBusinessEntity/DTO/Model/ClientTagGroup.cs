/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- ClientTagGroup                                                               *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the ClientTagGroup                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 24-OCT-2024                                                              *
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
    public  class ClientTagGroup
    {
         public int CTGID { get; set; }
        public int ClientID {  get; set; }
        public int TGID { get; set; }
        public int TagID { get; set; }
        public int TypeOfBinding { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
