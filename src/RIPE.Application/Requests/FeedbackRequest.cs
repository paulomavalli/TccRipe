using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public LoginRequest(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; set; }
        public string Password { get; set; }

    }
}
