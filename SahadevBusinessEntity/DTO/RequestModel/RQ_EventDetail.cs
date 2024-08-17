/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- RS_ClientDetail                                                          *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is request model for event detail                                   *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 17-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using SahadevBusinessEntity.DTO.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.RequestModel
{
    /// <summary>
    /// RQ_EventDetail
    /// </summary>
    public class RQ_EventDetail
    {
        /// <summary>
        /// lstEvent
        /// </summary>
        public Event objEvent { get; set; }

        /// <summary>
        /// objClientTopic
        /// </summary>
        public ClientTopic objClientTopic { get; set; }

        /// <summary>
        /// Tag
        /// </summary>
        public Tag objTag { get; set; }        

        /// <summary>
        /// objTagMap
        /// </summary>
        public TagMap objTagMap{ get; set; }

        /// <summary>
        /// objTagQuery
        /// </summary>
        public TagQuery objTagQuery { get; set; }
    }
}
