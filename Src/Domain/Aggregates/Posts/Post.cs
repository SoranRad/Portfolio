using System;
using System.Collections.Generic;
using Domain.Aggregates.Posts.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Posts
{
    public partial class Posts : AggregateRoot<Guid>
    {
        private Posts   ()
        {

        }

        public string               Content             { get; set; }

        public string               Title               { get; set; }

        public bool                 IsContentFirst      { get; set; }

        public string               Tags                { get; set; }

    }
}
