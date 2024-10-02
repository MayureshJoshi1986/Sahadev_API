/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- EventService                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is EventService class which contains all method related to Event    *
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
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.RequestModel;
using SahadevDBLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SahadevService.Sentry
{
    /// <summary>
    /// Interface EventService class  
    /// </summary>
    interface IEventService
    {
        bool Add(RQ_Event objRQ_Event);

        bool InsertFeedback(RQ_Feedback objRQ_Feedback);
        bool InsertUpdateBookMark(RQ_BookMark objRQ_BookMark);

        List<FeedbackType> GetAllFeedbackType();

        bool InsertDataRequest(RQ_DataRequest objRQ_DataRequest);

    }
    public class EventService : IEventService
    {
        private const string _className = "SahadevService.EventService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;


        /// <summary>
        /// Constructor defined for EventService class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public EventService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }

        /// <summary>
        /// This method is used to insert event detail in Event table
        /// </summary>
        /// <param name="objRQ_Event">request object containing Event detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedreason></modifiedreason>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from Event to RQ_Event</modifiedreason>
        /// <modifiedon>30-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping of the Request Model to Business Model</modifiedreason>
        ///  <modifiedon>02-10-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Changes done for the multiple tag queries</modifiedreason>
        public bool Add(RQ_Event objRQ_Event)
        {
            bool bReturn = false;
            try
            {
                //Check if Tag Exists get TagId else Create tag  and Get TagId from SahdevA2 Database

                //New Event as a Tag Mapping
                Tag objTag = new Tag();
                objTag.IGTagID = null;// what to map with ???  B Databas Tab
                objTag.TagName = objRQ_Event.EventName;
                objTag.TagDescription = objRQ_Event.Description;
                objTag.IsActive = true;


                int TagID = uw.A2Repository.InsertTag(objTag);

                objTag.TagID = TagID;


                //Mapping Rquest Event Object to Event Business Model 

                Event objEvent = new Event();
                objEvent.EventName = objRQ_Event.EventName;
                objEvent.Description = objRQ_Event.Description;
                objEvent.EventTypeID = objRQ_Event.EventTypeID;
                objEvent.ClientID = objRQ_Event.ClientID;
                objEvent.RefArticleURL = objRQ_Event.RefArticleURL;
                objEvent.Keywords = objRQ_Event.Keywords;
                objEvent.Query = objRQ_Event.Query;
                objEvent.Platform1 = objRQ_Event.Platform1;
                objEvent.Platform2 = objRQ_Event.Platform2;
                objEvent.Platform3 = objRQ_Event.Platform3;
                objEvent.Platform4 = objRQ_Event.Platform4;
                objEvent.StartDate = objRQ_Event.StartDate;
                objEvent.EndDate = objRQ_Event.EndDate;
                objEvent.StatusID = objRQ_Event.StatusID;


                //Assign TagId to the Event
                objEvent.TagID = TagID;

                //Insert Event anf get event Id

                int EventID = uw.C2Repository.InsertEvent(objEvent);
                objEvent.EventID = EventID;

                //DO entry in Client Topic

                ClientTopic objClientTopic = new ClientTopic();
                objClientTopic.RefTopicID = EventID;
                objClientTopic.TopicName = objRQ_Event.EventName;
                objClientTopic.TopicDescription = objRQ_Event.Description;
                objClientTopic.ClientID = objRQ_Event.ClientID;
                objClientTopic.Status = objRQ_Event.StatusID;
                objClientTopic.StartDate = objRQ_Event.StartDate;
                objClientTopic.EndDate = objRQ_Event.EndDate;
                objClientTopic.TopicTypeID = 2; // 1 general listener (client on board)  , 2 Sentry , 3 Dossier

                int ClientTopicId = uw.A2Repository.InsertClientTopic(objClientTopic);


                //do the enrty in Tag Map
                TagMap objTagMap = new TagMap();
                objTagMap.TagID = TagID;
                objTagMap.ClientTopicID = ClientTopicId;
                objTagMap.IsActive = objTag.IsActive;
                bool result = uw.A2Repository.InsertTagMap(objTagMap);

                //throw new TransactionAbortedException(); // Just to test the Transaction Rollback


                uw.C2Repository.InsertTag(objTag);



                if (objRQ_Event.rQ_TagQueries != null)
                {
                    foreach (var query in objRQ_Event.rQ_TagQueries)
                    {
                        TagQuery objTagQuery = new TagQuery();
                        objTagQuery.TagID = objTag.TagID;
                        objTagQuery.Query = query.Query;
                        objTagQuery.TypeOfQuery = "Keyword";
                        objTagQuery.IsActive = true;
                        objTagQuery.PlatformID = query.PlatformID;
                        objTagQuery.TagQueryID = uw.A2Repository.InsertTagQuery(objTagQuery);
                        uw.C2Repository.InsertTagQuery(objTagQuery);
                    }
                }

                
               

                uw.Commit();
                bReturn =  true;
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertEvent");
            }
            return bReturn;
        }

        /// <summary>
        /// This method is used to insert feedback in feeback table
        /// </summary>
        /// <param name="objRQ_Feedback">request object containing feeback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from Feedback to RQ_Feedback</modifiedreason>
        /// <modifiedon>30-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping of the Request Model to Business Model</modifiedreason>
        public bool InsertFeedback(RQ_Feedback objRQ_Feedback)
        {
            bool bReturn = false;
            try
            {
                //Mapping of Request Object to Feedback Business Model
                Feedback objFeedback = new Feedback();
                objFeedback.EventID = objRQ_Feedback.EventID;
                objFeedback.UserID = objRQ_Feedback.UserID;
                objFeedback.PlatformID = objRQ_Feedback.PlatformID;
                objFeedback.FTID = objRQ_Feedback.FTID;
                objFeedback.RecordID = objRQ_Feedback.RecordID;
                objFeedback.ScreenName = objRQ_Feedback.ScreenName;
                objFeedback.Description= objRQ_Feedback.FeedbackDescription;

                bReturn = uw.C2Repository.InsertFeedback(objFeedback);
                uw.Commit();
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertFeedback");
            }
            return bReturn;
        }

        /// <summary>
        /// This method is used to insert or update BookMark Table based on the action parameter
        /// </summary>
        /// <param name="objRQ_BookMark">object containing feeback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from BookMark to RQ_BookMark</modifiedreason>
        /// <modifiedon>30-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping of the Request Model to Business Model</modifiedreason>
        public bool InsertUpdateBookMark(RQ_BookMark objRQ_BookMark)
        {
            bool bReturn = false;
            try
            {
                //mapping Request Model to BookMark Business model

                BookMark objBookMark = new BookMark();
                objBookMark.BookMarkID = objRQ_BookMark.BookMarkID;
                objBookMark.EventID = objRQ_BookMark.EventID;
                objBookMark.UserID = objRQ_BookMark.UserID;
                objBookMark.PlatformID = objRQ_BookMark.PlatformID;
                objBookMark.RecordID = objRQ_BookMark.RecordID;


                switch (objRQ_BookMark.Action)
                {
                    case "Insert":
                        bReturn = uw.C2Repository.InsertBookMark(objBookMark);
                        break;
                    case "Update":
                        bReturn = uw.C2Repository.UpdateBookMark(objBookMark);
                        break;
                    case "Delete":
                        bReturn = uw.C2Repository.DeleteBookMark(objBookMark);
                        break;

                }

                uw.Commit();
                return bReturn;
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertUpdateBookMark");
                return false;
            }
        }

        /// <summary>
        /// This method is used to Fecth All Feedback Type in mstFeedbackType
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<FeedbackType> GetAllFeedbackType()
        {
            try
            {
                var data = uw.C2Repository.GetFeedbackType();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllFeedbackType");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to insert Data for Download in DataRequestTable
        /// </summary>
        /// <param name="objRQ_DataRequest">object containing DataRequest detaill</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from DataRequest to RQ_DataRequest</modifiedreason>
        /// <modifiedon>30-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping of the Request Model to Business Model</modifiedreason>
        public bool InsertDataRequest(RQ_DataRequest objRQ_DataRequest)
        {
            bool bReturn = false;
            try
            {   //mapping of Request Model to DataRequest Business Model
                DataRequest objDataRequest = new DataRequest();
                objDataRequest.UserID = objRQ_DataRequest.UserID;
                objDataRequest.EventID = objRQ_DataRequest.EventID;
                objDataRequest.PlatformID = objRQ_DataRequest.PlatformID;
                objDataRequest.StartDate   =Convert.ToDateTime(objRQ_DataRequest.StartDate);
                objDataRequest.EndDate = Convert.ToDateTime(objRQ_DataRequest.EndDate);
                objDataRequest.FilterJson = objRQ_DataRequest.FilterJson;
                objDataRequest.StatusID = objRQ_DataRequest.StatusID;


                bReturn = uw.C2Repository.InsertDataRequest(objDataRequest);
                uw.Commit();

            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertDataRequest");
            }

            return bReturn;
        }

    }
}