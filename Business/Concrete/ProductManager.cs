using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using DataAccess.Abstarct;
using Entities.Concrete;
using Entities.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {

        private IProductDal _productDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;     
            _categoryService = categoryService;
        }

        public IResult Add(Product product)
        {   
            ValidationTool.Validate(new ProductValidator(), product);            
            _productDal.Add(product);

            
            if (product.CategoryId != null)
            {
                var _category = _categoryService.GetById((int)product.CategoryId);
                if (_category != null )
                { 
                    if(_category.Data.MinimumStockQuantity <= product.StockQuantity)
                    {
                        product.IsLive = true;
                        _productDal.Update(product);
                        return new SuccessResult(Messages.ProductAddedAndLive);
                    }
                    else
                    {
                        return new ErrorResult(Messages.ProductNotReleased);
                    }
                    
                }
                else
                {
                    return new ErrorResult(Messages.NoCategory);
                }
            }
           
          

            return new SuccessResult(Messages.ProductAdded);
        }
        public IResult Update(Product product)
        {
            var products = _productDal.Get(p => p.Id == product.Id);
            if (products == null)
            {
                return new ErrorDataResult<Product>(Messages.NoProduct);
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
        public IResult Delete(Product product)
        {
            var products = _productDal.Get(p => p.Id == product.Id);
            if (products == null)
            {
                return new ErrorDataResult<Product>(Messages.NoProduct);
            }
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }
        public IDataResult<Product> GetById(int productId)
        {
            var products = _productDal.Get(p => p.Id == productId);
            if (products == null)
            {
                return new ErrorDataResult<Product>(Messages.NoProduct);
            }

            return new SuccessDataResult<Product>(products);
        }
        public IDataResult<List<ProductDTO>> GetList()
        {
            var products = _productDal.GetList().ToList();
            var productDTOList = new List<ProductDTO>();

            foreach (var product in products)
            {
                var category = _categoryService.GetById(product.CategoryId);

                var productDTO = new ProductDTO
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    StockQuantity = product.StockQuantity,
                    CategoryId = category.Data.CategoryId,
                    Name=category.Data.Name,
                    MinimumStockQuantity = category.Data.MinimumStockQuantity,
                    IsLive = product.IsLive,

                };

                productDTOList.Add(productDTO);
            }

            return new SuccessDataResult<List<ProductDTO>>(productDTOList);
        }
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            var categories = _productDal.GetList(p => p.CategoryId == categoryId).ToList();
            if (categories.Count == 0)
            {
                return new ErrorDataResult<List<Product>>(Messages.NoCategory);
            }
            return new SuccessDataResult<List<Product>>(categories);
            
        }
        public IDataResult<List<ProductDTO>> GetProductsByStockRange(int? min, int? max)
        {
            // Tüm ürünleri getir
            var allProducts = GetList().Data;

            List<ProductDTO> products = new List<ProductDTO>();

            // Her iki kıstas da belirtilmişse filtreleme yap
            if (min != null && max != null)
            {
                foreach (var product in allProducts)
                {
                    if (product.StockQuantity >= min && product.StockQuantity <= max)
                    {
                        products.Add(product);
                    }
                }
            }
            // Sadece min kıstası belirtilmişse
            else if (min != null)
            {
                foreach (var product in allProducts)
                {
                    if (product.StockQuantity >= min)
                    {
                        products.Add(product);
                    }
                }
            }
            // Sadece max kıstası belirtilmişse
            else if (max != null)
            {
                foreach (var product in allProducts)
                {
                    if (product.StockQuantity <= max)
                    {
                        products.Add(product);
                    }
                }
            }
            else
            {
                // Herhangi bir kıstas belirtilmemişse, tüm ürünleri döndür
                products = allProducts;
            }

            return new SuccessDataResult<List<ProductDTO>>(products);
        }
        public IDataResult<List<ProductDTO>> SearchProductsByKeyword(string searchKeyword)
        {
            var allProducts = GetList().Data;

            List<ProductDTO> products = new List<ProductDTO>();
            // Arama anahtar kelimesine göre filtreleme
            if (!string.IsNullOrEmpty(searchKeyword))
            {

                foreach (var product in allProducts)
                {
                    //searchkeyword product içinde var mı
                    if (searchKeyword == product.Name || searchKeyword == product.Description || searchKeyword == product.Title)
                    {
                        products.Add(product);
                    }


                }
            }
          
            return new SuccessDataResult<List<ProductDTO>>(products);
        }

    }
}
