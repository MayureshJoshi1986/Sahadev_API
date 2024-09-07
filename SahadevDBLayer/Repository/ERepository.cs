/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- ERepository                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is ERepository class which contains all functions &                 *
 *                     SP related to Sahadev_E repository                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- Saroj Laddha                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 06-SEP-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *  
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Dapper;
using SahadevBusinessEntity.DTO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of ERepository class
    /// </summary>
    public interface IERepository
    {
        List<dynamic> GetAllDossierReviewDataDetails(int platformID, string linkID);
        List<dynamic> GetAllDossierDraftDataDetails(int platformID, string linkID);
        List<dynamic> GetAllDossierTrashDataDetails(int platformID, string linkID);

        bool UpadateDataAfterEdit(int plateformID, int linkID, string Sentiment, string ArticleMention);
    }
    internal class ERepository : RepositoryBase, IERepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public ERepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }


        /// <summary>
        /// To fetch the Data links Details for the review
        /// </summary>
        /// <param name="linkID">To fetch the link Details</param>
        /// <param name="platformID"> To fetch the link Details of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing Data links Details for the review</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierReviewDataDetails(int platformID,string linkID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@platformID", platformID);
                dbparams.Add("@linkID", linkID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Review_FetchReviewDataDetails]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// To fetch the Data links Details that saved to draft
        /// </summary>
        /// <param name="linkID">To fetch the link Details of linkID</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing link Details that are Saved To draft</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierDraftDataDetails(int platformID, string linkID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@platformID", platformID);
                dbparams.Add("@linkID", linkID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Review_FetchDossierDraftDetails]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// To fetch the Data link Details that moved to trash 
        /// </summary>
        /// <param name="dossierID">To fetch the links Detail of a linkr</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing link Details that are moved To trash</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierTrashDataDetails(int platformID, string linkID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@platformID", platformID);
                dbparams.Add("@linkID", linkID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Review_ShowTrashDetails]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





        /// <summary>
        /// This method is used to Update Sentiment and articlemention details in LinkEnrichedDetail table for print , online
        /// </summary>
        /// <param name="platformID">contains platform information for which data is to update</param>
        /// <param name="linkID">contains refernce link id for which data need to be updated</param>
        /// <param name="Sentiment">sentiment to be updated</param>
        /// <param name="linkID">article mention detail to be updated</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>07-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool UpadateDataAfterEdit(int platformID, int linkID, string Sentiment, string ArticleMention)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@platformID", platformID);
                dbparams.Add("@linkID", linkID);
                dbparams.Add("@sentiment", Sentiment);
                dbparams.Add("@articleMention", ArticleMention);
                int iResult = UpdateByProcedure<int>(@"[dbo].[Dossier_Review_UpdateDataAfterEdit]", dbparams, _transaction);
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
