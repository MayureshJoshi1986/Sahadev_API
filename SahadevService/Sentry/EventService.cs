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

namespace SahadevService.Sentry
{
    /// <summary>
    /// Interface EventService class  
    /// </summary>
    interface IEventService
    {

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
        /// <createdby>PJ</createdby>
        /// <modifiedon>17-Aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason></modifiedreason>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from Event to RQ_Event</modifiedreason>
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

                //Assign TagId to the Event
                objRQ_Event.TagID = TagID;

                //Insert Event anf get event Id

                int EventID = uw.C2Repository.InsertEvent(objRQ_Event);
                objRQ_Event.EventID = EventID;

                //DO entry in Client Topic

                ClientTopic objClientTopic = new ClientTopic();
                objClientTopic.RefTopicID = EventID;
                objClientTopic.TopicName = objRQ_Event.EventName;
                objClientTopic.TopicDescription = objRQ_Event.Description;
                objClientTopic.ClientID = objRQ_Event.ClientID;
                objClientTopic.Status = objRQ_Event.StatusID;
                objClientTopic.StartDate = objRQ_Event.StartDate;
                objClientTopic.EndDate = objRQ_Event.EndDate;
                objClientTopic.TopicTypeID = objRQ_Event.EventTypeID;

                int ClientTopicId = uw.A2Repository.InsertClientTopic(objClientTopic);


                //do the enrty in Tag Map
                TagMap objTagMap = new TagMap();
                objTagMap.TagID = TagID;
                objTagMap.ClientTopicID = ClientTopicId;
                objTagMap.IsActive = objTag.IsActive;
                bool result = uw.A2Repository.InsertTagMap(objTagMap);

                //throw new TransactionAbortedException(); // Just to test the Transaction Rollback

                //do the entry in Tag query for all the selected plateform
                TagQuery objTagQuery = new TagQuery();

                if (objRQ_Event.Platform1 != 0)
                {
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objRQ_Event.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objRQ_Event.Platform1;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.A2Repository.InsertTagQuery(objTagQuery);
                }
                if (objRQ_Event.Platform2 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objRQ_Event.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objRQ_Event.Platform2;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.A2Repository.InsertTagQuery(objTagQuery);
                }

                if (objRQ_Event.Platform3 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objRQ_Event.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objRQ_Event.Platform3;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.A2Repository.InsertTagQuery(objTagQuery);
                }

                if (objRQ_Event.Platform4 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objRQ_Event.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objRQ_Event.Platform4;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.A2Repository.InsertTagQuery(objTagQuery);
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
        public bool InsertFeedback(RQ_Feedback objRQ_Feedback)
        {
            bool bReturn = false;
            try
            {
                bReturn = uw.C2Repository.InsertFeedback(objRQ_Feedback);
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
        public bool InsertUpdateBookMark(RQ_BookMark objRQ_BookMark)
        {
            bool bReturn = false;
            try
            {
                switch (objRQ_BookMark.Action)
                {
                    case "Insert":
                        bReturn = uw.C2Repository.InsertBookMark(objRQ_BookMark);
                        break;
                    case "Update":
                        bReturn = uw.C2Repository.UpdateBookMark(objRQ_BookMark);
                        break;
                    case "Delete":
                        bReturn = uw.C2Repository.DeleteBookMark(objRQ_BookMark);
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
        public bool InsertDataRequest(RQ_DataRequest objRQ_DataRequest)
        {
            bool bReturn = false;
            try
            {
                bReturn = uw.C2Repository.InsertDataRequest(objRQ_DataRequest);
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