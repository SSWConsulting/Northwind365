using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Northwind.Application.Common.Interfaces;

namespace Northwind.Application.Common.Behaviours;

public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PerformanceBehavior(ILogger<TRequest> logger, ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        if (_timer.ElapsedMilliseconds > 500)
        {
            var name = typeof(TRequest).Name;

            _logger.LogWarning("Northwind Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}", 
                name, _timer.ElapsedMilliseconds, _currentUserService.GetUserId(), request);
        }

        return response;
    }
}
