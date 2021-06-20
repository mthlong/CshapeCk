using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ModelEF.DAO;
using TestUngDung.Areas.Admin.Models;
using TestUngDung.Common;
using TestUngDung.Extensions;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
		{
			var session = Session[Constants.USER_SEESION]; //Tạo biến session kiểm tra xem có tồn tại hay không
			if (session != null)
			{
				return RedirectToAction("Index", "home");
				//Trả về trang chủ index nếu đã tồn tại session (đã đăng nhập thành công rồi nhưng lại trỏ url về lại trang login ấy)
			}
			return View();
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			Session.Abandon();
			return RedirectToAction("Index", "login");
		}
		[HttpPost]
		[ValidateAntiForgeryToken] //Sử dụng cái này để chống giả mạo request 
		public ActionResult Index(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var dao = new UserDAO(); //Khởi tạo constructor User

				var result = dao.Login(model.UserName, model.Password); //Tạo biến result để kiểm tra đăng nhập
				if (result == 1) //Nếu tên đăng nhập và mật khẩu đúng
				{
					//tạo biến user để lấy thông tin cần thiết, sau đó truyền vào session
					var user = dao.getUserByID(model.UserName);

					var userSession = new UserLogin
					{
						UserName = user.UserName,
						UserID = user.ID,
					};
					string fullName = user.UserName;
					Session.Add(Constants.USER_SEESION, userSession); //Session này để kiểm tra khi vào trang chủ index home
					Session["FullName"] = fullName.ToString(); //Session này dùng cho lời chào ở trang index home
					return RedirectToAction("Index", "home"); //Sau đó quay về trang index home
				}
				else if (result == 0) //Nếu tài khoản không tồn tại
				{
					this.AddNotification("Tài khoản không tồn tại", NotificationType.ERROR);
				}
				else if (result == -1)
				{
					this.AddNotification("Tài khoản đang bị khóa", NotificationType.ERROR);
				}
				else if (result == -2)
				{
					this.AddNotification("Sai mật khẩu", NotificationType.ERROR);
				}
				else
				{
					this.AddNotification("Sai tên đăng nhập", NotificationType.ERROR);
				}
			}
			return View("Index"); //Nếu modelstate == false thì ở yên tại chỗ
		}
	}
}