using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Queries;
using RIPE.Application.Responses;
using RIPE.CrossCutting.Extensions;
using RIPE.Domain;
using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.QueryHandlers
{
    public class QuestionsQueryHandler : IRequestHandler<CollateralExceededByValueQuery, Response<QuestionsResponse>>
    {
        private readonly ILogger<QuestionsQueryHandler> _logger;
        private readonly IRipeRepository _ripeRepository;
        public QuestionsQueryHandler(ILogger<QuestionsQueryHandler> logger, IRipeRepository ripeRepository)
        {
            _logger = logger;
            _ripeRepository = ripeRepository;
        }

        public async Task<Response<QuestionsResponse>> Handle(CollateralExceededByValueQuery request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid().ToString();
            if (request == null)
            {
                return Response<QuestionsResponse>.Fail(Messages.InvalidRequest);
            }
            if (request.CheckBoxes == null || !request.CheckBoxes.Any())
            {
                return Response<QuestionsResponse>.Fail(Messages.InvalidCustomerId);
            }
            
            var response = new QuestionsResponse();

         
           // var customerHash = customerId.GenerateSha256Hash();

            try
            {
                var getQuestions = await _ripeRepository.GetQuestions();
                if (getQuestions == null)
                {
                    return Response<QuestionsResponse>.Fail(new Error("GenericError",
                      $"RequestId: {requestId} - Erro ao obter questionário no banco",
                      StatusCodes.Status204NoContent));
                }

                response.survey = getQuestions;               

                return Response<QuestionsResponse>.Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RequestId: {requestId} - Erro ao obter questionário no banco");
                return Response<QuestionsResponse>.Fail(new Error("GenericError",
                    $"RequestId: {requestId} - Erro ao obter questionário no banco",
                    StatusCodes.Status500InternalServerError));
            }
        }

    }
}
