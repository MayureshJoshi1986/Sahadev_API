using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class DossierDef
    {
        public int DossierDefID { get; set; }
        public int ClientID { get; set; }
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
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public DossierSch DossierSch { get; set; }

        public DossierConf DossierConf { get; set; }

        public DossierTagGroup DossierTagGroup { get; set; }

        public DossierRecep DossierRecep { get; set; }
    }
}
