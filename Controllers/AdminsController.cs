using coba.Data;
using coba.Models;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly bapendaContext _context;

    public AdminController(bapendaContext context)
    {
        _context = context;
    }

    public IActionResult Index(string nama)
    {
        // simpan nama admin ke ViewData
        ViewData["NamaAdmin"] = nama;

        // tampilkan semua pegawai
        var pegawaiList = _context.Pegawais.ToList();
        return View(pegawaiList);
    }

    // POST: /Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Admin admin)
    {
        if (ModelState.IsValid)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
            return RedirectToAction("index", "Admin");

        }
        return View(admin);
    }

    // GET: /Admin/Create
    public IActionResult Create()
    {
        return View();
    }



}
