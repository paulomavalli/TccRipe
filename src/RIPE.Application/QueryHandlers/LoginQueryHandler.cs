using RIPE.Application.Interfaces.Repository;
using RIPE.Application.Queries;
using RIPE.Application.Responses;
using RIPE.Domain;
using RIPE.Domain.Domains;
using RIPE.Domain.Domains.PriorityAggregate;
using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.QueryHandlers
{
    public class LoginQueryHandler : IRequestHandler<ValidateLoginQuery, Response<ValidateLoginResponse>>
    {
        private readonly ILogger<LoginQueryHandler> _logger;
        private readonly IRipeRepository _collateralPriorityRepository;

        public LoginQueryHandler(ILogger<LoginQueryHandler> logger,
                                              IRipeRepository collateralPriorityRepository,
                                              IReportService collateralPriorityService)
        {
            _logger = logger;
            _collateralPriorityRepository = collateralPriorityRepository;
        }

        public async Task<Response<ValidateLoginResponse>> Handle(ValidateLoginQuery request, CancellationToken cancellationToken)
        {
            var response = new ValidateLoginResponse();

            var requestId = Guid.NewGuid().ToString();
            if (request == null)
            {
                return Response<ValidateLoginResponse>.Fail(Messages.InvalidRequest);
            }
            if (request.Login == null || !request.Login.Any())
            {
                return Response<ValidateLoginResponse>.Fail(Messages.InvalidLogin);
            }
            if (request.Password == null || !request.Password.Any())
            {
                return Response<ValidateLoginResponse>.Fail(Messages.InvalidPassword);
            }

            try
            {

                var sucessLogin = await _collateralPriorityRepository.WriteNewUser(request.Login, request.Password);

                if (!sucessLogin)
                { return Response<ValidateLoginResponse>.Fail(new Error("GenericError",
                 $"RequestId: {requestId} - Erro ao inserir dados de um novo usuário",
                 StatusCodes.Status500InternalServerError));
                }

                response.sucess = sucessLogin;

                return Response<ValidateLoginResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"RequestId: {requestId} - Erro ao inserir dados de um novo usuário");
                return Response<ValidateLoginResponse>.Fail(new Error("GenericError",
                    $"RequestId: {requestId} - Erro ao inserir dados de um novo usuário",
                    StatusCodes.Status500InternalServerError));
            }
        }
    }
}
