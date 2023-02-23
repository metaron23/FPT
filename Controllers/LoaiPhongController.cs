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
    public class LoaiPhongController : Controller
    {
        private readonly QlksContext _context;

        public LoaiPhongController(QlksContext context)
        {
            _context = context;
        }

        // GET: LoaiPhong
        public async Task<IActionResult> Index()
        {
            return _context.LoaiPhongs != null
                ? View(await _context.LoaiPhongs.ToListAsync())
                : Problem("Entity set 'QlksContext.LoaiPhongs'  is null.");
        }

        // GET: LoaiPhong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiPhongs == null)
            {
                return NotFound();
            }

            var loaiphong = await _context.LoaiPhongs.FirstOrDefaultAsync(m => m.Id == id);
            if (loaiphong == null)
            {
                return NotFound();
            }

            return View(loaiphong);
        }

        // GET: LoaiPhong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,MaLp,TenLp,DonGia")] LoaiPhong loaiphong
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiphong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiphong);
        }

        // GET: LoaiPhong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiPhongs == null)
            {
                return NotFound();
            }

            var loaiphong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiphong == null)
            {
                return NotFound();
            }
            return View(loaiphong);
        }

        // POST: LoaiPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,MaLp,TenLp,DonGia")] LoaiPhong loaiphong
        )
        {
            if (id != loaiphong.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiphong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiphongExists(loaiphong.Id))
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
            return View(loaiphong);
        }

        // GET: LoaiPhong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiPhongs == null)
            {
                return NotFound();
            }

            var loaiphong = await _context.LoaiPhongs.FirstOrDefaultAsync(m => m.Id == id);
            if (loaiphong == null)
            {
                return NotFound();
            }

            return View(loaiphong);
        }

        // POST: LoaiPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiPhongs == null)
            {
                return Problem("Entity set 'QlksContext.LoaiPhongs'  is null.");
            }
            var loaiphong = await _context.LoaiPhongs.FindAsync(id);
            if (loaiphong != null)
            {
                _context.LoaiPhongs.Remove(loaiphong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiphongExists(int id)
        {
            return (_context.LoaiPhongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
