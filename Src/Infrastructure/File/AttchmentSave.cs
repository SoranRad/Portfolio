using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using CSharpVitamins;
using Domain.SharedKernel;
using Microsoft.AspNetCore.Http;
using SharedKernel.Result;

namespace Infrastructure.File
{
    public class AttchmentSave : IAttachementSave, ITransientDependency
    {
        public async Task<Result> Save(Guid PostId,IFormFile FormFile)
        {
            // Get Location To Save
            var miniNumber  = new ShortGuid(PostId).ToString();
            var path        = Path.Combine(
                                   GetImageRootPath(),
                                   miniNumber
                               );


            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fileName = Path.Combine(path, FormFile.FileName);

            await using (var fs = new FileStream
                         (
                             fileName, FileMode.OpenOrCreate, 
                             FileAccess.ReadWrite, FileShare.Delete
                         )
                        )
            {
                await FormFile.CopyToAsync(fs);
                fs.Flush(true);
                fs.Close();
            }

            return Result.Success();
        }

        public string GetImageRootPath()
        {
            return Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "image",
                "post");
        }

    }
}
