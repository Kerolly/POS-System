using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;

namespace entitati
{
    public class Pachet:ProdusAbstract
    {
        List<ProdusAbstract> elemente_pachet = new List<ProdusAbstract>();

        public List<ProdusAbstract> Elemente_pachet { get => elemente_pachet; set => elemente_pachet = value; }

        public Pachet()
        {


        }
        public Pachet(uint id, string? nume, string? codIntern, int pret, string? categorie)
            : base(id, nume, codIntern, pret, categorie)
        {

        }

        public override string Descriere()
        {
            string descriere = "\nId: " + Id +
                "\nNume: " + Nume +
                "\nCod Intern: " + CodIntern +
                "\nPret: " + Pret +
                "\nCategorie: " + Categorie +
                "\n\nContinut pachet: ";

            foreach (ProdusAbstract elem in Elemente_pachet)
            {
                descriere +=  "\t\n" + elem.Descriere().Replace("\n", "\n\t") + "\n";
            };


            return descriere;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Pachet pachet)
            {
                return this.Nume == pachet.Nume &&
                    this.CodIntern == pachet.CodIntern;
                    
            }
            return false;
        }

        public void AddElement(ProdusAbstract element)
        {
            Elemente_pachet.Add(element);
        }

        
    }
}
