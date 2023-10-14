using System.Text;

namespace Northwind.WebUI;

public static class IApplicationBuilderExt
{
    public static void UseRegisteredServicesPage(this IApplicationBuilder app, IServiceProvider provider)
    {
        app.Map("/services", builder => builder.Run(async context =>
        {
            var sb = new StringBuilder();
            sb.Append("<h1>Registered Services</h1>");
            sb.Append("<table><thead>");
            sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
            sb.Append("</thead><tbody>");

            using var scope = provider.CreateScope();

            // TODO: Need to test this
            foreach (var svc in scope.ServiceProvider.GetServices<ServiceDescriptor>())
            {
                sb.Append("<tr>");
                sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                sb.Append($"<td>{svc.Lifetime}</td>");
                sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody></table>");
            await context.Response.WriteAsync(sb.ToString());
        }));
    }
}