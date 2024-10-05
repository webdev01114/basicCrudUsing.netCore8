using Microsoft.AspNetCore.Mvc;

namespace EcommerceShoppingApp.Controllers
{
    public class Productcontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
