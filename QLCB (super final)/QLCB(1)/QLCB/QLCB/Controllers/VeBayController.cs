using Microsoft.AspNetCore.Mvc;
using QLCB.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace QLCB.Controllers
{
    public class VeBayController : Controller
    {
        private readonly ApplicationDBContext _context;

        public VeBayController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: VeBay/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var veBay = await _context.VeBays
                .Include(v => v.MaChuyenBay)
                .FirstOrDefaultAsync(m => m.MaVeBay == id);

            if (veBay == null)
                return NotFound();

            return View(veBay);
        }
    }
}
