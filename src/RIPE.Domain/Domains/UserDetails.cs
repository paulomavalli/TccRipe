namespace RIPE.Domain.Domains
{
    public class UserDetails
    {
        public UserDetails(string login, string passwordHash, string userName, string foneNumber,
                            string office, string birth, string companyName)
        {
            Login = login;
            PasswordHash = passwordHash;
            UserName = userName;
            FoneNumber = foneNumber;
            Office = office;
            Birth = birth;
            CompanyName = companyName;
        }

        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string FoneNumber { get; set; }
        public string Office { get; set; }
        public string Birth { get; set; }
        public string CompanyName { get; set; }
    }
}
