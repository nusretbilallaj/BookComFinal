using System.Net;
using System.Security.Claims;
using BookCommerceCustom1.Data;
using BookCommerceCustom1.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BookCommerceCustom1.ViewComponents
{
    public class ShportaViewComponent:ViewComponent
    {
        private readonly Konteksti _konteksti;

        public ShportaViewComponent(Konteksti konteksti)
        {
            _konteksti = konteksti;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim!=null)
            {
                if (HttpContext.Session.GetInt32(Sd.ShportNeSession)!=null)
                {
                    return View((int)HttpContext.Session.GetInt32(Sd.ShportNeSession));
                }
                else
                {
                    HttpContext.Session.SetInt32(Sd.ShportNeSession,
                        _konteksti.Shportat.Where(x=>x.PerdorusiId==claim.Value).ToList().Count);
                    return View((int)HttpContext.Session.GetInt32(Sd.ShportNeSession));
                }
            }
            else
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
