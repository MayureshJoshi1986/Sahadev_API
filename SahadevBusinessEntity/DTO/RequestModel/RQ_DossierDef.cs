/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierDef                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for DossierDef                                             *
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
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for DossierDef
    /// </summary>
    public class RQ_DossierDef
    {

        [JsonPropertyName("dossier_def_id")]
        public int DossierDefID { get; set; }

        [JsonPropertyName("client_id")]
        public int ClientID { get; set; }

        [JsonPropertyName("dossier_type_id")]
        public int DossierTypeID { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("schedule_type_id")]
        public int ScheduleTypeID { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("event_type_id")]
        public int EventTypeID { get; set; }

        [JsonPropertyName("event_context")]
        public string EventContext { get; set; }

        [JsonPropertyName("event_ref_url")]
        public string EventRefURL { get; set; }

        [JsonPropertyName("event_query")]
        public string EventKQuery { get; set; }

        [JsonPropertyName("event_tag_id")]
        public int EventTagID { get; set; }

        [JsonPropertyName("platform1")]
        public int Platform1ID { get; set; }

        [JsonPropertyName("platform2")]
        public int Platform2ID { get; set; }

        [JsonPropertyName("platform3")]
        public int Platform3ID { get; set; }

        [JsonPropertyName("status_id")]
        public int StatusID { get; set; }

        #region DossierSch

        [JsonPropertyName("dossier_sch_id")]
        public int DossierSchID { get; set; }

        [JsonPropertyName("time1")]
        public string Time1 { get; set; }

        [JsonPropertyName("time2")]
        public string Time2 { get; set; }

        [JsonPropertyName("day_of_week")]
        public int DayOfWeek { get; set; }

        [JsonPropertyName("day_of_month")]
        public int DayOfMonth { get; set; }

        [JsonPropertyName("last_run")]
        public DateTime LastRun { get; set; }

        [JsonPropertyName("next_run")]
        public DateTime NextRun { get; set; }
        #endregion

        #region DossierConf
        [JsonPropertyName("dossier_conf_id")]
        public int DossierConfID { get; set; }

        [JsonPropertyName("json")]
        public string ConfJSON { get; set; }
        #endregion

        [JsonPropertyName("tag_group")]
        public List<RQ_DossierTagGroup> TagGroup { get; set; }

        [JsonPropertyName("recipient")]
        public List<RQ_DossierRecep> Recipient { get; set; }

        #region DossierTagGroup        
        //[JsonPropertyName("dossier_tag_group_id")]
        //public int DossierTagGroupID { get; set; }

        //[JsonPropertyName("taggroup_id")]
        //public int TGID { get; set; }

        //[JsonPropertyName("tag_id")]
        //public int TagID { get; set; }

        //[JsonPropertyName("type_of_binding")]
        //public int TypeOfBinding { get; set; }
        #endregion

        #region DossierRecep
        //[JsonPropertyName("dossier_recep_id")]
        //public int DossierRecepID { get; set; }

        //[JsonPropertyName("user_id")]
        //public int UserID { get; set; }
        #endregion

        //public RQ_DossierSch DossierSch { get; set; }

        //public RQ_DossierConf DossierConf { get; set; }

        //public RQ_DossierTagGroup DossierTagGroup { get; set; }

        //public RQ_DossierRecep DossierRecep { get; set; }
    }
}
