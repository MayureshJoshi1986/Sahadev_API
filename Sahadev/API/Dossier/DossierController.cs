/**********************************************************************************************
*  ClassName      :- DossierController                                                        *
*  -------------------------------------------------------------------------------------------*
*  Description    :- This is DossierController class which contain APIs related to Dossier    *
*  -------------------------------------------------------------------------------------------*
*  CreatedOn      :- 23-Aug-2024                                                              *
*  -------------------------------------------------------------------------------------------*
*  CreatedBy      :- PJ                                                                       *
*  -------------------------------------------------------------------------------------------*
*  ModifiedOn     :- 26-Sept-2024                                                             *
*  ModifiedBy     :- PJ                                                                       *
*  ModifiedReason :- Added new methods GetAllDossierScheduleType & GetAllDossierEventType     *
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
        /// This API is used to fetch all client topic details tag and tag queries
        /// </summary>
        /// <param name="clientTopicId">clientTopicId</param>
        /// <returns>object containing client topic details tag and tag queries</returns>
        /// <createdon>04-oct-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_ClientTopic_Fetch")]
        public IActionResult GetClientTopic(int clientTopicId)
        {
            try
            {

                var clientTopic = SS.DossierService.GetClientTopic(clientTopicId);
                if (clientTopic != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = clientTopic });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Client topic not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, _className, "GetClientTopic");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }



        /// <summary>
        /// This API is used to fetch all clients of given tagGroupName
        /// </summary>
        /// <param name="tgID">tag Group ID (tgID)</param>
        /// <returns>object containing client detail in name/value pair format else error message</returns>
        /// <createdon>23-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_CompetitorFetch")]
        public IActionResult GetAllClientsByTagID(int tgID)
        {
            try
            {

                List<dynamic> lstClient = SS.DossierService.GetAllClientsByTagID(tgID);
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
        /// <param name="userID">UserID</param>
        /// <param name="clientID">ID of client</param>
        /// <param name="statusID">StatusID of dossier</param>
        /// <param name="dtStartDate">StartDate of dossier</param>
        /// <param name="dtEndDate">End of dossier</param>
        /// <returns>list of object containing dossier configuration</returns>
        /// <createdon>28-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>26-Sep-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changes to handle multiple clientID</modifiedreason>
        [HttpGet]
        [Route("DossierConfiguration_FetchAll")]
        public IActionResult GetAllDossier(DateTime? dtStartDate, DateTime? dtEndDate, [FromQuery] int[] clientID, int statusID = 1, int userID = 0)
        {
            try
            {
                dynamic lstGetAllDossier = SS.DossierService.GetAllDossier(userID, clientID, statusID, dtStartDate, dtEndDate);
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
        /// <param name="userID">UserID</param>
        /// <param name="clientID">ID of client</param>
        /// <param name="statusID">StatusID of dossier</param>
        /// <param name="dtStartDate">StartDate of dossier</param>
        /// <param name="dtEndDate">End of dossier</param>
        /// <returns>list of object containing all dossier generated </returns>
        /// <createdon>28-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>26-Sep-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changes to handle multiple clientID</modifiedreason>
        [HttpGet]
        [Route("Dossier_GeneratedDossierlist_FetchAll")]
        public IActionResult GetAllGeneratedDossier(DateTime? dtStartDate, DateTime? dtEndDate, [FromQuery] int[] clientID, int statusID = 1, int userID = 0)
        {
            try
            {
                dynamic lstGetAllGeneratedDossier = SS.DossierService.GetAllGeneratedDossier(userID, clientID, statusID, dtStartDate, dtEndDate);
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
        /// This API is used to fetch generated dossiers by DossierDefID
        /// </summary>
        /// <param name="dossierDefID">DossierDefID</param>
        /// <returns>list of object containing dossier generated by DossierConfID </returns>
        /// <createdon>28-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_GeneratedDossier_Fetch")]
        public IActionResult GetDossierByDossierDefID(string dossierDefID)
        {
            try
            {
                
                dynamic lstGetGeneratedDossier = SS.DossierService.GetGeneratedDossier(Convert.ToInt32(dossierDefID));
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
                _logger.LogError(ex, _className, "GetDossierByDossierDefID");
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
        public IActionResult AddDossierConfiguration([FromForm] RQ_DossierDef objRQ_DossierDef)
        {
            try
            {
                bool bReturn = SS.DossierService.InsertDossierDef(objRQ_DossierDef);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Added, "Dossier") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.AddFailed, "Dossier") });
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
                List<AdditionalURL> lstAdditionalURL = SS.DossierService.GetAllAdditionalURL(Convert.ToInt32(dossierID));
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
        public IActionResult UpdateDossierConfiguration([FromForm] RQ_DossierDef objRQ_DossierDef)
        {
            try
            {
                bool bReturn = SS.DossierService.UpdateDossierDef(objRQ_DossierDef);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Updated, "Dossier") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.UpdateFailed, "Dossier") });
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

        /// <summary>
        /// This API is used to fetch all dossier schedule type for dropdown
        /// </summary>
        /// <returns>list of object containing dossier schedule type</returns>
        /// <createdon>26-Sept-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("DossierScheduleType_FetchAll")]
        public IActionResult GetAllDossierScheduleType()
        {
            try
            {
                dynamic lstDossierScheduleType = SS.DossierService.GetAllDossierScheduleType();
                if (lstDossierScheduleType != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstDossierScheduleType });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Data not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllDossierScheduleType");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to fetch all dossier event type for dropdown
        /// </summary>
        /// <returns>list of object containing dossier event type</returns>
        /// <createdon>26-Sept-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("DossierEventType_FetchAll")]
        public IActionResult GetAllDossierEventType()
        {
            try
            {
                dynamic lstDossierEventType = SS.DossierService.GetEventType();
                if (lstDossierEventType != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstDossierEventType });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Data not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllDossierEventType");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }


        /// <summary>
        /// This apoTo fetch the initial Review Data link Details to verify
        /// </summary>
        /// <param name="dossierID">To fetch the links of particular dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing list of Link Details for the verifcation</returns>
        /// <createdon>07-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_Review_FetchData")]
        public IActionResult GetAllDossierReviewDataDetails(int dossierID, int platformID)
        {
            try
            {

                dynamic lstReviewDataDetails = SS.DossierService.GetAllDossierReviewDataDetails(dossierID, platformID);
                if (lstReviewDataDetails != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstReviewDataDetails });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Links not found for the review" });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllDossierReviewDataDetails");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }



        /// <summary>
        /// This API is used To fetch the Data links Details that are moved to draft for the review
        /// </summary>
        /// <param name="dossierID">To fetch the links of particular dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing list of Link Details that are moved To draft for the review</returns>
        /// <createdon>07-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_Review_FetchDossierDetails")]
        public IActionResult GetAllDossierDraftDataDetails(int dossierID, int platformID)
        {
            try
            {

                dynamic lstReviewDraftDetails = SS.DossierService.GetAllDossierDraftDataDetails(dossierID, platformID);
                if (lstReviewDraftDetails != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstReviewDraftDetails });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Draft Links not found for the review" });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllDossierDraftDataDetails");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }



        /// <summary>
        /// This API is used To fetch the Data links that moved to trash 
        /// </summary>
        /// <param name="dossierID">To fetch the links of particular dossier</param>
        /// <param name="platformID"> To fetch the link of particular platform (Print, online) based on ID</param>
        /// <returns>list of object containing list of Link Details that are moved To trash</returns>
        /// <createdon>07-Sep-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_Review_ShowTrash")]
        public IActionResult GetAllDossierTrashDataDetails(int dossierID, int platformID)
        {
            try
            {

                dynamic lstDeletedDataDetails = SS.DossierService.GetAllDossierTrashDataDetails(dossierID, platformID);
                if (lstDeletedDataDetails != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstDeletedDataDetails });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Deleted Links not found for the review" });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetAllDossierTrashDataDetails");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }




        /// <summary>
        /// This API is used to Update DossierLinkMap table To mark the IsDraft = 1 to move data to Draft
        /// </summary>
        /// <param name="dossierLinkMapID">string list containing dossierLinkMapIDs to Update the record as Draft</param>
        ///<returns>success message if successfully updated else error message</returns>
        /// <createdon>07-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        [HttpPost]
        [Route("Dossier_Review_SaveDraft")]
        public IActionResult SaveToDraft([FromBody] List<string> dossierLinkMapID)
        {
            try
            {
                bool bReturn = SS.DossierService.SaveToDraft(dossierLinkMapID);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Updated, "Dossier for Draft Links") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.UpdateFailed, "Dossier for Draft Links") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "SaveToDraft");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }


        /// <summary>
        /// This API is used to Update DossierLinkMap table To mark isDeleted = 1 for the links that are moved to trash
        /// </summary>
        /// <param name="dossierLinkMapID">string list containing dossierLinkMapIDs to Update the record that are supposed to move to trash</param>
        ///<returns>success message if successfully updated else error message</returns>
        /// <createdon>07-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        [HttpPost]
        [Route("Dossier_Review_MoveToTrash")]
        public IActionResult MoveToTrash([FromBody] List<string> dossierLinkMapID)
        {
            try
            {
                bool bReturn = SS.DossierService.MoveToTrash(dossierLinkMapID);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Deleted, "Dossier Links") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.DeleteFailed, "Dossier Links") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "MoveToTrash");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }




        /// <summary>
        /// This API is used to Update DossierLinkMap table To mark the IsEdit = 1 for records edit and also update DossierEdit 
        /// table to save the old and new updated values
        /// </summary>
        /// <param name="lstLinksToUpdate">object containing dossierLinkMapID to Update the record and contains old to new record change history json</param> 
        /// <returns>true if successfully Updated else false</returns>
        /// <createdon>07-SEP-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        [HttpPost]
        [Route("Dossier_Review_UpdateDataAfterEdit")]
        public IActionResult UpdateDataAfterEdit([FromBody] List<RQ_DossierReviewLinks> lstLinksToUpdate)
        {
            try
            {
                bool bReturn = SS.DossierService.UpdateDataAfterEdit(lstLinksToUpdate);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Updated, "Dossier Review links") });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = string.Format(Common.UpdateFailed, "Dossier Review links") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "UpdateDataAfterEdit");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to get all client topic for given clientID and topicTypeId for dropdown
        /// </summary>
        /// <param name="clientId">ClientID for which client topic need to be fetched</param>
        /// <param name="topicTypeId">TopicTypeId (by default, 2)</param>
        /// <returns>list of object containing client topic for sentry topic type  </returns>
        /// <createdon>03-Oct-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Dossier_GetClientTopicOfSentry")]
        public IActionResult GetClientTopicOfSentry(int clientId, int topicTypeId = 2)
        {
            try
            {
                List<ClientTopic> lstClientTopic = SS.DossierService.GetAllClientTopicByClientID(topicTypeId, clientId);
                if (lstClientTopic!=null && lstClientTopic.Count>0)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = lstClientTopic });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Details not found." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "GetClientTopicOfSentry");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }
    }
}
