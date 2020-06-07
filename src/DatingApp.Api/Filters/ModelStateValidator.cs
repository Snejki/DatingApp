namespace DatingApp.Api.Filters
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Newtonsoft.Json;

    public class ModelStateValidator : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new Dictionary<string, string[]>();
                var result = new ContentResult();

                foreach (var item in context.ModelState)
                {
                    errors.Add(item.Key, item.Value.Errors.Select(s => s.ErrorMessage).ToArray());
                }

                var content = JsonConvert.SerializeObject(new { errors });
                result.Content = content;
                result.ContentType = "application/json";

                context.HttpContext.Response.StatusCode = 422;
                context.Result = result;
            }
        }
    }
}
