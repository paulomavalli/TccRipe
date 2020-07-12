using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class FeedbackRequest
    {
        public FeedbackRequest()
        {
        }

        public FeedbackRequest(string email, string customerFeedback)
        {
            Email = email;
            CustomerFeedback = customerFeedback;
        }

        public string Email { get; set; }
        public string CustomerFeedback { get; set; }
    }
}
