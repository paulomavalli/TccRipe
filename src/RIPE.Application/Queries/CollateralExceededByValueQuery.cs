using MediatR;
using RIPE.Application.Responses;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;

namespace RIPE.Application.Queries
{
    public class CollateralExceededByValueQuery : IRequest<Response<QuestionsResponse>>
    {

        public CollateralExceededByValueQuery(List<TypeQuestions> checkBoxes)
        {
            CheckBoxes = checkBoxes;
        }

        public List<TypeQuestions> CheckBoxes { get; }


    }
}
