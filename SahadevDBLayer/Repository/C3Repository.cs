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

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of C3Repository class
    /// </summary>
    public interface IC3Repository
    {
        //List<FeedbackType> GetFeedbackType();

        List<string> GetAllTagIDByTagGroupName(string tagGroupName);
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
        DossierRecep GetDossierRecep(int DossierDefID);
        DossierSch GetDossierSch(int DossierDefID);
        DossierConf GetDossierConf(int DossierDefID);
        DossierTagGroup GetDossierTagGroup(int DossierDefID);


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
                var data = GetByProcedure<DossierTagGroup>(@"[dbo].[USE_DossierTagGroup_Fetch]", dbparams, _transaction);
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
        /// <param name="objDossierDef">object containing DossierDef</param>
        /// <returns>PK of DossierDef if successfully inserted else 0</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierDef(DossierDef objDossier)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", objDossier.ClientID);
                dbparams.Add("@dossierTypeID", objDossier.DossierTypeID );
                dbparams.Add("@startDate", objDossier.StartDate );
                dbparams.Add("@endDate", objDossier.EndDate);
                dbparams.Add("@scheduleTypeID",objDossier.ScheduleTypeID );
                dbparams.Add("@title", objDossier.Title);
                dbparams.Add("@eventTypeID",objDossier.EventTypeID );
                dbparams.Add("@eventContext", objDossier.EventContext);
                dbparams.Add("@eventRefURL", objDossier.EventRefURL );
                dbparams.Add("@eventKQuery", objDossier.EventKQuery );
                dbparams.Add("@eventTagID", objDossier.EventTagID);
                dbparams.Add("@platform1ID", objDossier.Platform1ID);
                dbparams.Add("@platform2ID", objDossier.Platform2ID);
                dbparams.Add("@platform3ID", objDossier.Platform3ID );
                dbparams.Add("@statudID", objDossier.StatusID);
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
        public int InsertDossierRecep(DossierRecep objDossierRecep)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierRecep.DossierDefID );
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierSch(DossierSch objDossierSch)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierSch.DossierDefID );
                dbparams.Add("@scheduleTypeID", objDossierSch.ScheduleTypeID);
                dbparams.Add("@time1", objDossierSch.Time1 );
                dbparams.Add("@time2", objDossierSch.Time2 );
                dbparams.Add("@dayOfWeek", objDossierSch.DayOfWeek );
                dbparams.Add("@dayOfMonth", objDossierSch.DayOfMonth);
                dbparams.Add("@lastRub", objDossierSch.LastRun );
                dbparams.Add("@nextRun", objDossierSch.NextRun);
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertDossierConf(DossierConf objDossierConf)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierConf.DossierDefID );
                dbparams.Add("@confJSON", objDossierConf.ConfJSON );
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
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


        #endregion 



        #region Update

        /// <summary>
        /// This method is used to Update DossierDef detail in DossierDef Table
        /// </summary>
        /// <param name="objDossierDef">object containing DossierDef</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
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
                dbparams.Add("@statudID", objDossier.StatusID);
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
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
                dbparams.Add("@lastRub", objDossierSch.LastRun);
                dbparams.Add("@nextRun", objDossierSch.NextRun);
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateDossierTagGroup(DossierTagGroup objDossierTagGroup)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@dossierDefID", objDossierTagGroup.DossierTagGroupID);
                dbparams.Add("@dossierDefID", objDossierTagGroup.DossierDefID);
                dbparams.Add("@tgID", objDossierTagGroup.TGID);
                dbparams.Add("@tagID", objDossierTagGroup.TagID);
                dbparams.Add("@typeOfBinding", objDossierTagGroup.TypeOfBinding);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USE_DossierTagGroup_Update]", dbparams, _transaction);
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
