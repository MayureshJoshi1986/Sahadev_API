/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_AdditionalURL                                                         *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for Dossier AdditionalURL                                  *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 27-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  ModifiedOn     :- 01-Oct-2024                                                               *
 *  ModifiedBy     :- PJ                                                                        *
 *  ModifiedReason :- Removed unwanted fields like isProcessed, ErrorMsg etc;                   *
 *                    Changed parameter for multiple URLs                                       *
 //**********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{

    /// <summary>
    /// Request model for AdditionalURL
    /// </summary>
    public class RQ_AdditionalURL
    {
        /// <summary>
        /// DossierID
        /// </summary>
        [JsonPropertyName("dossier_id")]
        public int DossierID { get; set; }

        /// <summary>
        /// URL
        /// </summary>
        [JsonPropertyName("url")]
        public List<string> URL { get; set; }

        /// <summary>
        /// ErrorMsg
        /// </summary
        [JsonPropertyName("created_by")]
        public int CreatedBy { get; set; }
    }
}
