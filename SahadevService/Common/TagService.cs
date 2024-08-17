﻿/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- TagService                                                               *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is TagService class which contains all method related to tag        *
 *                     i.e Tag, TagMap, TagQuery                                                *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 16-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevDBLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevService.Common
{
    /// <summary>
    /// Interface TagService class  
    /// </summary>
    interface ITagService
    {
       
    }
    public class TagService : ITagService
    {
        private const string _className = "SahadevService.Common.TagService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;


        /// <summary>
        /// Constructor defined for TagService class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public TagService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }
    }

    
}
