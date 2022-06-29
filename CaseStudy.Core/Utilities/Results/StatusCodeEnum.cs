using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Core.Utilities.Results
{
    public enum StatusCodeEnum: int
    {
        Success = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        UnAuthorized = 401,
        NotFound = 404,
        MethodNotAllowed = 405,
        InternalServerError = 500,
        GatewayTimeout = 504
    }
}
