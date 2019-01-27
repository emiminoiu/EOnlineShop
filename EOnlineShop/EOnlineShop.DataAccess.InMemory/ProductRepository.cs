using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using EOnlineShop.core.Models;
namespace EOnlineShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        private List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product product)
        {
            products.Add(product);
        }

        public void Update(Product product)
        {
            Product tToUpdate = products.Find(i => i.Id == product.Id);

            if (tToUpdate != null)
            {
                tToUpdate = product;
            }
            else
            {
                throw new Exception("Product" + " Not found");
            }
        }

        public Product Find(string Id)
        {
            Product t = products.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception("Product" + " Not found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product tToDelete = products.Find(i => i.Id == Id);

            if (tToDelete != null)
            {
                products.Remove(tToDelete);
            }
            else
            {
                throw new Exception("Product" + " Not found");
            }
        }

    }
}
