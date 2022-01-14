using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OduncKitapAspNetMVCWebSolution_BLL.Managers;

namespace OduncKitapAspNetMVCWebSolution_UI.Controllers
{
    public class PartialsController : Controller
    {
        //Global Alan
        KitapManager myKitapManager = new KitapManager();
        //Partial'lar kısmi görünüm anlamına gelmektedir.
        //Tüm partial'ları tek bir yerden yönetmek amacıyla partialcontroller oluşturduk.
        //İsterseniz mevcutta yer alan controllerlarınız içine de partial oluşturabilirsiniz.
        public PartialViewResult MenuPartialResult()
        {
            int toplamKitapSayisi = myKitapManager.TumAktifKitaplariGetir().Count();
            TempData["ToplamKitapSayisi"] = toplamKitapSayisi;
            return PartialView("_PartialMenu");
        }
    }
}