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
	public class KompaniaController : Controller
    {
        private readonly Konteksti _konteksti;

        public KompaniaController(Konteksti konteksti)
        {
            _konteksti = konteksti;
        }
        public IActionResult Listo()
        {
            return View();
        }
        public IActionResult ShtoNdrysho(int? id)
        {
            Kompania kompania = new();

            if (id == null || id == 0)
            {
                return View(kompania);
            }
            else
            {
               var kompExistuese = _konteksti.Kompania.FirstOrDefault(u => u.Id == id);
                return View(kompExistuese);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShtoNdrysho(Kompania obj)
        {

            if (ModelState.IsValid)
            {
                
                if (obj.Id == 0)
                {
                    _konteksti.Kompania.Add(obj);
                }
                else
                {
                    _konteksti.Kompania.Update(obj);
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
            var productList = _konteksti.Kompania.ToList();
            return Json(new { data = productList });
        }

        //POST
        [HttpDelete]
        public IActionResult Fshi(int id)
        {
            var obj = _konteksti.Kompania.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { statusi = false, mesazhi = "Nuk gjindet" });
            }

            
            _konteksti.Kompania.Remove(obj);
            _konteksti.SaveChanges();
            return Json(new { statusi = true, mesazhi = "U fshi me sukese" });

        }

    }
}
