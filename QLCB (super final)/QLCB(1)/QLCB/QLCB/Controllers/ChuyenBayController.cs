using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLCB.Models;
using System.Threading.Tasks;

namespace QLCB.Controllers
{
    public class ChuyenBayController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ChuyenBayController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var chuyenBays = await _context.ChuyenBays
                                          .Include(c => c.MayBay)
                                          .Include(c => c.SanBayDi)
                                          .Include(c => c.SanBayDen)
                                          .ToListAsync();
            return View(chuyenBays);
        }
    }
} 