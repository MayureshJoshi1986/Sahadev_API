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
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using SahadevBusinessEntity.DTO.Model;
using System.Collections.Generic;
using System;
using System.Data;
using Dapper;
using SahadevBusinessEntity.DTO.RequestModel;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of C3Repository class
    /// </summary>
    public interface IC3Repository
    {
        //List<FeedbackType> GetFeedbackType();

        List<string> GetAllTagIDByTagGroupName(string tagGroupName);
        int InsertDossierDef(RQ_DossierDef objDossier);
        int InsertDossierRecep(RQ_DossierRecep objRQ_DossierRecep);
        int InsertDossierSch(RQ_DossierSch objRQ_DossierSch);
        int InsertDossierConf(RQ_DossierConf objRQ_DossierConf);
        int InsertDossierTagGroup(RQ_DossierTagGroup objRQ_DossierTagGroup);
        int InsertAdditonalURl(AdditionalURL objAdditionalURL);

        bool UpdateDossierDef(RQ_DossierDef objRQ_Dossier);
        bool UpdateDossierRecep(RQ_DossierRecep objRQ_DossierRecep);
        bool UpdateDossierSch(RQ_DossierSch objRQ_DossierSch);
        bool UpdateDossierConf(RQ_DossierConf objRQ_DossierConf);
        bool UpdateDossierTagGroup(RQ_DossierTagGroup objRQ_DossierTagGroup);

        DossierDef GetDossierDef(int DossierDefID);
        DossierRecep GetDossierRecep(int DossierDefID);
        DossierSch GetDossierSch(int DossierDefID);
        DossierConf GetDossierConf(int DossierDefID);
        DossierTagGroup GetDossierTagGroup(int DossierDefID);

        List<dynamic> GetAllDossier();
        List<dynamic> GetAllGeneratedDossier();
        dynamic GetGeneratedDossier(int dossierConfID);

        List<AdditionalURL> GetAllAdditionalUrl(int dossierID);


    }

    internal class C3Repository:RepositoryBase, IC3Repository
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
        /// <returns>list of object containing list of TagID</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<string> GetAllTagIDByTagGroupName(string tagGroupName)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagGroupName", tagGroupName);
                var data = GetAllByProcedure<string>(@"[dbo].[USP_Competitor_Fetch]", dbparams, _transaction);
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
        /// <returns>object containing DossierRecep detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public DossierRecep GetDossierRecep(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetByProcedure<DossierRecep>(@"[dbo].[USP_DossierRecep_Fetch]", dbparams, _transaction);
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
        /// <returns>list of object containing DossierTagGroup detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public DossierTagGroup GetDossierTagGroup(int dossierDefID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", dossierDefID);
                var data = GetByProcedure<DossierTagGroup>(@"[dbo].[USP_DossierTagGroup_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to fetch All Dossier  from Dossier Table
        /// </summary>
        /// <returns>list of object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public List<dynamic> GetAllDossier()
        {
            try
            {
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_DossierConfiguration_FetchAll]", null, _transaction);
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public List<dynamic> GetAllGeneratedDossier()
        {
            try
            {
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_GenratedDossier_FetchAll]", null, _transaction);
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
        /// <returns>object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>

        public dynamic GetGeneratedDossier(int dossierConfID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierConfID", dossierConfID);
                var data = GetByProcedure<dynamic>(@"[dbo].[USP_GenratedDossier_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to fetch AdditionalUrl of a Dossier  from AdditionalUrl Table
        /// </summary>
        /// <returns>object containing AdddionalUrl lIst</returns>
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
                var data = GetAllByProcedure<AdditionalURL>(@"[dbo].[USP_AdditionlURL_Fetch]", dbparams, _transaction);
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
        /// <param name="objRQ_Dossier">object containing DossierDef</param>
        /// <returns>PK of DossierDef if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierDef(RQ_DossierDef objRQ_Dossier)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", objRQ_Dossier.ClientID);
                dbparams.Add("@dossierTypeID", objRQ_Dossier.DossierTypeID );
                dbparams.Add("@startDate", objRQ_Dossier.StartDate );
                dbparams.Add("@endDate", objRQ_Dossier.EndDate);
                dbparams.Add("@scheduleTypeID", objRQ_Dossier.ScheduleTypeID );
                dbparams.Add("@title", objRQ_Dossier.Title);
                dbparams.Add("@eventTypeID", objRQ_Dossier.EventTypeID );
                dbparams.Add("@eventContext", objRQ_Dossier.EventContext);
                dbparams.Add("@eventRefURL", objRQ_Dossier.EventRefURL );
                dbparams.Add("@eventKQuery", objRQ_Dossier.EventKQuery );
                dbparams.Add("@eventTagID", objRQ_Dossier.EventTagID);
                dbparams.Add("@platform1ID", objRQ_Dossier.Platform1ID);
                dbparams.Add("@platform2ID", objRQ_Dossier.Platform2ID);
                dbparams.Add("@platform3ID", objRQ_Dossier.Platform3ID );
                dbparams.Add("@statusID ", objRQ_Dossier.StatusID);
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierRecep(RQ_DossierRecep objRQ_DossierRecep)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objRQ_DossierRecep.DossierDefID );
                dbparams.Add("@userID", objRQ_DossierRecep.UserID);
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
        /// <param name="objRQ_DossierSch">object containing DossierSch</param>
        /// <returns>PK of DossierSch if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierSch(RQ_DossierSch objRQ_DossierSch)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objRQ_DossierSch.DossierDefID );
                dbparams.Add("@scheduleTypeID", objRQ_DossierSch.ScheduleTypeID);
                dbparams.Add("@time1", objRQ_DossierSch.Time1 );
                dbparams.Add("@time2", objRQ_DossierSch.Time2 );
                dbparams.Add("@dayOfWeek", objRQ_DossierSch.DayOfWeek );
                dbparams.Add("@dayOfMonth", objRQ_DossierSch.DayOfMonth);
                dbparams.Add("@lastRun", objRQ_DossierSch.LastRun );
                dbparams.Add("@nextRun", objRQ_DossierSch.NextRun);
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
        /// <param name="objRQ_DossierConf">object containing DossierConf</param>
        /// <returns>PK of DossierConf if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierConf(RQ_DossierConf objRQ_DossierConf)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objRQ_DossierConf.DossierDefID );
                dbparams.Add("@confJSON", objRQ_DossierConf.ConfJSON );
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
        /// <param name="objRQ_DossierTagGroup">object containing DossierTagGroup</param>
        /// <returns>PK of DossierTagGroup if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierTagGroup(RQ_DossierTagGroup objRQ_DossierTagGroup)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objRQ_DossierTagGroup.DossierDefID);
                dbparams.Add("@tgID", objRQ_DossierTagGroup.TGID);
                dbparams.Add("@tagID", objRQ_DossierTagGroup.TagID);
                dbparams.Add("@typeOfBinding", objRQ_DossierTagGroup.TypeOfBinding);
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
        /// <returns>PK of AdditonalURl if successfully inserted else 0</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertAdditonalURl(AdditionalURL objAdditionalURL)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@DossierID", objAdditionalURL.DossierID);
                dbparams.Add("@URL",objAdditionalURL.URL);
                dbparams.Add("@IsProcessed",objAdditionalURL.IsProcessed);
                dbparams.Add("@RefLinkID",objAdditionalURL.RefLinkID);
                dbparams.Add("@TryCount",objAdditionalURL.TryCount);
                dbparams.Add("@ErrorMsg",objAdditionalURL.ErrorMsg);
                dbparams.Add("@CreatedBy",objAdditionalURL.CreatedBy);

                iResult = GetByProcedure<int>(@"[dbo].[USP_AdditionlURL_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }


        #endregion 



        #region Update

        /// <summary>
        /// This method is used to Update DossierDef detail in DossierDef Table
        /// </summary>
        /// <param name="objRQ_Dossier">object containing DossierDef</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierDef(RQ_DossierDef objRQ_Dossier)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();

                dbparams.Add("@dossierDefID", objRQ_Dossier.DossierDefID);
                dbparams.Add("@clientID", objRQ_Dossier.ClientID);
                dbparams.Add("@dossierTypeID", objRQ_Dossier.DossierTypeID);
                dbparams.Add("@startDate", objRQ_Dossier.StartDate);
                dbparams.Add("@endDate", objRQ_Dossier.EndDate);
                dbparams.Add("@scheduleTypeID", objRQ_Dossier.ScheduleTypeID);
                dbparams.Add("@title", objRQ_Dossier.Title);
                dbparams.Add("@eventTypeID", objRQ_Dossier.EventTypeID);
                dbparams.Add("@eventContext", objRQ_Dossier.EventContext);
                dbparams.Add("@eventRefURL", objRQ_Dossier.EventRefURL);
                dbparams.Add("@eventKQuery", objRQ_Dossier.EventKQuery);
                dbparams.Add("@eventTagID", objRQ_Dossier.EventTagID);
                dbparams.Add("@platform1ID", objRQ_Dossier.Platform1ID);
                dbparams.Add("@platform2ID", objRQ_Dossier.Platform2ID);
                dbparams.Add("@platform3ID", objRQ_Dossier.Platform3ID);
                dbparams.Add("@statusID", objRQ_Dossier.StatusID);
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
        /// <param name="objRQ_DossierRecep">object containing DossierRecep</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierRecep(RQ_DossierRecep objRQ_DossierRecep)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierRecepID", objRQ_DossierRecep.DossierRecepID);
                dbparams.Add("@dossierDefID", objRQ_DossierRecep.DossierDefID);
                dbparams.Add("@userID", objRQ_DossierRecep.UserID);
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierSch(RQ_DossierSch objRQ_DossierSch)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierSchID", objRQ_DossierSch.DossierSchID);
                dbparams.Add("@dossierDefID", objRQ_DossierSch.DossierDefID);
                dbparams.Add("@scheduleTypeID", objRQ_DossierSch.ScheduleTypeID);
                dbparams.Add("@time1", objRQ_DossierSch.Time1);
                dbparams.Add("@time2", objRQ_DossierSch.Time2);
                dbparams.Add("@dayOfWeek", objRQ_DossierSch.DayOfWeek);
                dbparams.Add("@dayOfMonth", objRQ_DossierSch.DayOfMonth);
                dbparams.Add("@lastRun", objRQ_DossierSch.LastRun);
                dbparams.Add("@nextRun", objRQ_DossierSch.NextRun);
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierConf(RQ_DossierConf objRQ_DossierConf)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierConfID", objRQ_DossierConf.DossierConfID);
                dbparams.Add("@dossierDefID", objRQ_DossierConf.DossierDefID);
                dbparams.Add("@confJSON", objRQ_DossierConf.ConfJSON);
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierTagGroup(RQ_DossierTagGroup objRQ_DossierTagGroup)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierTagGroupID", objRQ_DossierTagGroup.DossierTagGroupID);
                dbparams.Add("@dossierDefID", objRQ_DossierTagGroup.DossierDefID);
                dbparams.Add("@tgID", objRQ_DossierTagGroup.TGID);
                dbparams.Add("@tagID", objRQ_DossierTagGroup.TagID);
                dbparams.Add("@typeOfBinding", objRQ_DossierTagGroup.TypeOfBinding);
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




        #endregion


    }
}
