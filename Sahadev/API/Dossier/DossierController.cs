/**********************************************************************************************
*  ClassName      :- DossierController                                                        *
*  -------------------------------------------------------------------------------------------*
*  Description    :- This is DossierController class which contain APIs related to Dossier    *
*  -------------------------------------------------------------------------------------------*
*  CreatedOn      :- 23-Aug-2024                                                              *
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
using Sahadev.API.Sentry;
using SahadevBusinessEntity.DTO.RequestModel;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevService;
using System.Linq;
using System.Net;
using System;
using SahadevBusinessEntity.DTO.Model;
using System.Collections.Generic;
using SahadevBusinessEntity.Constant.Messages;

namespace Sahadev.API.Dossier
{
    /// <summary>
    /// This is DossierController class which contain all APIs related to Dossier
    /// </summary>
    [Route("v1/Dossier")]
    [ApiController]
    public class DossierController : ControllerBase
    {
        private const string _className = "API.DossierController";
        private readonly ILogger<DossierController> _logger;
        ServiceSingleton SS;

        /// <summary>
        /// Constructor of DossierController class
        /// </summary>
        /// <param name="SS">object of ServiceSingleton</param>
        /// <param name="logger">object of logger of serilog</param>
        public DossierController(IServiceSingleton SS, ILogger<DossierController> logger)
        {
            this.SS = SS as ServiceSingleton;
            _logger = logger;
        }

        /// <summary>
        /// This API is used to fetch all clients of given UserID
        /// </summary>
        /// <param name="userID">UserID</param>
        /// <returns>object containing client detail in name/value pair format else error message</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_ClientFetchByUser")]
        public IActionResult GetAllClientByUserID(string userID)
        {
            try
            {

                List<Client> lstClient = SS.DossierService.GetAllClientByUserID(Convert.ToInt32(userID));
                if (lstClient != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstClient });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Clients not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllClientByUserID");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.SDCOM001 });
            }
        }

        /// <summary>
        /// This API is used to fetch all clients of given tagGroupName
        /// </summary>
        /// <param name="tagGroupName">TagGroupName</param>
        /// <returns>object containing client detail in name/value pair format else error message</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_CompetitorFetch")]
        public IActionResult GetAllClientsByTagID(string tagGroupName)
        {
            try
            {

                List<Client> lstClient = SS.DossierService.GetAllClientsByTagID(tagGroupName);
                if (lstClient != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstClient });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Clients not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllClientsByTagID");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.SDCOM001 });
            }
        }


        /// <summary>
        /// This API is used to fetch all Users
        /// </summary>
        /// <returns>object containing client detail in name/value pair format else error message</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_UserFetch")]
        public IActionResult GetAllUser()
        {
            try
            {
                List<User> lstClient = SS.DossierService.GetAllUser();
                if (lstClient != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstClient });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Clients not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllUser");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.SDCOM001 });
            }
        }

        /// <summary>
        /// This API is used to fetch insert dossier def detail 
        /// </summary>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <param name="objRQ_DossierDef">object containing dossier def detail</param>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("DossierConfiguration_Create")]
        public IActionResult AddDossierConfiguration([FromBody] RQ_DossierDef objRQ_DossierDef)
        {
            try
            {
                bool bReturn = SS.DossierService.InsertDossierDef(objRQ_DossierDef);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.SDCOM006, "Dossier Configuration") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.SDCOM002, "Dossier Configuration") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "AddDossierConfiguration");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.SDCOM001 });
            }
        }

        /// <summary>
        /// This API is used to update dossier def and related detail 
        /// </summary>
        /// <returns>success message if successfully updated else error message</returns>
        /// <param name="objRQ_DossierDef">object containing dossier def detail</param>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("DossierConfiguration_Update")]
        public IActionResult UpdateDossierConfiguration([FromBody] RQ_DossierDef objRQ_DossierDef)
        {
            try
            {
                bool bReturn = SS.DossierService.UpdateDossierDef(objRQ_DossierDef);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.SDCOM007, "Dossier Configuration") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.SDCOM003, "Dossier Configuration") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "UpdateDossierConfiguration");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.SDCOM001 });
            }
        }

        /// <summary>
        /// This API is used to fetch dossier configuration detail by DossierDefID
        /// </summary>
        /// <returns>object containing dossier configuration detail else error message</returns>
        /// <param name="dossierDefID">DossierDefID</param>
        /// <createdon>26-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("DossierConfiguration_Fetch")]
        public IActionResult GetDossierConfiguration(string dossierDefID)
        {
            try
            {

                DossierDef objDossierDef = SS.DossierService.GetDossierDef(Convert.ToInt32(dossierDefID));
                if (objDossierDef  != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = objDossierDef });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.SDCOM005, "Dossier Configuration") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetDossierConfiguration");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.SDCOM001 });
            }
        }
    }
}
