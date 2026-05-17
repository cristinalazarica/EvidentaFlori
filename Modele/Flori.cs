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
        public string Nume { get; set; }
        public double Pret { get; set; }
        public int Stoc { get; set; }
        public Culoare Culoare { get; set; }
        public Optiuni Optiuni { get; set; }
        public string TipFloare { get; set; }
        public DateTime DataAdaugare { get; set; }
        public DateTime DataActualizare { get; set; }

        public Floare()
        {
            Nume = string.Empty;
            Pret = 0;
            Stoc = 0;
            Culoare = Culoare.Alb;
            Optiuni = Optiuni.Nimic;
            TipFloare = "Buchet";
            DataAdaugare = DateTime.Today;
            DataActualizare = DateTime.Today;
        }

        public Floare(string nume, double pret, int stoc, Culoare culoare, Optiuni optiuni)
        {
            Nume = nume;
            Pret = pret;
            Stoc = stoc;
            Culoare = culoare;
            Optiuni = optiuni;
            TipFloare = "Buchet";
            DataAdaugare = DateTime.Today;
            DataActualizare = DateTime.Today;
        }

        public Floare(string nume, double pret, int stoc, Culoare culoare, Optiuni optiuni, string tipFloare, DateTime dataAdaugare)
        {
            Nume = nume;
            Pret = pret;
            Stoc = stoc;
            Culoare = culoare;
            Optiuni = optiuni;
            TipFloare = tipFloare;
            DataAdaugare = dataAdaugare;
            DataActualizare = DateTime.Today;
        }

        public Floare(string linieFisier)
        {
            string[] date = linieFisier.Split(';');

            Nume = date.Length > 0 ? date[0] : string.Empty;

            Pret = date.Length > 1 && double.TryParse(date[1], out double pret)
                ? pret
                : 0;

            Stoc = date.Length > 2 && int.TryParse(date[2], out int stoc)
                ? stoc
                : 0;

            Culoare = date.Length > 3 && int.TryParse(date[3], out int culoare)
                ? (Culoare)culoare
                : Culoare.Alb;

            Optiuni = date.Length > 4 && int.TryParse(date[4], out int optiuni)
                ? (Optiuni)optiuni
                : Optiuni.Nimic;

            TipFloare = date.Length > 5 ? date[5] : "Buchet";

            DataAdaugare = date.Length > 6 && DateTime.TryParse(date[6], out DateTime dataAdaugare)
                ? dataAdaugare
                : DateTime.Today;

            DataActualizare = date.Length > 7 && DateTime.TryParse(date[7], out DateTime dataActualizare)
                ? dataActualizare
                : DateTime.Today;
        }

        public string ConversieLaSirPentruFisier()
        {
            return $"{Nume};{Pret};{Stoc};{(int)Culoare};{(int)Optiuni};{TipFloare};{DataAdaugare};{DataActualizare}";
        }

        public string Info()
        {
            return $"Floare: {Nume}, Pret: {Pret}, Stoc: {Stoc}, Culoare: {Culoare}, Optiuni: {Optiuni}, Tip: {TipFloare}, Data: {DataAdaugare}";
        }
    }
}