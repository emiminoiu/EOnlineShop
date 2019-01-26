﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EOnlineShop.core.Models
{
    public class Product
    {
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }

        [Range(0,10000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
