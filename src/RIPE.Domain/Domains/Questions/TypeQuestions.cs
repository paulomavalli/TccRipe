namespace RIPE.Domain.Domains.Questions
{
    public class TypeQuestions
    {
        public TypeQuestions()
        {

        }

        public TypeQuestions(string collateralId)
        {
            CollateralId = collateralId;
            
        }

        public string CollateralId { get; set; }
    }
}
