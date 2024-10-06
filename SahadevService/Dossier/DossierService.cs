/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- DossierService                                                           *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is DossierService class which contains all method related to        *
 *                     Dossier                                                                  *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 22-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  Added new methods GetAllDossierScheduleType & GetAllDossierEventType    *  
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  In InsertAdditionalURL method, removed unwanted fields like             *
 *                      isProcessed, ErrorMsg etc, & handle multiple url                        *
 //**********************************************************************************************/
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.RequestModel;
using SahadevDBLayer.UnitOfWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.SymbolStore;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static System.Net.WebRequestMethods;
namespace SahadevService.Dossier
{

    /// <summary>
    /// Interface DossierService class  
    /// </summary>
    interface IDossierService
    {
        List<dynamic> GetAllClientsByTagID(int tgID);
        List<dynamic> GetAllClientByUserID(int userID);
        List<dynamic> GetAllUser();
        DossierDef GetDossierDef(int dossierDefID);
        List<dynamic> GetAllDossier(int UserID, int[] ClientID, int StatusID, DateTime? StartDate = null, DateTime? EndDate = null);
        List<dynamic> GetAllGeneratedDossier(int UserID, int[] ClientID, int StatusID, DateTime? StartDate = null, DateTime? EndDate = null);
        dynamic GetGeneratedDossier(int dossierDefID);
        List<AdditionalURL> GetAllAdditionalURL(int dossierID);
        bool InsertDossierDef(RQ_DossierDef objRQ_DossierDef);
        bool InsertAdditionalURL(RQ_AdditionalURL objRQ_AdditonalURL);
        bool UpdateDossierDef(RQ_DossierDef objRQ_DossierDef);




        bool MoveToTrash(List<string> dossierLinkMapID);
        bool SaveToDraft(List<string> dossierLinkMapID);
        bool UpdateDataAfterEdit(List<RQ_DossierReviewLinks> lstLinksToUpdate);


        dynamic GetAllDossierReviewDataDetails(int dossierID, int plateformID);
        dynamic GetAllDossierDraftDataDetails(int dossierID, int plateformID);
        dynamic GetAllDossierTrashDataDetails(int dossierID, int plateformID);

        List<dynamic> GetAllDossierScheduleType();

        List<dynamic> GetEventType();

        List<ClientTopic> GetAllClientTopicByClientID(int topicTypeId, int clientId); //new end point required
        ClientTopic GetClientTopic(int clientTopicId);
    }

    public class DossierService : IDossierService
    {
        private const string _className = "SahadevService.DossierService";
        private readonly UnitOfWork uw = null;
        private readonly ILogger<ServiceSingleton> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor defined for DossierService class
        /// </summary>
        /// <param name="uw">object of UnitOfWork defined</param>
        /// <param name="logger">object of Logger defined for serilog</param>
        public DossierService(IUnitOfWork uw, ILogger<ServiceSingleton> logger)
        {
            this.uw = uw as UnitOfWork;
            this._logger = logger;
            this.SS = new ServiceSingleton(this.uw, logger);
        }

