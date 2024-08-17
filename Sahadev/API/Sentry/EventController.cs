/**********************************************************************************************
*  ClassName      :- EventController                                                          *
*  -------------------------------------------------------------------------------------------*
*  Description    :- This class contain API related to user device                            *
*  -------------------------------------------------------------------------------------------*
*  CreatedOn      :- 17-Aug-2024                                                              *
*  -------------------------------------------------------------------------------------------*
*  CreatedBy      :- PJ                                                                       *
*  -------------------------------------------------------------------------------------------*
*  ModifiedOn     :-                                                                          *
*  ModifiedBy     :-                                                                          *
*  ModifiedReason :-                                                                          *
*  -------------------------------------------------------------------------------------------*
*  ModifiedOn     :-                                                                          *
*  ModifiedBy     :-                                                                          *
*  ModifiedReason :-                                                                          *
**********************************************************************************************/
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevService;
using System.Net;
using System;
using SahadevBusinessEntity.DTO.RequestModel;

namespace Sahadev.API.Sentry
{
    [Route("v1/Sentry/Event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private const string _className = "API.EventController";
        private readonly ILogger<EventController> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor of EventController class
        /// </summary>
        /// <param name="SS">object of ServiceSingleton</param>
        /// <param name="logger">object of logger of serilog</param>
        public EventController(IServiceSingleton SS, ILogger<EventController> logger)
        {
            this.SS = SS as ServiceSingleton;
            _logger = logger;
        }

        /// <summary>
        /// This API is used to insert event detail in event table
        /// </summary>
        /// <param name="objRQ_EventDetail">request object containing event and tag detail</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] Event objEvent)
        {
            try
            {
                bool bReturn = SS.EventService.Add(objEvent);
                if (bReturn == true)
                {

                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = "Event detail added successfully.", });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Failed to add event detail. Please try again." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "Add");
                return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = "Server Error! Please try again later." });
            }
        }
    }
}
