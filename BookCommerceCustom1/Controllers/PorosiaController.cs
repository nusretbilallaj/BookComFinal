using System.Security.Claims;
using System.Xml.XPath;
using BookCommerceCustom1.Data;
using BookCommerceCustom1.Helpers;
using BookCommerceCustom1.Models;
using BookCommerceCustom1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace BookCommerceCustom1.Controllers
{
	[Authorize]
	public class PorosiaController : Controller
	{
		private readonly Konteksti _konteksti;
		[BindProperty]
		public PorosiaViewModel PorosiaViewModel { get; set; }
		public PorosiaController(Konteksti konteksti)
		{
			_konteksti = konteksti;
		}
		public IActionResult Listo()
		{
			return View();
		}
		public IActionResult Detajet(int porosiaId)
		{
			var porosiaViewModel = new PorosiaViewModel();
			porosiaViewModel.Porosia = _konteksti.Porosite.Include(x=>x.Perdorusi).FirstOrDefault(x => x.Id == porosiaId);
			porosiaViewModel.PorosiDetalet =
				_konteksti.PorosiDetalet.Include(x => x.Produkti).Where(x => x.PorosiaId == porosiaId);

			return View(porosiaViewModel);
		}
		[ActionName("Detajet")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Detajet_Paguaj()
		{

			PorosiaViewModel.Porosia = _konteksti.Porosite.
				Include(x => x.Perdorusi).FirstOrDefault(x => x.Id == PorosiaViewModel.Porosia.Id);
			PorosiaViewModel.PorosiDetalet =
				_konteksti.PorosiDetalet.
					Include(x => x.Produkti).Where(x => x.PorosiaId == PorosiaViewModel.Porosia.Id);

			var domain = "https://localhost:7032/";
			var options = new SessionCreateOptions
			{
				LineItems = new List<SessionLineItemOptions>()
				,
				Mode = "payment",
				SuccessUrl = domain + $"Porosia/KonfirmimiIPageses?porosiaId={PorosiaViewModel.Porosia.Id}",
				CancelUrl = domain + $"Porosia/Detajet?porosiaId={PorosiaViewModel.Porosia.Id}"
			};

			foreach (PorosiDetali porosiDetali in PorosiaViewModel.PorosiDetalet)
			{
				{
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long?)porosiDetali.Cmimi * 100,
							Currency = "eur",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = porosiDetali.Produkti.Emri,
							},
						},
						Quantity = (long?)porosiDetali.Sasia,
					};
					options.LineItems.Add(sessionLineItem);

				}
			}


			var service = new SessionService();
			Session session = service.Create(options);
			PorosiaViewModel.Porosia.SessionId = session.Id;
			PorosiaViewModel.Porosia.PaymentIntentId = session.PaymentIntentId;
			_konteksti.SaveChanges();
			Response.Headers.Add("Location", session.Url);
			return new StatusCodeResult(303);
		}
		public IActionResult KonfirmimiIPageses(int porosiaId)
		{
			var porosia = _konteksti.Porosite.FirstOrDefault(x => x.Id == porosiaId);
			if (porosia.StatusiIPageses == Sd.StatusIPagesesForPageseTeVonuar)
			{
				var service = new SessionService();
				Session session = service.Get(porosia.SessionId);
				if (session.PaymentStatus.ToLower() == "paid")
				{
					porosia.PaymentIntentId = session.PaymentIntentId;
					porosia.SessionId = porosia.SessionId;
					porosia.DataEPageses = DateTime.Now;
					porosia.StatusiIPageses = Sd.StatusIPagesesIAprovuar;
					_konteksti.SaveChanges();
				}
			}
			return View(porosiaId);
		}
		[Authorize(Roles = Sd.RoliAdmin+","+ Sd.RoliPunetor)]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AzhuroDetajetEPorosiese()
		{
			var por = _konteksti.Porosite.AsNoTracking().FirstOrDefault(x=>x.Id== PorosiaViewModel.Porosia.Id);
			if (por!=null)
			{
				por.Emri = PorosiaViewModel.Porosia.Emri;
				por.NumriITelefonit = PorosiaViewModel.Porosia.NumriITelefonit;
				por.Rruga = PorosiaViewModel.Porosia.Rruga;
				por.Qyteti = PorosiaViewModel.Porosia.Qyteti;
				por.Shteti = PorosiaViewModel.Porosia.Shteti;
				por.KodiPostal = PorosiaViewModel.Porosia.KodiPostal;

				if (PorosiaViewModel.Porosia.Posta!=null)
				{
					por.Posta = PorosiaViewModel.Porosia.Posta;
				}
				if (PorosiaViewModel.Porosia.NumriGjurmues != null)
				{
					por.NumriGjurmues = PorosiaViewModel.Porosia.NumriGjurmues;
				}
				_konteksti.Porosite.Update(por);
				_konteksti.SaveChanges();
				TempData["Suksesi"] = "Se sukses";
				
			}
			return RedirectToAction("Detajet", "Porosia", new { porosiaId = por.Id });
			// var porosiaViewModel = new PorosiaViewModel();
			// porosiaViewModel.Porosia = _konteksti.Porosite.Include(x => x.Perdorusi).FirstOrDefault(x => x.Id == porosiaId);
			// porosiaViewModel.PorosiDetalet =
			// 	_konteksti.PorosiDetalet.Include(x => x.Produkti).Where(x => x.PorosiaId == porosiaId);
			//
			// return View(porosiaViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Sd.RoliAdmin + "," + Sd.RoliPunetor)]
		public IActionResult FilloProcesimin()
		{
			var por = _konteksti.Porosite.AsNoTracking().
                FirstOrDefault(x => x.Id == PorosiaViewModel.Porosia.Id);
			por.StatusiIPorosise = Sd.StatusNeProces;
			_konteksti.Porosite.Update(por);
			_konteksti.SaveChanges();
			TempData["Suksesi"] = "Porosia u procesua me sukses";
			return RedirectToAction("Detajet", "Porosia", new { porosiaId = PorosiaViewModel.Porosia.Id });
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Sd.RoliAdmin + "," + Sd.RoliPunetor)]
		public IActionResult DergoPorosine()
		{

			var por = _konteksti.Porosite.AsNoTracking().
                FirstOrDefault(x => x.Id == PorosiaViewModel.Porosia.Id);
			if (por != null)
			{
				por.Emri = PorosiaViewModel.Porosia.Emri;
				por.NumriGjurmues = PorosiaViewModel.Porosia.NumriGjurmues;
				por.Posta = PorosiaViewModel.Porosia.Posta;
				por.StatusiIPorosise = Sd.StatusIDerguar;
				por.DataEDergeses=DateTime.Now;

				if (por.StatusiIPageses==Sd.StatusIPagesesForPageseTeVonuar)
				{
					por.DataPerPagese = DateTime.Now.AddDays(10);
				}

				_konteksti.Porosite.Update(por);
				_konteksti.SaveChanges();
				TempData["Suksesi"] = "Porosia u dergua me sukses";
			}
			return RedirectToAction("Detajet", "Porosia", new { porosiaId = por.Id });
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = Sd.RoliAdmin + "," + Sd.RoliPunetor)]
		public IActionResult AnuloPorosine()
		{

			var por = _konteksti.Porosite.AsNoTracking().FirstOrDefault(x => x.Id == PorosiaViewModel.Porosia.Id);
			if (por != null)
			{
				if (por.StatusiIPorosise==Sd.StatusIAprovuar)
				{
					var options = new RefundCreateOptions()
					{
						Reason = RefundReasons.RequestedByCustomer,
						PaymentIntent = por.PaymentIntentId,
					};
					var serv = new RefundService();
					Refund refundi = serv.Create(options);
					por.StatusiIPorosise = Sd.StatusIAnuluar;
					por.StatusiIPageses = Sd.StatusIKthyer;
				}
				else
				{
					por.StatusiIPorosise = Sd.StatusIAnuluar;
					por.StatusiIPageses = Sd.StatusIAnuluar;
				}
				_konteksti.Porosite.Update(por);
				_konteksti.SaveChanges();
				TempData["Suksesi"] = "Porosia u anulua me sukses";
			}
			return RedirectToAction("Detajet", "Porosia", new { porosiaId = por.Id });
		}
		[HttpGet]
		public IActionResult ListoTeGjitha(string statusi)
		{
			List<Porosia> porosite;

			if (User.IsInRole(Sd.RoliAdmin) || User.IsInRole(Sd.RoliPunetor))
			{
				porosite = _konteksti.Porosite.Include(x => x.Perdorusi)
					.ToList();
			}
			else
			{
				var claimsIdentity =(ClaimsIdentity) User.Identity;
				var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
				porosite = _konteksti.Porosite.Where(x=>x.PerdorusiId==claim.Value).
					Include(x => x.Perdorusi)
					.ToList();
			}


			switch (statusi)
			{

				case "NePritje":
					porosite = porosite.Where(x => x.StatusiIPageses == Sd.StatusIPagesesNePritje).ToList();
					break;
				case "NeProces":
					porosite = porosite.Where(x => x.StatusiIPorosise == Sd.StatusNeProces).ToList();
					break;
				case "TeKompletuara":
					porosite = porosite.Where(x => x.StatusiIPorosise == Sd.StatusIDerguar).ToList();
					break;
				case "TeAprovuara":
					porosite = porosite.Where(x => x.StatusiIPorosise == Sd.StatusIAprovuar).ToList();
					break;
				case "TeGjitha":
					porosite = porosite.Where(x => x.StatusiIPageses == Sd.StatusIPagesesNePritje).ToList();
					break;
			}

			return Json(new { data = porosite });
		}
	}
}
