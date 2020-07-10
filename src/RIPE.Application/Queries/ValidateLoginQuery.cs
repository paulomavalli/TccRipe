using MediatR;
using RIPE.Application.Responses;

namespace RIPE.Application.Queries
{
    public class ValidateLoginQuery : IRequest<Response<ValidateLoginResponse>>
    {
        public ValidateLoginQuery(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }

    }
}
