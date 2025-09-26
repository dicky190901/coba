using Microsoft.AspNetCore.Mvc;
using System.Linq;
using coba.Data;
using coba.Models;

namespace coba.Controllers
{
    public class AccountController : Controller
    {
        private readonly bapendaContext _context;

        public AccountController(bapendaContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ✅ Cek ke tabel Admin
                var admin = _context.Admins
                    .FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password);

                if (admin != null)
                {
                    return RedirectToAction("Index", "Admin", new { nama = admin.NamaAdmin });

                }


                // ✅ Cek ke tabel Pegawai
                var pegawai = _context.Pegawais
                    .FirstOrDefault(p => p.Username == model.Username && p.Password == model.Password);

                if (pegawai != null)
                {
                    // Kalau cocok pegawai → redirect ke Dashboard Pegawai + bawa nama pegawai
                    return RedirectToAction("Dashboard", "Pegawais", new { nama = pegawai.NamaPegawai });
                }

                // Kalau salah semua
                ModelState.AddModelError("", "Username atau Password salah.");
            }

            return View(model);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // hapus semua session
            return RedirectToAction("Login", "Account"); // kembali ke login
        }

    }
}
