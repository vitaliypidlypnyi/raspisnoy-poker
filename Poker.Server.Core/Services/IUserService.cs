using System.Threading.Tasks;
using Poker.ViewModels;

namespace Poker.Core.Services
{
    public interface IUserService
    {
        Task<UserViewModel> GetProfile(int userId);
    }
}
