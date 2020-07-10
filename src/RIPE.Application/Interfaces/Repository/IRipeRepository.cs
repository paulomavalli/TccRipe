using RIPE.Domain.Domains.Feedback;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIPE.Application.Interfaces.Repository
{
    public interface IRipeRepository
    {
        Task<bool> WriteNewUser(string login, string password);
        Task<bool> WriteFeedback(Feedback feedback);

        Task<IEnumerable<TypeQuestions>> GetQuestions();
        Task<IEnumerable<Domain.Domains.Collateral>> GetCollateralPerSecurityId(string securityId, string customerId);
    }
}
