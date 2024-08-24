using Microsoft.Extensions.Logging;
using SahadevDBLayer.UnitOfWork;
using SahadevService.Common;
using SahadevService.Dossier;
using SahadevService.Sentry;

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
        private EventService _EventService;
        private TagService _TagService;
        private DossierService _DossierService;

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

        /// <summary>
        /// DossierService
        /// </summary>
        public DossierService DossierService
        {
            get
            {
                if (_DossierService == null)
                {
                    _DossierService = new DossierService(uow, _logger);
                }
                return _DossierService;
            }
        }
    }
}
