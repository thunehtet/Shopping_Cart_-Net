using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

/*
 * this filter is designed for authorization.
 * [MustLogin] sign means user must login first when they access one page.
 * such as checkout page, order history page, order details page, and so on.
 * if user havn't login, then redirect them to login page.
 */
namespace Login_CA.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var NeedCheck = false;
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                NeedCheck = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true).Any(a => a.GetType().Equals(typeof(MustLoginAttribute)));
            }
            if (!NeedCheck)
            {
                return;
            }

            var username = context.HttpContext.Session.GetString("username");
            if (username == null)
            {
                context.Result = new RedirectResult("/Login/Index");
            }
            base.OnActionExecuted(context);
        }

        public class MustLoginAttribute : ActionFilterAttribute { }
    }
}
