using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SeedWork;
using SharedKernel.Result;

namespace Domain.Aggregates.Posts.ValueObjects
{
    public class YoutubLinkAddress : ValueObject
    {

        #region Factories

        public static Result<YoutubLinkAddress> Create(string Value)
        {

            var NewLink = new YoutubLinkAddress(Value);
            return Result.Success<YoutubLinkAddress>(NewLink);
        }

        #endregion

        private YoutubLinkAddress(string value)
        {
            Value = value;
        }

        public      string      Value { get; set; }

        protected   override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
