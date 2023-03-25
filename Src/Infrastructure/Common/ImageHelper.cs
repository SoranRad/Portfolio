using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Mime;
using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Common
{
    public static class ImageHelper
    {
        public static MemoryStream ToStream(this byte[] bytes)
        {
            return new MemoryStream(bytes);
        }
        public static byte[] ToBytes(this IFormFile FormFile)
        {
            using (var ms = new MemoryStream())
            {
                FormFile.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static Image ToImage(this byte[] bytes)
        {
            return Image.FromStream(bytes.ToStream());
        }
        public static Image ToImage(this IFormFile File)
        {
            return File.ToBytes().ToImage();
        }
        public static void OptimizeImage(string Path)
        {
            var optimizer = new ImageOptimizer();
            optimizer.Compress(Path);
        }
    }
}
