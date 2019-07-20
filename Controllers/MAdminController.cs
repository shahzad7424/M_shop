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
            ViewBag.Picture = mCategory.Picture;
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
                    string FilePath = _env.WebRootPath + "/SystemData/CategoryPicture/";
                    FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                    Picture.CopyTo(FS);
                    FS.Close();
                    mCategory.Picture = "/SystemData/CategoryPicture/" + FileName;
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
        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(MItem mitem ,IFormFile MainImage)
        {
            if (ModelState.IsValid)
            {
                if (MainImage != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                    string FilePath = _env.WebRootPath + "/SystemData/ItemPicture/";
                    FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                    MainImage.CopyTo(FS);
                    FS.Close();
                    mitem.MainImage = "/SystemData/ItemPicture/" + FileName;
                }

                mitem.Status = "Active";
                mitem.CreatedDate = DateTime.Now;
                mitem.CreatedBy = "Self";
                mitem.ModifiedDate = DateTime.Now;
                mitem.ModifiedBy = "Self";

                _context.Add(mitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ExistingItem));
            }
            return View(mitem);
        }
        public async Task<IActionResult> ExistingItem()
        {
            return View(await _context.MItem.ToListAsync());
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
            ViewBag.Picture = mCategory.Picture;
            return View(mCategory);
        }

        // POST: MAdmin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Picture,Status ")] MCategory mCategory, IFormFile Picture)
        {
            if (id != mCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Picture != null)
                    {
                        string FileName = Guid.NewGuid().ToString() + Path.GetExtension(Picture.FileName);
                        string FilePath = _env.WebRootPath + "/SystemData/CategoryPicture/";
                        FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                        Picture.CopyTo(FS);
                        FS.Close();
                        mCategory.Picture = "/SystemData/CategoryPicture/" + FileName;
                    }
                    _context.Update(mCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MItemExists(mCategory.Id))
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

            _context.MCategory.Remove(_context.MCategory.Find(id));
            await  _context.SaveChangesAsync();


            return RedirectToAction(nameof(ExistingCategory));
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

        private bool MItemExists(int id)
        {
            return _context.MCategory.Any(e => e.Id == id);
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> EditItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitem = await _context.MItem.FindAsync(id);
            if (mitem == null)
            {
                return NotFound();
            }
            ViewBag.MainPicture = mitem.MainImage;
            return View(mitem);
        }
        [HttpPost]
        public async Task<IActionResult> EditItem(int id, MItem mitem, IFormFile MainImage)
        {
            if (id != mitem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (MainImage != null)
                    {
                        string FileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImage.FileName);
                        string FilePath = _env.WebRootPath + "/SystemData/CategoryPicture/";
                        FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                        MainImage.CopyTo(FS);
                        FS.Close();
                        mitem.MainImage = "/SystemData/CategoryPicture/" + FileName;
                    }
                    _context.Update(mitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MItemExists(mitem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ExistingItem));
            }
            return View(mitem);
        }
        public async Task<IActionResult> DetailItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitem = await _context.MItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mitem == null)
            {
                return NotFound();
            }
            ViewBag.MainImage = mitem.MainImage;
            return View(mitem);
        }
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.MItem.Remove(_context.MItem.Find(id));
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(ExistingItem));
        }
    }
}
