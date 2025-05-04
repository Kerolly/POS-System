using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    public class CriteriuPret : ICriteriu
    {
        public int PretMaxim { get; set; }

        public CriteriuPret(int pretMaxim)
        {
            PretMaxim = pretMaxim;
        }

        public bool IsIndeplinit(ProdusAbstract element)
        {
            if (element.Pret <= PretMaxim)
                 return true;
            return false;
            
        }
    }
}
