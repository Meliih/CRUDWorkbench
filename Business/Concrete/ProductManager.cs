using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Result;
using DataAccess.Abstarct;
using Entities.Concrete;
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

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        public IResult Add(Product product)
        {   
            ValidationTool.Validate(new ProductValidator(), product);            
            _productDal.Add(product);

            
            if (product.Category != null)
            {
                var _category = _categoryService.GetById(product.Category.CategoryId);
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

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            var categories = _productDal.GetList(p => p.Category.CategoryId == categoryId).ToList();
            if (categories.Count == 0)
            {
                return new ErrorDataResult<List<Product>>(Messages.NoCategory);
            }
            return new SuccessDataResult<List<Product>>(categories);
            
        }
    }
}
