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

                List<dynamic> lstClient = SS.DossierService.GetAllClientByUserID(Convert.ToInt32(userID));
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
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
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

                List<dynamic> lstClient = SS.DossierService.GetAllClientsByTagID(tagGroupName);
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
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
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
                List<dynamic> lstClient = SS.DossierService.GetAllUser();
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
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
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
                if (objDossierDef != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = objDossierDef });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.RetrievalFailed, "Dossier Configuration") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetDossierConfiguration");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to fetch all dossier configurations
        /// </summary>
        /// <returns>list of object containing dossier configuration</returns>
        /// <createdon>28-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("DossierConfiguration_FetchAll")]
        public IActionResult GetAllDossier()
        {
            try
            {
                dynamic lstGetAllDossier = SS.DossierService.GetAllDossier();
                if (lstGetAllDossier != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstGetAllDossier });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Dossier Configurations not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllDossier");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }


        /// <summary>
        /// This API is used to fetch all generated dossiers
        /// </summary>
        /// <returns>list of object containing all dossier generated </returns>
        /// <createdon>28-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_GeneratedDossierlist_FetchAll")]
        public IActionResult GetAllGeneratedDossier()
        {
            try
            {
                dynamic lstGetAllGeneratedDossier = SS.DossierService.GetAllGeneratedDossier();
                if (lstGetAllGeneratedDossier != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstGetAllGeneratedDossier });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Dossier list not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllGeneratedDossier");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to fetch generated dossiers by DossierConfID
        /// </summary>
        /// <param name="dossierConfID">DossierConfID</param>
        /// <returns>list of object containing dossier generated by DossierConfID </returns>
        /// <createdon>28-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_GeneratedDossier_Fetch")]
        public IActionResult GetDossierByDossierConfID(string dossierConfID)
        {
            try
            {
                dynamic lstGetGeneratedDossier = SS.DossierService.GetGeneratedDossier(Convert.ToInt32(dossierConfID));
                if (lstGetGeneratedDossier != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstGetGeneratedDossier });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Dossier not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetDossierByDossierConfID");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to get all additional URLs of a dossier
        /// </summary>
        /// <returns>list of object containing all additional URLs of a dossier</returns>
        /// <param name="dossierID">DossierID</param>
        /// <createdon>27-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_FetchAdditionalUrl")]
        public IActionResult GetAllAdditionalURL(string dossierID)
        {
            try
            {
                List<RS_AdditionalURL> lstAdditionalURL = SS.DossierService.GetAllAdditionalURL(Convert.ToInt32(dossierID));
                if (lstAdditionalURL != null && lstAdditionalURL.Count != 0)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstAdditionalURL });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.RetrievalFailed, "Additional URL") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllAdditionalURL");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
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
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Added, "Dossier Configuration") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.AddFailed, "Dossier Configuration") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "AddDossierConfiguration");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to insert additional URL in AdditionlURL table
        /// </summary>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <param name="objRQ_AdditionalURL">object containing additional URL</param>
        /// <createdon>27-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("Dossier_InsertAdditionalUrl")]
        public IActionResult AddAdditionalURL([FromBody] RQ_AdditionalURL objRQ_AdditionalURL)
        {
            try
            {
                bool bReturn = SS.DossierService.InsertAdditionalURL(objRQ_AdditionalURL);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Added, "Additional URL") });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.AddFailed, "Additional URL") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "AddAdditionalURL");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
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
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Updated, "Dossier Configuration") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.UpdateFailed, "Dossier Configuration") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "UpdateDossierConfiguration");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }
    }
}
