using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using entitati;

namespace app1
{
    public class ProduseMgr:ProduseMgrAbstract
    {
        public override void ReadElemente(int nrProduse)
        {   
            // declarare variabile produse
            string? nume, producator, codIntern, categorie;
            int pret;

            // citim produsele 
            for (int cnt = 0; cnt < nrProduse; cnt++)
            {   
                Console.WriteLine("\nIntrodu un produs");
                Console.Write("Numele:");
                nume = Console.ReadLine();

                Console.Write("Codul intern:");
                codIntern = Console.ReadLine();

                Console.Write("Pretul:");
                pret = int.Parse(Console.ReadLine());

                Console.Write("Categorie:");
                categorie = Console.ReadLine();

                Console.Write("Producator:");
                producator = Console.ReadLine();

                // instantierea unui Produs 
                Produs prodNou = new Produs((uint)CountElemente, nume, codIntern, pret, categorie, producator);

                if (VerificareElemente(prodNou) == true)
                {
                    elemente.Add(prodNou);
                    CountElemente++;
                }
                else
                    Console.WriteLine("\nProdusul exista deja!");
            }
        }

        public override void WriteElemente()
        {
            base.WriteElemente();

            /*for (int i = 0; i < CountElemente; i++)
            {
                Produs prod = (Produs)elemente[i];
                Console.WriteLine(prod.Descriere());
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                Produs prod = (Produs)element;
                Console.WriteLine(prod.Descriere());
            }
        }

        public bool VerificareProduse(Produs prodNou)
        {
            /*for (int i = 0; i < CountElemente; i++)
            {
                if (((Produs)elemente[i]).VerificareProdus(prodNou))
                    return false;
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                if (((Produs)element).VerificareProdus(prodNou))
                    return false;
            }

            return true;
        }

        public Produs ReadUnProdus()
        {
            Console.Write("Numele: ");
            string? nume = Console.ReadLine();

            Console.Write("Cod Intern: ");
            string? codIntern = Console.ReadLine();

            Console.Write("Pretul:");
            int pret = int.Parse(Console.ReadLine());

            Console.Write("Categorie:");
            string? categorie = Console.ReadLine();

            Console.Write("Producator: ");
            string? producator = Console.ReadLine();

            return new Produs((uint)CountElemente, nume, codIntern, pret, categorie, producator);

        }

        public bool Cautare(Produs prodCautat)
        {
            /*for(int i = 0; i < CountElemente; i++)
            {
                if (elemente[i] == prodCautat)
                    return true;
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                if (element == prodCautat)
                    return true;
            }

            return false;
        }

        public bool Cautare(string? numeCautat)
        {
            /*for (int i = 0; i < CountElemente; i++)
            {
                if (elemente[i].Nume == numeCautat)
                    return true;
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                if (element.Nume == numeCautat)
                    return true;
            }
            return false;
        }

        

        public override void CitireFisier()
        {
            InitListaFromXml();
        }

        public override void CitireTastatura()
        {
            //introducere nr de servicii & produse
            Console.Write("Nr. produse:");
            int nrProduse = int.Parse(Console.ReadLine() ?? string.Empty);

            //citire & afisare produse din ProduseMgr
            ReadElemente(nrProduse);
            Write2Console(typeof(Produs));
        }



        
    }
}
