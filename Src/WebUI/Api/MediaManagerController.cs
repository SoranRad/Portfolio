using Infrastructure.Common;
using Infrastructure.File;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using WebUI.Api;
using WebUI.Models;

namespace Usra.Admin.api
{
    public class MediaManagerController : BaseApi
    {
        private readonly IMediaManagerService _mediaManagerService;

        public MediaManagerController
            (
                IMediaManagerService mediaManagerService
            )
        {
            _mediaManagerService = mediaManagerService;
        }

        [HttpGet]
        
        public async Task<MessageResult<MediaListDto>> Get()
        {

            return  _mediaManagerService.ImageList();
        }

        [HttpPost] 
        public async Task<MessageResult> Post([FromForm]MediaImagesViewModel Form)
        {
            //Disabled because this is demo version.
            //_mediaManagerService.SaveImage(Form.Image);
            return new MessageResult(true, ResultStatusCode.Success, "Image saved successfully.");
        }

        [HttpDelete] 
        public async Task<MessageResult> Delete([FromForm]string ImageName)
        {
            _mediaManagerService.DeleteImage(ImageName);
            return new MessageResult(true, ResultStatusCode.Success, "Image saved successfully.");
        }
    }
}
