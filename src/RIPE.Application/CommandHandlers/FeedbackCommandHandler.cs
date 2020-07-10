using RIPE.Application.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System;
using Easynvest.Ops;
using Microsoft.AspNetCore.Http;
using RIPE.Application.Command;
using RIPE.CrossCutting.Extensions;
using RIPE.Domain.Domains.Feedback;
using RIPE.Application.Interfaces.Repository;

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
            if (string.IsNullOrWhiteSpace(request.FeedbackOrigin))
            {
                return Response.Fail("Feedback Origin Vazio.");
            }
            if (string.IsNullOrWhiteSpace(request.CustomerFeedback))
            {
                return Response.Fail("Customer Feedback Vazio.");
            }

            var customerId = request.CustomerId;
            var customerHash = customerId.GenerateSha256Hash();

            try
            {
                await _feedbackRepository.WriteFeedback(new Feedback(customerId, request.FeedbackOrigin, request.CustomerFeedback, request.SuggestedLimit));

                return Response.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RequestId: {requestId} - Erro ao gravar feedback do cliente {customerHash}");
                return Response.Fail(new Error("GenericError",
                    $"RequestId: {requestId} - Erro ao gravar feedback do cliente {customerHash}",
                    StatusCodes.Status500InternalServerError));
            }
        }

    }
}
