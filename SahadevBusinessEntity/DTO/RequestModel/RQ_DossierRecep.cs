/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierRecep                                                          *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for DossierRecep                                           *
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
    /// Request model for DossierRecep
    /// </summary>
    public class RQ_DossierRecep
    {
        [JsonPropertyName("dossier_recep_id")]
        public int DossierRecepID { get; set; }

        //[JsonPropertyName("dossier_def_id")]
        //public int DossierDefID { get; set; }
        [JsonPropertyName("user_id")]
        public int UserID { get; set; }
    }
}
