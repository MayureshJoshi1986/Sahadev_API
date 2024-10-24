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
using SahadevBusinessEntity.DTO.Model;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SahadevService.ClientDashboard
{
    /// <summary>
    /// Interface of ClientService class  
    /// </summary>
    interface IClientService
    {
        dynamic GetAllIndustries();
        dynamic GetAllClientByIndustry(int industryId);
        dynamic GetAllKeyDetails(int IndustryId, int tgId);
        dynamic GetClientKeyDetails(int tgId, int clientId);
        dynamic GetSupportUser();
        dynamic GetCSTeam(string username);
        dynamic GetClientServiceTeam(int clientId);
        dynamic GetClientServices(int clientId);
        dynamic GetAllCountries();
        dynamic GetAllLanguage();
        dynamic GetAllTpoic();
        dynamic GetClientLinkDetails(int clientId);

       Client GetClientDetail(int clientId);


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
        /// <summary>
        ///  To Fetch All the industries
        /// </summary>
        /// <returns>all the industries</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetAllIndustries()
        {
            try
            {
                return uw.BRepository.GetIndustryAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllIndustries");
                throw ex;
            }
        }

        /// <summary>
        ///  To Fetch clients by industry
        /// </summary>
        /// <param name="IndustryId">pass industry id to get the industry specific clients</param>
        /// <returns>clients</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetAllClientByIndustry(int industryId)
        {
            try
            {
                return uw.A2Repository.GetClientDetailByIndustryID(industryId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllClientByIndustry");
                throw ex;
            }
        }

        /// <summary>
        ///  To Fetch All the key details
        /// </summary>
        /// <param name="IndustryId">pass industry id to get the industry specific key details</param>
        /// <param name="tgId">pass tgId to get tags of the specific tag group of the industry</param>
        /// <returns>all the  key details</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetAllKeyDetails(int IndustryId, int tgId)
        {
            return new List<dynamic>();
        }
        /// <summary>
        ///  To Fetch client selected key details
        /// </summary>
        /// <param name="clientId">pass client id for the getting selected keyDetails</param>
        /// <param name="tgId">pass tgId to get tags of the specific tag group of the industry</param>
        /// <returns>all the  key details</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetClientKeyDetails(int tgId, int clientId)
        {
            return new List<dynamic>();
        }
        /// <summary>
        ///  To Fetch All the Support User
        /// </summary>
        /// <returns>all the  Support User</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetSupportUser()
        {
            try
            {
                return uw.A2Repository.GetSupportExecutive();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetSupportUser");
                throw ex;
            }
        }

        /// <summary>
        ///  To Fetch All the CS Team by searchtext
        /// </summary>
        /// <param name="username">username search text</param>
        /// <returns> CS Team</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetCSTeam(string username)
        {

            try
            {
                return uw.A2Repository.GetClientServicingUser(username);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetCSTeam");
                throw ex;
            }
        }


        /// <summary>
        ///  To Fetch Client services
        /// </summary>
        /// <param name="clientId">fetch by clientID</param>
        /// <returns>all the services</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetClientServices(int clientId)
        {
            return new List<dynamic>();
        }
        /// <summary>
        ///  To Fetch client Service team
        /// </summary>
        /// <param name="clientId">pass client id for the getting client service team </param>
        /// <returns>all the  key details</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetClientServiceTeam(int clientId)
        {
            try
            {
                return uw.A2Repository.GetClientServicingUserByClientID(clientId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetClientServiceTeam");
                throw ex;
            }
        }

        /// <summary>
        ///  To Fetch All the countries
        /// </summary>
        /// <returns>all the Countires</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetAllCountries()
        { return new List<dynamic>(); }

        /// <summary>
        ///  To Fetch All the countries
        /// </summary>
        /// <returns>all the Countires</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetAllLanguage()
        { return new List<dynamic>(); }


        /// <summary>
        ///  To Fetch All theTopic
        /// </summary>
        /// <returns>all the Topic</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetAllTpoic()
        { return new List<dynamic>(); }


        /// <summary>
        ///  To fetch Client GuardRail
        /// </summary>
        /// <param name="clientId">pass client Id to get the client specific guardrail</param>
        /// <returns>all the Topic</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetClientGuardRail(int clientId)
        {
            return new List<dynamic>();
        }

        /// <summary>
        ///  To fetch the client selected groups
        /// </summary>
        /// <param name="clientId">pass client Id to get the client selected groups</param>
        /// <returns>all the Topic</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public dynamic GetClientLinkDetails(int clientId)
        { // need to return CoreTag and Selectd Group TagGroup Name
            return new List<dynamic>();
        }


        /// <summary>
        ///  To fetch the client selected groups
        /// </summary>
        /// <param name="clientId">pass client Id to get the client selected groups</param>
        /// <returns>all the Topic</returns>
        /// <createdon>24-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public Client GetClientDetail(int clientId)
        { 
            return null;
        }


    }


    
}
