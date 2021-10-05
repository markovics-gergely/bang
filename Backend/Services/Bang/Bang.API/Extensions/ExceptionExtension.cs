using Bang.BLL.Application.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Hellang.Middleware.ProblemDetails;

namespace Bang.API.Extensions
{
    public static class ExceptionExtension
    {
        public static void AddExceptionExtensions(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => false;
                options.Map<EntityNotFoundException>(
                (ctx, ex) =>
                {
                    var pd = StatusCodeProblemDetails.Create(StatusCodes.Status404NotFound);
                    pd.Title = ex.Message;
                    return pd;
                }
                );
            });
        }
    }
}
