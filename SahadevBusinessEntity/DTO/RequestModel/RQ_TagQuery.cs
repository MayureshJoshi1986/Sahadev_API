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
    public class RQ_TagQuery
    {
        [JsonPropertyName("tag_query_id")]
        public int TagQueryID {  get; set; }

        [JsonPropertyName("platform_id")]
        public int PlatformID { get; set; }

        [JsonPropertyName("query")]
        public string Query { get; set; }

        [JsonPropertyName("tag_Id")]
        public int TagID {  get; set; }


    }
}
