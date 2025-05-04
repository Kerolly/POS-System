using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entitati
{
    public class Serviciu:ProdusAbstract
    {
        
        public Serviciu(uint id, string? nume, string? codIntern, int pret, string? categorie)
            :base(id, nume, codIntern, pret, categorie)
        {
           
        }

        public void afisareServiciu()
        {
            Console.WriteLine("\nId: " + Id +
                "\nNume: " + Nume +
                "\nCod Intern: " + CodIntern);

        }


        // Verificare serviciu
        public bool VerificareServiciu(Serviciu serviciu)
        {
            return this.Nume == serviciu.Nume &&
               this.CodIntern == serviciu.CodIntern;

        }


        //Suprascriem metoda abstracta
        public override string Descriere()
        {
            return "\nId: " + Id +
                "\nNume: " + Nume +
                "\nCod Intern: " + CodIntern +
                "\nPret: " + Pret +
                "\nCategorie: " + Categorie;
        }

        //Suprascriem metoda virtuala
        public override string AltaDescriere()
        {
            return "\nId: " + Id + base.AltaDescriere();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Serviciu produs)
            {
                return this.Nume == produs.Nume &&
                    this.CodIntern == produs.CodIntern;
                    
            }
            return false;
        }

        public static bool operator ==(Serviciu? serv1, Serviciu? serv2)
        {
            if (ReferenceEquals(serv1, serv2))
                return true;

            if (serv1 is null || serv2 is null)
                return false;

            return serv1.Equals(serv2);
        }

        public static bool operator !=(Serviciu? serv1, Serviciu? serv2)
        {
            return !(serv1 == serv2);

        }

        public override bool canAddToPackage(Pachet pachet)
        {
            return true;
        }

    }

    
}