        /// <summary>
        /// This method is used to get all client by TagId
        /// by fecthing first tagID's from mstTagGroupTable against cometitor name (tagGroupName)
        /// and then taking all tagID and fetching All client for TagId assigned
        /// </summary>
        /// <returns>list of object containing client</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllClientsByTagID(int tgID)
        {
            try
            {
                var lstTagID = uw.C3Repository.GetAllTagIDByTagGroupName(tgID);
                string strTagID = String.Join(",", lstTagID);
                List<dynamic> lstGetAllClientByTagID = uw.C1Repository.GetAllClientByTagID(strTagID);
                return lstGetAllClientByTagID;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllClientsByTagID");
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to get fetch clienttopic  from clienttopic table
        /// </summary>
        /// <param name="clientId">pass client id for which client topic need to be fetched</param>
        /// <param name="topicTypeId">topictype id for event = 2, dossier 3, and ClientOnboard = 1</param>
        /// <returns>list of object containing client topic</returns>
        /// <createdon>01-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<ClientTopic> GetAllClientTopicByClientID(int topicTypeId, int clientId)
        {
            try
            {
                return uw.A2Repository.GetAllClientTopicByClientID(topicTypeId, clientId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllClientTopicByClientID");
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to fetch clienttopic  from clienttopic table
        /// </summary>
        /// <param name="clientTopicId">pass clienttopicid for which client topic need to be fetched</param>
        /// <returns>list of object containing client topic</returns>
        /// <createdon>01-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public ClientTopic GetClientTopic(int clientTopicId)
        {
            try
            {
                ClientTopic clientTopic = uw.A2Repository.GetClientTopic(clientTopicId);
                if (clientTopic != null)
                {
                    clientTopic.Tag = uw.A2Repository.GetTagByClientTopicID(clientTopicId);
                    if (clientTopic.Tag != null)
                    {
                        clientTopic.Tag.TagQuery = uw.A2Repository.GetAllTagQueryByTagID(clientTopic.Tag.TagID);
                    }
                }
                return clientTopic;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetClientTopic");
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to get all client by user id
        /// </summary>
        /// <returns>list of object containing client</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllClientByUserID(int userID)
        {
            try
            {
                List<dynamic> lstGetAllClientByUserID = uw.C1Repository.GetAllClientByUserID(userID);
                return lstGetAllClientByUserID;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllClientsByUserID");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get all user
        /// </summary>
        /// <returns>list of object containing user</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllUser()
        {
            try
            {
                List<dynamic> lstAllUser = uw.C1Repository.GetAllUser();
                return lstAllUser;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllUser");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get DossierDef Detail with all related Table
        /// </summary>
        /// <param name="dossierDefID">dossierDefID</param>
        /// <returns>object containing DossierDef and its related table Detail</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public DossierDef GetDossierDef(int dossierDefID)
        {
            try
            {
                DossierDef objDossierDef = uw.C3Repository.GetDossierDef(dossierDefID);

                if (objDossierDef != null)
                {
                    objDossierDef.DossierReceps = uw.C3Repository.GetAllDossierRecep(dossierDefID);
                    objDossierDef.DossierSch = uw.C3Repository.GetDossierSch(dossierDefID);
                    objDossierDef.DossierConf = uw.C3Repository.GetDossierConf(dossierDefID);
                    ClientTopic objClientTopic;
                    if (objDossierDef.DossierTypeID == 1 && objDossierDef.EventTagID > 0)
                    {
                        objDossierDef.Tag = uw.C3Repository.GetTagByTaID(objDossierDef.EventTagID);
                        objClientTopic = uw.A2Repository.GetClientTopic(objDossierDef.ClientID, objDossierDef.DossierDefID, 2);
                        if (objClientTopic != null)
                        {
                            objDossierDef.ClientTopicID = objClientTopic.ClientTopicID;

                        }
                        if (objDossierDef.Tag != null)
                        {
                            objDossierDef.Tag.TagQuery = uw.C3Repository.GetAllTagQueryByTagID(objDossierDef.Tag.TagID);
                        }
                    }
                    else if (objDossierDef.DossierTypeID == 1 && objDossierDef.EventTagID <= 0)
                    {
                        objClientTopic = uw.A2Repository.GetClientTopic(objDossierDef.ClientID, objDossierDef.DossierDefID, 3);
                        objDossierDef.Tag = uw.A2Repository.GetTagByClientTopicID(objClientTopic.ClientTopicID);
                        if (objDossierDef.Tag != null)
                        {
                            objDossierDef.Tag.TagQuery = uw.C3Repository.GetAllTagQueryByTagID(objDossierDef.Tag.TagID);
                        }

                    }
                    else if (objDossierDef.DossierTypeID == 2)
                    {
                        objDossierDef.DossierTagGroup = uw.C3Repository.GetDossierTagGroup(dossierDefID);
                    }
                }
                return objDossierDef;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetDossierDef");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get All Dossier
        /// </summary>
        /// <returns>object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>30-aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>get dossier according to userId, client,status and Active </modifiedreason>
        /// <modifiedon>26-Sep-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changes to handle multiple clientID</modifiedreason>
        public List<dynamic> GetAllDossier(int UserID, int[] ClientID, int StatusID, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            try
            {
                dynamic objDossier = uw.C3Repository.GetAllDossier(UserID, ClientID, StatusID, StartDate, EndDate);

                return objDossier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllDossier");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get All Generated Dossier 
        /// </summary>
        /// <returns>list of object containing All Generated Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>30-aug-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>get dossier according to userId, client,status and Active </modifiedreason>
        /// <modifiedon>26-Sep-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changes to handle multiple clientID</modifiedreason>
        public List<dynamic> GetAllGeneratedDossier(int UserID, int[] ClientID, int StatusID, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            try
            {
                List<dynamic> lstDossiers = uw.C3Repository.GetAllGeneratedDossier(UserID, ClientID, StatusID, StartDate, EndDate);
                return lstDossiers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllGeneratedDossier");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get GeneratedDossier of particular configuration
        /// </summary> 
        /// <param name="dossierConfID">DossierConfID</param>
        /// <returns>list of object containing Dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public dynamic GetGeneratedDossier(int dossierConfID)
        {
            try
            {
                dynamic objDossier = uw.C3Repository.GetGeneratedDossier(dossierConfID);
                return objDossier;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetGeneratedDossier");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get all additonal URLs of a dossier
        /// </summary>
        /// <param name="dossierID">dossierID</param>
        /// <returns>list of object containing all additional URLs of a dossier</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<AdditionalURL> GetAllAdditionalURL(int dossierID)
        {
            try
            {
                List<AdditionalURL> lstAdditionalURL = uw.C3Repository.GetAllAdditionalUrl(dossierID);
                return lstAdditionalURL;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, " GetAllAdditionalURL");
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to fetch all dossier schedule type
        /// </summary>
        /// <returns>list of object containing dossier schedule type</returns>
        /// <createdon>26-Sept-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllDossierScheduleType()
        {
            try
            {
                List<dynamic> lstDossierScheduleType = uw.C3Repository.GetAllDossierScheduleType();
                return lstDossierScheduleType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, " GetAllDossierScheduleType");
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to fetch all event type for dropdown
        /// </summary>
        /// <returns>list of object containing event type</returns>
        /// <createdon>26-Sept-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetEventType()
        {
            try
            {
                List<dynamic> lstEventType = uw.C2Repository.GetEventType();
                return lstEventType;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, " GetEventType");
                throw ex;
            }
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
        public dynamic GetAllDossierReviewDataDetails(int dossierID, int platformID)
        {
            dynamic lstlinkDetails = null;
            try
            {
                //fetch the link Id's from DossierLinkMap table for verify 
                List<dynamic> lstReviewLinks = uw.C3Repository.GetAllDossierReviewDataLinks(dossierID, platformID);
                if (lstReviewLinks != null)
                {
                    // extract link ID's // handling null and fecthing links whose id > 0 to fecth data from E database
                    List<string> lstLinkID = lstReviewLinks.Where(x => x.LinkID != null && ((int)x.LinkID) > 0).Select(x => ((int)x.LinkID).ToString()).ToList();

                    // make a single comma seperated string 
                    string strLinkID = String.Join(",", lstLinkID);



                    //fetch the link details from the E Database 
                    List<dynamic> lstReviewLinkDetails = uw.ERepository.GetAllDossierReviewDataDetails(platformID, strLinkID);


                    //fecthing all the records of the additional url added in Datalinkmap table that has link Id null
                    //and then fetching data for the link id details for online from E database
                    // so datafrom link map table can have link id null or 0 for the additional url and join will give only matching 
                    //result set so doing union all join to get matching linkId data from E database 
                    //and all the addition url with all necessary flags required

                    lstlinkDetails = lstReviewLinks.GroupJoin(lstReviewLinkDetails,
                                  links => links.LinkID,
                                  linkDetail => linkDetail.LinkID,
                                (links, linkDetailGroup) => new { links, linkDetailGroup })
                                .SelectMany(x => x.linkDetailGroup.DefaultIfEmpty(),//include the result  if there are no matches even(Left Join)
                                (x, linkDetail) =>

                                  {

                                      // to handle new columns added in Table and SP's  dynamically 
                                      // no requirement to do any change in code for getting data of a new column. this code will handle all 
                                      // the new columns added in SP's
                                      dynamic expando = new ExpandoObject();
                                      var dictionary = (IDictionary<string, object>)expando;

                                      //add all properties from the first object
                                      if (x.links != null)
                                      {
                                          foreach (var item in (IDictionary<string, object>)x.links)
                                          {

                                              dictionary[item.Key] = item.Value;
                                          }
                                      }

                                      //add all properties from the second object
                                      if (linkDetail != null)
                                      {
                                          foreach (var item in (IDictionary<string, object>)linkDetail)
                                          {
                                              dictionary[item.Key] = item.Value;
                                          }
                                      }
                                      else {

                                          foreach (var key in ((IDictionary<string, object>)lstReviewLinkDetails.First()).Keys)
                                          {
                                              dictionary[key] = null;
                                          }
                                      }
                                      return expando;

                                  }).ToList();



                }

                return lstlinkDetails;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllDossierReviewDataDetails");
                throw ex;
            }


        }

        /// <summary>
        /// To fetch the Data links Details that saved to draft
        /// </summary>
        /// <param name="dossierID">To fetch the links Details of a dossier</param>
        /// <param name="platformID"> To fetch the link Details of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing link Details that are Saved To draft</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public dynamic GetAllDossierDraftDataDetails(int dossierID, int platformID)
        {
            dynamic lstlinkDetails = null;
            try
            {
                //fetch the link Id's from DossierLinkMap table for verify Draft Data
                List<dynamic> lstReviewDraftLinks = uw.C3Repository.GetAllDossierReviewDraftDataLinks(dossierID, platformID);
                if (lstReviewDraftLinks != null)
                {
                    // extract link ID's
                    List<string> lstLinkID = lstReviewDraftLinks.Where(x => x.LinkID != null && ((int)x.LinkID) > 0).Select(x => ((int)x.LinkID).ToString()).ToList();

                    // make a single comma seperated string 
                    string strLinkID = String.Join(",", lstLinkID);

                    //fetch the link details from the E Database 
                    List<dynamic> lstReviewDraftLinkDetails = uw.ERepository.GetAllDossierDraftDataDetails(platformID, strLinkID);

                    //joining both records as we would require DossierMapLinkID from the Sehdev_C2_2 Database to perform actions
                    // and with all necessary details from the E Database
                    lstlinkDetails = lstReviewDraftLinks.GroupJoin(lstReviewDraftLinkDetails,
                                  links => links.LinkID,
                                  linkDetail => linkDetail.LinkID,
                                    (links, linkDetailGroup) => new { links, linkDetailGroup })
                                 .SelectMany(x => x.linkDetailGroup.DefaultIfEmpty(),//include the result  if there are no matches even(Left Join)
                                 (x, linkDetail) =>
                                 // (linkDetail, links) =>
                                 {

                                     // to handle new columns added in Table and SP's  dynamically 
                                     // no requirement to do any change in code for getting data of a new column. this code will handle all 
                                     // the new columns added in SP's
                                     dynamic expando = new ExpandoObject();
                                     var dictionary = (IDictionary<string, object>)expando;

                                     //add all properties from the first object
                                     if (x.links != null)
                                     {
                                         foreach (var item in (IDictionary<string, object>)x.links)
                                         {

                                             dictionary[item.Key] = item.Value;
                                         }
                                     }

                                     //add all properties from the second object
                                     if (linkDetail != null)
                                     {
                                         foreach (var item in (IDictionary<string, object>)linkDetail)
                                         {
                                             dictionary[item.Key] = item.Value;
                                         }
                                     }
                                     else
                                     {

                                         foreach (var key in ((IDictionary<string, object>)lstReviewDraftLinkDetails.First()).Keys)
                                         {
                                             dictionary[key] = null;
                                         }
                                     }
                                     return expando;

                                 }).ToList();



                }

                return lstlinkDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllDossierDraftDataDetails");
                throw ex;
            }
        }

        /// <summary>
        /// To fetch the Data link Details that moved to trash 
        /// </summary>
        /// <param name="dossierID">To fetch the links Detail of a link of a dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing link Details that are moved To trash</returns>
        /// <createdon>06-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public dynamic GetAllDossierTrashDataDetails(int dossierID, int platformID)
        {

            dynamic lstlinkDetails = null;
            try
            {
                //fetch the link Id's from DossierLinkMap table for showing Links That are deleted
                List<dynamic> lstDeletedLinks = uw.C3Repository.GetAllDossierTrashDataLinks(dossierID, platformID);
                if (lstDeletedLinks != null)
                {
                    // extract link ID's
                    List<string> lstLinkID = lstDeletedLinks.Where(x => x.LinkID != null && ((int)x.LinkID) > 0).Select(x => ((int)x.LinkID).ToString()).ToList();

                    // make a single comma seperated string 
                    string strLinkID = String.Join(",", lstLinkID);

                    //fetch the deleted links details from the E Database 
                    List<dynamic> lstDeletedLinkDetails = uw.ERepository.GetAllDossierTrashDataDetails(platformID, strLinkID);

                    //joining both records as we would require DossierMapLinkID from the Sehdev_C2_2 Database to perform actions
                    // and with all necessary details from the E Database
                    lstlinkDetails = lstDeletedLinks.GroupJoin(lstDeletedLinkDetails,
                                  links => links.LinkID,
                                  linkDetail => linkDetail.LinkID,
                                   (links, linkDetailGroup) => new { links, linkDetailGroup })
                                     .SelectMany(x => x.linkDetailGroup.DefaultIfEmpty(),
                                     (x, linkDetail) =>
                                      {

                                          // to handle new columns added in Table and SP's  dynamically 
                                          // no requirement to do any change in code for getting data of a new column. this code will handle all 
                                          // the new columns added in SP's
                                          dynamic expando = new ExpandoObject();
                                          var dictionary = (IDictionary<string, object>)expando;

                                          //add all properties from the first object
                                          if (x.links != null)
                                          {
                                              foreach (var item in (IDictionary<string, object>)x.links)
                                              {

                                                  dictionary[item.Key] = item.Value;
                                              }
                                          }

                                          //add all properties from the second object
                                          if (linkDetail != null)
                                          {
                                              foreach (var item in (IDictionary<string, object>)linkDetail)
                                              {
                                                  dictionary[item.Key] = item.Value;
                                              }
                                          }
                                          else
                                          {

                                              foreach (var key in ((IDictionary<string, object>)lstDeletedLinkDetails.First()).Keys)
                                              {
                                                  dictionary[key] = null;
                                              }
                                          }
                                          return expando;

                                      }).ToList();



                }

                return lstlinkDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetAllDossierTrashDataDetails");
                throw ex;
            }
        }




        /// <summary>
        /// This method is used to insert AdditionalURL in AdditionalURL table
        /// </summary>
        /// <param name="objRQ_AdditonalURL">request object containing AdditionalURL</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping of the Request Model to Business Model</modifiedreason>
        public bool InsertAdditionalURL(RQ_AdditionalURL objRQ_AdditonalURL)
        {
            bool bReturn = false;
            try
            {

                //mapping of the Request model RQ_AdditionalURL to Business model AdditionalURL
                foreach (var url in objRQ_AdditonalURL.URL)
                {
                    AdditionalURL objAdditionalURL = new AdditionalURL();
                    objAdditionalURL.URL = url;
                    objAdditionalURL.DossierID = objRQ_AdditonalURL.DossierID;
                    objAdditionalURL.CreatedBy = objRQ_AdditonalURL.CreatedBy;
                    bReturn = uw.C3Repository.InsertAdditionalURl(objAdditionalURL);

                    //passing is Added True as it is new additional url added
                    // passing 1 be default for platform as additonal url are only added for online
                    bReturn = uw.C3Repository.InsertAddlUrlInDataLinkMap(url, true, 1, objRQ_AdditonalURL.DossierID);

                }

                //Commit the change 
                uw.Commit();
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertAdditionalURL");
            }
            return bReturn;
        }



        /// <summary>
        /// This method is used to insert DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        /// </summary>
        /// <param name="objRQ_DossierDef">request object DossierDef</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>27-Aug-2024</modifiedby>
        /// <modifiedreason>Changed request model and handled condition accordingly</modifiedreason>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping Request model to business model</modifiedreason>
        /// <modifiedon>30-01-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Done changes for the Event Dossier Tag,TagMap, ClientTopic, Tag Queries according to client topic selected and 
        /// dossierTag Group to be added only periodical dossierType
        /// </modifiedreason>
        public bool InsertDossierDef(RQ_DossierDef objRQ_DossierDef)
        {
            bool bReturn = false;
            try
            {
                string _templateFileName = string.Empty;
                if (objRQ_DossierDef.TemplateFile != null && !string.IsNullOrEmpty(objRQ_DossierDef.TemplateFile.FileName))
                    _templateFileName = objRQ_DossierDef.TemplateFile.FileName;

                //Mapping Request Model to Business Model

                //Mapping of DossierDef
                DossierDef objDossierDef = new DossierDef();
                objDossierDef.ClientID = objRQ_DossierDef.ClientID;
                objDossierDef.DossierTypeID = objRQ_DossierDef.DossierTypeID;
                objDossierDef.StartDate = objRQ_DossierDef.StartDate;
                objDossierDef.EndDate = objRQ_DossierDef.EndDate;
                objDossierDef.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierDef.Title = objRQ_DossierDef.Title;
                objDossierDef.EventTypeID = objRQ_DossierDef.EventTypeID;
                objDossierDef.EventContext = objRQ_DossierDef.EventContext;
                objDossierDef.EventRefURL = objRQ_DossierDef.EventRefURL;
                objDossierDef.EventKQuery = objRQ_DossierDef.EventKQuery;
                //objDossierDef.EventTagID = objRQ_DossierDef.EventTagID;
                objDossierDef.Platform1ID = objRQ_DossierDef.Platform1ID;
                objDossierDef.Platform2ID = objRQ_DossierDef.Platform2ID;
                objDossierDef.Platform3ID = objRQ_DossierDef.Platform3ID;
                objDossierDef.StatusID = objRQ_DossierDef.StatusID;
                objDossierDef.TemplateFileName = _templateFileName;
                objDossierDef.ClientName = objRQ_DossierDef.ClientName;//newfield

                //Insert into the DossierDef Table and get the PrimaryKey of DossierDef
                int dossierDefID = uw.C3Repository.InsertDossierDef(objDossierDef);
                objDossierDef.DossierDefID = dossierDefID;


                if (!string.IsNullOrEmpty(_templateFileName))
                    UploadDossierTemplate(dossierDefID, objRQ_DossierDef.TemplateFile);


                //if dossier type = 1 (Event Dossier) and Client Topic (Event is not selected)
                if (objRQ_DossierDef.DossierTypeID == 1 && objRQ_DossierDef.ClientTopicID <= 0)
                {
                    ClientTopic objClientTopic = new ClientTopic();
                    objClientTopic.RefTopicID = dossierDefID;
                    objClientTopic.StartDate = objRQ_DossierDef.StartDate;
                    objClientTopic.EndDate = objRQ_DossierDef.EndDate;
                    objClientTopic.TopicTypeID = 3; // 1 general listener (client on board)  , 2 Sentry , 3 Dossier
                    objClientTopic.Status = 1;
                    objClientTopic.TopicName = objRQ_DossierDef.Title;
                    objClientTopic.TopicDescription = objRQ_DossierDef.EventContext; //evntcontext
                    objClientTopic.ClientID = objRQ_DossierDef.ClientID;
                    objClientTopic.ClientTopicID = uw.A2Repository.InsertClientTopic(objClientTopic);

                    Tag objTag = new Tag();
                    objTag.IsActive = true;
                    objTag.TagDescription = objRQ_DossierDef.EventContext;
                    objTag.TagName = objRQ_DossierDef.Title;
                    objTag.TagID = uw.A2Repository.InsertTag(objTag);
                    //insert tag entry in c3 Database
                    uw.C3Repository.InsertTag(objTag);


                    //call update dossier function to only update eventtagid 

                    objDossierDef.EventTagID = objTag.TagID;
                    uw.C3Repository.UpdateDossierDef(objDossierDef);

                    //tag map entry in A2 Database

                    TagMap objTagMap = new TagMap();
                    objTagMap.TagID = objTag.TagID;
                    objTagMap.ClientTopicID = objClientTopic.ClientTopicID;
                    objTagMap.IsActive = true;
                    uw.A2Repository.InsertTagMap(objTagMap);

                    //Add entries in Tag Query A2 Database

                    if (objRQ_DossierDef.TagQuery != null)
                    {
                        foreach (var query in objRQ_DossierDef.TagQuery)
                        {
                            TagQuery objTagQuery = new TagQuery();
                            objTagQuery.TagID = objTag.TagID;
                            objTagQuery.Query = query.Query;
                            objTagQuery.TypeOfQuery = "Keyword";
                            objTagQuery.IsActive = true;
                            objTagQuery.PlatformID = query.PlatformID;
                            objTagQuery.TagQueryID = uw.A2Repository.InsertTagQuery(objTagQuery);
                            uw.C3Repository.InsertTagQuery(objTagQuery);
                        }
                    }
                }
                //elseif dossier type =1 (Event Dossier) and Client Topic (Event is selected)
                else if (objRQ_DossierDef.DossierTypeID == 1 && objRQ_DossierDef.ClientTopicID > 0)
                {
                    if (objRQ_DossierDef.Tag != null)
                    {
                        Tag objTag = new Tag();
                        objTag.IsActive = objRQ_DossierDef.Tag.IsActive;
                        objTag.TagDescription = objRQ_DossierDef.Tag.TagDescription;
                        objTag.TagName = objRQ_DossierDef.Tag.TagName;
                        objTag.TagID = objRQ_DossierDef.Tag.TagID;
                        objTag.IGTagID = objRQ_DossierDef.Tag.IGTagID;
                        uw.C3Repository.InsertTag(objTag);
                    }

                    if (objRQ_DossierDef.TagQuery != null && objRQ_DossierDef.TagQuery.Count > 0)
                    {

                        foreach (RQ_TagQuery tagQuery in objRQ_DossierDef.TagQuery)
                        {
                            TagQuery objTagQuery = new TagQuery();
                            objTagQuery.TagID = tagQuery.TagID;
                            objTagQuery.TagQueryID = tagQuery.TagQueryID;
                            objTagQuery.IsActive = tagQuery.IsActive;
                            objTagQuery.Query = tagQuery.Query;
                            objTagQuery.TypeOfQuery = "Keyword";
                            objTagQuery.PlatformID = tagQuery.PlatformID;
                            uw.C3Repository.InsertTagQuery(objTagQuery);
                        }

                    }

                    //Tag objTag = uw.A2Repository.GetTagByClientTopicID(objRQ_DossierDef.ClientTopicID);
                    //if (objTag != null)
                    //{
                    //    uw.C3Repository.InsertTag(objTag);
                    //}

                    //List<TagQuery> lstTagQuery = uw.A2Repository.GetAllTagQueryByTagID(objTag.TagID);
                    //foreach (var query in lstTagQuery)
                    //{
                    //    uw.C3Repository.InsertTagQuery(query);
                    //}
                }





                //Mapping Of DossierSch
                DossierSch objDossierSch = new DossierSch();
                objDossierSch.DossierDefID = dossierDefID;
                objDossierSch.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierSch.Time1 = Convert.ToDateTime(objRQ_DossierDef.Time1);
                objDossierSch.Time2 = Convert.ToDateTime(objRQ_DossierDef.Time2);
                objDossierSch.DayOfMonth = objRQ_DossierDef.DayOfMonth;
                objDossierSch.DayOfWeek = objRQ_DossierDef.DayOfWeek;
                //objDossierSch.LastRun = objRQ_DossierDef.LastRun;
                //objDossierSch.NextRun = objRQ_DossierDef.NextRun;

                uw.C3Repository.InsertDossierSch(objDossierSch);


                //Mapping of DossierConf 

                //Insert into DossierConf table
                if (!string.IsNullOrEmpty(objRQ_DossierDef.ConfJSON))
                {
                    DossierConf objDossierConf = new DossierConf();
                    objDossierConf.DossierDefID = dossierDefID;
                    objDossierConf.ConfJSON = objRQ_DossierDef.ConfJSON;
                    uw.C3Repository.InsertDossierConf(objDossierConf);
                }

                //Multiple entries with for loop 
                //Insert into DossierRecep table
                foreach (var objRecipient in objRQ_DossierDef.Recipient)
                {
                    DossierRecep objDossierRecep = new DossierRecep();
                    objDossierRecep.DossierDefID = dossierDefID;
                    objDossierRecep.UserID = objRecipient.UserID;
                    uw.C3Repository.InsertDossierRecep(objDossierRecep);
                }

                if (objRQ_DossierDef.DossierTypeID == 2) // dossier tag group entry in only case of periodical dossier
                {
                    if (objRQ_DossierDef.TagGroup != null)
                    {
                        //multiple entries with for loop 
                        //Insert into DossierTagGroup table
                        foreach (var objTagGroup in objRQ_DossierDef.TagGroup)
                        {
                            DossierTagGroup objDossierTagGroup = new DossierTagGroup();
                            objDossierTagGroup.DossierDefID = dossierDefID;
                            objDossierTagGroup.TGID = objTagGroup.TGID;
                            objDossierTagGroup.TagID = objTagGroup.TagID;
                            objDossierTagGroup.TypeOfBinding = objTagGroup.TypeOfBinding;
                            uw.C3Repository.InsertDossierTagGroup(objDossierTagGroup);
                        }
                    }
                }


                //Commit the change 
                uw.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "InsertDossierDef");
            }
            return bReturn;
        }


        /// <summary>
        /// This method is used to Update DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        /// </summary>
        /// <param name="objRQ_DossierDef">request object DossierDef</param>
        /// <returns>true if successfully Update else false</returns>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>27-Aug-2024</modifiedby>
        /// <modifiedreason>Changed request model and handled condition accordingly</modifiedreason>
        /// <modifiedon>29-08-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Mapping Request model to business model</modifiedreason>
        /// <modifiedon>30-01-24</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>Done changes for the Event Dossier Tag Queries according to client topic selected and 
        /// dossierTag Group Add remove functionality added for periodical dossier 
        /// </modifiedreason>
        public bool UpdateDossierDef(RQ_DossierDef objRQ_DossierDef)
        {
            bool bReturn = false;
            try
            {
                string _templateFileName = string.Empty;
                if (objRQ_DossierDef.TemplateFile != null && !string.IsNullOrEmpty(objRQ_DossierDef.TemplateFile.FileName))
                    _templateFileName = objRQ_DossierDef.TemplateFile.FileName;

                //Mapping Request Model to Business Model

                //Mapping of DossierDef
                DossierDef objDossierDef = new DossierDef();
                objDossierDef.DossierDefID = objRQ_DossierDef.DossierDefID;
                objDossierDef.ClientID = objRQ_DossierDef.ClientID;
                objDossierDef.DossierTypeID = objRQ_DossierDef.DossierTypeID;
                objDossierDef.StartDate = objRQ_DossierDef.StartDate;
                objDossierDef.EndDate = objRQ_DossierDef.EndDate;
                objDossierDef.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierDef.Title = objRQ_DossierDef.Title;
                objDossierDef.EventTypeID = objRQ_DossierDef.EventTypeID;
                objDossierDef.EventContext = objRQ_DossierDef.EventContext;
                objDossierDef.EventRefURL = objRQ_DossierDef.EventRefURL;
                objDossierDef.EventKQuery = objRQ_DossierDef.EventKQuery;
                objDossierDef.EventTagID = objRQ_DossierDef.EventTagID;
                objDossierDef.Platform1ID = objRQ_DossierDef.Platform1ID;
                objDossierDef.Platform2ID = objRQ_DossierDef.Platform2ID;
                objDossierDef.Platform3ID = objRQ_DossierDef.Platform3ID;
                objDossierDef.TemplateFileName = _templateFileName;
                objDossierDef.StatusID = objRQ_DossierDef.StatusID;

                uw.C3Repository.UpdateDossierDef(objDossierDef);

                if (!string.IsNullOrEmpty(_templateFileName))
                    UploadDossierTemplate(objDossierDef.DossierDefID, objRQ_DossierDef.TemplateFile);

                //Mapping Of DossierSch
                DossierSch objDossierSch = new DossierSch();
                objDossierSch.DossierDefID = objRQ_DossierDef.DossierDefID;
                objDossierSch.DossierSchID = objRQ_DossierDef.DossierSchID;
                objDossierSch.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
                objDossierSch.Time1 = Convert.ToDateTime(objRQ_DossierDef.Time1);
                objDossierSch.Time2 = Convert.ToDateTime(objRQ_DossierDef.Time2);
                objDossierSch.DayOfMonth = objRQ_DossierDef.DayOfMonth;
                objDossierSch.DayOfWeek = objRQ_DossierDef.DayOfWeek;
                //objDossierSch.LastRun = objRQ_DossierDef.LastRun;
                //objDossierSch.NextRun = objRQ_DossierDef.NextRun;

                uw.C3Repository.UpdateDossierSch(objDossierSch);

                //Mapping of DossierConf 
                //Update DossierConf 
                if (!string.IsNullOrEmpty(objRQ_DossierDef.ConfJSON))
                {
                    DossierConf objDossierConf = new DossierConf();
                    objDossierConf.DossierConfID = objRQ_DossierDef.DossierConfID;
                    objDossierConf.DossierDefID = objRQ_DossierDef.DossierDefID;
                    objDossierConf.ConfJSON = objRQ_DossierDef.ConfJSON;
                    uw.C3Repository.UpdateDossierConf(objDossierConf);
                }


                //Multiple entries with for loop 

                //Update DossierRecep
                foreach (var objRecipient in objRQ_DossierDef.Recipient)
                {
                    DossierRecep objDossierRecep = new DossierRecep();
                    objDossierRecep.DossierRecepID = objRecipient.DossierRecepID;
                    objDossierRecep.DossierDefID = objRQ_DossierDef.DossierDefID;
                    objDossierRecep.UserID = objRecipient.UserID;
                    uw.C3Repository.UpdateDossierRecep(objDossierRecep);
                }


                //if dossier type =1 (Event Dossier) and Client Topic (Event is not selected)
                if (objRQ_DossierDef.DossierTypeID == 1 && objRQ_DossierDef.ClientTopicID <= 0)
                {
                    //Update entries in Tag Query A2 Database and c3 databae
                    if (objRQ_DossierDef.TagQuery != null)
                    {
                        foreach (var query in objRQ_DossierDef.TagQuery)
                        {
                            TagQuery objTagQuery = new TagQuery();
                            objTagQuery.TagQueryID = query.TagQueryID;
                            objTagQuery.TagID = query.TagID;
                            objTagQuery.Query = query.Query;
                            objTagQuery.TypeOfQuery = "Keyword";
                            objTagQuery.IsActive = true;
                            objTagQuery.PlatformID = query.PlatformID;
                            uw.A2Repository.UpdateTagQuery(objTagQuery);
                            uw.C3Repository.UpdateTagQuery(objTagQuery);

                        }
                    }
                }

                //multiple entries with for loop 
                //Update DossierTagGroup
                if (objRQ_DossierDef.DossierTypeID == 2) // dossier tag group entry in only case of periodical dossier
                {
                    if (objRQ_DossierDef.TagGroup != null)
                    {
                        foreach (var objTagGroup in objRQ_DossierDef.TagGroup)
                        {
                            if (objTagGroup.FlagToAddRemove == 1)
                            {
                                DossierTagGroup objDossierTagGroup = new DossierTagGroup();
                                objDossierTagGroup.DossierDefID = objRQ_DossierDef.DossierDefID;
                                objDossierTagGroup.TGID = objTagGroup.TGID;
                                objDossierTagGroup.TagID = objTagGroup.TagID;
                                objDossierTagGroup.TypeOfBinding = objTagGroup.TypeOfBinding;
                                uw.C3Repository.InsertDossierTagGroup(objDossierTagGroup);

                            }
                            else if (objTagGroup.FlagToAddRemove == 2)
                            {
                                uw.C3Repository.DeleteDossierTagGroup(objTagGroup.DossierTagGroupID);
                            }
                        }
                    }

                }


                //Commit the change 
                uw.Commit();
                bReturn = true;
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "UpdateDossierDef");
            }
            return bReturn;
        }


        /// <summary>
        /// This method is used to Update DossierLinkMap table To mark the IsDeleted = 1 to move data to trash
        /// </summary>
        /// <param name="dossierLinkMapID">object containing dossierLinkMapID to Update the record as deleted</param>
        /// <returns>true if successfully Update else false</returns>
        /// <createdon>06-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool MoveToTrash(List<string> dossierLinkMapID)
        {
            bool bReturn = false;
            try
            {
                string strLinkID = String.Join(",", dossierLinkMapID);
                bReturn = uw.C3Repository.MoveToTrash(strLinkID);

                //Commit the change 
                uw.Commit();
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "MoveToTrash");
            }
            return bReturn;
        }

        /// <summary>
        /// This method is used to Update DossierLinkMap table To mark the IsDraft = 1 to move data to Draft
        /// </summary>
        /// <param name="dossierLinkMapID">object containing dossierLinkMapID to Update the record as Draft</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>06-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool SaveToDraft(List<string> dossierLinkMapID)
        {
            bool bReturn = false;
            try
            {
                string strLinkID = String.Join(",", dossierLinkMapID);

                bReturn = uw.C3Repository.SaveToDraft(strLinkID);
                //Commit the change 
                uw.Commit();
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "SaveToDraft");
            }
            return bReturn;

        }

        /// <summary>
        /// This method is used to Update DossierLinkMap table To mark the IsEdit = 1 for records edit and also update DossierEdit 
        /// table to save the old and new updated values
        /// </summary>
        /// <param name="lstLinksToUpdate">object containing dossierLinkMapID to Update the record and contains old to new record change history json</param>
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>07-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        public bool UpdateDataAfterEdit(List<RQ_DossierReviewLinks> lstLinksToUpdate)
        {
            bool bIsUpdate = false;
            try
            {
                foreach (var link in lstLinksToUpdate)
                {
                    if (link.PlatformID == 0)
                    {
                        throw new Exception("platformId is not provided");
                    }
                    bIsUpdate = uw.C3Repository.UpdateDataAfterEdit(link.DossierLinkMapID, link.EditsJson, link.DossierID);
                    bIsUpdate = uw.ERepository.UpadateDataAfterEdit(link.PlatformID, link.LinkID, link.Sentiment, link.ArticleMention);
                }

                //Commit the change 
                uw.Commit();
            }
            catch (Exception ex)
            {
                uw.Rollback();
                _logger.LogError(ex, _className, "UpdateDataAfterEdit");
            }
            return bIsUpdate;

        }





        ///// <summary>
        ///// This method is used to insert DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        ///// </summary>
        ///// <param name="objRQ_DossierDef">request object DossierDef</param>
        ///// <returns>true if successfully inserted else false</returns>
        ///// <createdon>23-Aug-2024</createdon>
        ///// <createdby>Saroj Laddha</createdby>
        ///// <modifiedon>PJ</modifiedon>
        ///// <modifiedby>27-Aug-2024</modifiedby>
        ///// <modifiedreason>Changed request model and handled condition accordingly</modifiedreason>
        //public bool InsertDossierDef(RQ_DossierDef objRQ_DossierDef)
        //{
        //    bool bReturn = false;
        //    try
        //    {
        //        //Insert into the DossierDef Table and get the PrimaryKey of DossierDef
        //        int dossierDefID = uw.C3Repository.InsertDossierDef(objRQ_DossierDef);

        //        //Insert into DossierRecep table
        //        RQ_DossierRecep objRQ_DossierRecep = new RQ_DossierRecep();
        //        objRQ_DossierRecep.DossierDefID = dossierDefID;
        //        objRQ_DossierRecep.UserID = objRQ_DossierDef.UserID;
        //        uw.C3Repository.InsertDossierRecep(objRQ_DossierRecep);

        //        //Insert into DossierSch table
        //        RQ_DossierSch objRQ_DossierSch = new RQ_DossierSch();
        //        objRQ_DossierSch.DossierDefID = dossierDefID;
        //        objRQ_DossierSch.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
        //        objRQ_DossierSch.Time1 = objRQ_DossierDef.Time1;
        //        objRQ_DossierSch.Time2 = objRQ_DossierDef.Time2;
        //        objRQ_DossierSch.DayOfWeek = objRQ_DossierDef.DayOfWeek;
        //        objRQ_DossierSch.DayOfMonth = objRQ_DossierDef.DayOfMonth;
        //        objRQ_DossierSch.LastRun = objRQ_DossierDef.LastRun;
        //        objRQ_DossierSch.NextRun = objRQ_DossierDef.NextRun;
        //        uw.C3Repository.InsertDossierSch(objRQ_DossierSch);

        //        if (!string.IsNullOrEmpty(objRQ_DossierDef.ConfJSON))
        //        {
        //            //Insert into DossierConf table
        //            RQ_DossierConf objRQ_DossierConf = new RQ_DossierConf();
        //            objRQ_DossierConf.DossierDefID = dossierDefID;
        //            objRQ_DossierConf.ConfJSON = objRQ_DossierDef.ConfJSON;
        //            uw.C3Repository.InsertDossierConf(objRQ_DossierConf);
        //        }

        //        //Insert into DossierTagGroup table
        //        RQ_DossierTagGroup objRQ_DossierTagGroup = new RQ_DossierTagGroup();
        //        objRQ_DossierTagGroup.DossierDefID = dossierDefID;
        //        objRQ_DossierTagGroup.TGID = objRQ_DossierDef.TGID;
        //        objRQ_DossierTagGroup.TagID = objRQ_DossierDef.TagID;
        //        objRQ_DossierTagGroup.TypeOfBinding = objRQ_DossierDef.TypeOfBinding;
        //        uw.C3Repository.InsertDossierTagGroup(objRQ_DossierTagGroup);

        //        uw.Commit();
        //        bReturn = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        uw.Rollback();
        //        _logger.LogError(ex, _className, "InsertDossierDef");
        //    }
        //    return bReturn;
        ////}


        ///// <summary>
        ///// This method is used to Update DossierDef and DossierConf, DossierRecep, DossierSch, DossierTagGroup
        ///// </summary>
        ///// <param name="objRQ_DossierDef">request object DossierDef</param>
        ///// <returns>true if successfully Update else false</returns>
        ///// <createdon>26-Aug-2024</createdon>
        ///// <createdby>Saroj Laddha</createdby>
        ///// <modifiedon>PJ</modifiedon>
        ///// <modifiedby>27-Aug-2024</modifiedby>
        ///// <modifiedreason>Changed request model and handled condition accordingly</modifiedreason>
        //public bool UpdateDossierDef(RQ_DossierDef objRQ_DossierDef)
        //{
        //    bool bReturn = false;
        //    try
        //    {
        //        uw.C3Repository.UpdateDossierDef(objRQ_DossierDef);

        //        //Update DossierRecep
        //        RQ_DossierRecep objRQ_DossierRecep = new RQ_DossierRecep();
        //        objRQ_DossierRecep.DossierRecepID = objRQ_DossierDef.DossierRecepID;
        //        objRQ_DossierRecep.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        objRQ_DossierRecep.UserID = objRQ_DossierDef.UserID;
        //        uw.C3Repository.UpdateDossierRecep(objRQ_DossierRecep);

        //        //Update DossierSch
        //        RQ_DossierSch objRQ_DossierSch = new RQ_DossierSch();
        //        objRQ_DossierSch.DossierSchID = objRQ_DossierDef.DossierSchID;
        //        objRQ_DossierSch.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        objRQ_DossierSch.ScheduleTypeID = objRQ_DossierDef.ScheduleTypeID;
        //        objRQ_DossierSch.Time1 = objRQ_DossierDef.Time1;
        //        objRQ_DossierSch.Time2 = objRQ_DossierDef.Time2;
        //        objRQ_DossierSch.DayOfWeek = objRQ_DossierDef.DayOfWeek;
        //        objRQ_DossierSch.DayOfMonth = objRQ_DossierDef.DayOfMonth;
        //        objRQ_DossierSch.LastRun = objRQ_DossierDef.LastRun;
        //        objRQ_DossierSch.NextRun = objRQ_DossierDef.NextRun;
        //        uw.C3Repository.UpdateDossierSch(objRQ_DossierSch);

        //        //Update DossierConf 
        //        RQ_DossierConf objRQ_DossierConf = new RQ_DossierConf();
        //        objRQ_DossierConf.DossierConfID = objRQ_DossierDef.DossierConfID;
        //        objRQ_DossierConf.DossierDefID = objRQ_DossierDef.DossierDefID;
        //        objRQ_DossierConf.ConfJSON = objRQ_DossierDef.ConfJSON;
        //        uw.C3Repository.UpdateDossierConf(objRQ_DossierConf);

        //        //Update DossierTagGroup 
        //        RQ_DossierTagGroup objRQ_DossierTagGroup = new RQ_DossierTagGroup();
        //        objRQ_DossierTagGroup.DossierTagGroupID = objRQ_DossierDef.DossierTagGroupID;
        //        objRQ_DossierTagGroup.DossierDefID = objRQ_DossierDef.DossierConfID;
        //        objRQ_DossierTagGroup.TGID = objRQ_DossierDef.TGID;
        //        objRQ_DossierTagGroup.TagID = objRQ_DossierDef.TagID;
        //        objRQ_DossierTagGroup.TypeOfBinding = objRQ_DossierDef.TypeOfBinding;
        //        uw.C3Repository.UpdateDossierTagGroup(objRQ_DossierTagGroup);

        //        //Commit the change 
        //        uw.Commit();
        //        bReturn = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        uw.Rollback();
        //        _logger.LogError(ex, _className, "UpdateDossierDef");
        //    }
        //    return bReturn;
        //}


        private bool UploadDossierTemplate(int DossierDefID, IFormFile TemplateFile)
        {
            bool bReturn = false;
            try
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\dossiertemplate", DossierDefID.ToString());

                //Create folder with DossierDefID name if not exists
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                //save template file in folder
                var filePath = Path.Combine(folderPath, TemplateFile.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    TemplateFile.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "UploadDossierTemplate");
                throw ex;

            }
            return bReturn;
        }

    }
}
