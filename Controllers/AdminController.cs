using EcommerceShoppingApp.DataAccess;
using EcommerceShoppingApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceShoppingApp.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDBContext _applicationDBContext;
        private IWebHostEnvironment _webHostEnvironment;
        public AdminController(ApplicationDBContext applicationDBContext, IWebHostEnvironment webHostEnvironment) {
            _applicationDBContext = applicationDBContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> ManageProducts()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userType = HttpContext.Session.GetString("user_type");
            if (userId == null || userId == 0 || userType == null
                || (userType != null && userType != "admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            var productList = await this._applicationDBContext.Products.ToListAsync();
            return View("Index", productList);
           // return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userType = HttpContext.Session.GetString("user_type");
            if (userId == null || userId == 0 || userType == null
                || (userType != null && userType != "admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

         [HttpPost]
         public async Task<IActionResult> AddProduct(Product product)
         {
            var userId = HttpContext.Session.GetInt32("user_id");
            var userType = HttpContext.Session.GetString("user_type");
            if (userId == null || userId == 0 || userType == null
                || (userType != null && userType != "admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            try
            {
                product.pImg = UploadFile(product);
                if(product.pId != null && product.pId != 0)
                {
                    var dataexist = await this._applicationDBContext.Products.Where(x => x.pId == product.pId).FirstOrDefaultAsync();
                    if (dataexist != null)
                    {

                        dataexist.pName = product.pName;
                        dataexist.pDesc = product.pDesc;
                        dataexist.pPrice = product.pPrice;
                        dataexist.pImg = product.pImg;
                        await this._applicationDBContext.SaveChangesAsync();
                       // return Ok("data suceesfully updated");
                    }
                }
                else {
                    await this._applicationDBContext.Products.AddAsync(product);
                    await this._applicationDBContext.SaveChangesAsync();
                }
                return RedirectToAction("ManageProducts");
            }
            catch (Exception ex) {
                return RedirectToAction("ManageProducts");
            }
          }


        private string UploadFile(Product product)
        {
            string FileName = null;

            if (product.Pic != null)
            {
                // FileName = product.Pic.FileName;
                FileName = Guid.NewGuid().ToString() + Path.GetExtension(product.Pic.FileName);
                string Uploaddir = Path.Combine(_webHostEnvironment.WebRootPath, "products");

                string filepath = Path.Combine(Uploaddir, FileName);
                var filestream = new FileStream(filepath, FileMode.Create);
                product.Pic.CopyTo(filestream);
                if (product.pImg != null)
                {
                    try
                    {
                        string ExitingFile = Path.Combine(_webHostEnvironment.WebRootPath, "products", product.pImg);
                        var file = new FileInfo(ExitingFile);
                        if (file.Exists)
                        {
                            file.Delete();
                        }
                    }
                    catch (Exception ex) { }
                }

            }
            else if(product.pImg != null) 
            {
                    FileName = product.pImg;
            }

            return FileName;
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(Product product)
        {
            var productDetails = await this._applicationDBContext.Products.Where(x=>x.pId == product.pId).FirstOrDefaultAsync();
            ViewBag.ProductDetails = productDetails;
            return View("AddProduct");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(Product product)
        {
            var data = await this._applicationDBContext.Products.Where(x => x.pId == product.pId).FirstOrDefaultAsync();
            if (data != null)
            {
                try
                {
                    string ExitingFile = Path.Combine(_webHostEnvironment.WebRootPath, "products", data.pImg);
                    var file = new FileInfo(ExitingFile);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                catch (Exception ex) { }
                this._applicationDBContext.Products.Remove(data);
                await this._applicationDBContext.SaveChangesAsync();
            }
         
            return RedirectToAction("ManageProducts");
        }
    }

    
}
