﻿using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class LoginRequest
    {
        public LoginRequest()
        {
        }

        public LoginRequest(string login, string password, string userName, 
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
