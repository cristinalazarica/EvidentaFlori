namespace Modele
{
    public class Client
    {
        public string Nume { get; set; }
        public int NrComenzi { get; set; }

        public Client()
        {
            Nume = string.Empty;
            NrComenzi = 0;
        }

        public Client(string nume, int nrComenzi)
        {
            Nume = nume;
            NrComenzi = nrComenzi;
        }

        public Client(string linieFisier)
        {
            string[] date = linieFisier.Split(';');
            Nume = date[0];
            NrComenzi = int.Parse(date[1]);
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Nume};{NrComenzi}";
        }

        public string Info()
        {
            return $"Client: {Nume}, NrComenzi: {NrComenzi}";
        }
    }
}