using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Result;
using ActionExecutedContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext;
using ActionFilterAttribute = Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute;
using ContentResult         = Microsoft.AspNetCore.Mvc.ContentResult;

namespace WebUI.Filters
{
    public class ResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Result)
            {
                case OkObjectResult okObjectResult:
                    {
                        var MessageResult = Result.Success<object>(okObjectResult.Value);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case OkResult okResult:
                    {
                        var MessageResult = Result.Success();
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case BadRequestResult badRequestResult:
                    {
                        // Check
                        var MessageResult = Result.Fail(Resources.Messages.Request.BadRequest);// new MessageResult(false, ResultStatusCode.BadRequest);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case BadRequestObjectResult badRequestObjectResult:
                    {
                        var message = badRequestObjectResult.Value.ToString();
                        if (badRequestObjectResult.Value is SerializableError errors)
                        {
                            var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                            message = string.Join(" | ", errorMessages);
                        }
                        var MessageResult = Result.Fail(message);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case ContentResult contentResult:
                    {
                        var MessageResult = Result.Success(contentResult.Content);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case NoContentResult contentResult:
                    {
                        var MessageResult = Result.Success(Resources.Messages.Request.NoContentResult);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case NotFoundResult notFoundResult:
                    {
                        var MessageResult = Result.Fail(Resources.Messages.Request.NotFoundResult);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case NotFoundObjectResult notFoundObjectResult:
                    {
                        var MessageResult = Result.Fail<object>("", Resources.Messages.Request.NotFoundResult);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case ObjectResult objectResult when objectResult.StatusCode == null && !(objectResult.Value is Result):
                    {
                        var MessageResult = Result.Success(objectResult.Value);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case UnauthorizedResult unauthorizedResult:
                    {
                        var MessageResult = Result.Fail(Resources.Messages.Request.UnauthorizedResult);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }
                case UnauthorizedObjectResult unauthorizedObjectResult:
                    {
                        var MessageResult = Result.Fail(unauthorizedObjectResult.Value);
                        context.Result = new JsonResult(MessageResult);
                        break;
                    }

            }

            base.OnActionExecuted(context);
        }
    }
}
