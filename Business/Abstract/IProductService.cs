using Core.Utilities.Result;
using Entities.Concrete;
using Entities.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<Product> GetById(int product);
        IDataResult<List<ProductDTO>> GetList();
        IDataResult<List<Product>> GetListByCategory(int categoryId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);
        IDataResult<List<ProductDTO>> GetProductsByStockRange(int? min, int? max);
        IDataResult<List<ProductDTO>> SearchProductsByKeyword(string searchKeyword);

    }
}
