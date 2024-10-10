/*  --------------------------------------------------------------------------------------------*
 *  Class Name      :- C1Repository                                                             *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This is C1Repository class which contains all functions &                *
 *                     SP related to SahadevC1 repository                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- PJ                                                                       *
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 17-Aug-2024                                                              *
 *  --------------------------------------------------------------------------------------------*
 *  revised By      :-  PJ                                                                      *
 *  revised Details :-  Changed class name from SahadevC1Repository to C1Repository             *  
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
    /// Interface of C1Repository class
    /// </summary>
    public interface IC1Repository
    {
        List<Client> Get();
        List<dynamic> GetAllClientByUserID(int userID);
        List<dynamic> GetAllClientByTagID(string lstTagID);
        List<dynamic> GetAllUser();
        bool Insert(Client objClient);

        int InsertTask(Task objTask);

        bool InsertTaskLog(TaskLog objTaskLog);
        WorkFlowStatus GetWorkFlowStatus(int refID, int ttID);
    }

    internal class C1Repository : RepositoryBase, IC1Repository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;
        public C1Repository(IDbConnection connection, IDbTransaction transaction)
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
        /// This method is used to fecth all User
        /// </summary>
        /// <returns>list of object containing User</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public List<dynamic> GetAllUser()
        {
            try
            {
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_User_Fetch]", null, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// This method is used to get all client by user id
        /// </summary>
        /// <returns>list of object containing client</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>28-Aug-2024</modifiedby>
        /// <modifiedreason>Chanaged return type from List<Client> to List<dynamic></modifiedreason>
        public List<dynamic> GetAllClientByUserID(int userID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@userID", userID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[USP_UserClient_Fetch]",dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        /// <summary>
        /// This method is used to get all client by TagID's
        /// </summary>
        /// <returns>list of object containing client</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>PJ</modifiedon>
        /// <modifiedby>28-Aug-2024</modifiedby>
        /// <modifiedreason>Chanaged return type from List<Client> to List<dynamic></modifiedreason>
        public List<dynamic> GetAllClientByTagID(string lstTagID)
        {
            try
            {
               
                var dbparams = new DynamicParameters();
                dbparams.Add("@tagID", lstTagID);
                var data = GetAllByProcedure<dynamic>(@"[dbo].[Usp_FetchClient]", dbparams, _transaction);
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
        public bool Insert(Client objClient)
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
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public int InsertTask(Task objTask)
        {
            int iResult = 0;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@ttID", objTask.TTID);
                dbparams.Add("@refID", objTask.RefID);
                dbparams.Add("@createdBy", objTask.CreatedBy);
                dbparams.Add("@assignedTo", objTask.AssignedTo);
                iResult = GetByProcedure<int>(@"[dbo].[USP_Task_Insert]", dbparams, _transaction);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public bool InsertTaskLog(TaskLog objTaskLog)
        {
            bool bReturn = false;
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@taskID", objTaskLog.TaskID);
                dbparams.Add("@fromStatusID", objTaskLog.FromStatusID);
                dbparams.Add("@toStatusID", objTaskLog.ToStatusID);              
                int iResult = InsertByProcedure<int>(@"[dbo].[USP_TaskLog_Insert]", dbparams, _transaction);
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
        /// 
        /// </summary>
        /// <param name="refID"></param>
        /// <param name="ttID"></param>
        /// <returns></returns>
        /// <createdon>09-oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        public WorkFlowStatus GetWorkFlowStatus(int refID, int ttID)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("@refID", refID);
                dbparams.Add("@ttID", ttID);
                var data = GetByProcedure<WorkFlowStatus>(@"[dbo].[USP_GetWorkFlowStatus]", dbparams, _transaction);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
