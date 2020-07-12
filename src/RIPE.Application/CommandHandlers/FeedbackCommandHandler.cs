using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RIPE.Application.Command;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Responses;
using RIPE.Domain.Domains.Feedback;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.CommandHandlers
{
    public class FeedbackCommandHandler : IRequestHandler<FeedbackCommand, Response>
    {
        private readonly IRipeRepository _feedbackRepository;
        private readonly ILogger<FeedbackCommandHandler> _logger;

        public FeedbackCommandHandler
        (
                ILogger<FeedbackCommandHandler> logger,
                IRipeRepository feedbackRepository
        )
        {
            _logger = logger;
            _feedbackRepository = feedbackRepository;
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
                await _feedbackRepository.WriteFeedback(new Feedback(request.CustomerFeedback, request.Email));

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
