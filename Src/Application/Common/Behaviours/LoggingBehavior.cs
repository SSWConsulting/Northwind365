using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Common.Behaviours;

public class LoggingBehavior<TRequest>(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger _logger = logger;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var name = typeof(TRequest).Name;

        _logger.LogInformation("Northwind Request: {Name} {@UserId} {@Request}", 
            name, currentUserService.GetUserId(), request);

        return Task.CompletedTask;
    }
}
