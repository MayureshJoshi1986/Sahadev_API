/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- Dossier                                                                  *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 26-Aug-2024                                                              *
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
