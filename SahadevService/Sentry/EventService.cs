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
        public bool InsertEvent(Event objEvent)
        {
            bool bReturn = false;
            try
            {
                //Check if Tag Exists get TagId else Create tag  and Get TagId from SahdevA2 Database

                //New Event as a Tag Mapping
                Tag objTag = new Tag();
                objTag.IGTagID = null;// what to map with ???
                objTag.TagName = objEvent.EventName;
                objTag.TagDescription = objEvent.Description;
                objTag.IsActive = true;


                int TagID = uw.SahadevA2Repository.InsertTag(objTag, uw.GetSahadevA2Transaction());

                objTag.TagID = TagID;

                //Assign TagId to the Event
                objEvent.TagID = TagID;

                //Insert Event anf get event Id

                int EventID = uw.SahadevC2Repository.InsertEvent(objEvent, uw.GetSahadevC2Transaction());
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

                int ClientTopicId = uw.SahadevA2Repository.InsertClientTopic(objClientTopic, uw.GetSahadevA2Transaction());


                //do the enrty in Tag Map
                TagMap objTagMap = new TagMap();
                objTagMap.TagID = TagID;
                objTagMap.ClientTopicID = ClientTopicId;
                objTagMap.IsActive = objTag.IsActive;
                bool result = uw.SahadevA2Repository.InsertTagMap(objTagMap, uw.GetSahadevA2Transaction());

               // throw new TransactionAbortedException(); // Just to test the Transaction Rollback

                //do the entry in Tag query for all the selected plateform
                TagQuery objTagQuery = new TagQuery();

                if (objEvent.Platform1 != 0)
                {
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform1;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery, uw.GetSahadevA2Transaction());
                }
                if (objEvent.Platform2 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform2;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery, uw.GetSahadevA2Transaction());
                }

                if (objEvent.Platform3 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform3;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery, uw.GetSahadevA2Transaction());
                }

                if (objEvent.Platform4 != 0)
                {
                    objTagQuery = new TagQuery();
                    objTagQuery.TagID = TagID;
                    objTagQuery.Query = objEvent.Query;
                    objTagQuery.TypeOfQuery = string.Empty; // what to map with??
                    objTagQuery.PlatformID = objEvent.Platform4;
                    objTagQuery.IsActive = objTag.IsActive;
                    uw.SahadevA2Repository.InsertTagQuery(objTagQuery, uw.GetSahadevA2Transaction());
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


    }
}
