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
            _connection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            _connection.Open();
        }

        #region Repository Singleton
        /// <summary>
        /// USE FOLLOWING FOR DAPPER
        /// </summary>
        public IClientRepository ClientRepository
        {
            get { return _ClientRepository ?? (_ClientRepository = new ClientRepository(_transaction, _connection)); }
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

        #endregion 

        #region Implementing IDiosposable...

        #region private dispose variable declaration...
        private bool disposed = false;
        #endregion

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
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
        }

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
