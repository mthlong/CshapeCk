using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.Model;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        NguyenHoangLongContext context = new NguyenHoangLongContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            int stn = context.Products.Where(x => x.CategoryID == 1).Count();
            int svh = context.Products.Where(x => x.CategoryID == 2).Count();
            int skns = context.Products.Where(x => x.CategoryID == 3).Count();
            int skt = context.Products.Where(x => x.CategoryID == 4).Count();
            Ratio obj = new Ratio();
            obj.stn = stn;
            obj.svh = svh;
            obj.skns = skns;
            obj.skt = skt;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int stn { get; set; }
            public int svh { get; set; }
            public int skns { get; set; }
            public int skt { get; set; }
        }
    }
}