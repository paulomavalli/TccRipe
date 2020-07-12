namespace RIPE.Domain.Domains.Feedback
{
    public class Feedback
    {
        public Feedback(string email, string customerFeedback)
        {
            CustomerFeedback = customerFeedback;
            Email = email;
        }
        public string Email { get; }
        public string CustomerFeedback { get; }
    }
}
