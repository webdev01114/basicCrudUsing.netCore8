using Microsoft.AspNetCore.Mvc;
using EcommerceShoppingApp.DataAccess;
using EcommerceShoppingApp.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcommerceShoppingApp.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDBContext _applicationDBContext;
        public UserController(ApplicationDBContext applicationDBContext) {
            _applicationDBContext = applicationDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var userId = HttpContext.Session.GetInt32("user_id");
            if (userId == null || userId == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var productList = await this._applicationDBContext.Products.ToListAsync();
            
            return View(productList);
        }

        [HttpGet]
        public async Task<IActionResult> CartList() {
            var cartData = await (from cart in _applicationDBContext.Cart
                                  join product
                                         in _applicationDBContext.Products on cart.cProductId equals product.pId
                                  select new Cart
                                  {
                                      cProductId = cart.cProductId,
                                      cId = cart.cId,
                                      CProductName = product.pName,
                                      CProductDesc = product.pDesc,
                                      CProductPrice = product.pPrice,
                                      CProductImage = product.pImg
                                  }).ToListAsync();
            double totalPrice = 0.00;
            int totalItems = cartData.Count;
            foreach (var cart in cartData) {
                totalPrice += (double)cart.CProductPrice;
            }
            ViewBag.TotalPrice = totalPrice;
            ViewBag.TotalItems = totalItems;
            return View(cartData);
        }


        [HttpGet]
        public async Task<IActionResult> RemoveCartItem(Cart cart)
        {
            var data = await this._applicationDBContext.Cart.Where(x => x.cId == cart.cId).FirstOrDefaultAsync();
            if (data != null)
            {
                this._applicationDBContext.Cart.Remove(data);
               await this._applicationDBContext.SaveChangesAsync();
            }
            return RedirectToAction("CartList");
        }

        
        [HttpGet]
        public async Task<IActionResult> ProcessOrder()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("user_id");
                if (userId == null || userId == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                var cartList = await this._applicationDBContext.Cart.Where(x => x.cUserId == userId).ToListAsync();
                foreach (var item in cartList)
                {
                    var data = await this._applicationDBContext.Cart.Where(x => x.cId == item.cId).FirstOrDefaultAsync();
                    if (data != null)
                    {
                        this._applicationDBContext.Cart.Remove(data);
                        await this._applicationDBContext.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex) { }
            return View("Thanku");
        }


    }

    
}
