using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace entitati
{
    [XmlRoot("ServiciuParticularizat")]
    public class Serviciu:ProdusAbstract, IPackageable
    {

      

        public Serviciu()
        {

        }
        
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
            if (this is Produs)
                return pachet.Elemente_pachet.Count(e => e is Produs) < 2;

            if (this is Serviciu)
                return pachet.Elemente_pachet.Count(e => e is Serviciu) < 3;

            return false;
        }

        //Serializare serviciu
        public void save2XML(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Serviciu));
            StreamWriter sw = new StreamWriter(fileName + ".xml");
            xs.Serialize(sw, this);
            sw.Close();
            Console.WriteLine("Salvat cu succes!");
        }


        //deserializare serviciu
        public Serviciu? loadFromXml(string fileName)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Serviciu));
            
            FileStream fs = new FileStream(fileName + ".xml", FileMode.Open);
            XmlReader reader = new XmlTextReader(fs);

            Serviciu? serviciu = (Serviciu?)xs.Deserialize(reader);
            fs.Close();
            Console.WriteLine("Deserializat cu succes!");
            return serviciu;

        }



    }

    
}
