using System.ComponentModel.DataAnnotations;

namespace QLCB.Models
{
    public class MayBay
    {
        [Key]
        [StringLength(5)]
        public string MaMayBay { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiMayBay { get; set; }

        public int TamBay { get; set; }

        [StringLength(50)]
        public string? TrangThai { get; set; }  // ✅ Thêm thuộc tính này

        // Navigation property
        public ICollection<ChuyenBay>? ChuyenBays { get; set; }
    }
}
