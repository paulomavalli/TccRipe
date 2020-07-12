using System.Collections.Generic;

namespace RIPE.Domain.Domains.Questions
{
    public class TypeQuestions
    {
        public TypeQuestions()
        {

        }

        public TypeQuestions(string typeId, string typeDescription, List<string> questionDescription)
        {
            TypeId = typeId;
            TypeDescription = typeDescription;
            QuestionDescription = questionDescription;
        }

        public string TypeId { get; set; }
        public string TypeDescription { get; set; }
        public List<string> QuestionDescription { get; set; }
    }
}
