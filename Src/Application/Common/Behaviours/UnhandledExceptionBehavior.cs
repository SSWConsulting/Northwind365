﻿using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Northwind.Application.Common.Behaviours;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex, "CleanArchitecture Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);

            throw;
        }
    }
}
