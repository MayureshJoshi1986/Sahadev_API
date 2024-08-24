/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- SahadevC3Repository                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is SahadevC3Repository class which contains all functions &         *
 *                     SP related to SahadevC3 repository                                       *
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
using SahadevBusinessEntity.DTO.Model;
using System.Collections.Generic;
using System;
using System.Data;
using Dapper;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevC3Repository class
    /// </summary>
    public interface ISahadevC3Repository
    {
        //List<FeedbackType> GetFeedbackType();

        List<string> GetAllTagIDByTagGroupName(string tagGroupName);
        int InsertDossierDef(DossierDef objDossier);
        int InsertDossierRecep(DossierRecep objDossierRecep);
        int InsertDossierSch(DossierSch objDossierSch);
        int InsertDossierConf(DossierConf objDossierConf);
        int InsertDossierTagGroup(DossierTagGroup objDossierTagGroup);


    }

    internal class SahadevC3Repository:RepositoryBase, ISahadevC3Repository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public SahadevC3Repository(IDbConnection connection, IDbTransaction transaction)
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






    }
}
