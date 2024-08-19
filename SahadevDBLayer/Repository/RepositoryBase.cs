using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using static Dapper.SqlMapper;

namespace SahadevDBLayer.Repository
{
    internal abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get; private set; }

        public RepositoryBase(IDbTransaction transaction, IDbConnection connection)
        {
            Transaction = transaction;
            Connection = connection;
        }

        #region Functions

        #region GetConnection
        public DbConnection GetDbconnection()
        {
            return (DbConnection)Transaction.Connection;
        }
        #endregion

        #region GetMultiple
        public GridReader GetMultipleByQuery(string sp, DynamicParameters parms)
        {
            return GetMultiple(sp, parms, CommandType.Text);
        }
        public GridReader GetMultipleByProcedure(string sp, DynamicParameters parms)
        {
            return GetMultiple(sp, parms, CommandType.StoredProcedure);
        }
        public GridReader GetMultiple(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            GridReader result;
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
            try
            {
                result = Connection.QueryMultiple(sp, parms, commandType: commandType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion

        #region Get
        public T GetByQuery<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return Get<T>(sp, parms, CommandType.Text, transaction);
        }
        public T GetByProcedure<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return Get<T>(sp, parms, CommandType.StoredProcedure, transaction);
        }
        T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text, IDbTransaction transaction = null)
        {
            return Connection.Query<T>(sp, parms, commandType: commandType, transaction: transaction).FirstOrDefault();
        }
        #endregion

        #region GetAll
        public List<T> GetAllByQuery<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return GetAll<T>(sp, parms, CommandType.Text, transaction);
        }
        public List<T> GetAllByProcedure<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return GetAll<T>(sp, parms, CommandType.StoredProcedure, transaction);
        }
        List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null)
        {
            return Connection.Query<T>(sp, parms, commandType: commandType, transaction: transaction).ToList();
        }
        #endregion

        #region Insert
        public T InsertByQuery<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return Insert<T>(sp, parms, CommandType.Text, transaction);
        }
        public T InsertByProcedure<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return Insert<T>(sp, parms, CommandType.StoredProcedure, transaction);
        }
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction= null)
        {
            T result;
            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();

                result = Connection.ExecuteScalar<T>(sp, parms, commandType: commandType, transaction: transaction);
                result = (T)Convert.ChangeType(1, typeof(T));

            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(0, typeof(T));
                throw ex;
            }
            finally
            {
               //do not close connection here
            }

            return result;
        }
        #endregion

        #region Update
        public T UpdateByQuery<T>(string sp, DynamicParameters parms , IDbTransaction transaction)
        {
            return Update<T>(sp, parms, CommandType.Text, transaction);
        }
        public T UpdateByProcedure<T>(string sp, DynamicParameters parms, IDbTransaction transaction)
        {
            return Update<T>(sp, parms, CommandType.StoredProcedure, transaction);
        }
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure, IDbTransaction transaction = null)
        {
            T result;
            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();

                result = Connection.ExecuteScalar<T>(sp, parms, transaction: transaction, commandTimeout: null, commandType: commandType);
                result = (T)Convert.ChangeType(1, typeof(T));
            }
            catch (Exception ex)
            {
                result = (T)Convert.ChangeType(0, typeof(T));
                throw ex;
            }
            finally
            {
                //if (Connection.State == ConnectionState.Open)
                    //Connection.Close();
            }

            return result;
        }
        #endregion

        #endregion
    }
}
