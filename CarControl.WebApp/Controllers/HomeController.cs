using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarControl.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region POST
        public IActionResult Index()
        {
            return View();
        }
        #endregion       
    }
}
