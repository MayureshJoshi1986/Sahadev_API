using Microsoft.Extensions.Logging;
using SahadevDBLayer.UnitOfWork;
using SahadevService;
using SahadevService.Common;
using SahadevService.Sentry;
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

        private ClientService _ClientService;
        private EventService _EventService;
        private TagService _TagService;

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

        public EventService EventService
        {
            get
            {
                if (_EventService == null)
                {
                    _EventService = new EventService(uow, _logger);
                }
                return _EventService;
            }
        }

        public TagService TagService
        {
            get
            {
                if (_TagService == null)
                {
                    _TagService = new TagService(uow, _logger);
                }
                return _TagService;
            }
        }
    }
}
