/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- SahadevC1Repository                                                      *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is SahadevC1Repository class which contains all functions &         *
 *                     SP related to SahadevC1 repository                                       *
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

namespace SahadevDBLayer.Repository
{
    /// <summary>
    /// Interface of SahadevA2Repository class
    /// </summary>
    public interface ISahadevC1Repository
    {
        List<Client> Get();
        bool Insert(Client objClient, IDbTransaction transaction);
    }

    internal class SahadevC1Repository : RepositoryBase, ISahadevC1Repository
    {
        public SahadevC1Repository(IDbTransaction transaction, IDbConnection connection)
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
        public List<Client> Get()
        {
            try
            {
                var data = GetAllByProcedure<Client>(@"[dbo].[USP_ClientDetail_FetchAll]", null);
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
        public bool Insert(Client objClient, IDbTransaction transaction)
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
    }
}
