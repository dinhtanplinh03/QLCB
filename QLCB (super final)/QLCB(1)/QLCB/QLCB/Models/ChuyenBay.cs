using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QLCB.Models
{
    public class ChuyenBay : IValidatableObject
    {
        [Key]
        [Required(ErrorMessage = "Mã chuyến bay không được để trống")]
        public string MaChuyenBay { get; set; }

        [Required(ErrorMessage = "Tên chuyến bay không được để trống")]
        public string TenChuyenBay { get; set; }

        [Required(ErrorMessage = "Thời gian khởi hành không được để trống")]
        [Display(Name = "Thời gian khởi hành")]
        public DateTime ThoiGianKhoiHanh { get; set; }

        [Required(ErrorMessage = "Thời gian đến không được để trống")]
        [Display(Name = "Thời gian đến")]
        public DateTime ThoiGianDen { get; set; }

        [Required(ErrorMessage = "Số ghế không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số ghế không được âm")]
        public int SoGhe { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn máy bay")]
        public string MaMayBay { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sân bay đi")]
        public string MaSanBayDi { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sân bay đến")]
        public string MaSanBayDen { get; set; }

        [StringLength(20)]
        public string? TrangThai { get; set; } // VD: "Đúng giờ", "Delay", "Hủy"

        // Lưu mô tả thời tiết (nếu cần)
        [StringLength(100)]
        public string? ThoiTiet { get; set; }

        [Required(ErrorMessage = "Giá vé không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá vé phải lớn hơn hoặc bằng 0")]
        public decimal GiaVe { get; set; }

        // Navigation properties
        [ValidateNever]
        public MayBay MayBay { get; set; }

        [ValidateNever]
        [ForeignKey("MaSanBayDi")]
        public SanBay SanBayDi { get; set; }

        [ValidateNever]
        [ForeignKey("MaSanBayDen")]
        public SanBay SanBayDen { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ThoiGianKhoiHanh >= ThoiGianDen)
            {
                yield return new ValidationResult(
                    "Thời gian khởi hành không thể bằng hoặc lớn hơn thời gian đến.",
                    new[] { nameof(ThoiGianKhoiHanh) });
            }

            if (MaSanBayDi == MaSanBayDen)
            {
                yield return new ValidationResult(
                    "Sân bay đi không được trùng với sân bay đến.",
                    new[] { nameof(MaSanBayDen) });
            }
        }
    }
}
