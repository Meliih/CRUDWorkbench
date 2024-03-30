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
        private IProductService _productService;
        private ICategoryDal _categoryDal;
        private ICategoryService _categoryService;

        public ProductManager(IProductDal productDal,IProductService productService, ICategoryDal categoryDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _productService = productService;
            _categoryDal = categoryDal;
            _categoryService = categoryService;
        }

        public IResult Add(Product product)
        {   
            ValidationTool.Validate(new ProductValidator(), product);            
            _productDal.Add(product);

            
            if (product.Category != null)
            {
                Category _category = _categoryService.GetById(product.Category.CategoryId);
                if (_category != null )
                { 
                    if(_category.MinimumStockQuantity <= product.StockQuantity)
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

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.Id == productId);

        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.Category.CategoryId == categoryId).ToList());
            
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
