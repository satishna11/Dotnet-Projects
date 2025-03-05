using Microsoft.AspNetCore.Mvc;

namespace ClassManagement_self.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
