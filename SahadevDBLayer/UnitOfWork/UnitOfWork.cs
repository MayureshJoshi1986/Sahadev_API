using System;
using SahadevDBLayer.Repository;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SahadevDBLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        //private readonly DBContext.DBContext db;
        private readonly IConfiguration _config;

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public static string Connectionstring = "DefaultConnection";

        /// <summary>
        /// Connection configuration for the Databases
        /// Created By Saroj 12.58
        /// </summary>
        /// <param name="config"></param>
        
        //Configuration for SahadevA2Database
        private IDbConnection _A2Connection;
        private IDbTransaction _A2Transaction;
        public static string A2ConnectionSring = "A2ConnectionSring"; 
        private IA2Repository _A2Repository;


        //Configuration for SahadevC1Database
        private IDbConnection _C1Connection;
        private IDbTransaction _C1Transaction;
        public static string C1ConnectionSring = "C1ConnectionSring";
        private IC1Repository _C1Repository;


        //Configuration for SahadevC2Database
        private IDbConnection _C2Connection;
        private IDbTransaction _C2Transaction;
        public static string C2ConnectionSring = "C2ConnectionSring";
        private IC2Repository _C2Repository;


        //Configuration for C3Database
        private IDbConnection _C3Connection;
        private IDbTransaction _C3Transaction;
        public static string C3ConnectionSring = "C3ConnectionSring";
        private IC3Repository _C3Repository;



        //public UnitOfWork(DBContext.DBContext db, IConfiguration config)
        //{
        //    this.db = db as DBContext.DBContext;
        //    _config = config;
        //    _connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
        //    _connection.Open();
        //}

        public UnitOfWork(IConfiguration config)
        {
            _config = config;
            //_connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            //_connection.Open();

            _A2Connection = new SqlConnection(_config.GetConnectionString(A2ConnectionSring));
            _A2Connection.Open();
            _A2Transaction = _A2Connection.BeginTransaction();

            _C1Connection = new SqlConnection(_config.GetConnectionString(C1ConnectionSring));
            _C1Connection.Open();
            _C1Transaction = _C1Connection.BeginTransaction();

            _C2Connection = new SqlConnection(_config.GetConnectionString(C2ConnectionSring));
            _C2Connection.Open();
            _C2Transaction = _C2Connection.BeginTransaction();

            _C3Connection = new SqlConnection(_config.GetConnectionString(C3ConnectionSring));
            _C3Connection.Open();
            _C3Transaction = _C3Connection.BeginTransaction();

        }

        #region Repository Singleton
        /// <summary>
        /// USE FOLLOWING FOR DAPPER
        /// </summary>
        /// 

        public IA2Repository A2Repository
        {
            get { return _A2Repository ?? (_A2Repository = new A2Repository(_A2Connection, _A2Transaction)); }
        }

        public IC1Repository C1Repository
        {
            get { return _C1Repository ?? (_C1Repository = new C1Repository(_C1Connection, _C1Transaction)); }
        }

        public IC2Repository C2Repository
        {
            get { return _C2Repository ?? (_C2Repository = new C2Repository(_C2Connection, _C2Transaction)); }
        }

        public IC3Repository C3Repository
        {
            get { return _C3Repository ?? (_C3Repository = new C3Repository(_C3Connection, _C3Transaction)); }
        }

        
        #endregion

        #region Transaction 


        public void Rollback()
        {
            _A2Transaction.Rollback();
            _C1Transaction.Rollback();
            _C2Transaction.Rollback();
            _C3Transaction.Rollback();
        }
        public void Commit()
        {
            try
            {
                _A2Transaction.Commit();
                _C1Transaction.Commit();
                _C2Transaction.Commit();
                _C3Transaction.Commit();                                                                                                                                               
            }
            catch
            {
                _A2Transaction.Rollback();
                _C1Transaction.Rollback();
                _C2Transaction.Rollback();
                _C3Transaction.Rollback();
                throw;
            }
            finally
            {
                _A2Transaction.Dispose();
                _C1Transaction.Dispose();
                _C2Transaction.Dispose();
                _C3Transaction.Dispose();

                _A2Transaction = _A2Connection.BeginTransaction();
                _C1Transaction = _C1Connection.BeginTransaction();
                _C2Transaction = _C2Connection.BeginTransaction();
                _C3Transaction = _C3Connection.BeginTransaction();

                disposed = true;

                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _A2Repository = null;
            _C1Repository = null;
            _C2Repository = null;
            _C3Repository = null;
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_A2Transaction != null)
                    {
                        _A2Transaction.Dispose();
                        _A2Transaction = null;
                    }
                    if (_A2Connection != null)
                    {
                        _A2Connection.Dispose();
                        _A2Connection = null;
                    }

                    if (_C1Transaction != null)
                    {
                        _C1Transaction.Dispose();
                        _C1Transaction = null;
                    }
                    if (_C2Connection != null)
                    {
                        _C1Connection.Dispose();
                        _C1Connection = null;
                    }

                    if (_C2Transaction != null)
                    {
                        _C2Transaction.Dispose();
                        _C2Transaction = null;
                    }
                    if (_C2Connection != null)
                    {
                        _C2Connection.Dispose();
                        _C2Connection = null;
                    }


                    if (_C3Transaction != null)
                    {
                        _C3Transaction.Dispose();
                        _C3Transaction = null;
                    }
                    if (_C3Connection != null)
                    {
                        _C3Connection.Dispose();
                        _C3Connection = null;
                    }
                }
                disposed = true;
            }
        }

        #endregion 

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        /*protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //Debug.WriteLine("UnitOfWork is being disposed");
                    //db.Dispose();
                }
            }
            this.disposed = true;
        }*/

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
