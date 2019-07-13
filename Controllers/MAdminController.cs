using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mshop.Models;

namespace Mshop.Controllers
{
    public class MAdminController : Controller
    {
        private readonly mshopContext _context;

        public MAdminController(mshopContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {
            return View();
        }

        // GET: MAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.MCategory.ToListAsync());
        }

        // GET: MAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mCategory = await _context.MCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mCategory == null)
            {
                return NotFound();
            }

            return View(mCategory);
        }

        // GET: MAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Picture,Status,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] MCategory mCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mCategory);
        }

        // GET: MAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mCategory = await _context.MCategory.FindAsync(id);
            if (mCategory == null)
            {
                return NotFound();
            }
            return View(mCategory);
        }

        // POST: MAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Picture,Status,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] MCategory mCategory)
        {
            if (id != mCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MCategoryExists(mCategory.Id))
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
            return View(mCategory);
        }

        // GET: MAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mCategory = await _context.MCategory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mCategory == null)
            {
                return NotFound();
            }

            return View(mCategory);
        }

        // POST: MAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mCategory = await _context.MCategory.FindAsync(id);
            _context.MCategory.Remove(mCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MCategoryExists(int id)
        {
            return _context.MCategory.Any(e => e.Id == id);
        }
    }
}
