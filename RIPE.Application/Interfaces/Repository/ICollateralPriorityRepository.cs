using System.Threading.Tasks;

namespace RIPE.Application.Interfaces.Repository
{
    public interface ICollateralPriorityRepository
    {
        Task WriteNewUser(string login, string password);
    }
}
