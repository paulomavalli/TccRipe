using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class QuestionsRequest
    {
        public QuestionsRequest(List<TypeQuestions> typesQuestions)
        {
            TypesQuestions = typesQuestions;
        }

        public List<TypeQuestions> TypesQuestions { get; set; }

    }
}
