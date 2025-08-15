using Bulky.data.Irepository;
using Bulky.models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace BlukyWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly IunitOfWork _unitofwork;

        public OrderController(IunitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            List<OrderHeader> objcategoryList = _unitofwork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            // Apply filtering based on status, if provided
            switch (status)
            {
                case "pending":
                    objcategoryList = objcategoryList.Where(o => o.OrderStatus == "Not Approved").ToList();
                    break;
                case "completed":
                    objcategoryList = objcategoryList.Where(o => o.OrderStatus == "Approved").ToList();
                    break;
                case "approved":
                    objcategoryList = objcategoryList.Where(o => o.OrderStatus == "Approved").ToList();
                    break;
                case "all":
                    // No filtering needed for "all"
                    break;
                default:
                    // Handle other cases as needed
                    break;
            }

            return Json(new { data = objcategoryList });
        }

        #endregion
    }
}
