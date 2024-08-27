/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierConf                                                           *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for DossierConf                                            *
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
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for DossierConf
    /// </summary>
    public class RQ_DossierConf
    {
        [JsonPropertyName("dossier_conf_id")]
        public int DossierConfID { get; set; }

        [JsonPropertyName("dossier_def_id")]
        public int DossierDefID { get; set; }

        [JsonPropertyName("json")]
        public string ConfJSON { get; set; }
    }
}
