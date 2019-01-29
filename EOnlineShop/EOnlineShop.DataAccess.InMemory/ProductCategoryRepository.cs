using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using EOnlineShop.core.Models;

namespace EOnlineShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        private List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null)
            {
                productCategories = new List<ProductCategory>();
            }
        }
        public void Commit()
        {
            cache["productCategories"] = productCategories;
        }
        public void Insert(ProductCategory productCategory)
        {
            productCategories.Add(productCategory);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory tToUpdate = productCategories.Find(i => i.Id == productCategory.Id);

            if (tToUpdate != null)
            {
                tToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Categories " + " Not found");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory t = productCategories.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception("Product Categories" + " Not found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory tToDelete = productCategories.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                productCategories.Remove(tToDelete);
            }
            else
            {
                throw new Exception("Product Categories" + " Not found");
            }
        }

    }
}

