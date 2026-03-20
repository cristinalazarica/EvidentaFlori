namespace Floare
{
    public class Floare
    {
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Stoc { get; set; }

        public Floare()
        {
            Nume = string.Empty;
            Pret = 0;
            Stoc = 0;
        }

        public Floare(string nume, double pret, int stoc)
        {
            Nume = nume;
            Pret = pret;
            Stoc = stoc;
        }

        public string Info()
        {
            return $"Floare: {Nume}, Pret: {Pret}, Stoc: {Stoc}";
        }
    }
}