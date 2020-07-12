using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Queries;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.QueryHandlers
{
    public class ReportQueryHandler : IRequestHandler<ReportQuery, Response<ReportResponse>>
    {
        private readonly ILogger<ReportQueryHandler> _logger;
        private readonly IReadCacheRepository _readCacheRepository;

        public ReportQueryHandler(ILogger<ReportQueryHandler> logger, IReadCacheRepository readCacheRepository)
        {
            _logger = logger;
            _readCacheRepository = readCacheRepository;
        }

        public async Task<Response<ReportResponse>> Handle(ReportQuery request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid().ToString();
            if (request == null)
            {
                return Response<ReportResponse>.Fail(Messages.InvalidRequest);
            }
            if (request.QuantityPositiveAnswer == null )
            {
                return Response<ReportResponse>.Fail(Messages.InvalidCustomerId);
            }
            if (request.QuantityNegativeAnswer == null )
            {
                return Response<ReportResponse>.Fail(Messages.InvalidCustomerId);
            }
            if (request.QuantityNullableAnswer == null )
            {
                return Response<ReportResponse>.Fail(Messages.InvalidCustomerId);
            }


            var response = new ReportResponse();
            var habits = new BestHabits(new List<string>());
            int quantityPositiveAnswer = Int32.Parse(request.QuantityPositiveAnswer);
            int quantityNullableAnswer = Int32.Parse(request.QuantityNullableAnswer);
            int quantityNegativeAnswer = Int32.Parse(request.QuantityNegativeAnswer);

            try
            {
                decimal PerCentOkAsnwer = (decimal)0.714 * quantityPositiveAnswer;
                decimal PerCentNullableAsnwer = (decimal)0.714 * quantityNullableAnswer;
                decimal PerCentNegativeAsnwer = (decimal)0.714 * quantityNegativeAnswer;
                var logins = await _readCacheRepository.GetUser();


                if (PerCentOkAsnwer >= 89)
                {
                    habits = new BestHabits(new List<string> {
                                                                "1",
                                                                "2"
                                                              });
                    response.NivelMaturidade = "5";
                    response.Recomendacoes = habits;
                }
                else if (PerCentOkAsnwer >= 49 && PerCentOkAsnwer < 89)
                {
                    habits = new BestHabits(new List<string> {
                                                                "1",
                                                                "2"
                                                              });
                    response.NivelMaturidade = "4";
                    response.Recomendacoes = habits;
                }
                else if (PerCentOkAsnwer > 14 && PerCentOkAsnwer < 49)
                {
                    habits = new BestHabits(new List<string> {
                                                                "1",
                                                                "2"
                                                              });
                    response.NivelMaturidade = "3";
                    response.Recomendacoes = habits;
                }
                else if (PerCentOkAsnwer > 8 && PerCentOkAsnwer <= 14)
                {
                    habits = new BestHabits(new List<string> {
                                                                "1",
                                                                "2"
                                                              });
                    response.NivelMaturidade = "2";
                    response.Recomendacoes = habits;
                }
                else
                { 
                    
                    habits = new BestHabits (new List<string> {
                                                                "1",
                                                                "2" 
                                                              });
                    response.NivelMaturidade = "1";
                    response.Recomendacoes = habits;
                }
      
                response.PorcentagemRespostasNegativas = PerCentNegativeAsnwer.ToString();
                response.PorcentagemRespostasNulas = PerCentNullableAsnwer.ToString();
                response.PorcentagemRespostasPositivas = PerCentOkAsnwer.ToString();
                

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
