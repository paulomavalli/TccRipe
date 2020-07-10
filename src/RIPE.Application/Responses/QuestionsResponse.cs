using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;

namespace RIPE.Application.Responses
{
    public class QuestionsResponse
    {
        public IEnumerable<TypeQuestions> survey { get; set; }
    }
}
