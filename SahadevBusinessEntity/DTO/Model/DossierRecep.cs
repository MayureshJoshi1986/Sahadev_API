using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class DossierRecep
    {
        public int DossierRecepID { get; set; }
        public int DossierDefID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public DossierDef DossierDef { get; set; }
    }
}
