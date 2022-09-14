using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace SharedKernel.Result
{
    /// <summary>
    ///  از این کلاس برای ارسال نتایج یک عملیات استفاده می کنیم
    ///  همراه با داده اضافی 
    /// </summary>
    /// <typeparam name="T">نوع داده ای که باید در صورت موفقیت برگردانیم</typeparam>
    public partial class Result<T> : Result  
    {
        public T    Value    { get; private set; }
         
        public Result()
        {
            _errors = new Dictionary<string, List<string>>();
        }
         
        public Result   (T Value) : this()
        { 
            this.Value = Value;
        }

        public Result(bool IsSuccess, string Message) : this()
        {
            if (IsSuccess)
                this.Message = Message;
            else
                AddError("", Message);
        }
       
        public Result( string Message, T Value) : this(Value)
        {
            this.Message = Message;
        }

        public void SetValue(T Value)
        {
            if (this.IsSuccess)
                this.Value = Value ;
        }
      
    }
}
