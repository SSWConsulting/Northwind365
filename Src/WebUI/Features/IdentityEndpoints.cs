using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using SSW.CleanArchitecture.WebApi.Extensions;

namespace Northwind.WebUI.Features;

public static class IdentityEndpoints
{
    public static void MapIdentityEndpoints(this WebApplication app)
    {
        var group = app
                    .MapGroup("_configuration")
                    .WithTags("identity");

        group
            .MapGet("/{clientId}", (string clientId, IClientRequestParametersProvider clientRequestParametersProvider, IHttpContextAccessor contextAccessor) =>
                clientRequestParametersProvider.GetClientParameters(contextAccessor.HttpContext, clientId))
            .WithName("GetClientRequestParameters")
            .ProducesGet<IDictionary<string, string>>();
    }
}