namespace entitati
{
    public class Produs : ProdusAbstract
    {

        private String? producator;// producator 

       
        public Produs()
        {

        }

        public Produs(uint id, string? nume, string? codIntern, int pret, string? categorie, string? producator)
            : base(id, nume, codIntern, pret, categorie)
        {

            Producator = producator;
        }


        public string? Producator { get => producator; set => producator = value; }


        public void afisareProdus()
        {
            Console.WriteLine("\nId: " + Id +
                "\nNume: " + Nume +
                "\nCod Intern: " + CodIntern +
                "\nProducator: " + Producator);

        }

        public bool VerificareProdus(Produs produs)
        {
            return this.Nume == produs.Nume &&
                   this.CodIntern == produs.CodIntern &&
                   this.Producator == produs.Producator;

        }

        //Suprascriem metoda abstracta
        public override string Descriere()
        {
            return "\nId: " + Id +
                "\nNume: " + Nume +
                "\nCod Intern: " + CodIntern +
                "\nPret: " + Pret +
                "\nCategorie: " + Categorie +
                "\nProducator: " + Producator;
        }

        //Suprascriem metoda virtuala 
        public override string AltaDescriere()
        {
            return "\nId: " + Id + base.AltaDescriere() +
                "\nProducator: " + Producator;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Produs produs)
            {
                return this.Nume == produs.Nume &&
                    this.CodIntern == produs.CodIntern &&
                    this.Producator == produs.Producator;
            }
            return false;
        }

        public static bool operator ==(Produs? prod1, Produs? prod2)
        {
            if (ReferenceEquals(prod1, prod2))
                return true;

            if (prod1 is null || prod2 is null)
                return false;

            return prod1.Equals(prod2);
        }

        public static bool operator !=(Produs? prod1, Produs? prod2)
        {
            return !(prod1 == prod2);

        }


        



    }
}
