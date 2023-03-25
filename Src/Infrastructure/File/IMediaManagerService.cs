using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.File
{
    public interface IMediaManagerService
    {

        MessageResult<MediaListDto>   ImageList();

        void    SaveImage              (IFormFile ImageFile );
        void    DeleteImage            (string ImageName);
        string  GetGalleryRootPath     ();

    }

}
