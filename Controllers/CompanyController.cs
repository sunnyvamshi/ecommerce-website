using Bulky.data.data;
using Bulky.models;
using Microsoft.AspNetCore.Mvc;
using Bulky.data.Irepository.Repository;
using Bulky.data.Irepository;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlukyWeb.Controllers
{
    public class CompanyController : Controller
    {
		        
        //private readonly AppDbContext _context;
        private readonly IunitOfWork _unitofwork;
        public CompanyController(IunitOfWork iunitOfWork)
        {
			//_context = db;
			_unitofwork = iunitOfWork;
			

        }
        public IActionResult Index()
        {

			//List<Category> objcategoryList = _context.categories.ToList();
			List<Company> objCompanyList = _unitofwork.Company.GetAll().ToList();
           
            return View(objCompanyList);
        }
        public IActionResult Create()
        {
			return View(new Company());
        }
		
		public IActionResult Editss(int Id)
		{
			var companyobj = _unitofwork.Company.Get(u =>u.Id==Id);
			
			return View(companyobj);

           




        }

		// HTTP POST action to handle the edit form submission
		[HttpPost]
		
		public IActionResult Editss(Company model)
		{
            

           
               
          
			_unitofwork.Company.Update(model);
			
		   _unitofwork.Save();
			TempData["success"] = "category edited succesfully";

					// Redirect to the index action (list of categories) upon successful update
		return RedirectToAction("Index");
				
				
			

			
			
		}
		[HttpPost]
		public IActionResult Create(Company obj)
		{
			
				
           
                _unitofwork.Company.Add(obj);
			
				_unitofwork.Save();
				//_context.SaveChanges();
				TempData["success"] = "category create succesfully";
				return RedirectToAction("Index");
		}
		public IActionResult Delete(int Id)
		{
			var categoryToDelete =_unitofwork.Company.Get(u => u.Id==Id);
            if (categoryToDelete == null)
			{
				return NotFound();
			}
			

			return View(categoryToDelete);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int Id)
		{
			var categoryToDelete =_unitofwork.Company.Get(u => u.Id == Id);
            if (categoryToDelete == null)
			{
				return NotFound();
			}

			_unitofwork.Company.Remove(categoryToDelete);
			_unitofwork.Save();
			//

			return RedirectToAction("Index");
		}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objcategoryList = _unitofwork.Company.GetAll().ToList();
            return Json(new { data = objcategoryList });
        }
        #endregion


    }
}
