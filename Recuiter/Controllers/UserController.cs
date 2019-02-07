using Recuiter.CustomAuthentication;
using System.Web.Mvc;

namespace Recruiter.Controllers
{
    [CustomAuthorize(Roles = "User")]
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
    }
}