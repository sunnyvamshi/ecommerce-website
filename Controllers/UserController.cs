using Bulky.data.data;
using Bulky.models;
using Microsoft.AspNetCore.Mvc;
using Bulky.data.Irepository.Repository;
using Bulky.data.Irepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BlukyWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objcategoryList = _db.ApplicationUsers.Include(u=>u.Company).ToList();
            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in objcategoryList)
            {
                var userRole = userRoles.FirstOrDefault(u => u.UserId == user.Id);
                if (userRole != null)
                {
                    var role = roles.FirstOrDefault(u => u.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Role = role.Name;
                    }
                }

                if (user.Company == null)
                {
                    user.Company = new Company { Name = "He is not from company" };
                }
            }

            return Json(new { data = objcategoryList });
        }

        #endregion
    }
}
