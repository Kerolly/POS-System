using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    public class PachetMgr:ProduseMgrAbstract
    {
        

        public override void ReadElemente(int nrPachete)
        {
            //declarare variablie
            string? nume, codIntern, categorie;
            int pret = 0, nrProdusePachet, nrServiciiPachet;

            //citim 
            for (int cnt = 0; cnt < nrPachete; cnt++)
            {
                Console.WriteLine("\nIntrodu un pachet");
                Console.Write("Numele pachetului:");
                nume = Console.ReadLine();

                Console.Write("Codul intern:");
                codIntern = Console.ReadLine();

                Console.Write("Categorie:");
                categorie = Console.ReadLine();

                Console.WriteLine("Cate produse sa fie in pachet (max 2 produse): ");
                nrProdusePachet = int.Parse(Console.ReadLine());

                Console.WriteLine("Cate servicii sa fie in pachet (max 3 servicii): ");
                nrServiciiPachet = int.Parse(Console.ReadLine());

                // instantierea unui pachet 
                Pachet pachetNou = new Pachet((uint)CountElemente, nume, codIntern, 0, categorie);

                //citire produse pachet
                for (int i = 0; i < nrProdusePachet; i++)
                {
                    Console.WriteLine("Introdu produsul: ");

                    ProduseMgr produs = new ProduseMgr();

                    IPackageable produsNou = produs.ReadUnProdus();

                    //verificare addToPackage
                    if (produsNou is ProdusAbstract produsAbstract && 
                        produsAbstract.canAddToPackage(pachetNou))
                    {
                        pret += produsAbstract.Pret;
                        pachetNou.AddElement(produsNou);
                    }
                    else
                        Console.WriteLine("\nMaxim 2 produse!");
                }

                //citire servicii pachet
                for (int i = 0; i < nrServiciiPachet; i++)
                {
                    Console.WriteLine("Introdu serviciul: ");

                    ServiciiMgr serviciu = new ServiciiMgr();

                    IPackageable serviciuNou = serviciu.ReadUnServiciu();
                    pachetNou.AddElement(serviciuNou);

                    if (serviciuNou is ProdusAbstract serviciuAbstract &&
                        serviciuAbstract.canAddToPackage(pachetNou))
                        pret += serviciuAbstract.Pret;
                    else
                        Console.WriteLine("\nMaxim 3 servicii!");

                    
                }



                if (VerificareElemente(pachetNou) == true)
                {
                    elemente.Add(pachetNou);
                    CountElemente++;
                }

                else
                    Console.WriteLine("\nPachetul exista deja!");

                
                //Actualizare pretul pachetului
                pachetNou.Pret = pret;

            }

            
            
        }

        


        public override void CitireFisier()
        {
            InitListaFromXml();
        }

        public override void CitireTastatura()
        {
            //introducere nr de servicii & produse
            Console.Write("\nNr. pachete:");
            int nrPachete = int.Parse(Console.ReadLine() ?? string.Empty);

            //citire & afisare pachete din PachetMgr
            this.ReadElemente(nrPachete);
           
        }
    }
}
