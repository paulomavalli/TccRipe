namespace RIPE.Domain.Domains.Feedback
{
    public class Feedback
    {
        public Feedback(string email, string customerFeedback)
        {
            Email = email;
            CustomerFeedback = customerFeedback;
        }
        public string Email { get; set; }
        public string CustomerFeedback { get; set; }
    }
}
