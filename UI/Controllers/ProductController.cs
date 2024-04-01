
using Business.Abstract;
using Entities.Concrete;
using Entities.Concrete.DTO;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;

        public ProductController (IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var response = _productService.GetList();
            var categories = _categoryService.GetList();

            ViewBag.Categories = categories.Data;

            return View(response.Data);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            try
            {
                _productService.Delete(product);
                return RedirectToAction("Index"); // Silme işleminden sonra ilgili sayfaya yönlendir
            }
            catch (Exception ex)
            {
                // Hata durumunda bir işlem yapılabilir, örneğin hata mesajını loglamak veya kullanıcıya göstermek
                ViewBag.ErrorMessage = "Silme işlemi sırasında bir hata oluştu: " + ex.Message;
                return View("Error"); // Hata sayfasına yönlendir
            }
        }

       
        [HttpPost]
        public IActionResult Edit(ProductDTO product)
        {
            Console.WriteLine(product.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SearchProducts(string searchKeyword)
        {
            // Arama anahtar kelimesine göre ürünleri filtreleme işlemi
            var filteredProducts = _productService.SearchProductsByKeyword(searchKeyword);
            return View("index",filteredProducts.Data);
        }
    }
}
