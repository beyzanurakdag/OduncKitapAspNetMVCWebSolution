using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OduncKitapAspNetMVCWebSolution_BLL.Managers;

namespace OduncKitapAspNetMVCWebSolution_UI.Controllers
{
    public class DashboardController : Controller
    {
        KitapManager myKitapManager = new KitapManager();
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewBag.BugunEklenenKitapSayisi = myKitapManager.TumAktifKitaplariGetir().Where(x => x.KayitTarihi > DateTime.Now.AddDays(-1) && x.KayitTarihi < DateTime.Now.AddDays(1)).Count();
            return View();
        }
    }
}