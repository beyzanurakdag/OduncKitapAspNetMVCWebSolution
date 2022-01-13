﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapAspNetMVCWebSolution_BLL.Managers
{
    
    public class KitapManager
    {
        //Global Alan
        OduncKitapDBEntities dbcontext = new OduncKitapDBEntities();
        public List<Kitaplar> TumAktifKitaplariGetir()
        {
            try
            {
                List<Kitaplar> kitaplar = new List<Kitaplar>();
                //TODO:page işlemi eklenecek
                kitaplar = dbcontext.Kitaplar.Where(x => !x.SilindiMi && x.StokAdeti > 0).ToList();
                return kitaplar;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool YeniKitapEkle(Kitaplar yeniKitap)
        {
            try
            {
                dbcontext.Kitaplar.Add(yeniKitap);
                dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
