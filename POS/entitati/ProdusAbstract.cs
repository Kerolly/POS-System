using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Text.Json.Serialization;

namespace entitati
{

    [JsonDerivedType(typeof(Produs), "produs")]
    [JsonDerivedType(typeof(Serviciu), "serviciu")]
    [JsonDerivedType(typeof(Pachet), "pachet")]

    public abstract class ProdusAbstract: IPackageable
    {

        [XmlElement("Numele")]
        public string? Nume { get; set; }

        [XmlElement("CodulIntern")]
        public string? CodIntern { get; set; }

        [XmlElement("ID")]
        public uint Id { get; set; }
        public int Pret { get; set; }
        public string Categorie { get; set; }


        public ProdusAbstract()
        {

        }

        public ProdusAbstract(uint id, string? nume, string? codIntern, int pret, string? categorie)
        {
            Nume = nume;
            CodIntern = codIntern;
            Id = id;
            Pret = pret;
            Categorie = categorie;
        }

        //Metoda abstracta pt Descriere
        public abstract string Descriere();

        //Metoda virtuala pt Descriere
        public virtual string AltaDescriere()
        {
            return "\nNume: " + Nume +
                "\nCod Intern: " + CodIntern;
        }

        //Metoda suprascriere pentru Equals()
        public abstract override bool Equals(object? obj);

        public virtual bool canAddToPackage(Pachet pachet)
        {
            /*int produse = 0;
            int servicii = 0;

            foreach (var elem in pachet.Elemente_pachet)
            {
                if (elem is Produs)
                    produse++;
                else if (elem is Serviciu)
                    servicii++;
            }

            if (this is Produs && produse < 2)
                return true;
            else if (this is Serviciu && servicii < 3)
                return true;
            else
                return false;*/

            if (this is Produs)
                return pachet.Elemente_pachet.Count(e => e is Produs) < 2;

            if (this is Serviciu)
                return pachet.Elemente_pachet.Count(e => e is Serviciu) < 3;

            return false;
        }
    }
}
