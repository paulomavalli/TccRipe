using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class FeedbackRequest
    {
        public FeedbackRequest()
        {
        }

        public FeedbackRequest(string feedbackOrigin, string customerFeedback, decimal suggestedLimit)
        {
            FeedbackOrigin = feedbackOrigin;
            CustomerFeedback = customerFeedback;
            SuggestedLimit = suggestedLimit;
        }

        public string FeedbackOrigin { get; set; }
        public string CustomerFeedback { get; set; }
        public decimal SuggestedLimit { get; set; }
    }
}
