namespace Modele
{
    public class Client
    {
        public string Nume { get; set; }
        public int NrComenzi { get; set; }

        public Client()
        {
            Nume = "";
            NrComenzi = 0;
        }

        public Client(string nume, int nrComenzi)
        {
            Nume = nume;
            NrComenzi = nrComenzi;
        }

        public Client(string linieFisier)
        {
            var date = linieFisier.Split(';');

            Nume = date.Length > 0 ? date[0] : "";
            NrComenzi = date.Length > 1 && int.TryParse(date[1], out int nr) ? nr : 0;
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Nume};{NrComenzi}";
        }

        public override string ToString()
        {
            return $"{Nume} ({NrComenzi} comenzi)";
        }
    }
}