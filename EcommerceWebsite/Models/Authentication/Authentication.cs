using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class Authentication : ActionFilterAttribute
{
    private readonly string[] _allowedRoles;

    public Authentication(params string[] allowedRoles)
    {
        _allowedRoles = allowedRoles;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var userName = context.HttpContext.Session.GetString("UserName");

        if (userName == null)
        {
            // User is not authenticated, redirect to login page
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"Controller", "Access"},
                    {"Action", "Login" }
                });
        }
        else
        {
            // Check if the user has the required role
            var userRole = context.HttpContext.Session.GetString("UserRole");

            if (!_allowedRoles.Contains(userRole))
            {
                // User doesn't have the required role
                // Redirect to unauthorized page
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access"},
                        {"Action", "Unauthorized" }
                    });
            }
        }
    }
}
