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
using System.Text;

using System.Transactions;

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
        private const string _className = "SahadevService.Common.EventService";
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
                return true;


            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertEvent");
                return false;
            }
        }




        /// <summary>
        /// This method is used to insert feedback in feeback table
        /// </summary>
        /// <param name="objFeedback">object containing feeback detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertFeedback(Feedback objFeedback)
        {
            bool bReturn = false;
            try
            {
                int iResult = uw.SahadevC2Repository.InsertFeedback(objFeedback);
                if (iResult != 0)
                    bReturn = true;
                uw.Commit();


            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertFeedbak");
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
        public bool InsertUpdateBookMark(BookMark objBookMark, string action)
        {
            bool bReturn = false;
            int iResult = 0;
            try
            {
                switch (action)
                {
                    case "Insert":
                        iResult = uw.SahadevC2Repository.InsertBookMark(objBookMark);

                        if (iResult != 0)
                        {
                            objBookMark.BookMarkID = iResult;
                            bReturn = true;
                        }
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
                int iResult = uw.SahadevC2Repository.InsertDataRequest(objDataRequest);
                if (iResult != 0)
                    bReturn = true;
                uw.Commit();


            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertDataRequest");
            }

            return bReturn;
        }



        /// <summary>
        /// This method is used to insert event detail in Event table
        /// </summary>
        /// <param name="objRQ_EventDetail">request object containing Event detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>17-Aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason></modifiedreason>
        //public bool Add(RQ_EventDetail objRQ_EventDetail)
        //{
        //    bool bReturn = false;
        //    try
        //    {
        //        //Check if Tag Exists get TagId else Create tag  and Get TagId from SahdevA2 Database

        //        //New Event as a Tag Mapping
        //        objRQ_EventDetail.objTag = new Tag();
        //        objRQ_EventDetail.objTag.IGTagID = null;
        //        objRQ_EventDetail.objTag.TagName = objRQ_EventDetail.objEvent.EventName;
        //        objRQ_EventDetail.objTag.TagDescription = objRQ_EventDetail.objEvent.Description;
        //        objRQ_EventDetail.objTag.IsActive = true;


        //        int TagID = uw.SahadevA2Repository.InsertTag(objRQ_EventDetail.objTag, uw.GetSahadevA2Transaction());

        //        objRQ_EventDetail.objTag.TagID = TagID;

        //        //Assign TagId to the Event
        //        objRQ_EventDetail.objEvent.TagID = TagID;

        //        //Insert Event anf get event Id

        //        int EventID = uw.SahadevC2Repository.InsertEvent(objRQ_EventDetail.objEvent, uw.GetSahadevC2Transaction());
        //        objRQ_EventDetail.objEvent.EventID = EventID;
        //        //objRQ_EventDetail.objClientTopic.RefTopicID = EventID;
        //        //DO entry in Client Topic

        //        objRQ_EventDetail.objClientTopic = new ClientTopic();
        //        objRQ_EventDetail.objClientTopic.RefTopicID = EventID;
        //        objRQ_EventDetail.objClientTopic.TopicName = objRQ_EventDetail.objEvent.EventName;
        //        objRQ_EventDetail.objClientTopic.TopicDescription = objRQ_EventDetail.objEvent.Description;
        //        objRQ_EventDetail.objClientTopic.ClientID = objRQ_EventDetail.objEvent.ClientID;
        //        objRQ_EventDetail.objClientTopic.Status = objRQ_EventDetail.objEvent.StatusID;
        //        objRQ_EventDetail.objClientTopic.StartDate = objRQ_EventDetail.objEvent.StartDate;
        //        objRQ_EventDetail.objClientTopic.EndDate = objRQ_EventDetail.objEvent.EndDate;
        //        objRQ_EventDetail.objClientTopic.TopicTypeID = objRQ_EventDetail.objEvent.EventTypeID;

        //        int ClientTopicId = uw.SahadevA2Repository.InsertClientTopic(objRQ_EventDetail.objClientTopic, uw.GetSahadevA2Transaction());


        //        //do the enrty in Tag Map
        //        objRQ_EventDetail.objTagMap = new TagMap();
        //        objRQ_EventDetail.objTagMap.ClientTopicID = ClientTopicId;
        //        objRQ_EventDetail.objTagMap.TagID = TagID;
        //        objRQ_EventDetail.objTagMap.IsActive = objRQ_EventDetail.objTag.IsActive;
        //        bool result = uw.SahadevA2Repository.InsertTagMap(objRQ_EventDetail.objTagMap, uw.GetSahadevA2Transaction());

        //        // throw new TransactionAbortedException(); // Just to test the Transaction Rollback

        //        //do the entry in Tag query for all the selected plateform
        //        objRQ_EventDetail.objTagQuery = new TagQuery();
        //        objRQ_EventDetail.objTagQuery.TagID = TagID;
        //        objRQ_EventDetail.objTagQuery.IsActive = objRQ_EventDetail.objTag.IsActive;
        //        objRQ_EventDetail.objTagQuery.Query = objRQ_EventDetail.objEvent.Query;
        //        //            objTagQuery.TypeOfQuery = string.Empty; // what to map with??
        //        if (objRQ_EventDetail.objEvent.Platform1 != 0)
        //        {
        //            objRQ_EventDetail.objTagQuery.PlatformID = objRQ_EventDetail.objEvent.Platform1;
        //            uw.SahadevA2Repository.InsertTagQuery(objRQ_EventDetail.objTagQuery, uw.GetSahadevA2Transaction());
        //        }
        //        if (objRQ_EventDetail.objEvent.Platform2 != 0)
        //        {
        //            objRQ_EventDetail.objTagQuery.PlatformID = objRQ_EventDetail.objEvent.Platform2;
        //            uw.SahadevA2Repository.InsertTagQuery(objRQ_EventDetail.objTagQuery, uw.GetSahadevA2Transaction());
        //        }

        //        if (objRQ_EventDetail.objEvent.Platform3 != 0)
        //        {
        //            objRQ_EventDetail.objTagQuery.PlatformID = objRQ_EventDetail.objEvent.Platform3;
        //            uw.SahadevA2Repository.InsertTagQuery(objRQ_EventDetail.objTagQuery, uw.GetSahadevA2Transaction());
        //        }

        //        if (objRQ_EventDetail.objEvent.Platform4 != 0)
        //        {
        //            objRQ_EventDetail.objTagQuery.PlatformID = objRQ_EventDetail.objEvent.Platform4;
        //            uw.SahadevA2Repository.InsertTagQuery(objRQ_EventDetail.objTagQuery, uw.GetSahadevA2Transaction());
        //        }

        //        uw.Commit();
        //        return true;


        //    }
        //    catch (Exception ex)
        //    {
        //        uw.Rollback();
        //        _logger.LogError(ex, _className, "InsertEvent");
        //        return false;
        //    }
        //}


    }
}
