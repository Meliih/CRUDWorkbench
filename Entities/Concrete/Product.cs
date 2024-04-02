using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Title {get; set; }
        public string ?Description { get; set; }
        public int StockQuantity { get; set; }
      
        public int CategoryId { get; set; }

        public bool IsLive { get; set; } = false; 


        public Product(Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Description = product.Description;
            StockQuantity = product.StockQuantity;
            CategoryId = product.CategoryId;
            IsLive = product.IsLive;
        }

        public Product()
        {

        }
    }
}
