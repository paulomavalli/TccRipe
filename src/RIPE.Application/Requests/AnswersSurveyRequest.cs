using System.Diagnostics.CodeAnalysis;

namespace RIPE.Application.Requests
{
    [ExcludeFromCodeCoverage]
    public class AnswersSurveyRequest
    {
        public AnswersSurveyRequest(string quantityPositiveAnswer, string quantityNegativeAnswer, string quantityNullableAnswer)
        {
            QuantityPositiveAnswer = quantityPositiveAnswer;
            QuantityNegativeAnswer = quantityNegativeAnswer;
            QuantityNullableAnswer = quantityNullableAnswer;
        }

        public string QuantityPositiveAnswer { get; }
        public string QuantityNegativeAnswer { get; }
        public string QuantityNullableAnswer { get; }

    }

}

