 /*  -------------------------------------------------------------------------------------------*
 *  Class Name      :- ClientService                                                            *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is ClientService class which contains all method related to client  *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- MS                                                                       *                                                                 
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 10/Apr/2014                                                              *
 *  --------------------------------------------------------------------------------------------* 
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevDBLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SahadevService
{
    /// <summary>
    /// ClientService class Interface 
    /// </summary>
    interface IClientService
    {
        List<Client> GetDetail();
    }

    /// <summary>
    /// ClientService
    /// </summary>
    public class ClientService : IClientService
    {
        private const string _className = "SahadevService.ClientService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor defined for client service class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public ClientService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }

        /// <summary>
        /// This method is used to get all client detail 
        /// </summary>
        /// <returns>list of object containing client detail</returns>
        /// <createdon>14-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<Client> GetDetail()
        {
            try
            {
                List<Client> lstClient = uw.ClientRepository.Get();
                return lstClient;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetDetail");
                return null;
            }
        }

        /// <summary>
        /// This method is used to insert client detail in client table
        /// </summary>
        /// <param name="objClient">object containing client detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>14-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool Insert(Client objClient)
        {
            bool bReturn = false;
            try
            {
                bReturn = uw.ClientRepository.Insert(objClient);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "Insert");
            }
            return bReturn;
        }


    }
}
