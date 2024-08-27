using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class DossierTagGroup
    {
        public int DossierTagGroupID { get; set; }
        [ForeignKey("DossierDef")]
        public int DossierDefID { get; set; }
        public int TGID { get; set; }
        public int TagID { get; set; }
        public int TypeOfBinding { get; set; }
    }
}
