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
using static Dapper.SqlMapper;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevC2Repository class
    /// </summary>
    public interface ISahadevC2Repository
    {
        List<FeedbackType> GetFeedbackType();
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
        public List<FeedbackType> GetFeedbackType()
        {
            try
            {
                var data = GetAllByProcedure<FeedbackType>(@"[dbo].[USP_mstFeedbackType_FetchAll]", null);
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

        ///// <summary>
        ///// This method is used to insert event detail in event table
        ///// </summary>
        ///// <param name="objEvent">object containing feedback detail</param>
        ///// <returns>true if successfully inserted else false</returns>
        ///// <createdon>17-Aug-2024</createdon>
        ///// <createdby>PJ</createdby>
        ///// <modifiedon></modifiedon>
        ///// <modifiedby></modifiedby>
        ///// <modifiedreason></modifiedreason>
        //public bool InsertEvent(Event objEvent, IDbTransaction transaction)
        //{
        //    bool bReturn = false;
        //    try
        //    {
        //        var dbparams = new DynamicParameters();
        //        dbparams.Add("@clientID", objEvent.EventID);
        //        dbparams.Add("@eventTypeID", objEvent.UserID);
        //        dbparams.Add("@eventName", objEvent.PlatformID);
        //        dbparams.Add("@description", objEvent.FTID);
        //        dbparams.Add("@refArticleURL", objEvent.RecordID);
        //        dbparams.Add("@keywords", objEvent.ScreenName);
        //        dbparams.Add("@query", objEvent.FeedbackDescription);
        //        dbparams.Add("@query", objEvent.FeedbackDescription);
        //        dbparams.Add("@query", objEvent.FeedbackDescription);
        //        dbparams.Add("@query", objEvent.FeedbackDescription);
        //        dbparams.Add("@query", objEvent.FeedbackDescription);
        //        int iResult = InsertByProcedure<int>(@"[dbo].[USP_Feedback_Insert]", dbparams, transaction);
        //        if (iResult != 0)
        //            bReturn = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bReturn;

        //}
    }

}
