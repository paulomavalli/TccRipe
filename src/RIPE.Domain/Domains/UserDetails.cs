namespace RIPE.Domain.Domains
{
    public class UserDetails
    {
        public UserDetails(string login, string passwordHash)
        {
            Login = login;
            PasswordHash = passwordHash;
        }

        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
