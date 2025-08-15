using Bulky.data.data;
using Bulky.models;
using Microsoft.AspNetCore.Mvc;
using Bulky.data.Irepository.Repository;
using Bulky.data.Irepository;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlukyWeb.Controllers
{
    public class ProductController : Controller
    {
		private readonly IWebHostEnvironment _environment;	
        
        //private readonly AppDbContext _context;
        private readonly IunitOfWork _unitofwork;
        public ProductController(IunitOfWork iunitOfWork,IWebHostEnvironment webHostEnvironment)
        {
			//_context = db;
			_unitofwork = iunitOfWork;
			_environment=webHostEnvironment;

        }
        public IActionResult Index()
        {
			
			//List<Category> objcategoryList = _context.categories.ToList();
			List<Product> objcategoryList = _unitofwork.Product.GetAll(includeProperties:"Category").ToList();
           
            return View(objcategoryList);
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.GetAll().ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
			ViewBag.CategoryList = CategoryList;	
            return View();
        }
		
		public IActionResult Editss(int id)
		{
            IEnumerable<SelectListItem> CategoryList = _unitofwork.Category.GetAll().ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            ViewBag.CategoryList = CategoryList;

            //var categoryfromdbs = _context.categories.Find(id);
            var categoryFromDb=_unitofwork.Product.Get(u=>u.Id == id);

			// Check if the category exists
			//if (categoryfromdb == null)
			//{
				
			//	return NotFound();
			//}

			
			return View(categoryFromDb);
		}

		// HTTP POST action to handle the edit form submission
		[HttpPost]
		
		public IActionResult Editss(Product model, IFormFile? file)
		{
            //string wwwRooTPath = _environment.WebRootPath;
            //if (file != null)
            //{
            //    string fileName = Guid.NewGuid().ToString();
            //    string productPath = Path.Combine(wwwRooTPath, @"images\product");
            //    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
            //    {
            //        file.CopyTo(fileStream);
            //    }
            //    model.ImageUrl = "/images/product/" + fileName;

            //}

            string wwwRootPath = _environment.WebRootPath;
            if (file != null && file.Length > 0)
            {
                // Generate a unique file name with .jpeg extension
                string fileName = Guid.NewGuid().ToString() + ".jpeg";

                // Construct the full path where the file will be saved
                string productPath = Path.Combine(wwwRootPath, "images", "product");
                string filePath = Path.Combine(productPath, fileName);

                // Ensure the product directory exists; create it if it doesn't
                Directory.CreateDirectory(productPath);

                // Copy the uploaded file to the specified path
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                // Set the model's ImageUrl property to the relative path of the saved image
                model.ImageUrl = "/images/product/" + fileName;
            }
			_unitofwork.Product.Update(model);
			
					_unitofwork.Save();
				TempData["success"] = "category edited succesfully";

					// Redirect to the index action (list of categories) upon successful update
					return RedirectToAction("Index");
				
				
			

			
			
		}
		[HttpPost]
		public IActionResult Create(Product obj,IFormFile? file)
		{
			
				string wwwRooTPath = _environment.WebRootPath;
				if (file != null) {
					string fileName=Guid.NewGuid().ToString();
					string productPath = Path.Combine(wwwRooTPath, @"images\product");
					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
                obj.ImageUrl = "/images/product/" + fileName;

            }
                //_context.categories.Add(obj);
                _unitofwork.Product.Add(obj);
			
				_unitofwork.Save();
				//_context.SaveChanges();
				TempData["success"] = "category create succesfully";
				return RedirectToAction("Index");
		}
		public IActionResult Delete(int Id)
		{
			var categoryToDelete =_unitofwork.Product.Get(u => u.Id==Id);
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
			var categoryToDelete =_unitofwork.Product.Get(u => u.Id == Id);
            if (categoryToDelete == null)
			{
				return NotFound();
			}

			_unitofwork.Product.Remove(categoryToDelete);
			_unitofwork.Save();
			//

			return RedirectToAction("Index");
		}
        #region API CALLS
        [HttpGet]
		public IActionResult GetAll()
		{
            List<Product> objcategoryList = _unitofwork.Product.GetAll(includeProperties: "Category").ToList();
			return Json(new { data = objcategoryList });
        }
		#endregion


	}
}
