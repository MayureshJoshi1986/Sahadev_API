/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  DossierRecep                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier DossierRecep                               *
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
    public class DossierRecep
    {
        public int DossierRecepID { get; set; }
        public int DossierDefID { get; set; }
        public int UserID { get; set; }

        public DossierDef DossierDef { get; set; }

    }
}
