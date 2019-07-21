using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
        public bool AuthenticateUser()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return false;
            }

            return true;
        }
        [HttpPost]
        public IActionResult Login(MUser muser)
        {
            MUser LoggedInUser = _context.MUser.Where(abc => abc.Username == muser.Username && abc.Password == muser.Password).FirstOrDefault();

            if (LoggedInUser == null)
            {
                ViewBag.Message = "Invalid Username or Password";
                return View();
            }
            HttpContext.Session.SetString("Username", LoggedInUser.Username);
            HttpContext.Session.SetString("UserRole", LoggedInUser.Role);
            HttpContext.Session.SetString("UserDisplayName", LoggedInUser.DisplayName);

            if (LoggedInUser.Role == "Admin" || LoggedInUser.Role == "staff")
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(MUser muser, IFormFile ProfilePicture)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePicture != null)
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                    string FilePath = _env.WebRootPath + "/SystemData/ProfilePicture/";
                    FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                    ProfilePicture.CopyTo(FS);
                    FS.Close();
                    muser.ProfilePicture = "/SystemData/ProfilePicture/" + FileName;
                }
                muser.Role = "Staff";
                muser.Status = "Active";
                muser.CreatedDate = DateTime.Now;
                muser.CreatedBy = "Self";
                muser.ModifiedDate = DateTime.Now;
                muser.ModifiedBy = "Self";

                _context.Add(muser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(muser);
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(MUser muser)
        {
            
                try
                {
                    MailMessage Email = new MailMessage();
                    Email.From = new MailAddress("shahzadaptech143@gmail.com", "M Shahzad");

                    Email.To.Add(new MailAddress(muser.Email));
                    //string[] allccemails = CCEmails.Split(",");

                    //Email.CC.Add(new MailAddress("info@thetasolutions.pk"));
                    //Email.Bcc.Add(new MailAddress("info@thetasolutions.co.uk"));

                    Email.Subject = "Password Recovery";
                    Email.IsBodyHtml = true;

                    Email.Body = "Hi <b style='background-color:yellow;'>" + muser.DisplayName + "</b>,<br><br>Here Is you Password" + muser.Password + "<br><br>---<br>Theta Team />";

                    SmtpClient SMTPServer = new SmtpClient();
                    SMTPServer.Host = "smtp.gmail.com";
                    SMTPServer.Port = 587;
                    SMTPServer.EnableSsl = true;
                    SMTPServer.Credentials = new System.Net.NetworkCredential("shahzadaptech143@gmail.com", "accp12345");

                    SMTPServer.Send(Email);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        
           
            return RedirectToAction(nameof(Login));
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
                    if (!MUserExists(mCategory.Id))
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

        private bool MUserExists(int id)
        {
            return _context.MCategory.Any(e => e.Id == id);
        }
        public IActionResult Index()
        {
            if (AuthenticateUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
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
                    if (!MUserExists(mitem.Id))
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


        //public IList<string> CategoryNamesList()
        //{
        //    return _context.MCategory.Select(a => a.Name).ToList();
        //}

        public async Task<IActionResult> AllUser()
        {
            return View(await _context.MUser.ToListAsync());
        }
        public async Task<IActionResult> EditUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muser = await _context.MUser.FindAsync(id);
            if (muser == null)
            {
                return NotFound();
            }
            ViewBag.ProfilePicture = muser.ProfilePicture;
            return View(muser);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(int id, MUser muser, IFormFile ProfilePicture)
        {
            if (id != muser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ProfilePicture != null)
                    {
                        string FileName = Guid.NewGuid().ToString() + Path.GetExtension(ProfilePicture.FileName);
                        string FilePath = _env.WebRootPath + "/SystemData/ProfilePicture/";
                        FileStream FS = new FileStream(FilePath + FileName, FileMode.Create);
                        ProfilePicture.CopyTo(FS);
                        FS.Close();
                        muser.ProfilePicture = "/SystemData/ProfilePicture/" + FileName;
                    }
                    _context.Update(muser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MUserExists(muser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllUser));
            }
            return View(muser);
        }
        public async Task<IActionResult> DetailUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muser = await _context.MUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (muser == null)
            {
                return NotFound();
            }
            ViewBag.ProfileImage = muser.ProfilePicture;
            return View(muser);
        }
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _context.MUser.Remove(_context.MUser.Find(id));
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(AllUser));
        }
    }
}
