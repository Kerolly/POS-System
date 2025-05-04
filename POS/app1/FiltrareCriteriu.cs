using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    public class FiltrareCriteriu : IFiltrare
    {
        public IEnumerable<ProdusAbstract>Filtrare(IEnumerable<ProdusAbstract> elemente, ICriteriu criteriu)
        {
            /*foreach(var elem in elemente)
            {
                if (criteriu.IsIndeplinit(elem))
                    yield return elem;
            }*/

            IEnumerable<ProdusAbstract> interogare_linq =
            from elem in elemente
            where criteriu.IsIndeplinit(elem)
            orderby elem.Pret
            select elem;

            return interogare_linq;

        }
    }
}
