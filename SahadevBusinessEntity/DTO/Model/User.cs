/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  User                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the User                                *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 24-Aug-2024                                                              *
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
    public class User
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string EmailID { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt
        {
            get; set;

        }
    }
}
