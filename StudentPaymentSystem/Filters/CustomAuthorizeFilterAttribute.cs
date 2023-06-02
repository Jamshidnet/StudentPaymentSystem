using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace StudentPaymentSystem.Filters
{

    public class CustomAuthorizeFilterAttribute : AuthorizeAttribute
    {

        public CustomAuthorizeFilterAttribute( string permission)
        {
            Roles = permission;
            await OnAuthorizeAsync()
        }
        public  async override Task OnAuthorizeAsync(ActionExecutingContext actionContext)
        {
            if (!IsAuthorized(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected virtual bool IsAuthorized(ActionExecutedContext actionContext)
        {

            var accessToken = actionContext.Headers.Authorization?.Parameter;

            if (string.IsNullOrEmpty(accessToken) || !IsValidToken(accessToken))
            {
                return false;
            }

            return true;
        }

        protected virtual void HandleUnauthorizedRequest(ActionExecutedContext actionContext)
        {
            actionContext.Result = actionContext.ExceptionHandled;
             
        }

 


    }
}
