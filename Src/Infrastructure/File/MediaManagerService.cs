using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Common;
using Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Infrastructure.File
{
    public class MediaManagerService : IMediaManagerService
    {
        public MessageResult<MediaListDto> ImageList()
        {
            var path = GetGalleryRootPath();
             
            var galleryList = new MediaListDto()
            {
                Directories = null,
                Files = Directory.GetFiles(path).Select(Path.GetFileName).Select(GetImageUrl).ToList()
            };

            return new MessageResult<MediaListDto>(true, ResultStatusCode.Success, galleryList);
        }

        public void SaveImage(IFormFile ImageFile)
        {
            var path = GetGalleryRootPath();
             
            var img = ImageFile.ToImage();
            var filePath = Path.Combine(path, ImageFile.FileName);

            img.Save(filePath, img.RawFormat);
            ImageHelper.OptimizeImage(filePath);
        }

        public void DeleteImage(string ImageName)
        {
            throw new NotImplementedException();
        }

        public string GetGalleryRootPath()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "image",
                "gallery");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            
            return path;
        }

        public string GetImageUrl(string  FileName)
        {
            return "/Image/gallery/" + FileName;
        }
    }
}
