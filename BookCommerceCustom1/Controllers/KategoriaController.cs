using BookCommerceCustom1.Data;
using BookCommerceCustom1.Helpers;
using BookCommerceCustom1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCommerceCustom1.Controllers
{
    [Authorize(Roles = Sd.RoliAdmin)]
    public class KategoriaController : Controller
    {
        private readonly Konteksti _konteksti;

        public KategoriaController(Konteksti konteksti 
            )
        {
            _konteksti = konteksti;
        }
        public IActionResult Listo()
        {
            var lista = _konteksti.Kategorite.ToList();
            return View(lista);
        }

        public IActionResult Krijo()
        {
            var entiteti = new Kategoria();
            return View(entiteti);
        }
        [HttpPost]
        public IActionResult Krijo(Kategoria entiteti)
        {
            if (ModelState.IsValid)
            {
                _konteksti.Kategorite.Add(entiteti);
                _konteksti.SaveChanges();
                TempData["suksesi"] = "U shtua me sukses";
                return RedirectToAction("Listo");
            }
            return View();
        }
        public IActionResult Ndrysho(int? id)
        {
            if (id==null || id==0)
            {
                return NotFound();
            }

            var entiteti = _konteksti.Kategorite.FirstOrDefault(x => x.Id == id);
            if (entiteti==null)
            {
                return NotFound();
            }
            
            return View(entiteti);
        }
        [HttpPost]
        public IActionResult Ndrysho(Kategoria entiteti)
        {
            if (ModelState.IsValid)
            {
                _konteksti.Kategorite.Update(entiteti);
                _konteksti.SaveChanges();
                TempData["suksesi"] = "U ndryshua me sukses";
                return RedirectToAction("Listo");
            }
            return View();
        }
        public IActionResult Fshi(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var entiteti = _konteksti.Kategorite.FirstOrDefault(x => x.Id == id);
            if (entiteti == null)
            {
                return NotFound();
            }
            return View(entiteti);
        }
        [HttpPost,ActionName("Fshi")]
        public IActionResult FshiPost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var entiteti = _konteksti.Kategorite.FirstOrDefault(x => x.Id == id);
            if (entiteti == null)
            {
                return NotFound();
            }

            _konteksti.Remove(entiteti);
            _konteksti.SaveChanges();
            TempData["suksesi"] = "U fshi me sukses";
            return RedirectToAction("Listo");
        }
    }
}
