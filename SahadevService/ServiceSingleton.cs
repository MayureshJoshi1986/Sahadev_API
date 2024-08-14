using Microsoft.Extensions.Logging;
using SahadevDBLayer.UnitOfWork;
using SahadevService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahadevService
{
    public interface IServiceSingleton
    {

    }


    public class ServiceSingleton : IServiceSingleton
    {
        private UnitOfWork uow;
        private readonly ILogger<ServiceSingleton> _logger;
        public ServiceSingleton(IUnitOfWork uow, ILogger<ServiceSingleton> logger)
        {
            this.uow = uow as UnitOfWork;
            this._logger = logger;
        }

        //private MemberService _MemberService;
        private ClientService _ClientService;

        ///// <summary>
        ///// MemberService
        ///// </summary>
        //public MemberService MemberService
        //{
        //    get
        //    {
        //        if (_MemberService == null)
        //        {
        //            _MemberService = new MemberService(uow, _logger);
        //        }
        //        return _MemberService;
        //    }
        //}

        public ClientService ClientService
        {
            get
            {
                if (_ClientService == null)
                {
                    _ClientService = new ClientService(uow, _logger);
                }
                return _ClientService;
            }
        }
    }
}
