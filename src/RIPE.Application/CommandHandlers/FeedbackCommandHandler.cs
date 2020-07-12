using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RIPE.Application.Command;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Responses;
using RIPE.Domain.Domains.Feedback;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.CommandHandlers
{
    public class FeedbackCommandHandler : IRequestHandler<FeedbackCommand, Response>
    {
        private readonly IWriteCacheRepository _writeCacheRepository;
        private readonly ILogger<FeedbackCommandHandler> _logger;

        public FeedbackCommandHandler
        (
                ILogger<FeedbackCommandHandler> logger,
                IWriteCacheRepository writeCacheRepository
        )
        {
            _logger = logger;
            _writeCacheRepository = writeCacheRepository;
        }


        public async Task<Response> Handle(FeedbackCommand request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid().ToString();
            if (request == null)
            {
                return Response.Fail("Feedback Command Vazio.");
            }
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return Response.Fail("Email Vazio.");
            }
            if (string.IsNullOrWhiteSpace(request.CustomerFeedback))
            {
                return Response.Fail("Customer Feedback Vazio.");
            }

            try
            {
                var newFeedback = new Feedback(request.CustomerFeedback, request.Email);
                var feedBack = new List<Feedback> { newFeedback };
                await _writeCacheRepository.SetFeedBack(feedBack);

                return Response.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RequestId: {requestId} - Erro ao gravar feedback do cliente {request.Email}");
                return Response.Fail(new Error("GenericError",
                    $"RequestId: {requestId} - Erro ao gravar feedback do cliente {request.Email}",
                    StatusCodes.Status500InternalServerError));
            }
        }

    }
}
