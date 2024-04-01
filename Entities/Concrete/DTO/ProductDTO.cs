using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int MinimumStockQuantity { get; set; }
        public bool IsLive { get; set; } = false;
        
        }
}
