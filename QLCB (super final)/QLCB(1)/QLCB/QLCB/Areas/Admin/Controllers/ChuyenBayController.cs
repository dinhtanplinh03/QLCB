using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;
using QLCB.Services;
using System.Linq;
using System.Threading.Tasks;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChuyenBayController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly WeatherService _weatherService;


        public ChuyenBayController(ApplicationDBContext context, WeatherService weatherService)
        {
            _context = context;
            _weatherService = weatherService;
        }

        // GET: Admin/ChuyenBay
        public async Task<IActionResult> Index()
        {
            var danhSachChuyenBay = await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .ToListAsync();

            return View(danhSachChuyenBay);
        }

        // GET: Admin/ChuyenBay/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var chuyenBay = await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .FirstOrDefaultAsync(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return NotFound();
            }

            return View(chuyenBay);
        }

        // GET: Admin/ChuyenBay/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.MayBayList = await _context.MayBays
                    .Where(mb => mb.TrangThai == "Đang hoạt động")
                    .ToListAsync();

            ViewBag.SanBayDiList = await _context.SanBays
                .Where(sb => sb.TrangThai == "Hoạt động")
                .ToListAsync();

            ViewBag.SanBayDenList = await _context.SanBays
                .Where(sb => sb.TrangThai == "Hoạt động")
                .ToListAsync();
            return View();
        }

        // POST: Admin/ChuyenBay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChuyenBay chuyenBay)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ValidationErrors = ModelState
                    .Where(m => m.Value.Errors.Count > 0)
                    .Select(m => new
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    }).ToList();

                // Load lại các dropdown list
                ViewBag.MayBayList = await _context.MayBays
                    .Where(mb => mb.TrangThai == "Đang hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDiList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDenList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                TempData["ErrorMessage"] = "Thêm chuyến bay thất bại. Vui lòng kiểm tra lại thông tin.";
                return View(chuyenBay);
            }

            try
            {
                _context.ChuyenBays.Add(chuyenBay);
                await _context.SaveChangesAsync();

                // Tạo vé trống cho chuyến bay mới
                for (int i = 1; i <= chuyenBay.SoGhe; i++)
                {
                    var ve = new VeBay
                    {
                        MaVeBay = $"{chuyenBay.MaChuyenBay}_VE{i:D3}",
                        MaChuyenBay = chuyenBay.MaChuyenBay,
                        MaHanhKhach = null, // Chưa có người đặt
                        NgayDatVe = DateTime.Now,
                        GiaVe = chuyenBay.GiaVe, // Gán giá vé từ chuyến bay
                        MaMayBay = chuyenBay.MaMayBay,
                        MaSanBayDi = chuyenBay.MaSanBayDi,
                        MaSanBayDen = chuyenBay.MaSanBayDen
                    };
                    _context.VeBays.Add(ve);
                }
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Thêm chuyến bay mới thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                TempData["ErrorMessage"] = "Thêm chuyến bay thất bại: " + ex.Message;
                // Load lại dropdown list để hiển thị lại form
                ViewBag.MayBayList = await _context.MayBays
                    .Where(mb => mb.TrangThai == "Đang hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDiList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDenList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                return View(chuyenBay);
            }
        }

        // GET: Admin/ChuyenBay/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var chuyenBay = await _context.ChuyenBays.FindAsync(id);
            if (chuyenBay == null)
            {
                return NotFound();
            }

            ViewBag.MayBayList = await _context.MayBays
                    .Where(mb => mb.TrangThai == "Đang hoạt động")
                    .ToListAsync();

            ViewBag.SanBayDiList = await _context.SanBays
                .Where(sb => sb.TrangThai == "Hoạt động")
                .ToListAsync();

            ViewBag.SanBayDenList = await _context.SanBays
                .Where(sb => sb.TrangThai == "Hoạt động")
                .ToListAsync();

            return View(chuyenBay);
        }

        // POST: Admin/ChuyenBay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ChuyenBay chuyenBay)
        {
            if (id != chuyenBay.MaChuyenBay)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ValidationErrors = ModelState
                    .Where(m => m.Value.Errors.Count > 0)
                    .Select(m => new
                    {
                        Field = m.Key,
                        Errors = m.Value.Errors.Select(e => e.ErrorMessage).ToList()
                    }).ToList();

                ViewBag.MayBayList = await _context.MayBays
                    .Where(mb => mb.TrangThai == "Đang hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDiList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDenList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                TempData["ErrorMessage"] = "Cập nhật chuyến bay thất bại. Vui lòng kiểm tra lại thông tin.";
                return View(chuyenBay);
            }

            try
            {
                _context.Update(chuyenBay);
                // Cập nhật giá vé cho tất cả vé của chuyến bay này
                var veList = _context.VeBays.Where(v => v.MaChuyenBay == chuyenBay.MaChuyenBay).ToList();
                foreach (var ve in veList)
                {
                    ve.GiaVe = chuyenBay.GiaVe;
                }
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật chuyến bay thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChuyenBayExists(chuyenBay.MaChuyenBay))
                {
                    TempData["ErrorMessage"] = "Không tìm thấy chuyến bay để cập nhật.";
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                TempData["ErrorMessage"] = "Cập nhật chuyến bay thất bại: " + ex.Message;
                ViewBag.MayBayList = await _context.MayBays
                    .Where(mb => mb.TrangThai == "Đang hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDiList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();

                ViewBag.SanBayDenList = await _context.SanBays
                    .Where(sb => sb.TrangThai == "Hoạt động")
                    .ToListAsync();


                return View(chuyenBay);
            }
        }

        // GET: Admin/ChuyenBay/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var chuyenBay = await _context.ChuyenBays
                .Include(cb => cb.MayBay)
                .Include(cb => cb.SanBayDi)
                .Include(cb => cb.SanBayDen)
                .FirstOrDefaultAsync(cb => cb.MaChuyenBay == id);

            if (chuyenBay == null)
            {
                return NotFound();
            }

            return View(chuyenBay);
        }

        // POST: Admin/ChuyenBay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var chuyenBay = await _context.ChuyenBays.FindAsync(id);
            if (chuyenBay != null)
            {
                _context.ChuyenBays.Remove(chuyenBay);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa chuyến bay thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy chuyến bay để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ChuyenBayExists(string id)
        {
            return _context.ChuyenBays.Any(cb => cb.MaChuyenBay == id);
        }

        public async Task<IActionResult> CheckWeatherAndUpdateFlight(string maChuyenBay)
        {
            var cb = await _context.ChuyenBays.FindAsync(maChuyenBay);
            if (cb == null) return NotFound();

            var sanBayDi = await _context.SanBays.FindAsync(cb.MaSanBayDi); // bạn cần xác định thành phố sân bay

            string city = sanBayDi?.ThanhPho ?? "Ho Chi Minh"; // fallback
            var weatherDescription = await _weatherService.GetWeatherDescriptionAsync(city);

            cb.ThoiTiet = weatherDescription;

            // Đánh dấu delay nếu thời tiết xấu
            if (weatherDescription.Contains("mưa") || weatherDescription.Contains("bão") || weatherDescription.Contains("sương"))
            {
                cb.TrangThai = "Delay";
            }
            else
            {
                cb.TrangThai = "Đúng giờ";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}