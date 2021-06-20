using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.Model;
using PagedList;
using TestUngDung.Extensions;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        NguyenHoangLongContext db = null;
        // GET: Admin/User
        public ActionResult Index(string search, int? page)
        {
            db = new NguyenHoangLongContext();
            //= 0 la tai khoan quan tri web
            var user = db.UserAccounts.Where(x => x.UserType == 0);

            if (search != null)
            {
                user = user.Where(x => x.UserName.Contains(search));
                return View(user.ToList().ToPagedList(page ?? 1, 5)); //??  khi user < 5 thi tra ve trang 1
            }
            else
            {
                return View(user.ToList().ToPagedList(page ?? 1, 5));
            }
        }

        [HttpPost]
        public JsonResult Delete(int? id)
        {
            try
            {
                db = new NguyenHoangLongContext();

                var user = db.UserAccounts.SingleOrDefault(x => x.ID == id && x.UserType == 0);
                db.UserAccounts.Remove(user);

                db.SaveChanges(); //Luu lai

                this.AddNotification("Xóa thành công", NotificationType.SUCCESS);

                return this.Json(new { code = 200, msg = "Delete Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return this.Json(new { code = 500, msg = "Fail" + e }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
