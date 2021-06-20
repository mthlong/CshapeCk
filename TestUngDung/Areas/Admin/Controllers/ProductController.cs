using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Models;
using TestUngDung.Extensions;
using PagedList.Mvc;
using PagedList;


namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
		NguyenHoangLongContext db = null;
		// GET: Admin/Product
		public ActionResult Index(string search, int? page)
		{
			db = new NguyenHoangLongContext();
			IEnumerable<ProductViewModel> result = (from p in db.Products
													join c in db.Categories on p.CategoryID equals c.ID
													select new ProductViewModel
													{
														ID = p.ID,
														Name = p.Name,
														UniqueCost = p.UniqueCost,
														Quantity = p.Quantity,
														CategoryName = c.Name,
														Status = p.Status
													}).OrderBy(x => x.Quantity).ThenByDescending(x => x.UniqueCost);


			if (search != null)
			{
				result = result.Where(x => x.Name.Contains(search) || x.CategoryName.Contains(search));
				return View(result.ToList().ToPagedList(page ?? 1, 5));
			}
			else
			{
				return View(result.ToList().ToPagedList(page ?? 1, 5));
			}
		}

		// GET: Admin/Category/Details/5
		public ActionResult Details(int id)
		{
			var dao = new ProductDAO();
			var product = dao.ViewDetail(id);
			SetViewBag(product.CategoryID);

			return View(product);
		}

		// GET: Admin/Product/Create
		[HttpGet]
		public ActionResult Create()
		{
			SetViewBag();
			return View();
		}
		//Hiển thị dropdownlist loại sách
		public void SetViewBag(int? idselected = null)
		{
			var dao = new CategoryDAO();
			ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", idselected);
		}
		// POST: product/create
		//Thêm sản phẩm, xử lý thêm ở phần POST
		[HttpPost]
		public ActionResult Create(Product model, HttpPostedFileBase imageSave)
		{
			try
			{
				// TODO: Add insert logic here
				var db = new NguyenHoangLongContext();

				SetViewBag();

				string filename = Path.GetFileName(imageSave.FileName);
				string _filename = DateTime.Now.ToString("ddMMyyyy") + filename;
				string extension = Path.GetExtension(imageSave.FileName);
				string path = Path.Combine(Server.MapPath("~/Assets/Admin/img"), _filename);
				model.Image = "/Assets/Admin/img/" + _filename;


				if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
				{
					//Khởi tạo product để thêm trước
					Product product = new Product
					{
						Name = model.Name,
						Description = model.Description,
						Quantity = model.Quantity,
						UniqueCost = model.UniqueCost,
						Author = model.Author,
						Metatitle = model.Metatitle,
						CategoryID = model.CategoryID,
						Image = model.Image,
						Status = model.Status
					};

					db.Products.Add(product);
					if (db.SaveChanges() > 0)
					{
						imageSave.SaveAs(path);
					}
					this.AddNotification("Thêm sản phẩm thành công", NotificationType.SUCCESS);
					return RedirectToAction("Index", "product");
				}
				else
				{
					this.AddNotification("Không đúng định dạng ảnh", NotificationType.ERROR);
					return View();
				}
			}
			catch (Exception)
			{
				return View();
			}
		}

		// GET: Admin/product/Edit/5
		public ActionResult Edit(int? id)
		{
			db = new NguyenHoangLongContext();
			try
			{
				var product = db.Products.SingleOrDefault(x => x.ID == id);
				SetViewBag(product.CategoryID);
				TempData["imgPath"] = product.Image;
				Session["imgPath"] = product.Image;

				if (product == null)
				{
					return HttpNotFound();
				}

				return View(product);
			}
			catch (Exception)
			{
				return View();
			}
		}

		// POST: Admin/Category/Edit/5
		[HttpPost]
		public ActionResult Edit(Product model, HttpPostedFileBase imageSave)
		{
			try
			{
				// TODO: Add insert logic here
				string path = "";
				string oldImgPath = "";
				var db = new NguyenHoangLongContext();
				var product = db.Products.SingleOrDefault(x => x.ID == model.ID);
				SetViewBag();
				if (imageSave == null)
				{
					model.Image = product.Image;
				}
				else
				{
					string filename = Path.GetFileName(imageSave.FileName);
					string _filename = DateTime.Now.ToString("ddMMyyyy") + filename;
					string extension = Path.GetExtension(imageSave.FileName);
					path = Path.Combine(Server.MapPath("~/Assets/Admin/img"), _filename);
					model.Image = "/Assets/Admin/img/" + _filename;
					oldImgPath = Request.MapPath(product.Image.ToString());
					imageSave.SaveAs(path);
				}
				product.Name = model.Name;
				product.Description = model.Description;
				product.Metatitle = model.Metatitle;
				product.Quantity = model.Quantity;
				product.UniqueCost = model.UniqueCost;
				product.Author = model.Author;
				product.CategoryID = model.CategoryID;
				product.Status = model.Status;
				product.Image = model.Image;

				if (db.SaveChanges() > 0)
				{

					db.SaveChanges();//Lưu lại
					if (System.IO.File.Exists(oldImgPath))
					{
						System.IO.File.Delete(oldImgPath);
					}
					this.AddNotification("Sửa thành công", NotificationType.SUCCESS);
					return RedirectToAction("Index");
				}
				return View();
			}
			catch (Exception)
			{
				return View();
			}
		}

		// POST: Admin/Category/Delete/5
		[HttpPost]
		public JsonResult Delete(int? id)
		{
			try
			{
				db = new NguyenHoangLongContext();
				var product = db.Products.Find(id);
				string currentImg = Request.MapPath(product.Image);
				db.Products.Remove(product);
				if (db.SaveChanges() > 0)
				{
					if (System.IO.File.Exists(currentImg))
					{
						System.IO.File.Delete(currentImg);
					}
				}
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
