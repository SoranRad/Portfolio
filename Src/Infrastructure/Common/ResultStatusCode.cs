using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Common
{
    public enum ResultStatusCode
    {
        Success = 0,

        ServerError = 1,

        BadRequest = 2,

        NotFound = 3,

        ListEmpty = 4,

        LogicError = 5,

        UnAuthorized = 6
    }
}
