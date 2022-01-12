using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OduncKitapAspNetMVCWebSolution_BLL;
using OduncKitapAspNetMVCWebSolution_BLL.Managers;

namespace OduncKitapAspNetMVCWebSolution_UI.Controllers
{
    public class KitapIslemleriController : Controller
    {
        //Global Alan
        KitapManager myKitapManager = new KitapManager();
        // GET: KitapIslemleri
        public ActionResult Index()
        {
            try
            {
                List<Kitaplar> kitaplist = myKitapManager.TumAktifKitaplariGetir();
                ViewBag.KitapListCount = 0;
                if (kitaplist.Count>0)
                {
                    ViewBag.KitapListCount = kitaplist.Count;
                }
                return View(kitaplist);
            }
            catch (Exception ex) 
            {

                ViewBag.HataMesaji = "Beklenmedik bir hata oluştu!";
                return View();
            }
            
        }
    }
}