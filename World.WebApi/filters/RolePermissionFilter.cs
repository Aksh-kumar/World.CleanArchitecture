using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using World.WebApi.common;

namespace World.WebApi.filters
{
    public class RolePermissionFilter : ActionFilterAttribute
    {
        #region Public Methods

        /*public override void OnActionExecuted(ActionExecutedContext context)
        {
            ControllerActionDescriptor descriptor = (ControllerActionDescriptor)context.ActionDescriptor;

        }*/

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                // ControllerActionDescriptor descriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                // string actionName = descriptor.ActionName;
                // string controllerName = descriptor.ControllerName;

                //Get claim details passes by AuthorizationTokenFilter
                if (context.HttpContext.Items != null && context.HttpContext.Items.Count > 0)
                {
                    string method = context.HttpContext.Request.Method;
                    Claim? claim = (Claim?)context.
                        HttpContext.
                        Items[AutherizationConstant.HTTP_ALLOW_METHOD_PROPERTY];
                    bool isSuccess = AuthService.Authorize(claim, method);
                    // if (!isSuccess) throw new UnauthorizedAccessException("Method Not Allowed");
                }
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(MessageConstants.UnauthorizedAccess, ex);

            }
            base.OnActionExecuting(context);
        }
        #endregion
    }
}
