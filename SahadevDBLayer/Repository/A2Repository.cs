/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- A2Repository                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is A2Repository class which contains all functions &                *
 *                     SP related to SahadevA2 repository                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 17-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  Changed class name from SahadevA2Repository to A2Repository             *                                                         *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Dapper;
using SahadevBusinessEntity.DTO.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of A2Repository class
    /// </summary>
    public interface IA2Repository
    {
        List<Client> Get();
        bool InsertClient(Client objClient);

        int InsertClientTopic(ClientTopic objClientTopic);

        int InsertTag(Tag objTag);

        bool InsertTagMap(TagMap objTagMap);

        int InsertTagQuery(TagQuery objTagQuery);


        List<ClientTopic> GetAllClientTopicByClientID(int topicTypeId, int clientId);
        ClientTopic GetClientTopic(int topicTypeId, int clientId, int refTopicId);

        Tag GetTagByClientTopicID(int clientTopicId);

        List<TagQuery> GetAllTagQueryByTagID(int tagId);

        bool UpdateTagQuery(TagQuery objTagQuery);
        ClientTopic GetClientTopic(int clientTopicId);

        Client GetClientByClientID(int clientId, int coretagID);


    }

    internal class A2Repository : RepositoryBase, IA2Repository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public A2Repository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        /// <summary>
        /// This method is used to get fetch client detail from client table
        /// </summary>
        /// <returns>list of object containing client detail</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<Client> Get()
        {
            try
            {
                var data = GetAllByProcedure<Client>(@"[dbo].[USP_ClientDetail_FetchAll]", null, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="coretagID"></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public Client GetClientByClientID(int clientId, int coretagID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientId", clientId);
                dbparams.Add("@coretagID", coretagID);
                var data = GetByProcedure<Client>(@"[dbo].[USP_ClientDetail_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
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
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientId", clientId);
                dbparams.Add("@topicTypeId", topicTypeId);
                var data = GetAllByProcedure<ClientTopic>(@"[dbo].[USP_ClientTopic_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to get fetch clienttopic  from clienttopic table
        /// </summary>
        /// <param name="clientId">pass client id for which client topic need to be fetched</param>
        /// <param name="topicTypeId">topictype id for event = 2, dossier 3, and ClientOnboard = 1</param>
        /// <param name="refTopicId">refTopicId means eventId of sentry or DossierDefID or for clientonboard</param>
        /// <returns>object containing client topic</returns>
        /// <createdon>02-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public ClientTopic GetClientTopic(int clientId, int refTopicId, int topicTypeId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientId", clientId);
                dbparams.Add("@refTopicId", refTopicId);
                dbparams.Add("@topicTypeId", topicTypeId);
                var data = GetByProcedure<ClientTopic>(@"[dbo].[USP_Client_ClientTopic_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to fetch clienttopic  from clienttopic table
        /// </summary>
        /// <param name="clientTopicId">pass clienttopicId for which client topic need to be fetched</param>
        /// <returns>object containing client topic</returns>
        /// <createdon>02-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public ClientTopic GetClientTopic(int clientTopicId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientTopicId", clientTopicId);
                var data = GetByProcedure<ClientTopic>(@"[dbo].[USP_GetClientTopic]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to get fetch tag from tagmap (get tagid from clienttopicid) and then 
        /// get tag details from tag table
        /// </summary>
        /// <param name="clientTopicId">pass client id for which tag need to be fetched</param>

        /// <returns>return tag detail</returns>
        /// <createdon>01-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public Tag GetTagByClientTopicID(int clientTopicId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientTopicId", clientTopicId);
                var data = GetByProcedure<Tag>(@"[dbo].[USP_Tag_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to get fetch tagqueries for tag id
        /// </summary>
        /// <param name="tagId">pass tag id for which tagquery to be fetched</param>
        /// <returns>list of object containing tag Queries</returns>
        /// <createdon>01-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<TagQuery> GetAllTagQueryByTagID(int tagId)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagId", tagId);
                var data = GetAllByProcedure<TagQuery>(@"[dbo].[USP_TagQuery_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// This method is used to insert client detail in client table
        /// </summary>
        /// <param name="objClient">object containing client detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>14-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertClient(Client objClient)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@name", objClient.Name);
                dbparams.Add("@registeredName", objClient.RegisteredName);
                dbparams.Add("@description", objClient.Description);
                dbparams.Add("@bseListed", objClient.BSEListed);
                dbparams.Add("@nseListed", objClient.NSEListed);
                dbparams.Add("@coreTagID", objClient.CoreTagID);
                dbparams.Add("@activationFrom", objClient.ActivationFrom);
                dbparams.Add("@validUntil", objClient.ValidUntil);
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_ClientDetail_Insert]", dbparams, _transaction);
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
        /// This method is used to insert client topic detail in ClientTopic table
        /// </summary>
        /// <param name="objClientTopic">object containing client topic detail</param>
        /// <returns>PK of ClientTopic if successfully inserted else 0</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertClientTopic(ClientTopic objClientTopic)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientID", objClientTopic.ClientID);
                dbparams.Add("@topicTypeID", objClientTopic.TopicTypeID);
                dbparams.Add("@topicName", objClientTopic.TopicName);
                dbparams.Add("@topicDescription", objClientTopic.TopicDescription);
                dbparams.Add("@refTopicID", objClientTopic.RefTopicID);
                dbparams.Add("@status", objClientTopic.Status);
                dbparams.Add("@startDate", objClientTopic.StartDate);
                dbparams.Add("@endDate", objClientTopic.EndDate);

                iResult = GetByProcedure<int>(@"[dbo].[USP_ClientTopic_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }

        /// <summary>
        /// This method is used to insert tag detail in Tag table
        /// </summary>
        /// <param name="objTag">object containing tag detail</param>
        /// <returns>PK of Tag table if successfully inserted else 0</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertTag(Tag objTag)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@igTagID", objTag.IGTagID);
                dbparams.Add("@tagName", objTag.TagName);
                dbparams.Add("@tagDescription", objTag.TagDescription);
                dbparams.Add("@isActive", objTag.IsActive);
                iResult = GetByProcedure<int>(@"[dbo].[USP_Tag_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }

        /// <summary>
        /// This method is used to insert tag map detail in TagMap table
        /// </summary>
        /// <param name="objTagMap">object containing TagMap detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertTagMap(TagMap objTagMap)
        {
            bool bResult = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientTopicID", objTagMap.ClientTopicID);
                dbparams.Add("@tagID", objTagMap.TagID);
                dbparams.Add("@isActive", objTagMap.IsActive);

                int iResult = InsertByProcedure<int>(@"[dbo].[USP_TagMap_Insert]", dbparams, _transaction);
                if (iResult != -1)
                    bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bResult;

        }

        /// <summary>
        /// This method is used to insert tag query detail in TagQuery table
        /// </summary>
        /// <param name="objTagQuery">object containing TagQuery detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>28-00-2024</modifiedon>
        /// <modifiedby>Saroj Laddha</modifiedby>
        /// <modifiedreason>To return TagQueryId</modifiedreason>
        public int InsertTagQuery(TagQuery objTagQuery)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagID", objTagQuery.TagID);
                dbparams.Add("@platformID", objTagQuery.PlatformID);
                dbparams.Add("@query", objTagQuery.Query);
                dbparams.Add("@typeOfQuery", objTagQuery.TypeOfQuery);
                dbparams.Add("@isActive", objTagQuery.IsActive);
                return GetByProcedure<int>(@"[dbo].[USP_TagQuery_Insert]", dbparams, _transaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        /// <summary>
        /// This method is used to update tag query detail in TagQuery table
        /// </summary>
        /// <param name="objTagQuery">object containing TagQuery detail</param>
        /// <returns>true if successfully updated else false</returns>
        /// <createdon>01-Oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>

        public bool UpdateTagQuery(TagQuery objTagQuery)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagQueryId", objTagQuery.TagQueryID);
                dbparams.Add("@tagID", objTagQuery.TagID);
                dbparams.Add("@platformID", objTagQuery.PlatformID);
                dbparams.Add("@query", objTagQuery.Query);
                dbparams.Add("@typeOfQuery", objTagQuery.TypeOfQuery);
                dbparams.Add("@isActive", objTagQuery.IsActive);
                int iResult = UpdateByProcedure<int>(@"[dbo].[USP_TagQuery_Update]", dbparams, _transaction);
                if (iResult != 0)
                    bReturn = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bReturn;


        }
    }
}
