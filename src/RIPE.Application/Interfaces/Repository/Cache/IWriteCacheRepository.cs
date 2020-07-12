using RIPE.Domain.Domains;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIPE.Application.Interfaces.Repository.Cache
{
    public interface IWriteCacheRepository
    {
        Task SetResultQuestions(IEnumerable<TypeQuestions> policyResult);
        Task SetLogin(UserDetails newUser);
        Task SetReport(IEnumerable<TypeQuestions> policyResult);
    }
}
