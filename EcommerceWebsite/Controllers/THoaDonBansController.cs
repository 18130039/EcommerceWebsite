using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceWebsite.Models;

namespace EcommerceWebsite.Controllers
{
    public class THoaDonBansController : Controller
    {
        private readonly QlbanVaLiContext _context;

        public THoaDonBansController(QlbanVaLiContext context)
        {
            _context = context;
        }

        // GET: THoaDonBans
        public async Task<IActionResult> Index()
        {
            var qlbanVaLiContext = _context.THoaDonBans.Include(t => t.MaKhachHangNavigation).Include(t => t.MaNhanVienNavigation);
            return View(await qlbanVaLiContext.ToListAsync());
        }

        // GET: THoaDonBans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.THoaDonBans == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans
                .Include(t => t.MaKhachHangNavigation)
                .Include(t => t.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }

            return View(tHoaDonBan);
        }

        // GET: THoaDonBans/Create
        public IActionResult Create()
        {
            /*ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhachHang", "MaKhachHang");*/
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien");
            return View();
        }

        // POST: THoaDonBans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHoaDon,NgayHoaDon,MaKhachHang,MaNhanVien,TongTienHd,GiamGiaHd,PhuongThucThanhToan,MaSoThue,ThongTinThue,GhiChu")] THoaDonBan tHoaDonBan)
        {
            if (ModelState.IsValid)
            {
                tHoaDonBan.MaKhachHang = HttpContext.Session.GetString("UserName");
                _context.Add(tHoaDonBan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            /*ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhachHang", "MaKhachHang", tHoaDonBan.MaKhachHang);*/
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien", tHoaDonBan.MaNhanVien);
            return View(tHoaDonBan);
        }

        // GET: THoaDonBans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.THoaDonBans == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans.FindAsync(id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }
            ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhachHang", "MaKhachHang", tHoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien", tHoaDonBan.MaNhanVien);
            return View(tHoaDonBan);
        }

        // POST: THoaDonBans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaHoaDon,NgayHoaDon,MaKhachHang,MaNhanVien,TongTienHd,GiamGiaHd,PhuongThucThanhToan,MaSoThue,ThongTinThue,GhiChu")] THoaDonBan tHoaDonBan)
        {
            if (id != tHoaDonBan.MaHoaDon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tHoaDonBan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!THoaDonBanExists(tHoaDonBan.MaHoaDon))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKhachHang"] = new SelectList(_context.TKhachHangs, "MaKhachHang", "MaKhachHang", tHoaDonBan.MaKhachHang);
            ViewData["MaNhanVien"] = new SelectList(_context.TNhanViens, "MaNhanVien", "MaNhanVien", tHoaDonBan.MaNhanVien);
            return View(tHoaDonBan);
        }

        // GET: THoaDonBans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.THoaDonBans == null)
            {
                return NotFound();
            }

            var tHoaDonBan = await _context.THoaDonBans
                .Include(t => t.MaKhachHangNavigation)
                .Include(t => t.MaNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.MaHoaDon == id);
            if (tHoaDonBan == null)
            {
                return NotFound();
            }

            return View(tHoaDonBan);
        }

        // POST: THoaDonBans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.THoaDonBans == null)
            {
                return Problem("Entity set 'QlbanVaLiContext.THoaDonBans'  is null.");
            }
            var tHoaDonBan = await _context.THoaDonBans.FindAsync(id);
            if (tHoaDonBan != null)
            {
                _context.THoaDonBans.Remove(tHoaDonBan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool THoaDonBanExists(string id)
        {
          return (_context.THoaDonBans?.Any(e => e.MaHoaDon == id)).GetValueOrDefault();
        }
    }
}
