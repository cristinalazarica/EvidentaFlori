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

        public Floare(string linie)
        {
            var d = linie.Split(';');

            Nume = d.Length > 0 ? d[0] : "";
            Pret = d.Length > 1 && double.TryParse(d[1], out var p) ? p : 0;
            Stoc = d.Length > 2 && int.TryParse(d[2], out var s) ? s : 0;
            Culoare = d.Length > 3 && int.TryParse(d[3], out var c) ? (Culoare)c : Culoare.Rosu;
            Optiuni = d.Length > 4 && int.TryParse(d[4], out var o) ? (Optiuni)o : Optiuni.Nimic;
            TipFloare = d.Length > 5 ? d[5] : "";
            DataAdaugare = d.Length > 6 && DateTime.TryParse(d[6], out var dt) ? dt : DateTime.Today;
        }

        public string ToFile()
        {
            return $"{Nume};{Pret};{Stoc};{(int)Culoare};{(int)Optiuni};{TipFloare};{DataAdaugare}";
        }

        public override string ToString()
        {
            return $"{Nume} - {Pret} lei ({Culoare})";
        }
    }
}