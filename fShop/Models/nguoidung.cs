namespace fShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nguoidung")]
    public partial class nguoidung
    {
        [Key]
        [DisplayName("ID")]
        public int manguoidung { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Họ tên")]
        public string hoten { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Mật khẩu")]
        public string matkhau { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string email { get; set; }
    }
}
