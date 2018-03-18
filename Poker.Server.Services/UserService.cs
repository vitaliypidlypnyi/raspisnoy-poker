using System.Threading.Tasks;
using Poker.Core.Services;
using Poker.Core.SqlBuilders;
using Poker.Core.SqlOperations;
using Poker.Models.TableMaps;
using Poker.Models.User;
using Poker.Server.DataAccess;
using Poker.ViewModels;

namespace Poker.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> GetProfile(int userId)
        {
            var filter = new Filter();
            filter.And(Operation.IdEquals(userId));

            var user = await _userRepository.SelectFirstAsync(new UserTableMap(), filter);
            if(user == null)
            {
                throw new System.Exception();
            }

            return new UserViewModel(user.UserName, user.BirthDate);
        }
    }
}
