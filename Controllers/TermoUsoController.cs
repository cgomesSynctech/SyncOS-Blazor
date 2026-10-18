using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SyncOSCore.Controllers
{
    [AllowAnonymous]
    public class TermoUsoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
