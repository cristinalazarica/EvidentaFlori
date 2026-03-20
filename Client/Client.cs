namespace Client
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

        public string Info()
        {
            return $"Client: {Nume}, NrComenzi: {NrComenzi}";
        }
    }
}