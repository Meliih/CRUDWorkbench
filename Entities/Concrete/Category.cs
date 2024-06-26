﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int MinimumStockQuantity { get; set; }
    }
}
