/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  DossierSch                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier DossierSch                                 *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 24-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class DossierSch
    {
        public int DossierSchID { get; set; }

        [ForeignKey("DossierDef")]
        public int DossierDefID { get; set; }
        public int ScheduleTypeID { get; set; }
        public DateTime? Time1 { get; set; }
        public DateTime? Time2 { get; set; }
        public int DayOfWeek { get; set; }
        public int DayOfMonth { get; set; }
        public DateTime LastRun { get; set; }
        public DateTime NextRun { get; set; }

        public DossierDef DossierDef { get; set; }

        

    }
}
