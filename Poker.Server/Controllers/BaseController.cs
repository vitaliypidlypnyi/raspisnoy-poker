using System;
using System.Security.Claims;
using System.Web.Http;

namespace Poker.Server.Controllers
{
    public class BaseController : ApiController
    {
        protected int UserId => Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
