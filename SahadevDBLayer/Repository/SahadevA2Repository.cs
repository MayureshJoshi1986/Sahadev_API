﻿/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- SahadevA2Repository                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is SahadevA2Repository class which contains all functions &         *
 *                     SP related to SahadevA2 repository                                       *
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
using Dapper;
using SahadevBusinessEntity.DTO.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevA2Repository class
    /// </summary>
    public interface ISahadevA2Repository
    {
        List<Client> Get(IDbTransaction transaction);
        bool InsertClient(Client objClient, IDbTransaction transaction);

        int InsertClientTopic(ClientTopic objClientTopic, IDbTransaction transaction);
    }

    internal class SahadevA2Repository : RepositoryBase, ISahadevA2Repository
    {
        public SahadevA2Repository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {
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
        public List<Client> Get(IDbTransaction transaction)
        {
            try
            {
                var data = GetAllByProcedure<Client>(@"[dbo].[USP_ClientDetail_FetchAll]", null, transaction);
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
        public bool InsertClient(Client objClient, IDbTransaction transaction)
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
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_ClientDetail_Insert]", dbparams, transaction);
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
        public int InsertClientTopic(ClientTopic objClientTopic, IDbTransaction transaction)
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

                iResult = GetByProcedure<int>(@"[dbo].[USP_ClientTopic_Insert]", dbparams, transaction);
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
        public int InsertTag(Tag objTag, IDbTransaction transaction)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@igTagID", objTag.IGTagID);
                dbparams.Add("@tagName", objTag.TagName);
                dbparams.Add("@tagDescription", objTag.TagDescription);
                dbparams.Add("@isActive", objTag.IsActive);
                iResult = GetByProcedure<int>(@"[dbo].[USP_Tag_Insert]", dbparams, transaction);
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
        public bool InsertTagMap(TagMap objTagMap, IDbTransaction transaction)
        {
            bool bResult = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@clientTopicID", objTagMap.ClientTopicID);
                dbparams.Add("@tagID", objTagMap.TagID);
                dbparams.Add("@isActive", objTagMap.IsActive);

                int iResult = InsertByProcedure<int>(@"[dbo].[USP_TagMap_Insert]", dbparams, transaction);
                if (iResult != 0)
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
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertTagQuery(TagQuery objTagQuery, IDbTransaction transaction)
        {
            bool bResult = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagID", objTagQuery.TagID);
                dbparams.Add("@platformID", objTagQuery.PlatformID);
                dbparams.Add("@query", objTagQuery.Query);
                dbparams.Add("@typeOfQuery", objTagQuery.TypeOfQuery);
                dbparams.Add("@isActive", objTagQuery.IsActive);

                int iResult = InsertByProcedure<int>(@"[dbo].[USP_TagQuery_Insert]", dbparams, transaction);
                if (iResult != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bResult;

        }
    }
}
