/**********************************************************************************************
*  ClassName      :- EventController                                                          *
*  -------------------------------------------------------------------------------------------*
*  Description    :- This class contain API related to event                                  *
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SahadevBusinessEntity.DTO.Model;
using SahadevBusinessEntity.DTO.ResultModel;
using SahadevService;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using SahadevBusinessEntity.Constant.Messages;
using SahadevBusinessEntity.DTO.RequestModel;

namespace Sahadev.API.Sentry
{
    /// <summary>
    /// This is EventController class which contain all APIs related to Event
    /// </summary>
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
        /// <param name="objRQ_Event">request object containing event detail</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>17-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from Event to RQ_Event</modifiedreason>
        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody] RQ_Event objRQ_Event)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(417, new GenericResponse.APIResponse { code = HttpStatusCode.ExpectationFailed, message = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).FirstOrDefault() });
                }
                bool bReturn = SS.EventService.Add(objRQ_Event);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Added, "event") });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.AddFailed, "event") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "Add");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to insert Feedback detail in Feedback table
        /// </summary>
        /// <param name="objRQ_Feedback">request object containing Feedback</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from Feedback to RQ_Feedback</modifiedreason>
        [HttpPost]
        [Route("ShareFeedback")]
        public IActionResult ShareFeedback([FromBody] RQ_Feedback objRQ_Feedback)
        {
            try
            {

                bool bReturn = SS.EventService.InsertFeedback(objRQ_Feedback);
                if (bReturn == true)
                {

                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Format(Common.Added, "Feedback") });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.AddFailed, "Feedback") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "ShareFeedback");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to insert or update Bookmark detail in BookMark Table based on the action parameter
        /// </summary>
        /// <param name="objRQ_BookMark">request object containing BookMark</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from BookMark to RQ_BookMark</modifiedreason>
        [HttpPost]
        [Route("UpdateBookMark")]
        public IActionResult UpdateBookMark([FromBody] RQ_BookMark objRQ_BookMark)
        {
            try
            {
                bool bReturn = SS.EventService.InsertUpdateBookMark(objRQ_BookMark);
                if (bReturn == true)
                {
                    string message = string.Empty;
                    if (objRQ_BookMark.Action == "Insert")
                        message = string.Format(Common.Added, "BookMark");

                    else if (objRQ_BookMark.Action == "Update")
                        message = string.Format(Common.Updated, "BookMark");

                    else if (objRQ_BookMark.Action == "Delete")
                        message = string.Format(Common.Deleted, "BookMark");

                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = message, });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.AddFailed, "BookMark") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "UpdateBookMark");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to Fetch FeedbackType from mstFeedbackType
        /// </summary>
        /// <returns></returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Fetch_FeedbackTypes")]
        public IActionResult Fetch_FeedbackTypes()
        {
            try
            {
                List<FeedbackType> feedbackTypes = SS.EventService.GetAllFeedbackType();
                if (feedbackTypes != null)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, data = feedbackTypes });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Feedback Type not available." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "Fetch_FeedbackTypes");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

        /// <summary>
        /// This API is used to insert data request in DataRequest table
        /// </summary>
        /// <param name="objRQ_DataRequest">request object containing DataRequest detail</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>20-Aug-2024</createdon>
        /// <createdby>PJ</createdby>
        /// <modifiedon>23-Aug-2024</modifiedon>
        /// <modifiedby>PJ</modifiedby>
        /// <modifiedreason>changed request model from DataRequest to RQ_DataRequest</modifiedreason>
        [HttpPost]
        [Route("DownloadData")]
        public IActionResult DownloadData([FromBody] RQ_DataRequest objRQ_DataRequest)
        {
            try
            {
                bool bReturn = SS.EventService.InsertDataRequest(objRQ_DataRequest);
                if (bReturn == true)
                {
                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = string.Empty, });
                }
                else
                {
                    return BadRequest(new GenericResponse.APIResponse { code = HttpStatusCode.BadRequest, message = string.Format(Common.AddFailed, "DataRequest") });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "DownloadData");
                return StatusCode(500, new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = Common.ServerError });
            }
        }

    }
}
