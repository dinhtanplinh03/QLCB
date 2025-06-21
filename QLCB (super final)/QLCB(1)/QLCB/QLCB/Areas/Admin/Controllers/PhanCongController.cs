using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;

namespace QLCB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhanCongController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PhanCongController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/PhanCong
        public async Task<IActionResult> Index()
        {
            var list = await _context.PhanCongs.ToListAsync();
            return View(list);
        }

        // GET: Admin/PhanCong/Create
        public IActionResult Create()
        {
            ViewBag.DanhSachChuyenBay = _context.ChuyenBays
                .Select(cb => new SelectListItem
                {
                    Value = cb.MaChuyenBay,
                    Text = cb.TenChuyenBay
                }).ToList();

            ViewBag.DanhSachNhanVien = _context.NhanViens
                .Select(nv => new SelectListItem
                {
                    Value = nv.MaNhanVien,
                    Text = nv.HoTen
                }).ToList();

            return View();
        }


        // POST: Admin/PhanCong/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhanCong model)
        {
            if (ModelState.IsValid)
            {
                // Lấy chuyến bay
                var chuyenBay = await _context.ChuyenBays.FindAsync(model.MaChuyenBay);
                if (chuyenBay == null)
                {
                    ModelState.AddModelError("", "Chuyến bay không tồn tại.");
                }
                else
                {
                    // 🔥 Lấy loại máy bay từ bảng MayBay
                    var mayBay = await _context.MayBays.FindAsync(chuyenBay.MaMayBay);
                    if (mayBay == null)
                    {
                        ModelState.AddModelError("", "Không tìm thấy loại máy bay tương ứng.");
                    }
                    else
                    {
                        var loaiMayBay = mayBay.LoaiMayBay;

                        // Kiểm tra xem nhân viên đã được phân công cho chuyến bay khác tại cùng thời điểm chưa
                        // 🔥 Kiểm tra xung đột thời gian (chồng chéo)
                        var trungLich = _context.PhanCongs
                            .Where(p => p.MaNhanVien == model.MaNhanVien)
                            .Join(_context.ChuyenBays,
                                  pc => pc.MaChuyenBay,
                                  cb => cb.MaChuyenBay,
                                  (pc, cb) => cb)
                            .Any(cb =>
                                // kiểm tra thời gian có giao nhau không
                                !(cb.ThoiGianDen <= chuyenBay.ThoiGianKhoiHanh ||
                                  cb.ThoiGianKhoiHanh >= chuyenBay.ThoiGianDen)
                            );

                        if (trungLich)
                        {
                            ModelState.AddModelError("", "Nhân viên đã có chuyến bay trùng thời gian.");
                        }

                        // Đặt thời gian nghỉ tối thiểu giữa 2 chuyến bay (ví dụ: 2 giờ)
                        TimeSpan thoiGianNghiToiThieu = TimeSpan.FromHours(2);

                        // Lấy tất cả chuyến bay nhân viên đã được phân công
                        var cacChuyenBayDaPhanCong = _context.PhanCongs
                            .Where(p => p.MaNhanVien == model.MaNhanVien)
                            .Join(_context.ChuyenBays,
                                  pc => pc.MaChuyenBay,
                                  cb => cb.MaChuyenBay,
                                  (pc, cb) => cb)
                            .ToList(); // ép chuyển sang client-side

                        // Kiểm tra vi phạm thời gian nghỉ
                        bool viPhamThoiGianNghi = cacChuyenBayDaPhanCong.Any(cb =>
                            (chuyenBay.ThoiGianKhoiHanh - cb.ThoiGianDen < thoiGianNghiToiThieu && chuyenBay.ThoiGianKhoiHanh > cb.ThoiGianDen)
                            ||
                            (cb.ThoiGianKhoiHanh - chuyenBay.ThoiGianDen < thoiGianNghiToiThieu && cb.ThoiGianKhoiHanh > chuyenBay.ThoiGianDen)
                        );

                        if (viPhamThoiGianNghi)
                        {
                            ModelState.AddModelError("", $"Nhân viên chưa có đủ thời gian nghỉ giữa các chuyến bay (tối thiểu {thoiGianNghiToiThieu.TotalMinutes} phút).");
                        }

                        // 🔎 Kiểm tra trạng thái nhân viên
                        var nhanVien = await _context.NhanViens.FindAsync(model.MaNhanVien);
                        if (nhanVien == null)
                        {
                            ModelState.AddModelError("", "Nhân viên không tồn tại.");
                        }
                        else if (!nhanVien.TrangThai)
                        {
                            ModelState.AddModelError("", "Nhân viên hiện không hoạt động, không thể phân công.");
                        }



                        // Lấy chứng nhận của nhân viên
                        var chungNhans = _context.ChungNhans
                            .Where(cn => cn.MaNhanVien == model.MaNhanVien)
                            .ToList();

                        // So sánh loại máy bay với chứng nhận + kiểm tra hiệu lực
                        var chungNhanHopLe = chungNhans.FirstOrDefault(cn =>
                            cn.TenChungNhan.Equals(loaiMayBay, StringComparison.OrdinalIgnoreCase)
                            && cn.NgayHetHan >= chuyenBay.ThoiGianKhoiHanh);

                        if (chungNhanHopLe == null)
                        {
                            ModelState.AddModelError("", $"Nhân viên chưa có chứng nhận phù hợp với máy bay [{loaiMayBay}] hoặc chứng nhận đã hết hạn.");
                        }
                    }
                }

                // Nếu hợp lệ thì thêm phân công
                if (ModelState.IsValid)
                {
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Thêm phân công mới thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }

            // Load lại dropdown khi có lỗi
            ViewBag.DanhSachChuyenBay = _context.ChuyenBays
                .Select(cb => new SelectListItem
                {
                    Value = cb.MaChuyenBay,
                    Text = cb.TenChuyenBay
                }).ToList();

            ViewBag.DanhSachNhanVien = _context.NhanViens
                .Where(nv => nv.TrangThai) // Chỉ lấy nhân viên đang hoạt động
                .Select(nv => new SelectListItem
                {
                    Value = nv.MaNhanVien,
                    Text = nv.HoTen
                }).ToList();

            return View(model);
        }

        // GET: Admin/PhanCong/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong == null) return NotFound();

            return View(phanCong);
        }

        // POST: Admin/PhanCong/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhanCong model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Cập nhật phân công thành công!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PhanCongs.Any(p => p.Id == id))
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy phân công để cập nhật.";
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Cập nhật phân công thất bại. Vui lòng kiểm tra lại thông tin.";
            return View(model);
        }

        // GET: Admin/PhanCong/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong == null) return NotFound();

            return View(phanCong);
        }

        // POST: Admin/PhanCong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phanCong = await _context.PhanCongs.FindAsync(id);
            if (phanCong != null)
            {
                _context.PhanCongs.Remove(phanCong);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa phân công thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không tìm thấy phân công để xóa.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
