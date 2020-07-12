using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Queries;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Domain.Domains.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.QueryHandlers
{
    public class SurveyQueryHandler : IRequestHandler<SurveyQuery, Response<QuestionsResponse>>
    {
        private readonly ILogger<SurveyQueryHandler> _logger;
        private readonly IReadCacheRepository _readCacheRepository;
        public SurveyQueryHandler(ILogger<SurveyQueryHandler> logger, IReadCacheRepository readCacheRepository)
        {
            _logger = logger;
            _readCacheRepository = readCacheRepository;
        }

        public async Task<Response<QuestionsResponse>> Handle(SurveyQuery request, CancellationToken cancellationToken)
        {
            var requestId = Guid.NewGuid().ToString();
            if (request == null)
            {
                return Response<QuestionsResponse>.Fail(Messages.InvalidRequest);
            }
            if (request.ValidateUser == null)
            {
                return Response<QuestionsResponse>.Fail(Messages.InvalidCustomerId);
            }

            var response = new QuestionsResponse();

            try
            {
                var logins = await _readCacheRepository.GetUser();
                var validLogin = logins.Select(x => x.Login = request.ValidateUser);

                if (!validLogin.Any() || validLogin == null)
                {
                    return Response<QuestionsResponse>.Fail(new Error("GenericError",
                   $"RequestId: {requestId} - Erro ao autenticar usuário getQuestions",
                   StatusCodes.Status500InternalServerError));
                }

                var getQuestions = new List<TypeQuestions>
                {
                    new TypeQuestions { TypeId = "1",
                                        TypeDescription = "Organização",
                                        QuestionDescription =  new List<string> { "  Risco está relacionado com o nível de satisfação e a expectativa do cliente?"," Risco está relacionado com o alcance dos objetivos da organização?"," O conceito de risco tem mais foco no cliente do que na organização?"," Risco é o efeito acumulativo da probabilidade de ocorrências incertas que podem afetar positivamente ou negativamente os objetivos finais da organização?","A organização atualmente avalia o processo de análise de riscos como um fator de extrema importância?","A organização possui um processo exclusivo para análise de riscos?" }
                                      } ,
                    new TypeQuestions { TypeId = "2",
                                        TypeDescription = "Conceito de Risco para a organização",
                                        QuestionDescription =  new List<string> {"Existe uma estrutura de gestão de riscos de TI alinhada com a estrutura de gestão de riscos da organização?","Os contextos internos e externos estão documentados e alinhados?, Considerando que a gestão de riscos de TI aborde contextos internos, e a gestão de riscos no geral englobe os contextos externos","Existe o processo para avaliação de risco?","Existe uma documentação para descrever os objetivos das avaliações e os critérios pelos quais os riscos são avaliados?","Há métodos periódicos, cujo o objetivo seja mensurar os possíveis riscos?","São identificados eventos (importante ameaça real que explora significativas vulnerabilidades) com potencial impacto negativo nos objetivos ou nas operações da organização, incluindo aspectos de negócios, regulamentação, aspectos jurídicos, tecnologia, parcerias de negócio, recursos humanos e operacionais?","É avaliado regularmente a probabilidade e o impacto de todos os riscos identificados, utilizando métodos qualitativos e quantitativos?","É mantido um processo de respostas a riscos para assegurar que controles com uma adequada relação custo-benefício mitiguem a exposição aos riscos de forma contínua?","Existe um planejamento e priorização das atividades de controle para implementar as respostas aos riscos identificadas como necessárias?"," Esse planejamento inclui a identificação de custos, benefícios e os responsáveis pela execução de controle?","Existe um gerenciamento específico para o acesso ao banco de dados?","Há documentações e políticas para gravação, leitura e alteração de informações internas?","Existe um planejamento e priorização de informações?","Existem cópias de segurança para qualquer tipo de informação?","Existem cópias de segurança para informações essenciais para o funcionamento da organização?" }
                                      } ,
                    new TypeQuestions { TypeId = "3",
                                        TypeDescription = "Avaliar e controlar os Riscos de TI",
                                        QuestionDescription =  new List<string> { "  Riscos do tipo financeiro."," Riscos do tipo estratégico.","Riscos do tipo operacional","Riscos relacionados com desastres. ","Riscos relacionados com o ambiente externo","Riscos relacionados com o ambiente interno." }
                                      } ,
                    new TypeQuestions { TypeId = "4",
                                        TypeDescription = "Priorização de riscos",
                                        QuestionDescription =  new List<string> { "Existe um tratamento específico para requisitos de segurança de sistemas de informação?","Existe um processo de análise e especificação dos requisitos de segurança?","Existe alguma ferramenta que acompanhe o processamento correto nas aplicações utilizadas internamente?","Existem parâmetros de validação dos dados de entrada?","Existe algum controle do processamento de dados interno?","Existe alguma método de verificação de integridade de mensagens compartilhadas (Ex: e-mails)?","Existem parâmetros de validação dos dados de saída?","Há algum processo de controles criptográficos? Para uma possível troca de dados.","Existe uma política para o uso de controles criptográficos?","Caso as duas últimas perguntas se apliquem a organização, existem um gerenciamento de chaves utilizadas para criptografia?","Atualmente, qual o nível de segurança dos arquivos gerais do sistema interno da empresa?","Há um controle de software operacional?","Existe uma política de proteção de dados internos?","Há um controle de acesso aos códigos-fonte dos programas/ferramentas utilizadas?","Atualmente, em qual nível estaria a segurança adotada para processos de desenvolvimento e suporte?","Existem uma documentação com procedimentos para controle de mudanças, considerando as mudanças dentro do âmbito de tecnologia da informação?","Existe um processo de análise crítica/técnica das aplicações após mudanças?","Existe Restrições sobre mudanças em pacotes de software?","Há um controle para evitar/ter ciência de possíveis vazamentos de informações?","Há desenvolvimento terceirizado de software?, Ou dependência de um software terceiro?","Há uma gestão específica para vulnerabilidades técnicas?","Há controle de vulnerabilidades técnicas?" }
                                      } ,
                    new TypeQuestions { TypeId = "5",
                                        TypeDescription = "Aquisição, desenvolvimento e manutenção de sistemas de informação",
                                        QuestionDescription =  new List<string> { "Atualmente a organização tem uma área ou responsáveis específicos com a segurança da informação?","Existe uma coordenação da segurança da informação?","Existe uma distribuição de responsabilidades para ações de segurança da informação?","Existe algum processo de autorização para os recursos de processamento da informação?","Existem termos de confidencialidade?","Há políticas e documentos para especificar o uso de informações privadas?","Há algum contato com técnicos externos especialistas em segurança da informação?","Há uma análise crítica independente da segurança da informação?" }
                                      } ,
                    new TypeQuestions { TypeId = "6",
                                        TypeDescription = "Organizando a segurança da informação - Parte Interna e Externa",
                                        QuestionDescription =  new List<string> { "Há identificação dos riscos relacionados com partes externas?","Há identificações e alinhamento com a segurança da informação, quando existe relação com clientes externos?","Há identificação e alinhamento com a segurança da informação nos acordos com terceiros?","Existe uma política ou algum documento específico para lidar com o acesso a dados por terceiros?","Existe alguma medida preventiva para limitar esse acesso de terceiros?","Há algum método de armazenamento de ações de terceiros dentro da organização (exemplo: logs para rastreio)?","Quando há troca de dados sigilosos com terceiros existe um tratamento específico para assegurar a integridade dos dados em trânsito?","A organização toma medidas de segurança específicas para o relacionamento com terceiros?" }
                                      } ,
                    new TypeQuestions { TypeId = "7",
                                        TypeDescription = "Gerenciamento:  Gestão Operacional, Gestão de mudanças, Gerenciamento de serviços terceirizados, Gestão de capacidade, Gerenciamento da segurança em redes, Gerenciamento e definição de normas de conduta",
                                        QuestionDescription =  new List<string> { "Existe uma área para procedimentos e responsabilidades operacionais?","Existe Documentação dos procedimentos de operação?", "Há uma área específica ou que aborde o assunto: gestão de mudanças?","Há segregação de funções?", "Existe a separação dos recursos de desenvolvimento, teste e de produção?", "Há um gerenciamento de serviços terceirizados?", "Existe uma supervisão específica para as entregas de serviços?", "Há monitoramento e análise crítica de serviços terceirizados?", "Há gerenciamento de mudanças para serviços terceirizados?", "Há planejamento e aceitação dos sistemas?", "existe um gerenciamento de capacidade?", "Existe um termo para aceitação de sistemas?", "Há proteção contra códigos maliciosos e códigos móveis?", "Há controles contra códigos maliciosos?", "Há controles contra códigos móveis?", "Existem cópias de segurança?", "Existem cópias de segurança específicas para informações?", "Há gerenciamento da segurança em redes?", "Existem controles de redes?", "Há termos de segurança dos serviços de rede?", "Existem uma definição das normas de conduta?", "Há um termo para manuseio de mídias internamente?", "Há um gerenciamento de mídias removíveis?", "Há uma política para descarte de mídias?", "Existem procedimentos para tratamento de informação?", "Há termos de segurança e/ou documentação dos sistemas?", "Existe algum gerenciamento das normas de conduta?", "Existem políticas e procedimentos para troca de informações?", "As políticas costumam ser reavaliadas com o passar do tempo?", "Há política de segurança para mídias em trânsito, como mensagens eletrônicas?", "Existe sistemas específicos que armazene informações de negócios?", "A organização utiliza serviços de comércio eletrônico?", "A organização costuma atualizar os documentos e políticas de segurança da informação?", "A organização trabalha com transações online?", "Existem informações publicamente disponíveis?", "Há um banco específico para armazenar registros de ações (logs) para diferentes funções que atuem diretamente com informações restritas/secretas?", "Existem registros de auditoria?", "Há monitoramento de uso dos sistemas internos?", "Há proteções das informações dos registros (logs)?", "Há registros (logs) de administradores e operadores?", "Registros (log) de falhas", "Há sincronização dos relógios utilizados nos sistemas internos?" }
                                       } ,
                    new TypeQuestions { TypeId = "8",
                                        TypeDescription = "Gestão de ativos",
                                        QuestionDescription =  new List<string> { "Há uma gestão específica que especifique as responsabilidades pelos ativos?","Há um inventário dos ativos?","Existe um proprietário/responsável pelos ativos?","Há um termo de aceite para especifícar o uso aceitável dos ativos?","Existe algum tipo de classificação da informação?","Se houver classificação, há alguma recomendação ou norma específica para esta classificação?","Existem tratamentos da informação de acordo com sua classe?","A organização mensura em níveis de sigilo as informações/dados internos?" }
                                      } ,
                    new TypeQuestions { TypeId = "9",
                                        TypeDescription = "Gestão de continuidade de negócios",
                                        QuestionDescription =  new List<string> { "Existem alguma medida específica da área de T.I para considerar parâmetros de continuidade do negócio, relativos à segurança da informação?","Existe o processo de continuidade de negócios e análise/avaliação de riscos?","Atualmente a segurança da informação faz parte do processo de gestão da continuidade do negócio?","Há um desenvolvimento e implementação de planos de continuidade relativos à segurança da informação?","Atualmente existe um documento detalhando os planos de continuidade do negócio?","Existem testes periódicos, manutenção e reavaliação dos planos de continuidade do negócio?" }
                                      } ,
                    new TypeQuestions { TypeId = "10",
                                        TypeDescription = "Gestão de incidentes de segurança da informação",
                                        QuestionDescription =  new List<string> { "Há notificações sobre fragilidades e eventos de segurança da informação?","Há notificações de eventos de segurança da informação?","Há notificações de fragilidades de segurança da informação?","Há uma gestão de incidentes de segurança da informação?","Existe um termo para fidelizar as responsabilidades e procedimentos dentro do setor de T.I?","Existe um processo de documentação pós-incidente, com objetivo de aprendizado com os incidentes de segurança da informação e melhorias?","Há um processo de coleta de evidências?" }
                                      }
                 };

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
