/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  DossierConf                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier DossierConf                                *
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
    public  class DossierConf
    {
        public int DossierConfID { get; set; }
        public int DossierDefID  { get; set; }
        public string ConfJSON { get; set; }

        public DossierDef DossierDef { get; set; }
    }
}
