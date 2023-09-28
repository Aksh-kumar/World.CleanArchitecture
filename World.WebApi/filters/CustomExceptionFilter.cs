using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace World.WebApi.filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Pass all exception logs to logger
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            // ControllerActionDescriptor descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            //string actionName = descriptor.ActionName;
            //string controllerName = descriptor.ControllerName;
            // Logger.Log(LogType.ERROR, descriptor.ControllerName, descriptor.ActionName,context.Exception.Message, context.Exception.StackTrace);
        }
    }
}
