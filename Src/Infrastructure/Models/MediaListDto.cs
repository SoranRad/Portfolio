using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class MediaListDto
    {
        public string ParentDirectory { get; set; }
        public IEnumerable<DirectoryDto> Directories { get; set; }
        public IEnumerable<string> Files { get; set; }
    }
}
