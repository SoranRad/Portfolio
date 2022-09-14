using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Result
{
    
    public partial class Result
    {
         
        public string   Message         { get; protected set; }
        
        public string   Redirect        { get; set; }

        public bool     IsSuccess       => !_errors.Any();
       
        protected       Dictionary<string, List<string>>            _errors;
       
        public          Dictionary<string, List<string>> Errors =>  _errors;
 
        public Result()
        {
            _errors = new Dictionary<string, List<string>>();
        }

        public Result(bool IsSuccess,string Message) : this()
        {
            if (IsSuccess)
                this.Message = Message;
            else 
                AddError("",Message);
        }

        public void AddError(string MemberName, string ErrorMessage)
        {
            if (_errors.ContainsKey(MemberName))
            {
                var values = _errors[MemberName];
                values.Add(Message);
                _errors[MemberName] = values;
            }
            else
            {
                _errors.Add(MemberName,new List<string>(){ErrorMessage});
            }
        }
        
    }
}
