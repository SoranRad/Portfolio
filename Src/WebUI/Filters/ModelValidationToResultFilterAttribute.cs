using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Resources.Messages;
using SharedKernel.Result;

namespace WebUI.Filters
{
    public class ModelValidationToResultFilterAttribute : ActionFilterAttribute
    {
        public ModelValidationToResultFilterAttribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var modelError = new SerializableError(context.ModelState);

                Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

                foreach (var error in modelError)
                {
                    if (error.Value != null)
                    {

                        if (error.Value is string[] errors)
                        {
                            foreach (var e in errors)
                            {
                                dic.Add(error.Key, new List<string>() { e });
                            }
                        }
                        else if (error.Value is string errorValue)
                            dic.Add(error.Key, new List<string>(){ errorValue });

                    }
                }

                var result = Result.Fail(dic);
                context.Result = new JsonResult(result);
            }
        }

    }
}
