/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierTagQuery                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for TagQuery in Dossier                                    *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 28-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for RQ_DossierTagQuery
    /// </summary>
    public class RQ_DossierTagQuery
    {
        [JsonPropertyName("platform_id")]
        public int PlatformID { get; set; }

        [JsonPropertyName("tag_query")]
        public int TagQuery { get; set; }
    }
}
