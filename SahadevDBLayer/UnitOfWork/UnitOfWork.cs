using System;
using SahadevDBLayer.Repository;
//using Microsoft.EntityFrameworkCore.Storage;
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
        private IClientRepository _ClientRepository;

        /// <summary>
        /// Connection configuration for the Databases
        /// Created By Saroj 12.58
        /// </summary>
        /// <param name="config"></param>
        
        //Configuration for SahadevA2Database
        private IDbConnection _SahadevA2Connection;
        private IDbTransaction _SahadevA2Transaction;
        public static string SahadevA2ConnectionSring = "SahadevA2ConnectionSring"; 
        private ISahadevA2Repository _SahadevA2Repository;


        //Configuration for SahadevC1Database
        private IDbConnection _SahadevC1Connection;
        private IDbTransaction _SahadevC1Transaction;
        public static string SahadevC1ConnectionSring = "SahadevC1ConnectionSring";
        private ISahadevC1Repository _SahadevC1Repository;


        //Configuration for SahadevC2Database
        private IDbConnection _SahadevC2Connection;
        private IDbTransaction _SahadevC2Transaction;
        public static string SahadevC2ConnectionSring = "SahadevC2ConnectionSring";
        private ISahadevC2Repository _SahadevC2Repository;


        //Configuration for SahadevC3Database
        private IDbConnection _SahadevC3Connection;
        private IDbTransaction _SahadevC3Transaction;
        public static string SahadevC3ConnectionSring = "SahadevC3ConnectionSring";
        private ISahadevC3Repository _SahadevC3Repository;



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

            _SahadevA2Connection = new SqlConnection(_config.GetConnectionString(SahadevA2ConnectionSring));
            _SahadevA2Connection.Open();
            _SahadevA2Transaction = _SahadevA2Connection.BeginTransaction();

            _SahadevC1Connection = new SqlConnection(_config.GetConnectionString(SahadevC1ConnectionSring));
            _SahadevC1Connection.Open();
            _SahadevC1Transaction = _SahadevC1Connection.BeginTransaction();

            _SahadevC2Connection = new SqlConnection(_config.GetConnectionString(SahadevC2ConnectionSring));
            _SahadevC2Connection.Open();
            _SahadevC2Transaction = _SahadevC2Connection.BeginTransaction();

            _SahadevC3Connection = new SqlConnection(_config.GetConnectionString(SahadevA2ConnectionSring));
            _SahadevC3Connection.Open();
            _SahadevC3Transaction = _SahadevC3Connection.BeginTransaction();

        }

        #region Repository Singleton
        /// <summary>
        /// USE FOLLOWING FOR DAPPER
        /// </summary>
        /// 

        public ISahadevA2Repository SahadevA2Repository
        {
            get { return _SahadevA2Repository ?? (_SahadevA2Repository = new SahadevA2Repository(_SahadevA2Connection, _SahadevA2Transaction)); }
        }

        public ISahadevC1Repository SahadevC1Repository
        {
            get { return _SahadevC1Repository ?? (_SahadevC1Repository = new SahadevC1Repository(_SahadevC1Connection, _SahadevC1Transaction)); }
        }

        public ISahadevC2Repository SahadevC2Repository
        {
            get { return _SahadevC2Repository ?? (_SahadevC2Repository = new SahadevC2Repository(_SahadevC2Connection, _SahadevC2Transaction)); }
        }

        public ISahadevC3Repository SahadevC3Repository
        {
            get { return _SahadevC3Repository ?? (_SahadevC3Repository = new SahadevC3Repository(_SahadevC3Connection, _SahadevC3Transaction)); }
        }

        public IClientRepository ClientRepository
        {
            get { return _ClientRepository ?? (_ClientRepository = new ClientRepository(_connection, _transaction)); }
        }

        ///USE FOLLOWING FOR ENTITY FRAMEWORK
        //private _GenericRepository<MemberMaster> _MemberMaster = null;
        /// <summary>
        /// MemberMaster
        /// </summary>
        //public _GenericRepository<MemberMaster> MemberMaster
        //{
        //    get
        //    {
        //        if (_MemberMaster == null)
        //        {
        //            _MemberMaster = new _GenericRepository<MemberMaster>(db);
        //        }
        //        return _MemberMaster;
        //    }
        //}
        #endregion

        //public int Save()
        //{
        //    return db.SaveChanges();
        //}

        #region Transaction 



        //private IDbContextTransaction dbTrans;
        //public void BeginTransaction()
        //{
        //    if (dbTrans == null)
        //    {
        //        dbTrans = db.Database.BeginTransaction();
        //    }
        //}

        //public void CommitTransaction()
        //{
        //    dbTrans.Commit();
        //    dbTrans.Dispose();
        //}

        //public void RollbackTransaction()
        //{
        //    dbTrans.Rollback();
        //    dbTrans.Dispose();
        //}


        public void Rollback()
        {
            _SahadevA2Transaction.Rollback();
            _SahadevC1Transaction.Rollback();
            _SahadevC2Transaction.Rollback();
            _SahadevC3Transaction.Rollback();
        }
        public void Commit()
        {
            try
            {
                _SahadevA2Transaction.Commit();
                _SahadevC1Transaction.Commit();
                _SahadevC2Transaction.Commit();
                _SahadevC3Transaction.Commit();                                                                                                                                               
            }
            catch
            {
                _SahadevA2Transaction.Rollback();
                _SahadevC1Transaction.Rollback();
                _SahadevC2Transaction.Rollback();
                _SahadevC3Transaction.Rollback();
                throw;
            }
            finally
            {
                _SahadevA2Transaction.Dispose();
                _SahadevC1Transaction.Dispose();
                _SahadevC2Transaction.Dispose();
                _SahadevC3Transaction.Dispose();

                _SahadevA2Transaction = _SahadevA2Connection.BeginTransaction();
                _SahadevC1Transaction = _SahadevC1Connection.BeginTransaction();
                _SahadevC2Transaction = _SahadevC2Connection.BeginTransaction();
                _SahadevC3Transaction = _SahadevC3Connection.BeginTransaction();

                disposed = true;

                resetRepositories();
            }
        }

        private void resetRepositories()
        {
            _SahadevA2Repository = null;
            _SahadevC1Repository = null;
            _SahadevC2Repository = null;
            _SahadevC3Repository = null;
        }



        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (_SahadevA2Transaction != null)
                    {
                        _SahadevA2Transaction.Dispose();
                        _SahadevA2Transaction = null;
                    }
                    if (_SahadevA2Connection != null)
                    {
                        _SahadevA2Connection.Dispose();
                        _SahadevA2Connection = null;
                    }

                    if (_SahadevC1Transaction != null)
                    {
                        _SahadevC1Transaction.Dispose();
                        _SahadevC1Transaction = null;
                    }
                    if (_SahadevC2Connection != null)
                    {
                        _SahadevC1Connection.Dispose();
                        _SahadevC1Connection = null;
                    }

                    if (_SahadevC2Transaction != null)
                    {
                        _SahadevC2Transaction.Dispose();
                        _SahadevC2Transaction = null;
                    }
                    if (_SahadevC2Connection != null)
                    {
                        _SahadevC2Connection.Dispose();
                        _SahadevC2Connection = null;
                    }


                    if (_SahadevC3Transaction != null)
                    {
                        _SahadevC3Transaction.Dispose();
                        _SahadevC3Transaction = null;
                    }
                    if (_SahadevC3Connection != null)
                    {
                        _SahadevC3Connection.Dispose();
                        _SahadevC3Connection = null;
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
