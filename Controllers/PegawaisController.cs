using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using coba.Data;
using coba.Models;

namespace coba.Controllers
{
    public class PegawaisController : Controller
    {
        private readonly bapendaContext _context;

        public PegawaisController(bapendaContext context)
        {
            _context = context;
        }

        // ✅ Tambahan untuk halaman dashboard setelah login
        public IActionResult Dashboard(string nama)
        {
            ViewBag.Nama = nama;
            return View();
        }

        // GET: Pegawais
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pegawais.ToListAsync());
        }

        // GET: Pegawais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawai = await _context.Pegawais
                .FirstOrDefaultAsync(m => m.IdPegawai == id);
            if (pegawai == null)
            {
                return NotFound();
            }

            return View(pegawai);
        }

        // GET: Pegawais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pegawais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPegawai,NamaPegawai,Jabatan,Username,Password")] Pegawai pegawai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pegawai);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Admin");

            }
            return View(pegawai);
        }

        // GET: Pegawais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawai = await _context.Pegawais.FindAsync(id);
            if (pegawai == null)
            {
                return NotFound();
            }
            return View(pegawai);
        }

        // POST: Pegawais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPegawai,NamaPegawai,Jabatan,Username,Password")] Pegawai pegawai)
        {
            if (id != pegawai.IdPegawai)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pegawai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PegawaiExists(pegawai.IdPegawai))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(pegawai);
        }

        // GET: Pegawais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pegawai = await _context.Pegawais
                .FirstOrDefaultAsync(m => m.IdPegawai == id);
            if (pegawai == null)
            {
                return NotFound();
            }

            return View(pegawai);
        }

        // POST: Pegawais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pegawai = await _context.Pegawais.FindAsync(id);
            if (pegawai != null)
            {
                _context.Pegawais.Remove(pegawai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PegawaiExists(int id)
        {
            return _context.Pegawais.Any(e => e.IdPegawai == id);
        }
    }
}
