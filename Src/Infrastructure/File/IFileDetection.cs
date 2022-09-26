using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.File
{
    public interface IFileDetection
    {
        bool IsImage(string Path);

        bool IsSound(string Path);
    }
}
