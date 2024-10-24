/**********************************************************************************************
*  ClassName      :- ClientController                                                         *
*  -------------------------------------------------------------------------------------------*
*  Description    :- This is ClientController class which contain APIs                        *
*                    related to Client dashboard                                              *
*  -------------------------------------------------------------------------------------------*
*  CreatedBy      :- PJ                                                                       *
*  -------------------------------------------------------------------------------------------*
*  CreatedOn      :- 24-Oct-2024                                                              *
*  -------------------------------------------------------------------------------------------*
*  ModifiedOn     :-                                                                          *
*  ModifiedBy     :-                                                                          *
*  ModifiedReason :-                                                                          *
**********************************************************************************************/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sahadev.API.Dossier;
using SahadevService;

namespace Sahadev.API.Client
{
    [Route("v1/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private const string _className = "API.ClientController";
        private readonly ILogger<ClientController> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor of ClientController class
        /// </summary>
        /// <param name="SS">object of ServiceSingleton</param>
        /// <param name="logger">object of logger of serilog</param>
        public ClientController(IServiceSingleton SS, ILogger<ClientController> logger)
        {
            this.SS = SS as ServiceSingleton;
            _logger = logger;
        }
    }
}
