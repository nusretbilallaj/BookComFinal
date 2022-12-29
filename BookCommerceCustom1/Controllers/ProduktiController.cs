using BookCommerceCustom1.Data;
using BookCommerceCustom1.Helpers;
using BookCommerceCustom1.Models;
using BookCommerceCustom1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookCommerceCustom1.Controllers
{
	[Authorize(Roles = Sd.RoliAdmin)]
	public class ProduktiController : Controller
    {
        private readonly Konteksti _konteksti;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProduktiController(Konteksti konteksti, IWebHostEnvironment hostEnvironment)
        {
            _konteksti = konteksti;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Listo()
        {
            return View();
        }
        public IActionResult ShtoNdrysho(int? id)
        {
            ProduktiViewModel productVM = new()
            {
                Produkti = new(),
                Kategorite = _konteksti.Kategorite.ToList().Select(i => new SelectListItem
                {
                    Text = i.Emri,
                    Value = i.Id.ToString()
                }),
                Mbulesat = _konteksti.Mbulesa.ToList().Select(i => new SelectListItem
                {
                    Text = i.Emri,
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.Produkti = _konteksti.Produktet.FirstOrDefault(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShtoNdrysho(ProduktiViewModel obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"imazhet\produktet");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.Produkti.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Produkti.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Produkti.ImageUrl = @"\imazhet\produktet\" + fileName + extension;

                }
                if (obj.Produkti.Id == 0)
                {
                    _konteksti.Produktet.Add(obj.Produkti);
                }
                else
                {
                    _konteksti.Produktet.Update(obj.Produkti);
                }
                _konteksti.SaveChanges();
                TempData["suksesi"] = "Eshte shtuar me sukses";
                return RedirectToAction("Listo");
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult ListoTeGjitha()
        {
            var productList = _konteksti.Produktet.Include(x => x.Kategoria).Include(x => x.Mbulesa).ToList();
            return Json(new { data = productList });
        }

        //POST
        [HttpDelete]
        public IActionResult Fshi(int id)
        {
            var obj = _konteksti.Produktet.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { statusi = false, mesazhi = "Nuk gjindet" });
            }

            var pathiIVjeter = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(pathiIVjeter))
            {
                System.IO.File.Delete(pathiIVjeter);
            }

            _konteksti.Produktet.Remove(obj);
            _konteksti.SaveChanges();
            return Json(new { statusi = true, mesazhi = "U fshi me sukese" });

        }

    }
}
