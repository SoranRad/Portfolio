using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SharedKernel.Result;

namespace Application.Common.Interfaces
{
    public interface IAttachementSave
    {
        Task<Result> Save(Guid PostId, IFormFile FormFile);
        string GetImageRootPath();
    }
}
