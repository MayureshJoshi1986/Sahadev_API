/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- C2Repository                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is C2Repository class which contains all functions &         *
 *                     SP related to SahadevC2 repository                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- SL                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 16-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  Changed class name from SahadevC2Repository to C2Repository             *  
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Dapper;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.RequestModel;
using System;
using System.Collections.Generic;
using System.Data;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of C2Repository class
    /// </summary>
    public interface IC2Repository
    {
        List<FeedbackType> GetFeedbackType();

        int InsertEvent(RQ_Event objRQ_Event);

        bool InsertFeedback(RQ_Feedback objRQ_Feedback);

        bool InsertBookMark(RQ_BookMark objRQ_BookMark);

        bool UpdateBookMark(RQ_BookMark objRQ_BookMark);

        bool DeleteBookMark(RQ_BookMark objRQ_BookMark);

        bool InsertDataRequest(RQ_DataRequest objRQ_DataRequest);
    }

    internal class C2Repository : RepositoryBase, IC2Repository
    {

        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public C2Repository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction)
        {
            _connection = connection;
            _transaction = transaction;
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
                var data = GetAllByProcedure<FeedbackType>(@"[dbo].[USP_mstFeedbackType_FetchAll]", null, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to insert feedback detail in Feedback table
        /// </summary>
        /// <param name="objRQ_Feedback">object containing feedback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>18-Aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason></modifiedreason>
        /// <modifiedon>19-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed return type from int to bool & handled condition accordingly</modifiedreason>
        public bool InsertFeedback(RQ_Feedback objRQ_Feedback)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@eventID", objRQ_Feedback.EventID);
                dbparams.Add("@userID", objRQ_Feedback.UserID);
                dbparams.Add("@platformID", objRQ_Feedback.PlatformID);
                dbparams.Add("@ftID", objRQ_Feedback.FTID);
                dbparams.Add("@recordID", objRQ_Feedback.RecordID);
                dbparams.Add("@screenName", objRQ_Feedback.ScreenName);
                dbparams.Add("@feedback", objRQ_Feedback.FeedbackDescription);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_Feedback_Insert]", dbparams, _transaction);
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
        /// This method is used to insert dataRequest in DataRequest
        /// </summary>
        /// <param name="objRQ_DataRequest">object containing data reques</param>
        /// <returns>PK of Feedback if successfully inserted else 0</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>20-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed return type from int to bool & handled condition accordingly</modifiedreason>
        public bool InsertDataRequest(RQ_DataRequest objRQ_DataRequest)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@eventID", objRQ_DataRequest.EventID);
                dbparams.Add("@userID", objRQ_DataRequest.UserID);
                dbparams.Add("@platformID", objRQ_DataRequest.PlatformID);
                dbparams.Add("@startDate", objRQ_DataRequest.StartDate);
                dbparams.Add("@endDate", objRQ_DataRequest.EndDate);
                dbparams.Add("@filtersJson ", objRQ_DataRequest.FilterJson);
                dbparams.Add("@statusID", objRQ_DataRequest.StatusID);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_DataRequest_Insert]", dbparams, _transaction);
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
        /// This method is used to insert BookMark in BookMark Table
        /// </summary>
        /// <param name="objRQ_BookMark">object containing BookMark detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>19-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed return type from int to bool & handled condition accordingly</modifiedreason>
        public bool InsertBookMark(RQ_BookMark objRQ_BookMark)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@userID", objRQ_BookMark.UserID);
                dbparams.Add("@platformID", objRQ_BookMark.PlateformID);
                dbparams.Add("@eventID", objRQ_BookMark.EventID);
                dbparams.Add("@recordID", objRQ_BookMark.RecordID);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_Bookmark_Insert]", dbparams, _transaction);
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
        /// This method is used to Update BookMark in BookMark Table
        /// </summary>
        /// <param name="objRQ_BookMark">object containing BookMark detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>19-Aug-2024</modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool UpdateBookMark(RQ_BookMark objRQ_BookMark)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@bookMarkID", objRQ_BookMark.BookMarkID);
                dbparams.Add("@userID", objRQ_BookMark.UserID);
                dbparams.Add("@platformID", objRQ_BookMark.PlateformID);
                dbparams.Add("@eventID", objRQ_BookMark.EventID);
                dbparams.Add("@recordID", objRQ_BookMark.RecordID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_Bookmark_Update]", dbparams, _transaction);
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
        /// This method is used to delete BookMark from BookMark Table
        /// </summary>
        /// <param name="bookMarkID">bookMarkID to be deleted</param>
        /// <returns>true if successfully deleted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool DeleteBookMark(RQ_BookMark objRQ_BookMark)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@bookMarkID", objRQ_BookMark.BookMarkID);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_Bookmark_Delete]", dbparams, _transaction);
                if (iResult !=0)
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
        /// <param name="objEvent">object containing event detail</param>
        /// <returns>PK of Event if successfully inserted else 0</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertEvent(RQ_Event objRQ_Event)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", objRQ_Event.ClientID);
                dbparams.Add("@eventTypeID", objRQ_Event.EventTypeID);
                dbparams.Add("@eventName", objRQ_Event.EventName);
                dbparams.Add("@description", objRQ_Event.Description);
                dbparams.Add("@refArticleURL", objRQ_Event.RefArticleURL);
                dbparams.Add("@keywords", objRQ_Event.Keywords);
                dbparams.Add("@query", objRQ_Event.Query);
                dbparams.Add("@platform1", objRQ_Event.Platform1);
                dbparams.Add("@platform2", objRQ_Event.Platform2);
                dbparams.Add("@platform3", objRQ_Event.Platform3);
                dbparams.Add("@platform4", objRQ_Event.Platform4);
                dbparams.Add("@startDate", objRQ_Event.StartDate);
                dbparams.Add("@endDate", objRQ_Event.EndDate);
                dbparams.Add("@statusID", objRQ_Event.StatusID);
                dbparams.Add("@tagID", objRQ_Event.TagID);
                iResult = GetByProcedure<int>(@"[dbo].[USP_Event_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }



    }

}
