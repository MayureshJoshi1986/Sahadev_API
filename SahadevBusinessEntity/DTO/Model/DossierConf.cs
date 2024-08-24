using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public  class DossierConf
    {
        public int DossierConfID { get; set; }

        [ForeignKey("DossierDef")]
        public int DossierDefID  { get; set; }
        public string ConfJSON { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public DossierDef DossierDef { get; set; }
    }
}
