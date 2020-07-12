﻿using MediatR;
using RIPE.Application.Responses;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;

namespace RIPE.Application.Queries
{
    public class ReportQuery : IRequest<Response<ReportResponse>>
    {
        public ReportQuery(int quantityPositiveAnswer, int quantityNegativeAnswer, int quantityNullableAnswer)
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
