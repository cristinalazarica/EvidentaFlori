namespace Modele
{
    public enum Culoare
    {
        Rosu,
        Alb,
        Galben,
        Mov,
        Albastru
    }

    [Flags]
    public enum Optiuni
    {
        Nimic = 0,
        Parfumata = 1,
        Decorativa = 2
    }

    public class Floare
    {
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Stoc { get; set; }

        public Culoare Culoare { get; set; }
        public Optiuni Optiuni { get; set; }

        public Floare()
        {
            Nume = string.Empty;
            Pret = 0;
            Stoc = 0;
            Culoare = Culoare.Alb;
            Optiuni = Optiuni.Nimic;
        }

        public Floare(string nume, double pret, int stoc, Culoare culoare, Optiuni optiuni)
        {
            Nume = nume;
            Pret = pret;
            Stoc = stoc;
            Culoare = culoare;
            Optiuni = optiuni;
        }

        public string Info()
        {
            return $"Floare: {Nume}, Pret: {Pret}, Stoc: {Stoc}, Culoare: {Culoare}, Optiuni: {Optiuni}";
        }
    }
}