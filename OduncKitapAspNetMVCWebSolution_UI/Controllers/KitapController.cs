using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OduncKitapAspNetMVCWebSolution_BLL;
using OduncKitapAspNetMVCWebSolution_BLL.Managers;
using OduncKitapAspNetMVCWebSolution_UI.Models;

namespace OduncKitapAspNetMVCWebSolution_UI.Controllers
{
    public class KitapController : Controller
    {
        //Global Alan
        KitapManager myKitapManager = new KitapManager();
        YazarManager myYazarManager = new YazarManager();
        TurManager myTurManager = new TurManager();
        private const int pageSize= 10;
        // GET: Kitap
        public ActionResult Index(int? page=1)
        {
            try
            {
                //Paging 1.yöntem:Bu yöntem en klasik yöntemdir.
                List<Kitaplar> kitaplist = myKitapManager.TumAktifKitaplariGetir().Skip((page.Value<1 ? 1:page.Value-1)*pageSize
                    ).Take(pageSize).ToList();
                var total = myKitapManager.TumAktifKitaplariGetir().Count();
                ViewBag.ToplamSayfa = (int)Math.Ceiling(total / (double)pageSize);
                ViewBag.Suan = page;
                ViewBag.PageSize = pageSize;
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
        [HttpGet]
        public ActionResult Ekle()
        {
            //DisplayMember ve ValueMember
            List<SelectListItem> turListesi = new List<SelectListItem>();
            myTurManager.AktifTumTurleriGetir().ForEach(x=>
            turListesi.Add(new SelectListItem() { 
            Text=x.TurAdi,
            Value=x.Id.ToString()
            }));
            ViewBag.TurListesi = turListesi;

            List<SelectListItem> yazarListesi = new List<SelectListItem>();
            myYazarManager.TumAktifYazarlariGetir().ForEach(x =>
            yazarListesi.Add(new SelectListItem()
            {
                Text = x.YazarAdi + " " + x.YazarSoyadi,
                Value = x.Id.ToString()
            })) ;
            ViewBag.YazarListesi = yazarListesi;
            return View(new KitapViewModel());
        }       
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Ekle(KitapViewModel yeniKitap)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniKitap);
                }
                Kitaplar eklenecekKitap = new Kitaplar()
                {
                    KayitTarihi = DateTime.Now,
                    KitapAdi = yeniKitap.KitapAdi,
                    SayfaSayisi = yeniKitap.SayfaSayisi,
                    StokAdeti = yeniKitap.StokAdeti,
                    YazarId = yeniKitap.YazarId,
                    TurId = yeniKitap.TurId
                };
                //Resim null değilse sisteme kaydolacak
                if (yeniKitap.Resim != null
                    && yeniKitap.Resim.ContentType.Contains("image")
                    && yeniKitap.Resim.ContentLength > 0
                    )
                {
                    //string filename = Path
                    //    .GetFileNameWithoutExtension(yeniKitap.Resim.FileName);
                    string filename = SiteSettings.CharacterFormatConverter(yeniKitap.KitapAdi).ToLower();
                    string extName = Path
                        .GetExtension(yeniKitap.Resim.FileName);
                    //
                    filename += "-" + Guid.NewGuid()
                        .ToString().Replace("-", "");
                    var directoryPath =
                        Server.MapPath($"~/BookImages/");
                    var filePath = Server
                        .MapPath($"~/BookImages/")
                        + filename + extName;
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    yeniKitap.Resim.SaveAs(filePath);
                    eklenecekKitap.ResimLink =
                        @"/BookImages/" + filename + extName;
                }

                //managerı çağırabiliriz.
                if (myKitapManager.YeniKitapEkle(eklenecekKitap))
                {
                    return RedirectToAction("Index", "Kitap");
                }
                else
                {
                    ModelState.AddModelError("", "Giriş işlemlerinizi eksiksiz tamamlayınız!");
                    return View(yeniKitap);
                }
            }
            catch (Exception ex)
            {
                // UI'da ex.Message'lar gönderilmez. exception mesajları loglanır. Ama biz şimdilik geçici olarak bir hata olursa görmek amacıyla yazıverdik.

                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!" + ex.Message);
                //TO DO: ex.Message loglanabilir
                return View(yeniKitap);
            }
        }




    }
}