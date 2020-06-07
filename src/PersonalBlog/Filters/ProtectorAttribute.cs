namespace PersonalBlog.Filters
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using PersonalBlog.Interfaces;

    public class ProtectorAttribute : Attribute, IActionFilter
    {
        private readonly IAuthorizer authorizer;

        public ProtectorAttribute(IAuthorizer authorizer)
        {
            this.authorizer = authorizer;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (!this.authorizer.IsAuthorized())
            {
                context.Result = new RedirectToActionResult("Error", "Home", null);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
