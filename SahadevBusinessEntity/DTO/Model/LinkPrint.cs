/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- LinkPrint                                                                *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the LinkPrint                                          *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 06-SEP-2024                                                              *
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
    public class LinkPrint
    {
        public int LPID { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string UrlHash { get; set; }
        public string Text { get; set; }
        public string Summary { get; set; }
        public int PublicationID { get; set; }
        public int LanguageID { get; set; }
        public int AgencyID { get; set; }
        public int AuthorID { get; set; }
        public string Source { get; set; }
        public string Supplement { get; set; }
        public string NewsLocation { get; set; }
        public long AVE {  get; set; }
        public int MTrackRefID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModiifiedAt { get; set; }

    }
}
