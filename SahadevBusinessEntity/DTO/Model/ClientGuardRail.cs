/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- ClientGuardRail                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the ClientGuardRail                                     *
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
    public class ClientGuardRail
    {
        public int GRID { get; set; }
        public int ClientID { get; set; }
        public int GRType { get; set; }
        public string GRValue { get; set; }
        public bool isActive { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
