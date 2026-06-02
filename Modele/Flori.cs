using System;

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
        public string Nume { get; set; } = "";
        public double Pret { get; set; }
        public int Stoc { get; set; }
        public Culoare Culoare { get; set; }
        public Optiuni Optiuni { get; set; }
        public string TipFloare { get; set; } = "";
        public DateTime DataAdaugare { get; set; }

        public Floare() { }

        public Floare(string nume, double pret, int stoc,
            Culoare culoare, Optiuni optiuni, string tip, DateTime data)
        {
            Nume = nume ?? "";
            Pret = pret;
            Stoc = stoc;
            Culoare = culoare;
            Optiuni = optiuni;
            TipFloare = tip ?? "";
            DataAdaugare = data;
        }

        public Floare(string linie)
        {
            var d = linie.Split(';');

            Nume = d[0];
            Pret = double.Parse(d[1]);
            Stoc = int.Parse(d[2]);
            Culoare = (Culoare)int.Parse(d[3]);
            Optiuni = (Optiuni)int.Parse(d[4]);
            TipFloare = d[5];
            DataAdaugare = DateTime.Parse(d[6]);
        }

        public string ToFile()
        {
            return $"{Nume};{Pret};{Stoc};{(int)Culoare};{(int)Optiuni};{TipFloare};{DataAdaugare}";
        }
    }
}