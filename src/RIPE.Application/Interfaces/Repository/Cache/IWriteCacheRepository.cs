using RIPE.Domain.Domains;
using RIPE.Domain.Domains.Feedback;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIPE.Application.Interfaces.Repository.Cache
{
    public interface IWriteCacheRepository
    {
        Task SetResultQuestions(IEnumerable<TypeQuestions> policyResult);
        Task SetLogin(IEnumerable<UserDetails> newUser);
        Task SetReport(IEnumerable<TypeQuestions> policyResult);
        Task SetFeedBack(IEnumerable<Feedback> feedback);
    }
}
