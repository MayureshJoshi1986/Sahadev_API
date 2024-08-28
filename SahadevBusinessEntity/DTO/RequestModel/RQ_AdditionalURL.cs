﻿/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_AdditionalURL                                                         *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for Dossier AdditionalURL                                  *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 27-Aug-2024                                                              *
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
    /// Request model for AdditionalURL
    /// </summary>
    public class RQ_AdditionalURL
    {
        /// <summary>
        /// URL
        /// </summary>
        [JsonPropertyName("url")]
        public string URL { get; set; }

        /// <summary>
        /// DossierID
        /// </summary>
        [JsonPropertyName("dossier_id")]
        public int DossierID { get; set; }

        /// <summary>
        /// IsProcessed
        /// </summary
        [JsonPropertyName("is_processed")]
        public bool IsProcessed { get; set; }


        /// <summary>
        /// RefLinkID
        /// </summary
        [JsonPropertyName("ref_link_id")]
        public int RefLinkID { get; set; }


        /// <summary>
        /// TryCount
        /// </summary
        [JsonPropertyName("try_count")]
        public int TryCount { get; set; }


        /// <summary>
        /// ErrorMsg
        /// </summary
        [JsonPropertyName("error_msg")]
        public string ErrorMsg { get; set; }

        /// <summary>
        /// ErrorMsg
        /// </summary
        [JsonPropertyName("created_by")]
        public int CreatedBy { get; set; }
    }
}