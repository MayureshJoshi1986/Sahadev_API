/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- BRepository                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is BRepository class which contains all functions &                 *
 *                     SP related to SahadevB database                                          *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 24-Oct-2024                                                              *
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

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of BRepository class
    /// </summary>
    public interface IBRepository
    {
        List<dynamic> GetIndustryAll();

        int InsertTag(Tag objTag);
    }
    internal class BRepository : RepositoryBase, IBRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public BRepository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        /// <summary>
        /// This method is used to get all industry from mstIndustry table
        /// </summary>
        /// <returns>list of object containing industry detail</returns>
        /// <createdon>24-Oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetIndustryAll()
        {
            try
            {
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_Industry_FetchAll]", null, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is used to insert tag detail in Tag table
        /// </summary>
        /// <param name="objTag">object containing tag detail</param>
        /// <returns>PK of Tag table if successfully inserted else 0</returns>
        /// <createdon>24-Oct-2024</createdon>
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
        /// This method is used to insert tag query detail in TagQuery table
        /// </summary>
        /// <param name="objTagQuery">object containing TagQuery detail</param>
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>23-Oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
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
        /// <returns>true if successfully inserted else false</returns>
        /// <createdon>23-Oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
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
