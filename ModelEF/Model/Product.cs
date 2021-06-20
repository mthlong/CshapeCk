namespace ModelEF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [StringLength(255)]
        [Display(Name ="Tên sản phẩm")]
        public string Name { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [Display(Name = "Tiêu đề")]
        public string Metatitle { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [Display(Name = "Đơn giá")]
        public decimal? UniqueCost { get; set; }


        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống ảnh sản phẩm")]
        [Display(Name = "Ảnh")]
        [Column(TypeName = "text")]
        public string Image { get; set; }


        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [Display(Name = "Mô tả")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Không được bỏ trống tên sản phẩm")]
        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        public int CategoryID { get; set; }

        [StringLength(255)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; }

        public virtual Category Category { get; set; }
    }
}
