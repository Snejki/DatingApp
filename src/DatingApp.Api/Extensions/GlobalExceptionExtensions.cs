namespace DatingApp.Api.Extensions
{
    using DatingApp.Api.Middleware;
    using Microsoft.AspNetCore.Builder;

    public static class GlobalExceptionExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
