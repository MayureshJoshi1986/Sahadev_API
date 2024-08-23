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
    /// Interface ClientService class  
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
        /// <param name="objEvent">object containing Event detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>17-Aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool Add(Event objEvent)
        {
            bool bReturn = false;
            try
            {
                //Check if Tag Exists get TagId else Create tag  and Get TagId from SahdevA2 Database

                //New Event as a Tag Mapping
                Tag objTag = new Tag();
                objTag.IGTagID = null;// what to map with ???  B Databas Tab
                objTag.TagName = objEvent.EventName;
                objTag.TagDescription = objEvent.Description;
                objTag.IsActive = true;


                int TagID = uw.SahadevA2Repository.InsertTag(objTag);

                objTag.TagID = TagID;

                //Assign TagId to the Event
                objEvent.TagID = TagID;

                //Insert Event anf get event Id

                int EventID = uw.SahadevC2Repository.InsertEvent(objEvent);
                objEvent.EventID = EventID;

                //DO entry in Client Topic

                ClientTopic objClientTopic = new ClientTopic();
                objClientTopic.RefTopicID = EventID;
                objClientTopic.TopicName = objEvent.EventName;
                objClientTopic.TopicDescription = objEvent.Description;
                objClientTopic.ClientID = objEvent.ClientID;
                objClientTopic.Status = objEvent.StatusID;
                objClientTopic.StartDate = objEvent.StartDate;
                objClientTopic.EndDate = objEvent.EndDate;
                objClientTopic.TopicTypeID = objEvent.EventTypeID;

                int ClientTopicId = uw.SahadevA2Repository.InsertClientTopic(objClientTopic);


                //do the enrty in Tag Map
                TagMap objTagMap = new TagMap();
                objTagMap.TagID = TagID;
                objTagMap.ClientTopicID = ClientTopicId;
                objTagMap.IsActive = objTag.IsActive;
                bool result = uw.SahadevA2Repository.InsertTagMap(objTagMap);

                //throw new TransactionAbortedException(); // Just to test the Transaction Rollback

                //do the entry in Tag query for all the selected plateform
                TagQuery objTagQuery = new TagQuery();

                if (objEvent.Platform1 != 0)
                {
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform1;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery);
                }
                if (objEvent.Platform2 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform2;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery);
                }

                if (objEvent.Platform3 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform3;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery);
                }

                if (objEvent.Platform4 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform4;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery);
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertFeedback(RQ_Feedback objRQ_Feedback)
        {
            bool bReturn = false;
            try
            {
                bReturn = uw.SahadevC2Repository.InsertFeedback(objRQ_Feedback);
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
        /// <param name="objFeedback">object containing feeback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertUpdateBookMark(BookMark objBookMark)
        {
            bool bReturn = false;
            try
            {
                switch (objBookMark.Action)
                {
                    case "Insert":
                        bReturn = uw.SahadevC2Repository.InsertBookMark(objBookMark);
                        break;
                    case "Update":
                        bReturn = uw.SahadevC2Repository.UpdateBookMark(objBookMark);
                        break;
                    case "Delete":
                        bReturn = uw.SahadevC2Repository.DeleteBookMark(objBookMark);
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
                var data = uw.SahadevC2Repository.GetFeedbackType();
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
        /// <param name="objDataRequest">object containing DataRequest detaill</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertDataRequest(DataRequest objDataRequest)
        {
            bool bReturn = false;
            try
            {
                bReturn = uw.SahadevC2Repository.InsertDataRequest(objDataRequest);
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
