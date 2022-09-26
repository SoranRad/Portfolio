
using Domain.SharedKernel; 

namespace Infrastructure.File
{
    public class FileDetective : IFileDetection, ITransientDependency
    {
        public bool IsImage(string extention)
        {
            var imgExtentions = new string[] {"png","jpg","jpeg" ,"gif", "bmp", "webp"};

            return imgExtentions.Contains(extention);
        }

        public bool IsSound(string extention)
        {
            var soundExtentions = new string[] { "mp3", "wav", "ogg" };
             

            return soundExtentions.Contains(extention);
        }
    }
}
