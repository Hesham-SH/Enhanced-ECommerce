using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Error are the path the dark side. Error lead to anger. Anger Leads to hate. Hate leads to career change",
                _ => null
                        
            };
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}