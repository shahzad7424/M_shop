using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mshop.Models;

namespace Mshop.Controllers
{
    public class MAdminController : Controller
    {
        private readonly mshopContext _context;
        private readonly IHostingEnvironment _env;

        public MAdminController(mshopContext context , IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Login()
        {
            return View();
        }
    

        // GET: MAdmin

        public async Task<IActionResult> ExistingCategory()
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
        public IActionResult AddCategory()
        {
            return View();
        }

        // POST: MAdmin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory([Bind("Name,Picture")]MCategory mCategory, IFormFile Picture)
        {
            if (ModelState.IsValid)
            {
                if (Picture != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(Picture.FileName);
                    string FilePath = _env.WebRootPath + "~/SystemData/CategoryPicture/";
                    FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                    Picture.CopyTo(FS);
                    FS.Close();
                    mCategory.Picture = "~/SystemData/CategoryPicture/" + FileName;
                }

                mCategory.Status = "Active";
                mCategory.CreatedDate = DateTime.Now;
                mCategory.CreatedBy = "Self";
                mCategory.ModifiedDate = DateTime.Now;
                mCategory.ModifiedBy = "Self";

                _context.Add(mCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ExistingCategory));
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
                return RedirectToAction(nameof(ExistingCategory));
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
            return RedirectToAction(nameof(ExistingCategory));
        }

        private bool MCategoryExists(int id)
        {
            return _context.MCategory.Any(e => e.Id == id);
        }
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
