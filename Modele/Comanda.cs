namespace Modele
{
    public class Comanda
    {
        public string NumeClient { get; set; }
        public string NumeFloare { get; set; }
        public int Cantitate { get; set; }

        public Comanda()
        {
            NumeClient = string.Empty;
            NumeFloare = string.Empty;
            Cantitate = 0;
        }

        public Comanda(string numeClient, string numeFloare, int cantitate)
        {
            NumeClient = numeClient;
            NumeFloare = numeFloare;
            Cantitate = cantitate;
        }

        public Comanda(string linieFisier)
        {
            string[] date = linieFisier.Split(';');
            NumeClient = date[0];
            NumeFloare = date[1];
            Cantitate = int.Parse(date[2]);
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{NumeClient};{NumeFloare};{Cantitate}";
        }

        public string Info()
        {
            return $"Client: {NumeClient}, Floare: {NumeFloare}, Cantitate: {Cantitate}";
        }
    }
}