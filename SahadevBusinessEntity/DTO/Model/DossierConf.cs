using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public  class DossierConf
    {
        public int DossierConfID { get; set; }
        public int DossierDefID  { get; set; }
        public string ConfJSON { get; set; }
    }
}
