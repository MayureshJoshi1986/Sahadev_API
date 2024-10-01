/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierReviewLinks                                                    *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for Dossier Review Links                                   *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 07-Sep-2024                                                              *
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
    /// Request model for Dossier Link to Review 
    /// </summary>
    public class RQ_DossierReviewLinks
    {
        [JsonPropertyName("dossier_link_map_id")]
        public int DossierLinkMapID { get; set; }

        [JsonPropertyName("edits_json")]
        public string EditsJson { get; set; }

        [JsonPropertyName("sentiment")]
        public string Sentiment {  get; set; }

        [JsonPropertyName("article_mention")]
        public string ArticleMention { get; set; }

        [JsonPropertyName("link_Id")]
        public int LinkID { get; set; }

        [JsonPropertyName("dossier_Id")]
        public int DossierID { get; set; }

        [JsonPropertyName("platform_Id")]
        public int PlatformID { get; set; }


       
    }
}
