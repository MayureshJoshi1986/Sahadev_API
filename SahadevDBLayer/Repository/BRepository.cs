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
    }
}
