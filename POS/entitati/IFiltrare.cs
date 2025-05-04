using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entitati
{
    public interface IFiltrare
    {
        public IEnumerable<ProdusAbstract> Filtrare(
            IEnumerable<ProdusAbstract> elemente, 
            ICriteriu criteriu);
    }
}
