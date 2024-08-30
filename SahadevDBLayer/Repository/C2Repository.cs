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

        int InsertEvent(Event objEvent);

        bool InsertFeedback(Feedback objRQ_Feedback);

        bool InsertBookMark(BookMark objRQ_BookMark);

        bool UpdateBookMark(BookMark objRQ_BookMark);

        bool DeleteBookMark(BookMark objRQ_BookMark);

        bool InsertDataRequest(DataRequest objRQ_DataRequest);
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
        /// <param name="objFeedback">object containing feedback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>18-Aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason></modifiedreason>
        /// <modifiedon>19-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed return type from int to bool & handled condition accordingly</modifiedreason>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool InsertFeedback(Feedback objFeedback)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@eventID", objFeedback.EventID);
                dbparams.Add("@userID", objFeedback.UserID);
                dbparams.Add("@platformID", objFeedback.PlatformID);
                dbparams.Add("@ftID", objFeedback.FTID);
                dbparams.Add("@recordID", objFeedback.RecordID);
                dbparams.Add("@screenName", objFeedback.ScreenName);
                dbparams.Add("@feedback", objFeedback.Description);
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
        /// <param name="objDataRequest">object containing data reques</param>
        /// <returns>PK of Feedback if successfully inserted else 0</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>20-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed return type from int to bool & handled condition accordingly</modifiedreason>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool InsertDataRequest(DataRequest objDataRequest)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@eventID", objDataRequest.EventID);
                dbparams.Add("@userID", objDataRequest.UserID);
                dbparams.Add("@platformID", objDataRequest.PlatformID);
                dbparams.Add("@startDate", objDataRequest.StartDate);
                dbparams.Add("@endDate", objDataRequest.EndDate);
                dbparams.Add("@filtersJson ", objDataRequest.FilterJson);
                dbparams.Add("@statusID", objDataRequest.StatusID);
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
        /// <param name="objBookMark">object containing BookMark detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>19-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed return type from int to bool & handled condition accordingly</modifiedreason>
        ///<modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool InsertBookMark(BookMark objBookMark)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@userID", objBookMark.UserID);
                dbparams.Add("@platformID", objBookMark.PlatformID);
                dbparams.Add("@eventID", objBookMark.EventID);
                dbparams.Add("@recordID", objBookMark.RecordID);
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
        /// <param name="objBookMark">object containing BookMark detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>19-Aug-2024</modifiedby>
        /// <modifiedreason></modifiedreason>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool UpdateBookMark(BookMark objBookMark)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@bookMarkID", objBookMark.BookMarkID);
                dbparams.Add("@userID", objBookMark.UserID);
                dbparams.Add("@platformID", objBookMark.PlatformID);
                dbparams.Add("@eventID", objBookMark.EventID);
                dbparams.Add("@recordID", objBookMark.RecordID);
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
        /// <param name="objBookMark">objBookmark Conatining details of object to be deleted</param>
        /// <returns>true if successfully deleted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public bool DeleteBookMark(BookMark objBookMark)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@bookMarkID", objBookMark.BookMarkID);
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
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changed Request Model to Business Model</modifiedreason>
        public int InsertEvent(Event objEvent)
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
