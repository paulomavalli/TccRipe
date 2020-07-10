using RIPE.Application.Responses;
using Easynvest.Ops;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.HandlersValidators
{
    [ExcludeFromCodeCoverage]
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Response
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(r => r.Errors)
                .Where(e => e != null)
                .ToList();

            return failures.Any()
                ? GenerateErrorResponse(failures)
                : next();
        }

        private Task<TResponse> GenerateErrorResponse(IEnumerable<ValidationFailure> failures)
        {
            var errorMessages = failures.Select(f => f.ErrorMessage);
            var type = typeof(TResponse);
            var method = type.GetMethod("Fail", new[] { typeof(Error), typeof(IEnumerable<string>) });
            var response = method.Invoke(null,
                new object[] { new Error("InvalidParameters", "Parâmetros inválidos", StatusCodes.Status400BadRequest), errorMessages });

            return Task.FromResult(response as TResponse);
        }
    }
}