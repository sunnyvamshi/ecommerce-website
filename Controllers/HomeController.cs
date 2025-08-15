using System.Diagnostics;
using System.Security.Claims;
using Bulky.data.Irepository;
using Bulky.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bulkymodels.utilities;

namespace BlukyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IunitOfWork _unitofwork;

        public HomeController(ILogger<HomeController> logger, IunitOfWork unitofwork)
        {
            _logger = logger;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = _unitofwork.Product.GetAll(includeProperties: "Category");
            return View(productlist);
        }

        public IActionResult Details(int Id)
        {
            ShoppingCart cart = new() { 
            
                Product = _unitofwork.Product.Get(u => u.Id == Id, includeProperties: "Category"),
                Count = 1,
                ProductId = Id

            };
             return View(cart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingcart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId=claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingcart.ApplicationUserId = userId;
           
            
           ShoppingCart CartFromDb=_unitofwork.ShoppingCart.Get(u=>u.ApplicationUserId == userId && u.ProductId== shoppingcart.ProductId);
            if (CartFromDb != null) {
                shoppingcart.Id = 0;
                CartFromDb.Count += 1;
                _unitofwork.ShoppingCart.Update(CartFromDb);
                _unitofwork.Save();

            }
            else {
                shoppingcart.Id = 0;
                _unitofwork.ShoppingCart.Add(shoppingcart);
                _unitofwork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitofwork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId).Count());

            }
           
            
            return RedirectToAction("Index");
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
    }
}
