/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- C3Repository                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is C3Repository class which contains all functions &                *
 *                     SP related to SahadevC3 repository                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 17-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  Changed class name from SahadevC3Repository to C3Repository             *  
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  Added new methods GetAllDossierScheduleType  & GetAllDossierEventType   *  
 //**********************************************************************************************/

using SahadevBusinessEntity.DTO.Model;
using System.Collections.Generic;
using System;
using System.Data;
using Dapper;
using SahadevBusinessEntity.DTO.RequestModel;
using SahadevBusinessEntity.DTO.ResultModel;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of C3Repository class
    /// </summary>
    public interface IC3Repository
    {
        //List<FeedbackType> GetFeedbackType();

        List<string> GetAllTagIDByTagGroupName(int tgID);
        int InsertDossierDef(DossierDef objDossier);
        int InsertDossierRecep(DossierRecep objDossierRecep);
        int InsertDossierSch(DossierSch objDossierSch);
        int InsertDossierConf(DossierConf objDossierConf);
        int InsertDossierTagGroup(DossierTagGroup objDossierTagGroup);

        bool UpdateDossierDef(DossierDef objDossier);
        bool UpdateDossierRecep(DossierRecep objDossierRecep);
        bool UpdateDossierSch(DossierSch objDossierSch);
        bool UpdateDossierConf(DossierConf objDossierConf);
        bool UpdateDossierTagGroup(DossierTagGroup objDossierTagGroup);

        DossierDef GetDossierDef(int DossierDefID);
        List<DossierRecep> GetAllDossierRecep(int DossierDefID);
        DossierSch GetDossierSch(int DossierDefID);
        DossierConf GetDossierConf(int DossierDefID);
        List<DossierTagGroup> GetDossierTagGroup(int DossierDefID);

        List<dynamic> GetAllDossier(int[] clientID, int statusID, int dossierDefID, int userID, string userType, DateTime? startDate = null, DateTime? endDate = null);
        List<dynamic> GetAllGeneratedDossier(int UserID, int[] ClientID, int StatusID, DateTime? StartDate = null, DateTime? EndDate = null);
        List<dynamic> GetGeneratedDossier(int dossierDefID);

        List<AdditionalURL> GetAllAdditionalUrl(int dossierID);
        bool InsertAdditionalURl(AdditionalURL objAdditionalURL);


        List<dynamic> GetAllDossierReviewDataLinks(int dossierID, int platformID);
        List<dynamic> GetAllDossierTrashDataLinks(int dossierID, int platformID);
        List<dynamic> GetAllDossierReviewDraftDataLinks(int dossierID, int platformID);

        bool MoveToTrash(string dossierLinkMapID);
        bool SaveToDraft(string dossierLinkMapID);
        bool UpdateDataAfterEdit(int dossierLinkMapID, string editJson, int dossierID);
        List<TagQuery> GetAllTagQueryByTagID(int tagId);
        Tag GetTagByTaID(int tagId);


        List<dynamic> GetAllDossierScheduleType();

        void InsertTag(Tag objTag);

        void InsertTagQuery(TagQuery objTagQuery);

        bool DeleteDossierTagGroup(int dossierTagGroupId);

        bool UpdateTagQuery(TagQuery objTagQuery);

        bool InsertAddlUrlInDataLinkMap(string url, bool isAdded, int platformId, int dossierId);

        #region Task & Notification
        List<dynamic> GetDossierTaskStatus(string clientID, int userID, string userType);
        bool UpdateDosserDefStatus(int dossierDefID, int statusID);
        bool UpdateDosserStatus(int dossierID, int statusID);
        bool UpdateDraftStatus(int dossierID, string dossierLinkMapID);
        #endregion
    }

    internal class C3Repository : RepositoryBase, IC3Repository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public C3Repository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }


        /// <summary>
        /// This method is used to get fetch client detail from All Tag ID from 
        /// </summary>
        /// <param name="tagGroupName">providing competitor name or tag group name to fecth all related tags for the company</param>
        /// <returns>list of object containing list of TagID</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<string> GetAllTagIDByTagGroupName(int tgID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tgID", tgID);
                var data = GetAllByProcedure<string>(@"[dbo].[USP_Competitor_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// To fetch the Data links that moved to trash 
        /// </summary>
        /// <param name="dossierID">To fetch the links of particular dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing list of LinkID's  and DossierLinkMapID that are moved To trash</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierTrashDataLinks(int dossierID, int platformID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", dossierID);
                dbparams.Add("@platformID", platformID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Review_FetchTrashLinks]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// To fetch the Data links that are moved to draft for the review
        /// </summary>
        /// <param name="dossierID">To fetch the links of particular dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing list of LinkID's  and DossierLinkMapID that are moved To draft for the review</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierReviewDraftDataLinks(int dossierID, int platformID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", dossierID);
                dbparams.Add("@platformID", platformID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Review_FetchDossierDraftLinks]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// To fetch the initial Review Data link to verify
        /// </summary>
        /// <param name="dossierID">To fetch the links of particular dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing list of LinkID's  and DossierLinkMapID for the verifcation</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierReviewDataLinks(int dossierID, int platformID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", dossierID);
                dbparams.Add("@platformID", platformID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Review_FetchReviewDataLinks]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #region Fetch


        /// <summary>
        /// This method is used to fetch Dossier detail from DossierDef Table
        /// </summary>
        ///  <param name="dossierDefID">dossierDefID to Fetch DossierDef </param>
        /// <returns>object containing DossierDef detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public DossierDef GetDossierDef(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetByProcedure<DossierDef>(@"[dbo].[USP_DossierDef_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        /// <summary>
        /// This method is used to fetch DossierRecep detail from DossierRecep Table
        /// </summary>
        /// <param name="dossierDefID">dossierDefID to Fetch DossierRecep records for the dossier</param>
        /// <returns>object containing DossierRecep detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>@8-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Converted into returning single object to lost of object</modifiedreason>

        public List<DossierRecep> GetAllDossierRecep(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetAllByProcedure<DossierRecep>(@"[dbo].[USP_DossierRecep_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





        /// <summary>
        /// This method is used to fetch DossierSch detail from DossierSch Table
        /// </summary>
        /// <param name="dossierDefID">dossierDefID to Fetch DossierSch record for the dossier</param>
        /// <returns>object containing DossierSch detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public DossierSch GetDossierSch(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetByProcedure<DossierSch>(@"[dbo].[USP_DossierSch_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        /// <summary>
        /// This method is used to fetch DossieConf detail from DossierConf Table
        /// </summary>
        /// <param name="dossierDefID">dossierDefID to Fetch DossierConf record for the dossier</param>
        /// <returns>object containing DossierConf detail</returns>

        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public DossierConf GetDossierConf(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetByProcedure<DossierConf>(@"[dbo].[USP_DossierConf_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to fetch DossierTagGroup detail from DossierTagGroup Table
        /// </summary>
        /// <param name="dossierDefID">dossierDefID to Fetch DossierTagGroup record for the dossier</param>
        /// <returns>list of object containing DossierTagGroup detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public List<DossierTagGroup> GetDossierTagGroup(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetAllByProcedure<DossierTagGroup>(@"[dbo].[USP_DossierTagGroup_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to fetch All Dossier from Dossier Table
        /// </summary>
        /// <returns>list of object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>26-Sep-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changes to handle multiple clientID</modifiedreason>

        public List<dynamic> GetAllDossier(int[] clientID, int statusID, int dossierDefID, int userID, string userType, DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", clientID.Length != 0 ? string.Join(",", clientID) : string.Empty);
                dbparams.Add("@statusID", statusID);
                dbparams.Add("@dossierDefID", dossierDefID);
                dbparams.Add("@userID", userID);
                dbparams.Add("@userType", userType.ToLower());
                dbparams.Add("@startDate", startDate);
                dbparams.Add("@endDate", endDate);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_DossierConfiguration_FetchAll]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to fetch All Generated Dossier  from Dossier Table
        /// </summary>
        /// <returns>list of object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>26-Sep-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changes to handle multiple clientID</modifiedreason>

        public List<dynamic> GetAllGeneratedDossier(int UserID, int[] ClientID, int StatusID, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@userID", UserID);
                dbparams.Add("@ClientID", ClientID.Length != 0 ? string.Join(",", ClientID) : string.Empty);
                dbparams.Add("@StatusID", StatusID);
                dbparams.Add("@startDate", StartDate);
                dbparams.Add("@endDate", EndDate);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_GeneratedDossier_FetchAll]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to fetch Generated Dossier  from Dossier Table
        /// </summary>
        /// <param name="dossierDefID">dossierDefID to Fetch GeneratedDossier record for the dossier</param>
        /// <returns>object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public List<dynamic> GetGeneratedDossier(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_GeneratedDossier_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to fetch all AdditionalUrl of a Dossier from AdditionalUrl Table
        /// </summary>
        /// <param name="dossierID">dossierID to Fetch All AditionalURl record for the dossier</param>
        /// <returns>list of object containing AdditionalURL list</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public List<AdditionalURL> GetAllAdditionalUrl(int dossierID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", dossierID);
                var data = GetAllByProcedure<AdditionalURL>(@"[dbo].[USP_AdditionalUrl_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to fetch all dossier schedule type from mstScheduleType table
        /// </summary>
        /// <returns>list of object containing dossier schedule type</returns>
        /// <createdon>26-Sept-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public List<dynamic> GetAllDossierScheduleType()
        {
            try
            {
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_DossierScheduleType_FetchAll]", null, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion



        #region Insert


        /// <summary>
        /// This method is used to insert DossierDef detail in DossierDef Table
        /// </summary>
        /// <param name="objDossier">object containing DossierDef</param>
        /// <returns>PK of DossierDef if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public int InsertDossierDef(DossierDef objDossier)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", objDossier.ClientID);
                dbparams.Add("@dossierTypeID", objDossier.DossierTypeID);
                dbparams.Add("@startDate", objDossier.StartDate);
                dbparams.Add("@endDate", objDossier.EndDate);
                dbparams.Add("@scheduleTypeID", objDossier.ScheduleTypeID);
                dbparams.Add("@title", objDossier.Title);
                dbparams.Add("@eventTypeID", objDossier.EventTypeID);
                dbparams.Add("@eventContext", objDossier.EventContext);
                dbparams.Add("@eventRefURL", objDossier.EventRefURL);
                dbparams.Add("@eventKQuery", objDossier.EventKQuery);
                dbparams.Add("@eventTagID", objDossier.EventTagID);
                dbparams.Add("@platform1ID", objDossier.Platform1ID);
                dbparams.Add("@platform2ID", objDossier.Platform2ID);
                dbparams.Add("@platform3ID", objDossier.Platform3ID);
                dbparams.Add("@statusID ", objDossier.StatusID);
                dbparams.Add("@templateFileName ", objDossier.TemplateFileName);
                dbparams.Add("@clientName", objDossier.ClientName);
                iResult = GetByProcedure<int>(@"[dbo].[USP_DossierDef_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }



        /// <summary>
        /// This method is used to insert DossierRecep detail in DossierRecep Table
        /// </summary>
        /// <param name="objDossierRecep">object containing DossierRecepf</param>
        /// <returns>PK of DossierRecep if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public int InsertDossierRecep(DossierRecep objDossierRecep)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierRecep.DossierDefID);
                dbparams.Add("@userID", objDossierRecep.UserID);
                iResult = GetByProcedure<int>(@"[dbo].[USP_DossierRecep_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }


        /// <summary>
        /// This method is used to insert DossierSch detail in DossierSch Table
        /// </summary>
        /// <param name="objDossierSch">object containing DossierSch</param>
        /// <returns>PK of DossierSch if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public int InsertDossierSch(DossierSch objDossierSch)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierSch.DossierDefID);
                dbparams.Add("@scheduleTypeID", objDossierSch.ScheduleTypeID);
                dbparams.Add("@time1", objDossierSch.Time1);
                dbparams.Add("@time2", objDossierSch.Time2);
                dbparams.Add("@dayOfWeek", objDossierSch.DayOfWeek);
                dbparams.Add("@dayOfMonth", objDossierSch.DayOfMonth);
                //dbparams.Add("@lastRun", objDossierSch.LastRun);
                //dbparams.Add("@nextRun", objDossierSch.NextRun);
                iResult = GetByProcedure<int>(@"[dbo].[USP_DossierSch_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }



        /// <summary>
        /// This method is used to insert DossierConf detail in DossierConf Table
        /// </summary>
        /// <param name="objDossierConf">object containing DossierConf</param>
        /// <returns>PK of DossierConf if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public int InsertDossierConf(DossierConf objDossierConf)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierConf.DossierDefID);
                dbparams.Add("@confJSON", objDossierConf.ConfJSON);
                iResult = GetByProcedure<int>(@"[dbo].[USP_DossierConf_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }

        /// <summary>
        /// This method is used to insert DossierTagGroup detail in DossierTagGroup Table
        /// </summary>
        /// <param name="objDossierTagGroup">object containing DossierTagGroup</param>
        /// <returns>PK of DossierTagGroup if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public int InsertDossierTagGroup(DossierTagGroup objDossierTagGroup)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierTagGroup.DossierDefID);
                dbparams.Add("@tgID", objDossierTagGroup.TGID);
                dbparams.Add("@tagID", objDossierTagGroup.TagID);
                dbparams.Add("@typeOfBinding", objDossierTagGroup.TypeOfBinding);
                iResult = GetByProcedure<int>(@"[dbo].[USP_DossierTagGroup_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }


        /// <summary>
        /// This method is used to insert Additional URL detail in AdditionalURL Table
        /// </summary>
        /// <param name="objAdditonalURl">object containing AdditonalURl</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>27-Aug-2024</modifiedby>
        /// <modifiedreason>Changed request model and return type from int to bool</modifiedreason>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool InsertAdditionalURl(AdditionalURL objAdditionalURL)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", objAdditionalURL.DossierID);
                dbparams.Add("@url", objAdditionalURL.URL);
                dbparams.Add("@createdBy", objAdditionalURL.CreatedBy);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_AdditionalUrl_Insert]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }


        #endregion



        #region Update

        /// <summary>
        /// This method is used to Update DossierDef detail in DossierDef Table
        /// </summary>
        /// <param name="objDossier">object containing DossierDef</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool UpdateDossierDef(DossierDef objDossier)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();

                dbparams.Add("@dossierDefID", objDossier.DossierDefID);
                dbparams.Add("@clientID", objDossier.ClientID);
                dbparams.Add("@dossierTypeID", objDossier.DossierTypeID);
                dbparams.Add("@startDate", objDossier.StartDate);
                dbparams.Add("@endDate", objDossier.EndDate);
                dbparams.Add("@scheduleTypeID", objDossier.ScheduleTypeID);
                dbparams.Add("@title", objDossier.Title);
                dbparams.Add("@eventTypeID", objDossier.EventTypeID);
                dbparams.Add("@eventContext", objDossier.EventContext);
                dbparams.Add("@eventRefURL", objDossier.EventRefURL);
                dbparams.Add("@eventKQuery", objDossier.EventKQuery);
                dbparams.Add("@eventTagID", objDossier.EventTagID);
                dbparams.Add("@platform1ID", objDossier.Platform1ID);
                dbparams.Add("@platform2ID", objDossier.Platform2ID);
                dbparams.Add("@platform3ID", objDossier.Platform3ID);
                dbparams.Add("@statusID", objDossier.StatusID);
                dbparams.Add("@templateFileName ", objDossier.TemplateFileName);
                dbparams.Add("@clientName", objDossier.ClientName);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierDef_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }



        /// <summary>
        /// This method is used to Update DossierRecep detail in DossierRecep Table
        /// </summary>
        /// <param name="objDossierRecep">object containing DossierRecep</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool UpdateDossierRecep(DossierRecep objDossierRecep)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierRecepID", objDossierRecep.DossierRecepID);
                dbparams.Add("@dossierDefID", objDossierRecep.DossierDefID);
                dbparams.Add("@userID", objDossierRecep.UserID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierRecep_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }


        /// <summary>
        /// This method is used to Update DossierSch detail in DossierSch Table
        /// </summary>
        /// <param name="objDossierSch">object containing DossierSch</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool UpdateDossierSch(DossierSch objDossierSch)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierSchID", objDossierSch.DossierSchID);
                dbparams.Add("@dossierDefID", objDossierSch.DossierDefID);
                dbparams.Add("@scheduleTypeID", objDossierSch.ScheduleTypeID);
                dbparams.Add("@time1", objDossierSch.Time1);
                dbparams.Add("@time2", objDossierSch.Time2);
                dbparams.Add("@dayOfWeek", objDossierSch.DayOfWeek);
                dbparams.Add("@dayOfMonth", objDossierSch.DayOfMonth);
                // dbparams.Add("@lastRun", objDossierSch.LastRun);
                // dbparams.Add("@nextRun", objDossierSch.NextRun);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierSch_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }



        /// <summary>
        /// This method is used to Update DossierConf detail in DossierConf Table
        /// </summary>
        /// <param name="objDossierConf">object containing DossierConf</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool UpdateDossierConf(DossierConf objDossierConf)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierConfID", objDossierConf.DossierConfID);
                dbparams.Add("@dossierDefID", objDossierConf.DossierDefID);
                dbparams.Add("@confJSON", objDossierConf.ConfJSON);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierConf_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }

        /// <summary>
        /// This method is used to Update DossierTagGroup detail in DossierTagGroup Table
        /// </summary>
        /// <param name="objDossierTagGroup">object containing DossierTagGroup</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool UpdateDossierTagGroup(DossierTagGroup objDossierTagGroup)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierTagGroupID", objDossierTagGroup.DossierTagGroupID);
                dbparams.Add("@dossierDefID", objDossierTagGroup.DossierDefID);
                dbparams.Add("@tgID", objDossierTagGroup.TGID);
                dbparams.Add("@tagID", objDossierTagGroup.TagID);
                dbparams.Add("@typeOfBinding", objDossierTagGroup.TypeOfBinding);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierTagGroup_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }


        /// <summary>
        /// This method is used to delete dossierTagGroup
        /// </summary>
        /// <param name="dossierTagGroupId">pass id to delete dossiertaggroup</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>01-Oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>

        public bool DeleteDossierTagGroup(int dossierTagGroupId)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierTagGroupID", dossierTagGroupId);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierTagGroup_Delete]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }



        /// <summary>
        /// This method is used to Update DossierLinkMap table To mark the IsDeleted = 1 to move data to trash
        /// </summary>
        /// <param name="dossierLinkMapID">object containing dossierLinkMapID to Update the record as deleted</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>06-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool MoveToTrash(string dossierLinkMapID)
        {

            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierLinkMapID", dossierLinkMapID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_Review_MoveToTrash]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }



        /// <summary>
        /// This method is used to Insert additional url in DossierLinkMap table To mark the IsAdded =true
        /// </summary>
        /// <param name="url">additional url to be added in DatalinkMap Table</param>
        /// <param name="dossierId">dossier for which additonal url added</param>
        /// <param name="isAdded">Is Added  true</param>
        /// <param name="platformId">for which platform it is added</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>05-OCT-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool InsertAddlUrlInDataLinkMap(string url, bool isAdded, int platformId, int dossierId)
        {

            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@url", url);
                dbparams.Add("isAdded", isAdded);
                dbparams.Add("@platformID",platformId);
                dbparams.Add("@dossierID", dossierId);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_AddUrlLinkMap_Insert]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }


        /// <summary>
        /// This method is used to Update DossierLinkMap table To mark the IsDraft = 1 to move data to Draft
        /// </summary>
        /// <param name="dossierLinkMapID">object containing dossierLinkMapID to Update the record as Draft</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>06-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool SaveToDraft(string dossierLinkMapID)
        {

            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierLinkMapID", dossierLinkMapID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_Review_SaveDraft]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }


        /// <summary>
        /// This method is used to Update DossierLinkMap table To mark the IsEdit = 1 for records edit and also update DossierEdit 
        /// table to save the old and new updated values
        /// </summary>
        /// <param name="dossierLinkMapID">object containing dossierLinkMapID to Update the record</param>
        /// <param name="editJson">contains old to new record change history json</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>06-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool UpdateDataAfterEdit(int dossierLinkMapID, string editJson, int dossierID)
        {

            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierLinkMapID", dossierLinkMapID);
                dbparams.Add("@editsJSON", editJson);
                dbparams.Add("@dossierID", dossierID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_Review_UpdateDataAfterEdit]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;
        }



        #endregion



        /// <summary>
        /// This method is used to insert tag detail in Tag table (to replicate from A2 master)
        /// </summary>
        /// <param name="objTag">object containing tag detail</param>
        /// <createdon>28-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public void InsertTag(Tag objTag)
        {

            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagID", objTag.TagID);
                dbparams.Add("@igTagID", objTag.IGTagID);
                dbparams.Add("@tagName", objTag.TagName);
                dbparams.Add("@tagDescription", objTag.TagDescription);
                dbparams.Add("@isActive", objTag.IsActive);
                GetByProcedure<int>(@"[dbo].[USP_Tag_Insert]", dbparams, _transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to insert tag query detail in TagQuery table (to replicate from A2 master)
        /// </summary>
        /// <param name="objTagQuery">object containing TagQuery detail</param>
        /// <createdon>28-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public void InsertTagQuery(TagQuery objTagQuery)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagQueryID", objTagQuery.TagQueryID);
                dbparams.Add("@tagID", objTagQuery.TagID);
                dbparams.Add("@platformID", objTagQuery.PlatformID);
                dbparams.Add("@query", objTagQuery.Query);
                dbparams.Add("@typeOfQuery", objTagQuery.TypeOfQuery);
                dbparams.Add("@isActive", objTagQuery.IsActive);
                InsertByProcedure<int>(@"[dbo].[USP_TagQuery_Insert]", dbparams, _transaction);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to update tag query detail in TagQuery table
        /// </summary>
        /// <param name="objTagQuery">object containing TagQuery detail</param>
        /// <returns>true if successfully updated else false</returns>
        /// <createdon>01-Oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>

        public bool UpdateTagQuery(TagQuery objTagQuery)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagQueryId", objTagQuery.TagQueryID);
                dbparams.Add("@tagID", objTagQuery.TagID);
                dbparams.Add("@platformID", objTagQuery.PlatformID);
                dbparams.Add("@query", objTagQuery.Query);
                dbparams.Add("@typeOfQuery", objTagQuery.TypeOfQuery);
                dbparams.Add("@isActive", objTagQuery.IsActive);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_TagQuery_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;


        }




        /// <summary>
        /// This method is used to fetch tag 
        /// get tag details from tag table
        /// </summary>
        /// <param name="tagId">pass client id for which tag need to be fetched</param>

        /// <returns>return tag detail</returns>
        /// <createdon>02-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public Tag GetTagByTaID(int tagId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagId", tagId);
                var data = GetByProcedure<Tag>(@"[dbo].[USP_Tag_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get fetch tagqueries for tag id
        /// </summary>
        /// <param name="tagId">pass tag id for which tagquery to be fetched</param>
        /// <returns>list of object containing tag Queries</returns>
        /// <createdon>02-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<TagQuery> GetAllTagQueryByTagID(int tagId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagId", tagId);
                var data = GetAllByProcedure<TagQuery>(@"[dbo].[USP_TagQuery_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #region Task & Notification
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientID">Comma seperated ClientID</param>
        /// <param name="userID">userID</param>
        /// <param name="userType">userType</roleName>
        /// <returns></returns>
        /// <createdon>11-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetDossierTaskStatus(string clientID, int userID, string userType)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", clientID);
                dbparams.Add("@userID", userID);
                dbparams.Add("@userType", userType.ToLower());
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_GetDossierTaskFetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dossierDefID"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDosserDefStatus(int dossierDefID, int statusID)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                dbparams.Add("@statusID", statusID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierDef_UpdateStatus]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;

                return bReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dossierDefID"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDosserStatus(int dossierID, int statusID)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", dossierID);
                dbparams.Add("@statusID", statusID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_Dossier_UpdateStatus]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;

                return bReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dossierID"></param>
        /// <param name="dossierLinkMapID"></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDraftStatus(int dossierID, string dossierLinkMapID)
        {

            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierID", dossierID);
                dbparams.Add("@dossierLinkMapID", dossierLinkMapID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_DossierLinkMap_UpdateDraftStatus]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;

        }
    }
}
