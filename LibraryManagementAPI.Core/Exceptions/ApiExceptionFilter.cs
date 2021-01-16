using System;
using LibraryManagementAPI.Core.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryManagementAPI.Core.Exceptions
{
    public class ApiExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                ApiError apiError = null;
                if (context.Exception is ApiException)
                {
                    var ex = context.Exception as ApiException;
                    context.Exception = null;
                    apiError = new ApiError(ex.Message)
                    {
                        StackTrace = ex.StackTrace
                    };

                    context.HttpContext.Response.StatusCode = ex.StatusCode;
                }
                else if (context.Exception is UnauthorizedAccessException)
                {
                    apiError = new ApiError(MessagesResource.ACCESS_DENIED);
                    context.HttpContext.Response.StatusCode = 401;
                }
                else
                {
#if !DEBUG
                string exMessage = MessagesResource.UNHANDLED_ERROR;                
                string stack = null;
#else
                    string exMessage = context.Exception.GetBaseException().Message;
                    string stack = context.Exception.StackTrace;
#endif
                    apiError = new ApiError(exMessage);
                    apiError.StackTrace = stack;

                    context.HttpContext.Response.StatusCode = 500;
                }
                context.Result = new JsonResult(apiError);

                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                ApiError apiError = new ApiError(context.ModelState);
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new JsonResult(apiError);
            }
        }
    }
}
