using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Common.Behaviours;

public class PerformanceBehavior<TRequest, TResponse>(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var name = typeof(TRequest).Name;

            logger.LogWarning("Northwind Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}", 
                name, _timer.ElapsedMilliseconds, currentUserService.GetUserId(), request);
        }

        return response;
    }
}
