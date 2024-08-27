/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierTagGroup                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for DossierTagGroup                                        *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 26-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using SahadevBusinessEntity.DTO.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for DossierTagGroup
    /// </summary>
    public class RQ_DossierTagGroup
    {
        [JsonPropertyName("dossier_tag_group_id")]
        public int DossierTagGroupID { get; set; }
        [JsonPropertyName("dossier_def_id")]
        public int DossierDefID { get; set; }

        [JsonPropertyName("taggroup_id")]
        public int TGID { get; set; }

        [JsonPropertyName("tag_id")]
        public int TagID { get; set; }

        [JsonPropertyName("type_of_binding")]
        public int TypeOfBinding { get; set; }
    }
}
