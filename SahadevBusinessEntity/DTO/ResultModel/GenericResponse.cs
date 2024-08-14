using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SahadevBusinessEntity.DTO.ResultModel
{
    public class GenericResponse
    {
        /// <summary>
        /// APIResponse class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class APIResponse
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public object data { get; set; }

            //public APIResponse(HttpStatusCode statusCode, string responseMessage, object responseData)
            //{
            //    code = statusCode;
            //    message = responseMessage;
            //    data = responseData;
            //}

            public APIResponse()
            {
            }
        }

        /// <summary>
        /// APIResponse class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class APIResponseList
        {
            public HttpStatusCode code { get; set; }
            public string message { get; set; }
            public List<object> data { get; set; }

            //public APIResponseList(HttpStatusCode statusCode, string responseMessage, List<object> responseData)
            //{
            //    code = statusCode;
            //    message = responseMessage;
            //    data = responseData;
            //}
            public APIResponseList()
            {
            }
        }
    }
    }

