using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestUngDung.Areas.Admin.Models
{
    public class ProductViewModel
    {
		public int ID { get; set; }

		[Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
		[Display(Name = "Tên sản phẩm")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Không được bỏ trống đơn giá")]
		[Display(Name = "Đơn giá")]
		public Nullable<decimal> UniqueCost { get; set; }

		[Required(ErrorMessage = "Không được bỏ trống số lượng")]
		[Display(Name = "Số lượng")]
		public Nullable<int> Quantity { get; set; }

		[Required(ErrorMessage = "Không được bỏ trống ảnh sản phẩm")]
		[Display(Name = "Ảnh sản phẩm")]
		public string Image { get; set; }

		[Display(Name = "Mô tả sản phẩm")]
		public string Description { get; set; }

		[Display(Name = "Trạng thái")]
		public bool Status { get; set; }


		[Required(ErrorMessage = "Không được bỏ trống tên loại hàng")]
		[Display(Name = "Tên loại sách")]
		public int CategoryID { get; set; }

		[Display(Name = "Tác giả")]
		public string Author { get; set; }

		[Required(ErrorMessage = "Không được bỏ trống tên loại hàng")]
		[Display(Name = "Tên loại hàng")]
		public string CategoryName { get; set; }
	}
}
