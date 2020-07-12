using MediatR;
using RIPE.Application.Responses;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;

namespace RIPE.Application.Queries
{
    public class ReportQuery : IRequest<Response<ReportResponse>>
    {
        public ReportQuery(string quantityPositiveAnswer, string quantityNegativeAnswer, string quantityNullableAnswer)
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
