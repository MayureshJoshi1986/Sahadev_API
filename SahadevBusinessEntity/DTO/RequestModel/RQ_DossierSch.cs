/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RQ_DossierSch                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- Request model for DossierSch                                             *
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
using System;
using System.Text.Json.Serialization;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// Request model for DossierSch
    /// </summary>
    public class RQ_DossierSch
    {
        [JsonPropertyName("dossier_sch_id")]
        public int DossierSchID { get; set; }

        [JsonPropertyName("dossier_def_id")]
        public int DossierDefID { get; set; }

        [JsonPropertyName("schedule_ype_id")]
        public int ScheduleTypeID { get; set; }

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
    }
}
