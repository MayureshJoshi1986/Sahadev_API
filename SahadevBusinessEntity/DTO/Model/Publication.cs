/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- Publication                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :-  Entity Model for the Publication                                        *
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
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevBusinessEntity.DTO.Model
{
    public  class Publication
    {
        public int PublicationID { get; set; }
        public string PublicationName { get; set; }
        public string PublicationCategory { get; set; }
        public string Language { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int DA { get; set; }
        public int GlobalRank { get; set; }
        public string Country { get; set; }
        public int CountryRank { get; set; }
        public long TotalVisits { get; set; }
        public int AverageVisitDurationInSec { get; set; }
        public int AverageVisitDurationInMin {  get; set; } 
        public int AvgPagesPerVisit { get; set; }
        public decimal BounceRate { get; set; }
        public decimal TrafficFromIndia { get; set; }
        public string LogoUrl { get; set; } 
        public int PlatformType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}
