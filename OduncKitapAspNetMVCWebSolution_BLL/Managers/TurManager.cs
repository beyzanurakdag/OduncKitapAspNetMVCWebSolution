using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OduncKitapAspNetMVCWebSolution_BLL.Managers
{
    public class TurManager
    {
        //Global Alan
        OduncKitapDBEntities dbcontext = new OduncKitapDBEntities();
        public List<Turler> AktifTumTurleriGetir()
        {
            try
            {
                List<Turler> turler = new List<Turler>();
                turler = dbcontext.Turler.Where(x => x.SilindiMi == false).ToList();
                return turler;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
