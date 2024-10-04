/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_Tag                                                    *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for Tag                               *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
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
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    public class RQ_Tag
    {
        [JsonPropertyName("tag_id")]
        public int TagID { get; set; }

        [JsonPropertyName("ig_tag_id")]
        public int? IGTagID { get; set; }

        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }

        [JsonPropertyName("tag_description")]
        public string TagDescription { get; set; }

        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }
    }
}
