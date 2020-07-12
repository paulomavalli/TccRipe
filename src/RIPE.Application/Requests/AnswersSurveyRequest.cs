using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class AnswersSurveyRequest
    {
        public AnswersSurveyRequest(int quantityPositiveAnswer, int quantityNegativeAnswer, int quantityNullableAnswer)
        {
            QuantityPositiveAnswer = quantityPositiveAnswer;
            QuantityNegativeAnswer = quantityNegativeAnswer;
            QuantityNullableAnswer = quantityNullableAnswer;
        }

        public int QuantityPositiveAnswer { get; }
        public int QuantityNegativeAnswer { get; }
        public int QuantityNullableAnswer { get; }

    }

}

