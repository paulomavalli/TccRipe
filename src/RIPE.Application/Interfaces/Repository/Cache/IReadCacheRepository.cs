using RIPE.Domain.Domains;
using RIPE.Domain.Domains.Questions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RIPE.Application.Interfaces.Repository.Cache
{
    public interface IReadCacheRepository
    {
        Task<IEnumerable<TypeQuestions>> GetQuestions();
        Task<IEnumerable<TypeQuestions>> GetResults();
        Task<IEnumerable<BestHabits>> GetHabits();
        Task<IEnumerable<UserDetails>> GetUser();
    }
}
