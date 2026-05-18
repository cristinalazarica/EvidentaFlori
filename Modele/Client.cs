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

            Nume = date.Length > 0 ? date[0] : string.Empty;
            NrComenzi = date.Length > 1 && int.TryParse(date[1], out int nr) ? nr : 0;
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