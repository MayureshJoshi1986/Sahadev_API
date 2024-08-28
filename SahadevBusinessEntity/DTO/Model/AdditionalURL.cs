/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- AdditionalURL                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Dossier AdditionalURL                              *
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
    public class AdditionalURL
    {
        public int AdditionaURLlID { get; set; }
        public string URL { get; set; }
        public int DossierID { get; set; }
        public bool IsProcessed { get; set; }
        public int RefLinkID { get; set; }
        public int TryCount { get; set; }
        public string ErrorMsg { get; set; }
        public int CreatedBy { get; set; }
    }
}
