using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevService;
using System;
using System.Collections.Generic;
using System.Net;

namespace Sahadev.API
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/Client")]
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

        /// <summary>
        /// This API is used get all records of client.
        /// </summary>
        /// <returns>list of object containing client detail</returns>
        /// <createdon>14-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                object lstClientDetail = SS.ClientService.GetDetail();
                if (lstClientDetail != null)
                {

                    return Ok(new GenericResponse.APIResponse{ code = HttpStatusCode.OK, message = string.Empty, data = lstClientDetail });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Client detail not available." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAll");
                return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = "Server Error! Please try again later." });
            }
        }

        /// <summary>
        /// This API is used to insert client detail in client table
        /// </summary>
        /// <param name="objClient">object containing client detail</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>14-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] Client objClient)
        {
            try
            {
                bool bReturn = SS.ClientService.Insert(objClient);
                if (bReturn == true)
                {

                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = "Client detail added successfully.", });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Failed to add client detail. Please try again." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "Insert");
                return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = "Server Error! Please try again later." });
            }
        }
    }
}
