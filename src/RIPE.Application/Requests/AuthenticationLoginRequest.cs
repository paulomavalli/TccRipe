using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationLoginRequest
    {
        public AuthenticationLoginRequest()
        {
        }

        public AuthenticationLoginRequest(string login, string password)
        {
            Login = login;
            Password = password;          
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}
