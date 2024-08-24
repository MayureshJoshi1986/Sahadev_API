/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- DossierService                                                           *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is DossierService class which contains all method related to        *
 *                     Dossier                                                                  *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 22-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Dapper;
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.RequestModel;
using SahadevDBLayer.UnitOfWork;
using SahadevService.Sentry;
using System;
using System.Collections.Generic;
using System.Text;

using System.Transactions;
namespace SahadevService.Dossier
{

    /// <summary>
    /// Interface DossierService class  
    /// </summary>
    interface IDossierService
    {

    }

    public class DossierService : IDossierService
    {
        private const string _className = "SahadevService.DossierService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor defined for DossierService class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public DossierService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }


        /// <summary>
        /// This method is used to get all client by TagId
        /// by fecthing first tagID's from mstTagGroupTable against cometitor name (tagGroupName)
        /// and then taking all tagID and fetching All client for TagId assigned
        /// </summary>
        /// <returns>list of object containing client</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<Client> GetAllClientsByTagID(string tagGroupName)
        {
            try
            {
                var lstTagID = uw.C3Repository.GetAllTagIDByTagGroupName(tagGroupName);
                string strTagID = String.Join(",", lstTagID);
                var data = uw.C1Repository.GetAllClientByTagID(strTagID);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllClientsByTagID");
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to get all client by user id
        /// </summary>
        /// <returns>list of object containing client</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<Client> GetAllClientByUserID(int userID)
        {
            try
            {
                var data = uw.C1Repository.GetAllClientByUserID(userID);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllClientsByUserID");
                throw ex;
            }

        }




        /// <summary>
        /// This method is used to get all user
        /// </summary>
        /// <returns>list of object containing usert</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<User> GetAllUser()
        {
            try
            {
                var data = uw.C1Repository.GetAllUser();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllUser");
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to insert DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        /// </summary>
        /// <param name="objDossierDef">request object DossierDef</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertDossierDef(DossierDef objDossierDef)
        {
            bool bReturn = false;
            try
            {
                //Insert into the DossierDef Table and get the PrimaryKey of DossierDef
                int dossierDefID = uw.C3Repository.InsertDossierDef(objDossierDef);


                //Assign DossierDefID to the DossierRecep and Insert Data
                if (objDossierDef.DossierRecep != null)
                {
                    DossierRecep objDossierRecep = objDossierDef.DossierRecep;
                    objDossierRecep.DossierDefID = dossierDefID;
                    uw.C3Repository.InsertDossierRecep(objDossierRecep);
                }

                //Assign DossierDefID to the DossierSch and Insert Data
                if (objDossierDef.DossierSch != null)
                {
                    DossierSch objDossierSch = objDossierDef.DossierSch;
                    objDossierSch.DossierDefID = dossierDefID;
                    uw.C3Repository.InsertDossierSch(objDossierSch);
                }

                //Assign DossierDefID to the DossierConf and Insert Data
                if (objDossierDef.DossierConf != null)
                {
                    DossierConf objDossierConf = objDossierDef.DossierConf;
                    objDossierConf.DossierDefID = dossierDefID;
                    uw.C3Repository.InsertDossierConf(objDossierConf);

                }

                //Assign DossierDefID to the DossierTagGroup and Insert Data
                if (objDossierDef.DossierTagGroup != null)
                {
                    DossierTagGroup objDossierTagGroup = objDossierDef.DossierTagGroup;
                    objDossierTagGroup.DossierDefID = dossierDefID;
                    uw.C3Repository.InsertDossierTagGroup(objDossierTagGroup);
                }

                //Commit the change 
                uw.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertDossierDef");
            }
            return bReturn;
        }

    }
}
