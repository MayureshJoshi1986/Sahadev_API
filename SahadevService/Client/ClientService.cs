/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- ClientService                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is ClientService class which contains all method related            *
 *                     to Client Dashboard                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 24-Oct-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Microsoft.Extensions.Logging;
using SahadevDBLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevService.Client
{
    /// <summary>
    /// Interface of ClientService class  
    /// </summary>
    interface IClientService
    {

    }
    public class ClientService : IClientService
    {
        private const string _className = "SahadevService.Client.ClientService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor defined for ClientService class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public ClientService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }
    }
}
