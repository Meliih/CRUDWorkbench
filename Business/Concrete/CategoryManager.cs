using Business.Abstract;
using Business.Constants;
using Core.Utilities.Result;
using DataAccess.Abstarct;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }
        public IResult Update(Category category)
        {
            var categories = _categoryDal.Get(p => p.CategoryId == category.CategoryId);
            if(categories == null)
            {
                return new ErrorDataResult<Category>(Messages.NoCategory);
            }
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
        
        public IResult Delete(Category category)
        {
            var categories = _categoryDal.Get(p => p.CategoryId == category.CategoryId);
            if (categories == null)
            {
                return new ErrorDataResult<Category>(Messages.NoCategory);
            }
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var categories = _categoryDal.Get(p => p.CategoryId == categoryId);
            if (categories == null)
            {
                return new ErrorDataResult<Category>(Messages.NoCategory);

            }
            return new SuccessDataResult<Category>(categories);

        }
        
        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
        }

      
    }
}
