namespace Modele
{
    public class Comanda
    {
        public string NumeClient { get; set; }
        public string NumeFloare { get; set; }
        public int Cantitate { get; set; }

        public Comanda()
        {
            NumeClient = "";
            NumeFloare = "";
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
            var date = linieFisier.Split(';');

            NumeClient = date.Length > 0 ? date[0] : "";
            NumeFloare = date.Length > 1 ? date[1] : "";
            Cantitate = date.Length > 2 && int.TryParse(date[2], out int c) ? c : 0;
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{NumeClient};{NumeFloare};{Cantitate}";
        }

        public override string ToString()
        {
            return $"{NumeClient} - {NumeFloare} ({Cantitate})";
        }
    }
}