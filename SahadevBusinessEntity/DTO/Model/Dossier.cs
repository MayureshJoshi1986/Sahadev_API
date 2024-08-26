using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class Dossier
    {
        public int DossierID { get; set; }
        public int DossierDefID { get; set; }
        public int StatusID { get; set; }
        public string OutputFileName { get; set; }
        public int MachineNo { get; set; }
        public int TryCount { get; set; }
        public string ErrorMsg { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public DossierDef DossierDef { get; set; }

        public List<AdditionalURL> AdditionalUrls { get; set; }
    }
}
