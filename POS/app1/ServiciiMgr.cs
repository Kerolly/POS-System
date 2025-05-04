using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entitati;

namespace app1
{
    public class ServiciiMgr:ProduseMgrAbstract
    {
        public override void ReadElemente(int nrServicii)
        {
            //declarare variablie servicii
            string? nume, codIntern, categorie;
            int pret;

            //citim serviciile
            for (int cnt = 0; cnt < nrServicii; cnt++)
            {
                Console.WriteLine("\nIntrodu un servici");
                Console.Write("Numele serviciului:");
                nume = Console.ReadLine();

                Console.Write("Codul intern:");
                codIntern = Console.ReadLine();

                Console.Write("Pretul:");
                pret = int.Parse(Console.ReadLine());

                Console.Write("Categorie:");
                categorie = Console.ReadLine();

                // instantierea unui Serviciu 
                Serviciu servNou = new Serviciu((uint)CountElemente, nume, codIntern, pret, categorie);

                if (VerificareElemente(servNou) == true)
                {
                    elemente.Add(servNou);
                    CountElemente++;
                }

                else
                    Console.WriteLine("\nServiciul exista deja!");

            }
        }


        public override void WriteElemente()
        {
            base.WriteElemente();

            /*for (int i = 0; i < CountElemente; i++)
            {
                Serviciu serv = (Serviciu)elemente[i];
                Console.WriteLine(serv.AltaDescriere());
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                Serviciu serv = (Serviciu)element;
                Console.WriteLine(serv.AltaDescriere());
            }
        }

        public bool VerificareServicii(Serviciu servNou)
        {
            /*for (int i = 0; i < CountElemente; i++)
            {
                if (((Serviciu)elemente[i]).VerificareServiciu(servNou))
                    return false;
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                if (((Serviciu)element).VerificareServiciu(servNou))
                    return false;
            }

            return true;
        }

        //Citire un serviciu
        public Serviciu ReadUnServiciu()
        {
            Console.Write("Numele: ");
            string? nume = Console.ReadLine();

            Console.Write("Cod Intern: ");
            string? codIntern = Console.ReadLine();

            Console.Write("Pretul:");
            int pret = int.Parse(Console.ReadLine());

            Console.Write("Categorie:");
            string? categorie = Console.ReadLine();

            return new Serviciu((uint)CountElemente, nume, codIntern, pret, categorie);

        }

        public override void CitireFisier()
        {
            InitListaFromXml();
        }

        public override void CitireTastatura()
        {
            //introducere nr de servicii & produse
            Console.Write("\nNr. servicii:");
            int nrServicii = int.Parse(Console.ReadLine() ?? string.Empty);

            //citire & afisare produse din ProduseMgr
            this.ReadElemente(nrServicii);
            Write2Console(typeof(Serviciu));
        }
    }
}
