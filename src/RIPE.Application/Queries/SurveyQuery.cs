using MediatR;
using RIPE.Application.Responses;

namespace RIPE.Application.Queries
{
    public class SurveyQuery : IRequest<Response<QuestionsResponse>>
    {
        public SurveyQuery(string validateUser)
        {
            ValidateUser = validateUser;
        }

        public string ValidateUser { get; }

    }
}
