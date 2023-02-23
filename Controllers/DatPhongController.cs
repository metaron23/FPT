using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EF_MVC_Project.Data;
using EF_MVC_Project.Models;

namespace EF_MVC_Project.Controllers
{
    public class DatPhongController : Controller
    {
        private readonly QlksContext _context;

        public DatPhongController(QlksContext context)
        {
            _context = context;
        }

        // GET: DatPhong
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var listP = await _context.Phongs
                .Include(p => p.MaLpNavigation)
                .OrderByDescending(p => p.SoPhong)
                .ToListAsync();
            if (!String.IsNullOrEmpty(searchString))
            {
                listP = listP
                    .Where(
                        s =>
                            s.SoPhong.ToString().Contains(searchString)
                            || s.MaLpNavigation.TenLp.Contains(searchString)
                    )
                    .ToList();
            }
            var listobject = new List<object>();
            listobject.Add(listP);
            return View(listobject);
        }

        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int? id)
        {
            //1 là rảnh, 0 là đã đặt, 2 là tất cả
            var listobject = new List<object>();
            if (id == 0)
            {
                ViewData["checkNav"] = 0;
                listobject.Add(
                    await _context.Phongs
                        .Where(p => p.TrangThaiPhong == 0)
                        .Include(p => p.MaLpNavigation)
                        .OrderByDescending(p => p.SoPhong)
                        .ToListAsync()
                );
            }
            else if (id == 1)
            {
                ViewData["checkNav"] = 1;
                listobject.Add(
                    await _context.Phongs
                        .Where(p => p.TrangThaiPhong == 1)
                        .Include(p => p.MaLpNavigation)
                        .OrderByDescending(p => p.SoPhong)
                        .ToListAsync()
                );
            }
            else
            {
                ViewData["checkNav"] = 2;
                var listP = await _context.Phongs
                    .Include(p => p.MaLpNavigation)
                    .Include(p => p.MaLpNavigation)
                    .OrderByDescending(p => p.SoPhong)
                    .ToListAsync();
                listobject.Add(listP);
            }

            return View(listobject);
        }

        // GET: DatPhong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DatPhongs == null)
            {
                return NotFound();
            }

            var datphong = await _context.DatPhongs
                .Include(d => d.MaKhNavigation)
                .Include(d => d.MaPNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datphong == null)
            {
                return NotFound();
            }

            return View(datphong);
        }

        // GET: DatPhong/Create
        public IActionResult Create()
        {
            ViewData["MaP"] = new SelectList(_context.Phongs, "MaP", "SoPhong");
            return View();
        }

        // POST: DatPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,MaDp,SoNguoi,NgayBatDau,NgayKetThuc,MaP,MaKh")] DatPhong datphong,
            [Bind("Id, MaKh, TenKh")] KhachHang khachhang
        )
        {
            var khachHangs = await _context.KhachHangs
                .OrderByDescending(kh => kh.MaKh)
                .FirstOrDefaultAsync();
            var makh = "KH" + (Int32.Parse(khachHangs.MaKh.Substring(2)) + 1).ToString();
            datphong.MaKh = makh;
            khachhang.MaKh = makh;
            if (ModelState.IsValid)
            {
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                _context.Add(datphong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["MaP"] = new SelectList(_context.Phongs, "MaP", "SoPhong", datphong.MaP);
            return View(datphong);
        }

        // GET: DatPhong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //nv so 0, ql 1, admin 99, -1 bi khoa -> access denied
            string tenDN = HttpContext.Request.Cookies["cookieName"];
            if (HttpContext.Request.Cookies["cookieName"] != null) { }
            if (id == null || _context.DatPhongs == null)
            {
                return NotFound();
            }

            var datphong = await _context.DatPhongs.FindAsync(id);
            if (datphong == null)
            {
                return NotFound();
            }
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", datphong.MaKh);
            ViewData["MaP"] = new SelectList(_context.Phongs, "MaP", "MaP", datphong.MaP);
            return View(datphong);
        }

        // POST: DatPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,MaDp,SoNguoi,ThoiGianBatDau,ThoiGianKetThuc,MaP,MaKh")] DatPhong datphong
        )
        {
            if (id != datphong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datphong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatphongExists(datphong.Id))
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
            ViewData["MaKh"] = new SelectList(_context.KhachHangs, "MaKh", "MaKh", datphong.MaKh);
            ViewData["MaP"] = new SelectList(_context.Phongs, "MaP", "MaP", datphong.MaP);
            return View(datphong);
        }

        // GET: DatPhong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DatPhongs == null)
            {
                return NotFound();
            }

            var datphong = await _context.DatPhongs
                .Include(d => d.MaKhNavigation)
                .Include(d => d.MaPNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (datphong == null)
            {
                return NotFound();
            }

            return View(datphong);
        }

        // POST: DatPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DatPhongs == null)
            {
                return Problem("Entity set 'QlksContext.DatPhongs'  is null.");
            }
            var datphong = await _context.DatPhongs.FindAsync(id);
            if (datphong != null)
            {
                _context.DatPhongs.Remove(datphong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatphongExists(int id)
        {
            return (_context.DatPhongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
