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
using SahadevBusinessEntity.DTO.ResultModel;
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
        List<dynamic> GetAllClientsByTagID(int tgID);
        List<dynamic> GetAllClientByUserID(int userID);
        List<dynamic> GetAllUser();
        DossierDef GetDossierDef(int dossierDefID);
        List<dynamic> GetAllDossier();
        List<dynamic> GetAllGeneratedDossier();
        dynamic GetGeneratedDossier(int dossierConfID);
        List<RS_AdditionalURL> GetAllAdditionalURL(int dossierID);
        bool InsertDossierDef(RQ_DossierDef objRQ_DossierDef);
        bool InsertAdditionalURL(RQ_AdditionalURL objRQ_AdditonalURL);
        bool UpdateDossierDef(RQ_DossierDef objRQ_DossierDef);
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
        public List<dynamic> GetAllClientsByTagID(int tgID)
        {
            try
            {
                var lstTagID = uw.C3Repository.GetAllTagIDByTagGroupName(tgID);
                string strTagID = String.Join(",", lstTagID);
                List<dynamic> lstGetAllClientByTagID = uw.C1Repository.GetAllClientByTagID(strTagID);
                return lstGetAllClientByTagID;
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
        public List<dynamic> GetAllClientByUserID(int userID)
        {
            try
            {
                List<dynamic> lstGetAllClientByUserID = uw.C1Repository.GetAllClientByUserID(userID);
                return lstGetAllClientByUserID;
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
        /// <returns>list of object containing user</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllUser()
        {
            try
            {
                List<dynamic> lstAllUser = uw.C1Repository.GetAllUser();
                return lstAllUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllUser");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get DossierDef Detail with all related Table
        /// </summary>
        /// <returns>object containing DossierDef and its related table Detail</returns>
        /// <param name="dossierDefID">dossierDefID</param>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public DossierDef GetDossierDef(int dossierDefID)
        {
            try
            {
                DossierDef objDossierDef = uw.C3Repository.GetDossierDef(dossierDefID);

                if (objDossierDef != null)
                {
                    objDossierDef.DossierReceps = uw.C3Repository.GetAllDossierRecep(dossierDefID);
                    objDossierDef.DossierSch = uw.C3Repository.GetDossierSch(dossierDefID);
                    objDossierDef.DossierConf = uw.C3Repository.GetDossierConf(dossierDefID);
                    objDossierDef.DossierTagGroup = uw.C3Repository.GetDossierTagGroup(dossierDefID);
                }

                return objDossierDef;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetDossierDef");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get All Dossier
        /// </summary>
        /// <returns>object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossier()
        {
            try
            {
                dynamic objDossier = uw.C3Repository.GetAllDossier();

                return objDossier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllDossier");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get All Generated Dossier 
        /// </summary>
        /// <returns>list of object containing All Generated Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllGeneratedDossier()
        {
            try
            {
                List<dynamic> lstDossiers = uw.C3Repository.GetAllGeneratedDossier();
                return lstDossiers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllGeneratedDossier");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get GeneratedDossier of particular configuration
        /// </summary>
        /// <returns>list of object containing Dossier</returns>
        /// <param name="dossierConfID">DossierConfID</param>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public dynamic GetGeneratedDossier(int dossierConfID)
        {
            try
            {
                dynamic objDossier = uw.C3Repository.GetGeneratedDossier(dossierConfID);
                return objDossier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetGeneratedDossier");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get all additonal URLs of a dossier
        /// </summary>
        /// <returns>list of object containing all additional URLs of a dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<RS_AdditionalURL> GetAllAdditionalURL(int dossierID)
        {
            try
            {
                List<RS_AdditionalURL> lstAdditionalURL = uw.C3Repository.GetAllAdditionalUrl(dossierID);
                return lstAdditionalURL;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, " GetAllAdditionalURL");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to insert DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        /// </summary>
        /// <param name="objRQ_DossierDef">request object DossierDef</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>27-Aug-2024</modifiedby>
        /// <modifiedreason>Changed request model and handled condition accordingly</modifiedreason>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>30-Aug-2024</modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertDossierDef(RQ_DossierDef objRQ_DossierDef)
        {
            bool bReturn = false;
            try
            {
                //Insert into the DossierDef Table and get the PrimaryKey of DossierDef
                DossierDef objDossierDef = new DossierDef();
                objDossierDef.DossierDefID = objRQ_DossierDef.DossierDefID;
                objDossierDef.ClientID = objRQ_DossierDef.ClientID;
                objDossierDef.DossierTypeID = objRQ_DossierDef.DossierTypeID;
                objDossierDef.StartDate = objRQ_DossierDef.StartDate;
                objDossierDef.EndDate = objRQ_DossierDef.EndDate;
                objDossierDef.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierDef.Title = objRQ_DossierDef.Title;
                objDossierDef.EventTypeID = objRQ_DossierDef.EventTypeID;
                objDossierDef.EventContext = objRQ_DossierDef.EventContext;
                objDossierDef.EventRefURL = objRQ_DossierDef.EventRefURL;
                objDossierDef.EventKQuery = objRQ_DossierDef.EventKQuery;
                objDossierDef.EventTagID = objRQ_DossierDef.EventTagID;
                objDossierDef.Platform1ID = objRQ_DossierDef.Platform1ID;
                objDossierDef.Platform2ID = objRQ_DossierDef.Platform2ID;
                objDossierDef.Platform3ID = objRQ_DossierDef.Platform3ID;
                objDossierDef.StatusID = objRQ_DossierDef.StatusID;
                int dossierDefID = uw.C3Repository.InsertDossierDef(objDossierDef);

                //Insert into DossierRecep table
                foreach (var objRecipient in objRQ_DossierDef.Recipient)
                {
                    DossierRecep objDossierRecep = new DossierRecep();
                    objDossierRecep.DossierDefID = dossierDefID;
                    objDossierRecep.UserID = objRecipient.UserID;
                    uw.C3Repository.InsertDossierRecep(objDossierRecep);
                }

                //Insert into DossierSch table
                DossierSch objDossierSch = new DossierSch();
                objDossierSch.DossierDefID = dossierDefID;
                objDossierSch.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierSch.Time1 = objRQ_DossierDef.Time1;
                objDossierSch.Time2 = objRQ_DossierDef.Time2;
                objDossierSch.DayOfWeek = objRQ_DossierDef.DayOfWeek;
                objDossierSch.DayOfMonth = objRQ_DossierDef.DayOfMonth;
                objDossierSch.LastRun = objRQ_DossierDef.LastRun;
                objDossierSch.NextRun = objRQ_DossierDef.NextRun;
                uw.C3Repository.InsertDossierSch(objDossierSch);

                //Insert into DossierConf table
                if (!string.IsNullOrEmpty(objRQ_DossierDef.ConfJSON))
                {
                    DossierConf objDossierConf = new DossierConf();
                    objDossierConf.DossierDefID = dossierDefID;
                    objDossierConf.ConfJSON = objRQ_DossierDef.ConfJSON;
                    uw.C3Repository.InsertDossierConf(objDossierConf);
                }

                //Insert into DossierTagGroup table
                //RQ_DossierTagGroup objRQ_DossierTagGroup = new RQ_DossierTagGroup();
                //objRQ_DossierTagGroup.DossierDefID = dossierDefID;
                //objRQ_DossierTagGroup.TGID = objRQ_DossierDef.TGID;
                //objRQ_DossierTagGroup.TagID = objRQ_DossierDef.TagID;
                //objRQ_DossierTagGroup.TypeOfBinding = objRQ_DossierDef.TypeOfBinding;
                //uw.C3Repository.InsertDossierTagGroup(objRQ_DossierTagGroup);

                //Insert into DossierTagGroup table
                foreach (var objTagGroup in objRQ_DossierDef.TagGroup)
                {
                    DossierTagGroup objDossierTagGroup = new DossierTagGroup();
                    objDossierTagGroup.DossierDefID = dossierDefID;
                    objDossierTagGroup.TGID = objTagGroup.TGID;
                    objDossierTagGroup.TagID = objTagGroup.TagID;
                    objDossierTagGroup.TypeOfBinding = objTagGroup.TypeOfBinding;
                    uw.C3Repository.InsertDossierTagGroup(objDossierTagGroup);
                }

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

        /// <summary>
        /// This method is used to insert AdditionalURL in AdditionalURL table
        /// </summary>
        /// <param name="objRQ_AdditonalURL">request object containing AdditionalURL</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertAdditionalURL(RQ_AdditionalURL objRQ_AdditonalURL)
        {
            bool bReturn = false;
            try
            {
                bReturn = uw.C3Repository.InsertAdditionalURl(objRQ_AdditonalURL);

                //Commit the change 
                uw.Commit();
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertAdditionalURL");
            }
            return bReturn;
        }

        /// <summary>
        /// This method is used to Update DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        /// </summary>
        /// <param name="objRQ_DossierDef">request object DossierDef</param>
        /// <returns>true if successfully Update else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>27-Aug-2024</modifiedby>
        /// <modifiedreason>Changed request model and handled condition accordingly</modifiedreason>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>30-Aug-2024</modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierDef(RQ_DossierDef objRQ_DossierDef)
        {
            bool bReturn = false;
            try
            {
                DossierDef objDossierDef = new DossierDef();
                objDossierDef.DossierDefID = objRQ_DossierDef.DossierDefID;
                objDossierDef.ClientID = objRQ_DossierDef.ClientID;
                objDossierDef.DossierTypeID = objRQ_DossierDef.DossierTypeID;
                objDossierDef.StartDate = objRQ_DossierDef.StartDate;
                objDossierDef.EndDate = objRQ_DossierDef.EndDate;
                objDossierDef.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierDef.Title = objRQ_DossierDef.Title;
                objDossierDef.EventTypeID = objRQ_DossierDef.EventTypeID;
                objDossierDef.EventContext = objRQ_DossierDef.EventContext;
                objDossierDef.EventRefURL = objRQ_DossierDef.EventRefURL;
                objDossierDef.EventKQuery = objRQ_DossierDef.EventKQuery;
                objDossierDef.EventTagID = objRQ_DossierDef.EventTagID;
                objDossierDef.Platform1ID = objRQ_DossierDef.Platform1ID;
                objDossierDef.Platform2ID = objRQ_DossierDef.Platform2ID;
                objDossierDef.Platform3ID = objRQ_DossierDef.Platform3ID;
                objDossierDef.StatusID = objRQ_DossierDef.StatusID;
                uw.C3Repository.UpdateDossierDef(objDossierDef);

                //Update DossierRecep
                foreach (var objRecipient in objRQ_DossierDef.Recipient)
                {
                    DossierRecep objDossierRecep = new DossierRecep();
                    objDossierRecep.DossierRecepID = objRecipient.DossierRecepID;
                    objDossierRecep.DossierDefID = objRQ_DossierDef.DossierDefID;
                    objDossierRecep.UserID = objRecipient.UserID;
                    uw.C3Repository.UpdateDossierRecep(objDossierRecep);
                }

                //Update DossierSch
                DossierSch objDossierSch = new DossierSch();
                objDossierSch.DossierSchID = objRQ_DossierDef.DossierSchID;
                objDossierSch.DossierDefID = objRQ_DossierDef.DossierDefID;
                objDossierSch.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierSch.Time1 = objRQ_DossierDef.Time1;
                objDossierSch.Time2 = objRQ_DossierDef.Time2;
                objDossierSch.DayOfWeek = objRQ_DossierDef.DayOfWeek;
                objDossierSch.DayOfMonth = objRQ_DossierDef.DayOfMonth;
                objDossierSch.LastRun = objRQ_DossierDef.LastRun;
                objDossierSch.NextRun = objRQ_DossierDef.NextRun;
                uw.C3Repository.UpdateDossierSch(objDossierSch);

                //Update DossierConf 
                if (!string.IsNullOrEmpty(objRQ_DossierDef.ConfJSON))
                {
                    DossierConf objDossierConf = new DossierConf();
                    objDossierConf.DossierConfID = objRQ_DossierDef.DossierConfID;
                    objDossierConf.DossierDefID = objRQ_DossierDef.DossierDefID;
                    objDossierConf.ConfJSON = objRQ_DossierDef.ConfJSON;
                    uw.C3Repository.UpdateDossierConf(objDossierConf);
                }

                //Update DossierTagGroup
                foreach (var objTagGroup in objRQ_DossierDef.TagGroup)
                {
                    DossierTagGroup objDossierTagGroup = new DossierTagGroup();
                    objDossierTagGroup.DossierTagGroupID = objTagGroup.DossierTagGroupID;
                    objDossierTagGroup.DossierDefID = objRQ_DossierDef.DossierDefID;
                    objDossierTagGroup.TGID = objTagGroup.TGID;
                    objDossierTagGroup.TagID = objTagGroup.TagID;
                    objDossierTagGroup.TypeOfBinding = objTagGroup.TypeOfBinding;
                    uw.C3Repository.UpdateDossierTagGroup(objDossierTagGroup);
                }


                //Commit the change 
                uw.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "UpdateDossierDef");
            }
            return bReturn;
        }

        ///// <summary>
        ///// This method is used to insert DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        ///// </summary>
        ///// <param name="objRQ_DossierDef">request object DossierDef</param>
        ///// <returns>true if successfully inserted else false</returns>
        ///// <createdon>23-Aug-2024</createdon>
        ///// <createdby>Saroj Laddha</createdby>
        ///// <modifiedon></modifiedon>
        ///// <modifiedby></modifiedby>
        ///// <modifiedreason></modifiedreason>
        //public bool InsertDossierDef(DossierDef objRQ_DossierDef)
        //{
        //    bool bReturn = false;
        //    try
        //    {
        //        //Insert into the DossierDef Table and get the PrimaryKey of DossierDef
        //        int dossierDefID = uw.C3Repository.InsertDossierDef(objRQ_DossierDef);


        //        //Assign DossierDefID to the DossierRecep and Insert Data
        //        if (objRQ_DossierDef.DossierRecep != null)
        //        {
        //            //DossierRecep objDossierRecep = objRQ_DossierDef.DossierRecep;
        //            objRQ_DossierDef.DossierRecep.DossierDefID = dossierDefID;
        //            uw.C3Repository.InsertDossierRecep(objRQ_DossierDef.DossierRecep);
        //        }

        //        //Assign DossierDefID to the DossierSch and Insert Data
        //        if (objRQ_DossierDef.DossierSch != null)
        //        {
        //            //DossierSch objDossierSch = objDossierDef.DossierSch;
        //            objRQ_DossierDef.DossierSch.DossierDefID = dossierDefID;
        //            uw.C3Repository.InsertDossierSch(objRQ_DossierDef.DossierSch);
        //        }

        //        //Assign DossierDefID to the DossierConf and Insert Data
        //        if (objRQ_DossierDef.DossierConf != null)
        //        {
        //            //DossierConf objDossierConf = objDossierDef.DossierConf;
        //            objRQ_DossierDef.DossierConf.DossierDefID = dossierDefID;
        //            uw.C3Repository.InsertDossierConf(objRQ_DossierDef.DossierConf);

        //        }

        //        //Assign DossierDefID to the DossierTagGroup and Insert Data
        //        if (objRQ_DossierDef.DossierTagGroup != null)
        //        {
        //            //DossierTagGroup objDossierTagGroup = objDossierDef.DossierTagGroup;
        //            objRQ_DossierDef.DossierTagGroup.DossierDefID = dossierDefID;
        //            uw.C3Repository.InsertDossierTagGroup(objRQ_DossierDef.DossierTagGroup);
        //        }

        //        //Commit the change 
        //        uw.Commit();
        //        bReturn = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        uw.Rollback();
        //        _logger.LogError(ex, _className, "InsertDossierDef");
        //    }
        //    return bReturn;
        //}


        ///// <summary>
        ///// This method is used to Update DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        ///// </summary>
        ///// <param name="objRQ_DossierDef">request object DossierDef</param>
        ///// <returns>true if successfully Update else false</returns>
        ///// <createdon>26-Aug-2024</createdon>
        ///// <createdby>Saroj Laddha</createdby>
        ///// <modifiedon></modifiedon>
        ///// <modifiedby></modifiedby>
        ///// <modifiedreason></modifiedreason>
        //public bool UpdateDossierDef(RQ_DossierDef objRQ_DossierDef)
        //{
        //    bool bReturn = false;
        //    try
        //    {
        //        uw.C3Repository.UpdateDossierDef(objRQ_DossierDef);

        //        objRQ_DossierDef.DossierRecep.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        uw.C3Repository.UpdateDossierRecep(objRQ_DossierDef.DossierRecep);

        //        objRQ_DossierDef.DossierSch.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        uw.C3Repository.UpdateDossierSch(objRQ_DossierDef.DossierSch);

        //        objRQ_DossierDef.DossierConf.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        uw.C3Repository.UpdateDossierConf(objRQ_DossierDef.DossierConf);

        //        objRQ_DossierDef.DossierTagGroup.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        uw.C3Repository.UpdateDossierTagGroup(objRQ_DossierDef.DossierTagGroup);

        //        //Commit the change 
        //        uw.Commit();
        //        bReturn = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        uw.Rollback();
        //        _logger.LogError(ex, _className, "UpdateDossierDef");
        //    }
        //    return bReturn;
        //}








    }
}
