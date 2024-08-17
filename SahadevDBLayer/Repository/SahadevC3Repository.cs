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
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevC3Repository class
    /// </summary>
    public interface ISahadevC3Repository
    {
        //List<FeedbackType> GetFeedbackType();
    }

    internal class SahadevC3Repository:RepositoryBase, ISahadevC3Repository
    {
        private readonly IDbTransaction _transaction;
        private readonly IDbConnection _connection;

        public SahadevC3Repository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {
            _transaction = transaction;
            _connection = connection;
        }
    }
}
