    using entitati;

    namespace app1
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                //Testare serializare serviciu
                /*Serviciu unServiciu = new Serviciu(12, "TestServ", "22", 122, "IT");         
                unServiciu.save2XML("test");

                Serviciu serviciuNou = new Serviciu();
                serviciuNou = serviciuNou.loadFromXml("test");

                serviciuNou.afisareServiciu();*/

                Meniu();
            

                // Testare suprascriere operatori

                //Testare produse
                //ProduseMgr produseTest = new ProduseMgr();

                //Console.WriteLine("Introdu un singur produs:");
                //Produs prod1 = produseTest.ReadUnProdus();
                /*Produs prod2 = produseTest.ReadUnProdus();

                Console.WriteLine($"Rezultat == : {prod1 == prod2} ");
                Console.WriteLine($"Rezultat != : {prod1 != prod2}");
                Console.WriteLine($"Rezultat Equals() : {prod1.Equals(prod2)}");


                //Testare servicii
                Console.WriteLine("Introdu un singur serviciu:");
                Produs serv1 = produseTest.ReadUnProdus();
                Produs serv2 = produseTest.ReadUnProdus();

                Console.WriteLine($"Rezultat == : {serv1 == serv2} ");
                Console.WriteLine($"Rezultat != : {serv1 != serv2}");
                Console.WriteLine($"Rezultat Equals() : {serv1.Equals(serv2)}");*/

                //Console.WriteLine(produseTest.Cautare(prod1));


            }

            static void Meniu()
            {
                int optiune;
                ProduseMgr mgrProduse = new ProduseMgr(); //instantiere
                ServiciiMgr mgrServicii = new ServiciiMgr(); //instantiere
                PachetMgr mgrPachete = new PachetMgr(); //instantiere

            do
            {
                Console.WriteLine("\n---------------------------------");
                Console.WriteLine("1. Intoducere produse/servicii de la tastatura");
                Console.WriteLine("2. Citire si afisare pachete de la tastatura");
                Console.WriteLine("3. Citire elemente din fisier");
                Console.WriteLine("4. Afisare produse si servicii");
                Console.WriteLine("5. Afisare pachete");
                Console.WriteLine("6. Afisare interogare linq");
                Console.WriteLine("7. Afisare produse/pachete dupa criterii");
                Console.WriteLine("8. Serializare elemente XML");
                Console.WriteLine("9. Deserializare elemente XML");
                Console.WriteLine("10. Serializare elemente JSON");
                Console.WriteLine("11. Deserializare elemente JSON");
                Console.WriteLine("0. Iesire program");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Introdu optiunea: ");
                optiune = int.Parse(Console.ReadLine());

                
                    switch (optiune)
                    {
                        case 1:
                            mgrProduse.CitireTastatura();
                            mgrServicii.CitireTastatura();
                            
                            break;

                        case 2:   
                            mgrPachete.CitireTastatura();
                            mgrServicii.SortarePacheteCrescatorPret();
                            mgrPachete.Write2Console(typeof(Pachet));
                        break;

                        case 3:
                            mgrProduse.CitireFisier();
                        Console.WriteLine("Elemente citite cu succes!");
                        break;

                        case 4:
                            mgrProduse.Write2Console(typeof(Produs));
                        
                        break;

                        case 5:
                            mgrPachete.SortarePacheteCrescatorPret();
                            mgrPachete.Write2Console(typeof(Pachet));
                            
                            break;

                        case 6:
                            mgrProduse.AfisareInterogareLinq();
                            
                            break;

                        case 7:
                                CriteriiMeniu();
                            break;

                        case 8:
                            mgrPachete.save2XML("elemente");

                            break;

                        case 9:
                        mgrPachete.loadFromXml("elemente");
                        break;

                        case 10:
                        mgrPachete.save2JSON("elemente");
                        break;

                        case 11:
                        mgrPachete.loadFromJson("elemente");
                        break;

                        case 0:
                            Console.WriteLine("Iesire program ...");
                            break;

                        default:
                            Console.WriteLine("Eroare, aceasta optiune nu exista!");
                            break;
                    }
                } while (optiune != 0);
            }

            static void CriteriiMeniu()
            {
            ProduseMgr mgrProduse = new ProduseMgr();
            var filtrare = new FiltrareCriteriu();
            int optiune;
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("\n1.Afisare dupa categorie");
            Console.WriteLine("\n2.Afisare dupa pret (mai mic)");
            Console.WriteLine("\n---------------------------------");
            Console.WriteLine("Introdu optiunea: ");
            optiune = int.Parse(Console.ReadLine());

            switch(optiune)
            {
                case 1:
                    Console.WriteLine("\nIntrodu dupa ce categorie: ");
                    string categorieSort = Console.ReadLine();
                    ICriteriu criteriuCategorie = new CriteriuCategorie(categorieSort);
                    //mgrProduse.FiltrareDupaCategorie(categorieSort);

                    mgrProduse.Filtru(criteriuCategorie);
                    break;

                case 2:
                    Console.WriteLine("\nIntrodu pretul maxim: ");
                    int pretMaxim = int.Parse(Console.ReadLine());
                    ICriteriu criteriuPret = new CriteriuPret(pretMaxim);
                    
                    mgrProduse.Filtru(criteriuPret);

                    break;

            }

        }
        
        }
    }
