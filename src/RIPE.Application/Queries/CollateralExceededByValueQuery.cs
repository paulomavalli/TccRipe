using RIPE.Application.Responses;
using MediatR;
using System.Collections.Generic;
using RIPE.Domain.Domains.Questions;

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
