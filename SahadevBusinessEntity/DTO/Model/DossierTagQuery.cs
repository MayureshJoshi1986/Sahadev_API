/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  DossierTagQuery                                                         *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the DossierTagQuery                                    *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 28-Sept-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.Model
{
    /// <summary>
    /// DossierTagQuery
    /// </summary>
    public class DossierTagQuery
    {
        public int PlatformID { get; set; }
        public int TagID { get; set; }
        public int TagQuery { get; set; }
    }
}
