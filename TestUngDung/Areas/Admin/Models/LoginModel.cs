using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestUngDung.Areas.Admin.Models
{
    public class LoginModel
    {
		[Required(ErrorMessage = "Không được bỏ trống tên đăng nhập")]
		[Display(Name = "Tên đăng nhập")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
		[Display(Name = "Mật khẩu")]
		public string Password { get; set; }

		public Nullable<bool> Status { get; set; }
	}
}