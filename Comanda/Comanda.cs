namespace Comanda
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

        public string Info()
        {
            return $"Client: {NumeClient}, Floare: {NumeFloare}, Cantitate: {Cantitate}";
        }
    }
}