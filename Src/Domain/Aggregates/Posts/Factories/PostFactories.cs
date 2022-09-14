using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Result;

namespace Domain.Aggregates.Posts
{
    public partial class Posts
    {
        public static Result<Posts> Create(string Content,string Title)
        {
            


            var newPost = new Posts();
            return Result.Success(newPost);
        }
    }
}
