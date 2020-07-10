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
    public class ReportQueryHandler : IRequestHandler<ReportQuery, Response<ReportResponse>>
    {
        private readonly ILogger<ReportQueryHandler> _logger;
        private readonly IRipeRepository _ripeRepository;
        public ReportQueryHandler(ILogger<ReportQueryHandler> logger, IRipeRepository ripeRepository)
        {
            _logger = logger;
            _ripeRepository = ripeRepository;
        }

        public async Task<Response<ReportResponse>> Handle(ReportQuery request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid().ToString();
            if (request == null)
            {
                return Response<ReportResponse>.Fail(Messages.InvalidRequest);
            }
            if (request.CheckBoxes == null)
            {
                return Response<ReportResponse>.Fail(Messages.InvalidCustomerId);
            }
            

            var response = new ReportResponse();

            //var customerId = request.CustomerId;
            
            //var customerHash = customerId.GenerateSha256Hash();

            try
            {
                decimal teste = 0;
                var collateral = await _ripeRepository.GetQuestions();
                if (collateral != null) teste = 1;

                //var custodyQuantityUsedAsCollateral = collateral?.Sum(c => c.Quantity) ?? 0;

                //if (custodyQuantityUsedAsCollateral == 0)
                //{
                //    response.ExceededQuantity = 0;
                //    return Response<ReportResponse>.Ok(response);
                //}

                //var exceededQuantityInCollateral = securityQuantity - custodyQuantityUsedAsCollateral - withdrawalQuantity;

                //if (exceededQuantityInCollateral < 0)
                //    response.ExceededQuantity = decimal.Round(exceededQuantityInCollateral, 2) * -1;
                response.ExceededQuantity = teste;

                return Response<ReportResponse>.Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RequestId: {requestId} - Erro ao obter relatório do cliente ");
                return Response<ReportResponse>.Fail(new Error("GenericError",
                    $"RequestId: {requestId} -  Erro ao obter relatório do cliente",
                    StatusCodes.Status500InternalServerError));
            }
        }

    }
}
