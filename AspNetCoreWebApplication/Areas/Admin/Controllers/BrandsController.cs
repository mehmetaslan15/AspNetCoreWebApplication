﻿using AspNetCoreWebApplication.Data;
using AspNetCoreWebApplication.Entities;
using AspNetCoreWebApplication.Tools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly DatabaseContext _context;

        public BrandsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: BrandsController
        public async Task<ActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // GET: BrandsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BrandsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Brand brand, IFormFile? Logo)
        {
            try
            {
                brand.Logo = await FileHelper.FileLoaderAsync(Logo);
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View(brand);
        }

        // GET: BrandsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var marka = await _context.Brands.FindAsync(id);
            if (marka == null) return NotFound();  // Eğer gönderilen id ye ait bir marka veritabanında yoksa geriye NotFound(Bulunamadı) hatası dön. 
            return View(marka);
        }

        // POST: BrandsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Brand brand, IFormFile? Logo, bool resmiSil)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (resmiSil)  // if (resmiSil == true) demek ile aynı şey. Çünkü yukarıdan yani post metodundan true ya da false gelecek sadece 
                    {
                        FileHelper.FileRemover(brand.Logo);
                        brand.Logo = string.Empty;
                    }
                    if (Logo != null) brand.Logo = await FileHelper.FileLoaderAsync(formFile: Logo);
                    //_context.Brands.Update(brand);  // 1. güncelleme yöntemi
                    _context.Entry(brand).State = EntityState.Modified; // 2. güncelleme yöntemi
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            return View(brand);
        }

        // GET: BrandsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            return View(await _context.Brands.FindAsync(id));
        }

        // POST: BrandsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Brand brand)
        {
            try
            {
                //_context.Brands.Remove(brand);  // 1. silme yöntemi
                _context.Entry(brand).State = EntityState.Deleted;  // 2 Silme yöntemi. 
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
