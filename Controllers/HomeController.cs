using EcommerceShoppingApp.DataAccess;
using EcommerceShoppingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace EcommerceShoppingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDBContext _applicationDbContext;
        public HomeController(ApplicationDBContext applicationDbContext)
        {
           
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userType = HttpContext.Session.GetString("user_type");
            List<int> cartProductIdList = new List<int>();
            if (userId != null) {
                if(userType != null && userType == "admin")
                {
                    return RedirectToAction("ManageProducts", "Admin");
                }else { 
                    var cartList = this._applicationDbContext.Cart.Where(x=>x.cUserId == userId).ToList();
                    foreach (var item in cartList)
                    {
                        cartProductIdList.Add(item.cProductId);
                    }
                }
                // var userType = HttpContext.Session.GetString("user_type");
                // return RedirectToAction("Index", userType == "general_user" ? "User" : "Admin");
            }
            var productList = await this._applicationDbContext.Products.ToListAsync();
           
            foreach (var item in productList)
            {
                if(cartProductIdList.Count > 0 && cartProductIdList.Contains(item.pId)) { 
                    item.isDisabledAddToCart = 1;
                }
                
            }
            return View(productList);
        }
        [HttpGet]
        public IActionResult Login()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            if (userId != null)
            {
                var userType = HttpContext.Session.GetString("user_type");
               // return RedirectToAction("Index", userType == "general_user" ? "Home" : "Admin");
                return RedirectToAction(userType == "general_user" ? "Index" : "ManageProducts", userType == "general_user" ? "Home" : "Admin");
            }
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("user_name");
                HttpContext.Session.Remove("user_type");
                HttpContext.Session.Remove("user_id");
            }
            catch (Exception ex) { }
            return RedirectToAction("Index");
        }   

        [HttpPost]
        public async Task<IActionResult> DoLogin(User user)
        {
            try
            {
                var data = await (from el in this._applicationDbContext.Users.Where(x => x.userName == user.userName && x.password == user.password) select el).FirstOrDefaultAsync();

                if (data == null) {
                    throw new Exception("Invalid login!");
                }
                var userType = data.userType;
                HttpContext.Session.SetString("user_name", data.userName);
                HttpContext.Session.SetString("user_type", data.userType);
                HttpContext.Session.SetInt32("user_id", data.userId);
                return RedirectToAction(data.userType == "general_user" ? "Index" : "ManageProducts", data.userType == "general_user" ? "Home" : "Admin");

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = ex.Message;
                
               return RedirectToAction("Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddToCart(Product product)
        {
            var userId = HttpContext.Session.GetInt32("user_id");

            if (userId == 0 || userId == null) {
                return RedirectToAction("Login");
            }

            Cart cart = new Cart()
            {
                cUserId = (int)userId,
                cProductId = product.pId
            };
            this._applicationDbContext.Cart.Add(cart);
            this._applicationDbContext.SaveChanges();
            return RedirectToAction("CartList","User");
        }
    }
}
