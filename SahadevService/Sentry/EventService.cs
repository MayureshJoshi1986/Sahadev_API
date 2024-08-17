/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- EventService                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is EventService class which contains all method related to Event    *
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
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevDBLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevService.Sentry
{
    /// <summary>
    /// Interface ClientService class  
    /// </summary>
    interface IEventService
    {

    }
    public class EventService : IEventService
    {
        private const string _className = "SahadevService.Common.EventService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;


        /// <summary>
        /// Constructor defined for EventService class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public EventService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }

        /// <summary>
        /// This method is used to insert event detail in Event table
        /// </summary>
        /// <param name="objEvent">object containing Event detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertEvent(Event objEvent)
        {
            bool bReturn = false;
            try
            {
                 //bReturn = uw.SahadevC2Repository.InsertEvent(objEvent);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "InsertEvent");
            }
            return bReturn;
        }


    }
}
