using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OduncKitapAspNetMVCWebSolution_BLL;
using OduncKitapAspNetMVCWebSolution_BLL.Managers;

namespace OduncKitapAspNetMVCWebSolution_UI.Controllers
{
    public class TurController : Controller
    {
        //Global Alan
        TurManager myTurManager = new TurManager();
        // GET: Tur
        public ActionResult Index()
        {
            //Tüm türler gelecek.
            try
            {
                List<Turler> turlist = myTurManager.AktifTumTurleriGetir();
                ViewBag.TurListCount = 0;
                if (turlist.Count>0)
                {
                    ViewBag.TurListCount = turlist.Count;
                }
                return View(turlist);
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}