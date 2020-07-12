using MediatR;
using RIPE.Application.Responses;

namespace RIPE.Application.Queries
{
    public class NewUserQuery : IRequest<Response<ValidateLoginResponse>>
    {
        public NewUserQuery(string login, string password, string userName,
                            string foneNumber, string office,
                            string birth, string companyName)
        {
            Login = login;
            Password = password;
            UserName = userName;
            FoneNumber = foneNumber;
            Office = office;
            Birth = birth;
            CompanyName = companyName;
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FoneNumber { get; set; }
        public string Office { get; set; }
        public string Birth { get; set; }
        public string CompanyName { get; set; }
    }

}
