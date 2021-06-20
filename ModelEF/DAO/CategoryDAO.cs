using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelEF.Model;

namespace ModelEF.DAO
{
    public class CategoryDAO
    {
        NguyenHoangLongContext db = null;
        public CategoryDAO()
        {
            db = new NguyenHoangLongContext();
        }
        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public List<Category> ListAll()
        {
            return db.Categories.ToList();
        }
        public Category ViewDetail(int? id)
        {
            return db.Categories.Find(id);
        }

        public bool Delete(int? id)
        {
            try
            {
                var cat = db.Categories.Find(id);
                db.Categories.Remove(cat);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
