using MediatR;
using RIPE.Application.Responses;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;

namespace RIPE.Application.Queries
{
    public class ReportQuery : IRequest<Response<ReportResponse>>
    {
        public ReportQuery(List<TypeQuestions> checkBoxes)
        {
            CheckBoxes = checkBoxes;
        }

        public List<TypeQuestions> CheckBoxes { get; }

    }
}
