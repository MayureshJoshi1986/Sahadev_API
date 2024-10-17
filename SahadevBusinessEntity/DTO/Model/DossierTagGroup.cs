/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :-  DossierTagGroup                                                         *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier DossierTagGroup                            *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 24-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :- PJ                                                                       *
 *  revised On      :- 17-Oct-2024                                                              *
 *  revised Details :- Added new parameter IsSync                                               *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/

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
        public bool IsSync { get; set; }

        DossierDef DossierDef { get; set; }
    }
}
