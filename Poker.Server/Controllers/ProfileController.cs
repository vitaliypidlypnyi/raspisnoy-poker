using System.Threading.Tasks;
using System.Web.Http;
using Poker.Core.Services;
using Poker.ViewModels;

namespace Poker.Server.Controllers
{
    [RoutePrefix("api/profile")]
    [Authorize]
    public sealed class ProfileController : BaseController
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("my")]
        public async Task<UserViewModel> MyProfile()
        {
            return await _userService.GetProfile(UserId);
        }
    }
}
