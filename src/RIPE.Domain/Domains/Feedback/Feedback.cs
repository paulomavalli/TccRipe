namespace RIPE.Domain.Domains.Feedback
{
    public class Feedback
    {
        public Feedback(string customerId, string feedbackOrigin, string customerFeedback, decimal? suggestedLimit)
        {
            CustomerId = customerId;
            FeedbackOrigin = feedbackOrigin;
            CustomerFeedback = customerFeedback;
            SuggestedLimit = suggestedLimit;
        }
        public string CustomerId { get; }
        public string FeedbackOrigin { get; }
        public string CustomerFeedback { get; }
        public decimal? SuggestedLimit { get; }
    }
}
