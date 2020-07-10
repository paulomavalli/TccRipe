using RIPE.Application.Responses;
using MediatR;

namespace RIPE.Application.Queries
{
    public class SurveyQuery: IRequest<Response<QuestionsResponse>>
    {
        public SurveyQuery(string validateUser)
        {
            ValidateUser = validateUser;
        }

        public string ValidateUser { get; }

    }
}
