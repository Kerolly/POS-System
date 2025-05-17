using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using entitati;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace app1
{
    public abstract class ProduseMgrAbstract
    {

        /*protected ProdusAbstract[] elemente = new ProdusAbstract[100];*/
        protected static List<ProdusAbstract> elemente = new List<ProdusAbstract>();
        /*protected static ListaGen<ProdusAbstract> elemente = new entitati.ListaGen<ProdusAbstract> ();*/
        protected static int Id = 0;
        protected int CountElemente { get; set; } = 0;


        public uint IdGenerator()
        {
            return (uint)Interlocked.Increment(ref Id);
        }


        IEnumerable<ProdusAbstract> interogare_linq =
            from elem in elemente
            where elem.Categorie == "Tehnologia Informatiei"
            orderby elem.Nume
            select elem;


        public void AfisareInterogareLinq()
        {
            foreach (ProdusAbstract element in interogare_linq)
            {
                Console.WriteLine(element.Descriere());
            }
        }


        public abstract void ReadElemente(int nrElemente);
        public virtual void WriteElemente()
        {
            // afisam serviciile
            Console.WriteLine("\n-------------------");
            Console.WriteLine("Serviciile sunt:");
        }

        public void Write2Console(Type type)
        {
            /*for(int i = 0; i < CountElemente; i++)
            {
                Console.WriteLine(elemente[i].Descriere());
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                if (type ==  element.GetType())
                {
                    Console.WriteLine("\n-------------------");
                    Console.WriteLine(element.Descriere());
                } 
                else if (type == typeof(Produs) && (element is Produs || element is Serviciu) ||
                    type == typeof(Serviciu) && (element is Produs || element is Serviciu))
                {
                    Console.WriteLine("\n-------------------");
                    Console.WriteLine(element.Descriere());
                }


            }
        }

        public bool VerificareElemente(ProdusAbstract elemNou)
        {
            /*for (int i = 0; i < CountElemente; i++)
            {
                if (elemente[i].Equals(elemNou))
                    return false;
            }*/

            foreach (ProdusAbstract element in elemente)
            {
                if (element.Equals(elemNou))
                    return false;
            }

            return true;
        }

        

        public void InitListaFromXml()
        {
            //initializare fisier xml
            XmlDocument xmlDoc = new XmlDocument();

            //incarcare fisier
            xmlDoc.Load("D:\\Andrei Facultate\\Programare Orientata pe Obiecte\\Laborator\\Proiect\\POS\\app1\\Produse.xml");

            //selectare noduri
            XmlNodeList lista_noduri_produse = xmlDoc.SelectNodes("/elemente/Produs");
            XmlNodeList lista_noduri_servicii = xmlDoc.SelectNodes("/elemente/Serviciu");

            XmlNodeList lista_noduri_pachete = xmlDoc.SelectNodes("/elemente/Pachet");
            //XmlNodeList lista_noduri_produse_pachet = xmlDoc.SelectNodes("/elemente/Pachet/Produs");
            //XmlNodeList lista_noduri_servicii_pachet = xmlDoc.SelectNodes("/elemente/Pachet/Serviciu");


            //Produsele
            foreach (XmlNode nod in lista_noduri_produse)
            {
                string nume = nod["Nume"].InnerText;
                string codIntern = nod["CodIntern"].InnerText;
                string producator = nod["Producator"].InnerText;
                int pret = int.Parse(nod["Pret"].InnerText);
                string categorie = nod["Categorie"].InnerText;

                //adaugare in lista
                elemente.Add(new Produs(IdGenerator(), nume, codIntern, pret, categorie, producator));
            }


            //Serviciile
            foreach (XmlNode nod in lista_noduri_servicii)
            {
                string nume = nod["Nume"].InnerText;
                string codIntern = nod["CodIntern"].InnerText;
                int pret = int.Parse(nod["Pret"].InnerText);
                string categorie = nod["Categorie"].InnerText;

                //adaugare in lista
                elemente.Add(new Serviciu((uint)elemente.Count + 1, nume, codIntern, pret, categorie));
            }

            //Pachetele
            foreach(XmlNode nodPachet in lista_noduri_pachete)
            {
                string numePachet = nodPachet["Nume"].InnerText;
                string codInternPachet = nodPachet["CodIntern"].InnerText;
                string categoriePachet = nodPachet["Categorie"].InnerText;

                int pretTotalPachet = 0;

                Pachet pachetNou = new Pachet(IdGenerator(), numePachet, codInternPachet, 0, categoriePachet);

                XmlNodeList produsePachet = nodPachet.SelectNodes("Produs");
                XmlNodeList serviciiPachet = nodPachet.SelectNodes("Serviciu");

                foreach (XmlNode nodProdus in produsePachet)
                {   

                    string numeProdusPachet = nodProdus["Nume"].InnerText;
                    string codInternProdusPachet = nodProdus["CodIntern"].InnerText;
                    int pretProdusPachet = int.Parse(nodProdus["Pret"].InnerText);
                    string categorieProdusPachet = nodProdus["Categorie"].InnerText;
                    string producatorProdusPachet = nodProdus["Producator"].InnerText;

                    

                    Produs produsNou = new Produs(
                        IdGenerator(),
                        numeProdusPachet,
                        codInternProdusPachet,
                        pretProdusPachet,
                        categorieProdusPachet,
                        producatorProdusPachet);
                    

                    if (produsNou.canAddToPackage(pachetNou))
                    {
                        pachetNou.AddElement(produsNou);
                        pretTotalPachet += pretProdusPachet;
                    }
                    else          
                        break;
                    

                }

                foreach (XmlNode nodServiciu in serviciiPachet)
                {
                    string numeServiciuPachet = nodServiciu["Nume"].InnerText;
                    string codInternServiciuPachet = nodServiciu["CodIntern"].InnerText;
                    int pretServiciuPachet = int.Parse(nodServiciu["Pret"].InnerText);
                    string categorieServiciuPachet = nodServiciu["Categorie"].InnerText;


                    Serviciu serviciuNou = new Serviciu(
                        IdGenerator(),
                        numeServiciuPachet,
                        codInternServiciuPachet,
                        pretServiciuPachet,
                        categorieServiciuPachet);

                    if (serviciuNou.canAddToPackage(pachetNou))
                    {
                        pachetNou.AddElement(serviciuNou);
                        pretTotalPachet += pretServiciuPachet;
                    }
                    else
                        break;
                    
                }


                pachetNou.Pret = pretTotalPachet;
                elemente.Add(pachetNou);

            }
  
            

        }

        public void SortarePacheteCrescatorPret()
        {
            var pachete = elemente.OfType<Pachet>().ToList();

            /*foreach (var elem in elemente)
            {
                if (elem is Pachet)
                    elemente.Remove(elem);
            }*/

            elemente.RemoveAll(elm => elm is Pachet);

            pachete.Sort((a, b) => a.Pret.CompareTo(b.Pret));

            foreach (var elem in pachete)
            {
                elemente.Add(elem);
            }

        }


        public abstract void CitireTastatura();
        public abstract void CitireFisier();

        
        public void FiltrareDupaCategorie(string categorieSort)
        {
            var filtru = new FiltrareCriteriu();
            var criteriu = new CriteriuCategorie(categorieSort);
            var elementeFiltrate = filtru.Filtrare(elemente, criteriu).ToList();


            if (elementeFiltrate.Count() == 0)
            {
                Console.WriteLine("Nu exista elemente dupa acest criteriu!");
            }
            else
            {

                Console.WriteLine("\nRezultate filtrare: ");
                //Console.WriteLine($"Debug: Produse găsite după filtrare: {elementeFiltrate.Count}");
                foreach (var elem in elementeFiltrate)
                {
                    Console.WriteLine(elem.Descriere());
                }

            }
        }

        public void Filtru(ICriteriu criteriu)
        {
            var filtru = new FiltrareCriteriu();

            var elementeFiltrate = filtru.Filtrare(elemente, criteriu);

            if (elementeFiltrate.Count() == 0)
            {
                Console.WriteLine("Nu exista elemente dupa acest criteriu!");
            }
            else
            {

                Console.WriteLine("\nRezultate filtrare: ");
                //Console.WriteLine($"Debug: Produse găsite după filtrare: {elementeFiltrate.Count}");
                foreach (var elem in elementeFiltrate)
                {
                    Console.WriteLine(elem.Descriere());
                }

            }
        }

        //Serializare 
        public void save2XML(string fileName)
        {
            Type[] prodAbstractTypes = new Type[3];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);
            prodAbstractTypes[2] = typeof(Pachet);

            try { 
            XmlSerializer xs = new XmlSerializer(typeof(List<ProdusAbstract>), prodAbstractTypes);
            StreamWriter writer = new StreamWriter(fileName + ".xml");
            xs.Serialize(writer, elemente);
            writer.Close();
            Console.WriteLine("Salvat cu succes!");
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Eroare la serializare:\n" + ex.Message);
            }catch(IOException ex)
            {
                Console.WriteLine("Eroare la scrierea fisierului:\n" + ex.Message);
            }
        }


        //deserializare
        public ProdusAbstract? loadFromXml(string fileName)
        {

            Type[] prodAbstractTypes = new Type[3];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);
            prodAbstractTypes[2] = typeof(Pachet);

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<ProdusAbstract>), prodAbstractTypes);

                FileStream fs = new FileStream(fileName + ".xml", FileMode.Open);
                XmlReader reader = new XmlTextReader(fs);

                var loadedElem = (List<ProdusAbstract>?)xs.Deserialize(reader);
                elemente.AddRange(loadedElem);
                fs.Close();
                Console.WriteLine("Deserializat cu succes!");
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine("Eroare la deserializare:\n" + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Eroare la citirea fisierului:\n" + ex.Message);
            }

            return null;
        }


        public void save2JSON(string fileName)
        {
            Type[] prodAbstractTypes = new Type[3];
            prodAbstractTypes[0] = typeof(Serviciu);
            prodAbstractTypes[1] = typeof(Produs);
            prodAbstractTypes[2] = typeof(Pachet);

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };


            string Json = JsonSerializer.Serialize(elemente, options);
            File.WriteAllText(fileName + ".json", Json);

            Console.WriteLine("Salvat cu succes!");
        }

        public void loadFromJson(string fileName)
        {   string jsonContent = File.ReadAllText(fileName + ".json");

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                TypeInfoResolver = new DefaultJsonTypeInfoResolver(), //polymorphic deser
                PropertyNameCaseInsensitive = true
            };

            var loadedElem = JsonSerializer.Deserialize<List<ProdusAbstract>>(jsonContent);

            elemente.AddRange(loadedElem);

            Console.WriteLine("Deserializat cu succes!");
        }



    }
}
