/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  DossierDef                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier DossierDef                                 *
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
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class DossierDef
    {
        public int DossierDefID { get; set; }
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public int DossierTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ScheduleTypeID { get; set; }
        public string Title { get; set; }
        public int EventTypeID { get; set; }
        public string EventContext { get; set; }
        public string EventRefURL { get; set; }
        public string EventKQuery { get; set; }
        public int EventTagID { get; set; }
        public int Platform1ID { get; set; }
        public int Platform2ID { get; set; }
        public int Platform3ID { get; set; }
        public int StatusID { get; set; }
        public string TemplateFileName { get; set; }

        public DossierSch DossierSch { get; set; }

        public DossierConf DossierConf { get; set; }

        public List<DossierTagGroup> DossierTagGroup { get; set; }

        public List<DossierRecep> DossierReceps { get; set; }

        public List<Dossier> Dossiers { get; set; }
    }
}
