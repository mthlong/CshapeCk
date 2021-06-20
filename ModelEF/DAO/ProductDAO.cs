using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelEF.Model;

namespace ModelEF.DAO
{
    public class ProductDAO
    {
        NguyenHoangLongContext db = null;
        public ProductDAO()
        {
            db = new NguyenHoangLongContext();
        }
        public long Insert(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }
        public List<Product> ListAll()
        {
            return db.Products.ToList();
        }
        public Product ViewDetail(int? id)
        {
            var result = (from p in db.Products
                          join c in db.Categories on p.CategoryID equals c.ID
                          where p.ID == id
                          select new
                          {

                          }).FirstOrDefault();
            return db.Products.Find(id);
        }

        public List<Product> ListRelatedProducts(int? id)
        {
            var product = db.Products.Find(id);
            return db.Products.Where(x => x.ID != id && x.CategoryID == product.CategoryID).ToList();
        }
    }
}
