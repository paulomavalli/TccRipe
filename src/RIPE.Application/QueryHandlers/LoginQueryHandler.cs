using Easynvest.Ops;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RIPE.Application.Interfaces.Repository.Cache;
using RIPE.Application.Queries;
using RIPE.Application.Responses;
using RIPE.CrossCutting.Extensions;
using RIPE.Domain;
using RIPE.Domain.Domains;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RIPE.Application.QueryHandlers
{
    public class LoginQueryHandler : IRequestHandler<ValidateLoginQuery, Response<ValidateLoginResponse>>
    {
        private readonly ILogger<LoginQueryHandler> _logger;
        private readonly IReadCacheRepository _readCacheRepository;

        public LoginQueryHandler(ILogger<LoginQueryHandler> logger,
                                 IReadCacheRepository readCacheRepository)
        {
            _logger = logger;
            _readCacheRepository = readCacheRepository;
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

            var passwordHash = request.Password.GenerateSha256Hash();

            try
            {
                //var user = new UserDetails(request.Login, passwordHash);
                var logins = await _readCacheRepository.GetUser();

                var validLogin = logins.Where(x =>x.Login == request.Login);
                var validKey = logins.Where(x => x.PasswordHash == passwordHash);

                if (!validLogin.Any() || validLogin == null || !validKey.Any() || validKey == null)
                {
                    return Response<ValidateLoginResponse>.Fail(new Error("GenericError",
                   $"RequestId: {requestId} - Erro ao autenticar o login do usuário",
                   StatusCodes.Status500InternalServerError));
                }

                response.sucess = true;

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
