using MediatR;
using RIPE.Application.Responses;

namespace RIPE.Application.Command
{
    public class FeedbackCommand : IRequest<Response>
    {
        public FeedbackCommand(string email, string customerFeedback)
        {
            Email = email;
            CustomerFeedback = customerFeedback;
        }

        public string Email { get; }
        public string CustomerFeedback { get; }
    }
}
