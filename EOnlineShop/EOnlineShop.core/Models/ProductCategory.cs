﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOnlineShop.core.Models
{
    public class ProductCategory : BaseEntity
    {
        public string Category { get; set; }

        public virtual List<Brand> CategoryBrands { get; set; }

    }
}
