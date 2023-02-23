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
    public class TacVuPhongController : Controller
    {
        private readonly QlksContext _context;

        public TacVuPhongController(QlksContext context)
        {
            _context = context;
        }

        // GET: TacVuPhong
        public async Task<IActionResult> Index()
        {
              return _context.TacVuPhongs != null ? 
                          View(await _context.TacVuPhongs.ToListAsync()) :
                          Problem("Entity set 'QlksContext.TacVuPhongs'  is null.");
        }

        // GET: TacVuPhong/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TacVuPhongs == null)
            {
                return NotFound();
            }

            var tacvuphong = await _context.TacVuPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tacvuphong == null)
            {
                return NotFound();
            }

            return View(tacvuphong);
        }

        // GET: TacVuPhong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TacVuPhong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaTvp,TenTvp")] TacVuPhong tacvuphong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tacvuphong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacvuphong);
        }

        // GET: TacVuPhong/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TacVuPhongs == null)
            {
                return NotFound();
            }

            var tacvuphong = await _context.TacVuPhongs.FindAsync(id);
            if (tacvuphong == null)
            {
                return NotFound();
            }
            return View(tacvuphong);
        }

        // POST: TacVuPhong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaTvp,TenTvp")] TacVuPhong tacvuphong)
        {
            if (id != tacvuphong.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tacvuphong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TacvuphongExists(tacvuphong.Id))
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
            return View(tacvuphong);
        }

        // GET: TacVuPhong/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TacVuPhongs == null)
            {
                return NotFound();
            }

            var tacvuphong = await _context.TacVuPhongs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tacvuphong == null)
            {
                return NotFound();
            }

            return View(tacvuphong);
        }

        // POST: TacVuPhong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TacVuPhongs == null)
            {
                return Problem("Entity set 'QlksContext.TacVuPhongs'  is null.");
            }
            var tacvuphong = await _context.TacVuPhongs.FindAsync(id);
            if (tacvuphong != null)
            {
                _context.TacVuPhongs.Remove(tacvuphong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TacvuphongExists(int id)
        {
          return (_context.TacVuPhongs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
