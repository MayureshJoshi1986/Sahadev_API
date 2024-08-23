/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- SahadevC3Repository                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is SahadevC3Repository class which contains all functions &         *
 *                     SP related to SahadevC3 repository                                       *
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
using SahadevBusinessEntity.DTO.Model;
using System.Collections.Generic;
using System;
using System.Data;
using Dapper;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevC3Repository class
    /// </summary>
    public interface ISahadevC3Repository
    {
        //List<FeedbackType> GetFeedbackType();

        List<string> GetAllTagIDByTagGroupName(string tagGroupName);
    }

    internal class SahadevC3Repository:RepositoryBase, ISahadevC3Repository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public SahadevC3Repository(IDbConnection connection, IDbTransaction transaction)
            : base(connection, transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }


        /// <summary>
        /// This method is used to get fetch client detail from All Tag ID from 
        /// </summary>
        /// <returns>list of object containing list of TagID</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<string> GetAllTagIDByTagGroupName(string tagGroupName)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagGroupName", tagGroupName);
                var data = GetAllByProcedure<string>(@"[dbo].[USP_Competitor_Fetch]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
