using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class DirectoryDto
    {
        public string Name { get; set; }
        public int FilesCount { get; set; }
        public IEnumerable<string> SomeFiles { get; set; }
    }
}
