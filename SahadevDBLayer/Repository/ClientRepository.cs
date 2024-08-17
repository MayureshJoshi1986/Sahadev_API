/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- ClientRepository                                                         *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is ClientRepository class which contains all function &             * 
 *                     SP related to client                                                     *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- MS                                                                       *                                                                 
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 10/Apr/2014                                                              *
 *  --------------------------------------------------------------------------------------------* 
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using Dapper;
using Microsoft.AspNetCore.Mvc;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevDBLayer.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace SahadevDBLayer.Repository
{

    public interface IClientRepository
    {
        List<Client> Get(IDbTransaction transaction);
        bool Insert(Client objClient, IDbTransaction transaction);
    }
    internal class ClientRepository : RepositoryBase, IClientRepository
    {

        public ClientRepository(IDbTransaction transaction, IDbConnection connection)
            : base(transaction, connection)
        {
        }

        /// <summary>
        /// This method is used to get fetch client detail from client table
        /// </summary>
        /// <returns>list of object containing client detail</returns>
        /// <createdon>14-Aug-2024</createdon>
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
        public bool Insert(Client objClient,IDbTransaction transaction)
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
