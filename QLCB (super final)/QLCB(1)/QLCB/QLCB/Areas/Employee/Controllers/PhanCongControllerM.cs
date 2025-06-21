using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Employee")]
    public class PhanCongControllerM : Controller
    {
        private readonly ApplicationDBContext _context;

        public PhanCongControllerM(ApplicationDBContext context)
        {
            _context = context;
        }
        // GET: Admin/PhanCong
        public async Task<IActionResult> Index(string searchString)
        {
            var query = _context.PhanCongs
                .Include(p => p.NhanVien) // đảm bảo có join bảng NhanVien
                .Include(p => p.ChuyenBay)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.NhanVien.HoTen.Contains(searchString));
            }

            var list = await query.ToListAsync();
            ViewBag.SearchString = searchString; // để giữ lại giá trị khi hiển thị lại
            return View(list);
        }

    }
}
