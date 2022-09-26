using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Posts.Queries
{
    public class PagedPostDto
    {
        public Guid Id { get; set; }

        public string ShortId { get; set; }

        public string               Content             { get; set; }

        public string               Title               { get; set; }

        public bool                 IsContentFirst      { get; set; }

        public string               Tags                { get; set; }

        public string               FileName            { get; set; }

        public DateTime             CreatedDate         { get; set; }

    }
}
