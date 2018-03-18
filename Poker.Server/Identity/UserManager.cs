using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Poker.Models.User;

namespace Poker.Server.Identity
{
    public class UserManager : UserManager<User, int>
    {
        public UserManager() : base(new UserStore<User>())
        {
            PasswordHasher = new PasswordHasher();
        }

        public override async Task<User> FindAsync(string userName, string password)
        {
            var user = await Store.FindByNameAsync(userName);

            var result = PasswordHasher.VerifyHashedPassword(user.Password, password);
            if (result == PasswordVerificationResult.SuccessRehashNeeded)
            {
                return user;
            }

            return null;
        }
    }
}