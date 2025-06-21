using Microsoft.AspNetCore.Mvc;
using QLCB.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using QLCB.Models.ViewModels;

namespace QLCB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string diemDi, string diemDen, DateTime? ngayDi)
        {
            var query = from cb in _context.ChuyenBays
                join sbDi in _context.SanBays on cb.MaSanBayDi equals sbDi.MaSanBay
                join sbDen in _context.SanBays on cb.MaSanBayDen equals sbDen.MaSanBay
                select new
                {
                    cb,
                    TenSanBayDi = sbDi.TenSanBay,
                    TenSanBayDen = sbDen.TenSanBay
                };

            /* Lọc theo điểm đi */
            if (!string.IsNullOrEmpty(diemDi))
            {
                query = query.Where(x => x.TenSanBayDi.Contains(diemDi));
            }

            /* Lọc theo điểm đến */
            if (!string.IsNullOrEmpty(diemDen))
            {
                query = query.Where(x => x.TenSanBayDen.Contains(diemDen));
            }

            /* Lọc theo ngày đi */
            if (ngayDi.HasValue)
            {
                var ngay = ngayDi.Value.Date;
                query = query.Where(x => x.cb.ThoiGianKhoiHanh.Date == ngay);
            }

            var danhSach = query.Select(x => new ChuyenBayViewModel
            {
                MaChuyenBay = x.cb.MaChuyenBay,
                TenChuyenBay = x.cb.TenChuyenBay,
                ThoiGianKhoiHanh = x.cb.ThoiGianKhoiHanh,
                ThoiGianDen = x.cb.ThoiGianDen,
                SoGhe = x.cb.SoGhe,
                TenSanBayDi = x.TenSanBayDi,
                TenSanBayDen = x.TenSanBayDen,
                VeBays = _context.VeBays
                    .Where(v => v.MaChuyenBay == x.cb.MaChuyenBay)
                    .Select(v => new VeBayViewModel
                    {
                        MaVeBay = v.MaVeBay,
                        NgayDatVe = v.NgayDatVe,
                        GiaVe = v.GiaVe,
                        MaHanhKhach = v.MaHanhKhach
                    }).ToList()
            }).ToList();

            ViewData["diemDi"] = diemDi;
            ViewData["diemDen"] = diemDen;
            ViewData["ngayDi"] = ngayDi;
            return View(danhSach);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DatVe(string maChuyenBay)
        {
            var ve = _context.VeBays.FirstOrDefault(v => v.MaChuyenBay == maChuyenBay && (v.MaHanhKhach == null || v.MaHanhKhach == ""));
            if (ve == null)
            {
                var veList = _context.VeBays.Where(v => v.MaChuyenBay == maChuyenBay).ToList();
                TempData["Error"] = $"Tổng vé: {veList.Count}, Vé trống: {veList.Count(v => v.MaHanhKhach == null || v.MaHanhKhach == "")}, DB={_context.Database.GetDbConnection().Database}";
                return RedirectToAction("Index");
            }
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Không xác định được người dùng.";
                return RedirectToAction("Index");
            }
            ve.MaHanhKhach = userId;
            ve.NgayDatVe = DateTime.Now;
            _context.SaveChanges();
            TempData["Success"] = "Đặt vé thành công!";
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult VeDaDat()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Không xác định được người dùng.";
                return RedirectToAction("Index", "Home");
            }

            var veBays = _context.VeBays
                .Where(v => v.MaHanhKhach == userId)
                .ToList();

            return View(veBays);
        }

        [Authorize]
        [HttpGet]
        public IActionResult ThanhToan(string maVeBay)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Không xác định được người dùng.";
                return RedirectToAction("VeDaDat");
            }
            var ve = _context.VeBays.FirstOrDefault(v => v.MaVeBay == maVeBay && v.MaHanhKhach == userId);
            if (ve == null)
            {
                TempData["Error"] = "Không tìm thấy vé để thanh toán.";
                return RedirectToAction("VeDaDat");
            }
            return View("ChonPhuongThucThanhToan", ve);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToan(string maVeBay, string paymentMethod)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Không xác định được người dùng.";
                return RedirectToAction("VeDaDat");
            }
            var ve = _context.VeBays.FirstOrDefault(v => v.MaVeBay == maVeBay && v.MaHanhKhach == userId);
            if (ve == null)
            {
                TempData["Error"] = "Không tìm thấy vé để thanh toán.";
                return RedirectToAction("VeDaDat");
            }
            ve.IsPaid = true;
            ve.PaymentMethod = paymentMethod;
            _context.SaveChanges();
            TempData["Success"] = $"Thanh toán vé {ve.MaVeBay} thành công bằng hình thức: {paymentMethod}!";
            return RedirectToAction("VeDaDat");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ThanhToanTatCa()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Không xác định được người dùng.";
                return RedirectToAction("VeDaDat");
            }
            var veChuaThanhToan = _context.VeBays.Where(v => v.MaHanhKhach == userId && !v.IsPaid).ToList();
            if (!veChuaThanhToan.Any())
            {
                TempData["Error"] = "Không có vé nào cần thanh toán.";
                return RedirectToAction("VeDaDat");
            }
            ViewBag.SoLuong = veChuaThanhToan.Count;
            ViewBag.TongTien = veChuaThanhToan.Sum(v => v.GiaVe);
            return View("ChonPhuongThucThanhToanTatCa");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThanhToanTatCa(string paymentMethod)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "Không xác định được người dùng.";
                return RedirectToAction("VeDaDat");
            }
            var veChuaThanhToan = _context.VeBays.Where(v => v.MaHanhKhach == userId && !v.IsPaid).ToList();
            if (!veChuaThanhToan.Any())
            {
                TempData["Error"] = "Không có vé nào cần thanh toán.";
                return RedirectToAction("VeDaDat");
            }
            foreach (var ve in veChuaThanhToan)
            {
                ve.IsPaid = true;
                ve.PaymentMethod = paymentMethod;
            }
            _context.SaveChanges();
            TempData["Success"] = $"Đã thanh toán thành công {veChuaThanhToan.Count} vé bằng hình thức: {paymentMethod}!";
            return RedirectToAction("VeDaDat");
        }
    }
}