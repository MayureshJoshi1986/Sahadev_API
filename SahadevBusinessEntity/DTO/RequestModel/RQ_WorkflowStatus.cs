using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for RQ_WorkflowStatus
    /// </summary>
    public class RQ_WorkflowStatus
    {
        [JsonPropertyName("dossier_id")]
        public int DossierID { get; set; }

        [JsonPropertyName("dossier_link_map_id")]
        public List<int> DossierLinkMapID { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusID { get; set; }

    }
}
