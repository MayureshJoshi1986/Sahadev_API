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
using System.Collections.Generic;

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
        /// <param name="objEvent">request object containing event detail</param>
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




        /// <summary>
        /// This API is used to insert Feedback detail in Feedback table
        /// </summary>
        /// <param name="objFeedback">request object containing Feedback</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("ShareFeedback")]
        public IActionResult ShareFeedback([FromBody] Feedback objFeedback)
        {
            try
            {
                
                bool bReturn = SS.EventService.InsertFeedback(objFeedback);
                if (bReturn == true)
                {

                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message = "Feedback added successfully.", });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Failed to add Feedback detail. Please try again." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "ShareFeedback");
                return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = "Server Error! Please try again later." });
            }
        }




        /// <summary>
        /// This API is used to insert or update BookMark Table based on the action parameter
        /// </summary>
        /// <param name="objBookMark">request object containing BookMark</param>
        /// <returns>success message if successfully inserted else error message</returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpPost]
        [Route("UpdateBookMark")]
        public IActionResult UpdateBookMark([FromBody] BookMark objBookMark, string action)
        {
            try
            {


                bool bReturn = SS.EventService.InsertUpdateBookMark(objBookMark, action);
                if (bReturn == true)
                {
                    string message = string.Empty;
                    if (action == "Insert")
                        message = "BookMark added successfully";

                    else if (action == "Update")
                        message = "BookMark Updated successfully";

                    else if (action == "Delete")
                        message = "BookMark deleted successfully";


                    return Ok(new GenericResponse.APIResponse { code = HttpStatusCode.OK, message= message, });
                }
                else
                {
                    return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.NotFound, message = "Failed to add BookMark detail. Please try again." });
                }
            }
            catch (Exception ex)
            {
                //For error user Log.LogError methods
                //For warning user Log.LogWarning methods
                //For information user Log.LogInformation methods
                _logger.LogError(ex, _className, "UpdateBookMark");
                return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = "Server Error! Please try again later." });
            }
        }

        /// <summary>
        /// This API is used to Fetch FeedbackType from mstFeedbackType
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <createdon>18-Aug-2024</createdon>
        /// <createdby>Saroj Laddha</createdby>
        /// <modifiedon></modifiedon>
        /// <modifiedby></modifiedby>
        /// <modifiedreason></modifiedreason>
        [HttpGet]
        [Route("Fetch_FeedbackTypes")]
        public IActionResult Fecth_FeedbackTypes()
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
                _logger.LogError(ex, _className, "Fecth_FeedbackTypes");
                return NotFound(new GenericResponse.APIResponse { code = HttpStatusCode.InternalServerError, message = "Server Error! Please try again later." });
            }
        }

    }
}
