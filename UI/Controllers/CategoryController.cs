using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;

        public CategoryController (ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetList();
            return View(categories.Data);
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoryService.Add(category);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditCategory(int categoryId,string editCategoryName,int editCategoryMinimumStock) {
            var response = new Category();
            response.CategoryId = categoryId;
            response.Name = editCategoryName;
            response.MinimumStockQuantity = editCategoryMinimumStock;
            _categoryService.Update(response);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete (Category category)
        {
            _categoryService.Delete(category);
            return RedirectToAction("Index");
        }

    }
}
