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
                int somaRespostas = quantityPositiveAnswer + quantityNullableAnswer + quantityNegativeAnswer;

                if (somaRespostas < 140) quantityNullableAnswer = 140 - somaRespostas;

                decimal PerCentOkAsnwer = (decimal)0.714 * quantityPositiveAnswer;
                decimal PerCentNullableAsnwer = (decimal)0.714 * quantityNullableAnswer;
                decimal PerCentNegativeAsnwer = (decimal)0.714 * quantityNegativeAnswer;
                var logins = await _readCacheRepository.GetUser();


                if (PerCentOkAsnwer >= 89)
                {
                    habits = new BestHabits(new List<string> {
                                                                " <li>   Permanecer em constante otimização; </li> <br>" +
                                                                " <li>   Atualização periódica da documentação e eventos que vierem a surgir; </li> <br>"+
                                                                " <li>   Definir um plano de continuidade, afim de permanecer com o nível 5 de maturidade. </li> <br>"
                                                              });
                    response.NivelMaturidade = "5";
                    response.Recomendacoes = habits;
                }
                else if (PerCentOkAsnwer >= 49 && PerCentOkAsnwer < 89)
                {
                    habits = new BestHabits(new List<string> {
                                                                " <li>    Os requisitos do processo não se limitam as necessidades atuais, é necessária uma visão de futuro; </li> <br>"
                                                                +" <li>    Os treinos realizados internamente acabam gerando boas práticas para o uso de outras empresas; </li> <br>"
                                                                +" <li>    Procedimentos e técnicas precisam estar bem estabelecidos para que boas práticas externas sejam aplicadas; </li> <br>"
                                                                +" <li>    É necessário o uso intensivo de tecnologia para consolidar práticas sofisticadas; </li> <br>"
                                                                +" <li>    A avaliação de desempenho é utilizada em todos os aspectos, inconsistências são notadas e corrigidas rapidamente quando a causa raiz é encontrada; </li> <br>"
                                                                +" <li>    Necessária a utilização de ajuda externa na melhoria dos processos. </li> <br>"
                                                              });
                    response.NivelMaturidade = "4";
                    response.Recomendacoes = habits;
                }
                else if (PerCentOkAsnwer > 14 && PerCentOkAsnwer < 49)
                {
                    habits = new BestHabits(new List<string> {
                                                                " <li>    Requisitos do processo são compreendidos plenamente; </li> <br>"
                                                               +" <li>    Treinos formais precisam suportar um programa de capacitação do funcionário; </li> <br>"
                                                               +" <li>    Papéis e responsabilidades são definidos e documentados; </li> <br>"
                                                               +" <li>    Técnicas e ferramentas formam uma abordagem tática; </li> <br>"
                                                               +" <li>    É necessário medir o desempenho destes processos+ analisar a causa raiz de problemas e identificar as inconsistências; </li> <br>"
                                                               +" <li>    Envolvimento de todos os funcionários relevantes para o setor. </li> <br>"
                                                              });
                    response.NivelMaturidade = "3";
                    response.Recomendacoes = habits;
                }
                else if (PerCentOkAsnwer > 8 && PerCentOkAsnwer <= 14)
                {
                    habits = new BestHabits(new List<string> {
                                                                " <li>    Compreensão da necessidade dos processos e as ações necessárias para agir; </li> <br>"
                                                               +" <li>    Treinos informais são feitos e auxiliam na formação individual do profissional; </li> <br>"
                                                               +" <li>    Práticas necessitam ser padronizadas e documentadas; </li> <br>"
                                                               +" <li>    Ferramentas utilizadas são padronizadas; </li> <br>"
                                                               +" <li>    Avaliação de desempenho começa a surgir+ porém análise dos problemas ainda é intuitiva; </li> <br>"
                                                               +" <li>    Apenas profissionais de TI são envolvidos nos processos. </li> <br>"
                                                              });
                    response.NivelMaturidade = "2";
                    response.Recomendacoes = habits;
                }
                else
                { 
                    
                    habits = new BestHabits (new List<string> {
                                                                " <li>    Necessária a atenção para com as necessidades do processo; </li> <br>"
                                                               +" <li>    Necessária a troca de informações a respeito das necessidades e problemas encontrados; </li> <br>"
                                                               +" <li>    Maneiras de abordar os problemas acabam surgindo intuitivamente de várias fontes; </li> <br>"
                                                               +" <li>    Ferramentas passam a surgir com o intuito de resolver o problema; </li> <br>"
                                                               +" <li>    Ainda não é estabelecida a avaliação do desempenho. </li> <br>" 
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
