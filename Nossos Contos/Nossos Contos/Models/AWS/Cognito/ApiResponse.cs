using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Models.AWS.Cognito
{
    public class ApiResponse
    {
        public ApiResponse(Codes _code = Codes.OK, string _message = "") { }

        public bool error { get; set; }
        public int code { get; set; }
        public string message { get; set; }

        public enum Codes
        {
            OK = 0,
            ITEM_NOT_FOUND = 1,
            ITEM_ALREADY_EXISTS = 2,
            INVALID_INPUT = 3,
            RESOURCE_UNAVAILABLE = 4,
            UNABLE_PROCESS_REQUEST = 5,
            UNEXPECTED_ERROR = 99
        }


    }
}
