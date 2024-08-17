/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- SahadevC2Repository                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is SahadevC2Repository class which contains all functions &         *
 *                     SP related to SahadevC2 repository                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- SL                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 16-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevDBLayer.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using static Dapper.SqlMapper;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevC2Repository class
    /// </summary>
    public interface ISahadevC2Repository
    {
        List<FeedbackType> GetFeedbackType(IDbTransaction transaction);

        int InsertEvent(Event objEvent, IDbTransaction transaction);
    }
    internal class SahadevC2Repository : RepositoryBase, ISahadevC2Repository
    {

        public SahadevC2Repository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {
        }

        /// <summary>
        /// This method is used to get fetch all feedback types from mstFeedbackType table
        /// </summary>
        /// <returns>list of object containing feedback types</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<FeedbackType> GetFeedbackType(IDbTransaction transaction)
        {
            try
            {
                var data = GetAllByProcedure<FeedbackType>(@"[dbo].[USP_mstFeedbackType_FetchAll]", null, transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to insert feedback detail in feedback table
        /// </summary>
        /// <param name="objEvent">object containing feedback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertFeedback(Feedback objEvent, IDbTransaction transaction)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@eventID", objEvent.EventID);
                dbparams.Add("@userID", objEvent.UserID);
                dbparams.Add("@platformID", objEvent.PlatformID);
                dbparams.Add("@ftID", objEvent.FTID);
                dbparams.Add("@recordID", objEvent.RecordID);
                dbparams.Add("@screenName", objEvent.ScreenName);
                dbparams.Add("@feedback", objEvent.FeedbackDescription);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_Feedback_Insert]", dbparams, transaction);
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
        /// This method is used to insert event detail in event table
        /// </summary>
        /// <param name="objEvent">object containing feedback detail</param>
        /// <returns>PK of feedback if successfully inserted else 0</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertEvent(Event objEvent, IDbTransaction transaction)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", objEvent.ClientID);
                dbparams.Add("@eventTypeID", objEvent.EventTypeID);
                dbparams.Add("@eventName", objEvent.EventName);
                dbparams.Add("@description", objEvent.Description);
                dbparams.Add("@refArticleURL", objEvent.RefArticleURL);
                dbparams.Add("@keywords", objEvent.Keywords);
                dbparams.Add("@query", objEvent.Query);
                dbparams.Add("@platform1", objEvent.Platform1);
                dbparams.Add("@platform2", objEvent.Platform2);
                dbparams.Add("@platform3", objEvent.Platform3);
                dbparams.Add("@platform4", objEvent.Platform4);
                dbparams.Add("@startDate", objEvent.StartDate);
                dbparams.Add("@endDate", objEvent.EndDate);
                dbparams.Add("@statusID", objEvent.StatusID);
                dbparams.Add("@tagID", objEvent.TagID);
                iResult = GetByProcedure<int>(@"[dbo].[USP_Event_Insert]", dbparams, transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }
    }

}
