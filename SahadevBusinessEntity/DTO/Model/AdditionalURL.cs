using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public class AdditionalURL
    {
        public int AdditionaURLlID { get; set; }
        public string URL { get;   set;}
        public int DossierID { get; set; }
        public bool IsProcessed {  get; set; }   
        public int RefLinkID { get; set; }
        public int TryCount {  get; set; }  
        public string ErrorMsg { get; set; }    
        public int CreatedBy { get; set; }  
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public Dossier Dossier { get; set; }
    }
}
