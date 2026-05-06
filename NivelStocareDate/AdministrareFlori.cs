using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modele;

namespace NivelStocareDate
{
    public class AdministrareFlori
    {
        private string numeFisier;

        public AdministrareFlori(string numeFisier)
        {
            this.numeFisier = numeFisier;

            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public void AdaugaFloare(Floare floare)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(floare.ConversieLaSirPentruFisier());
            }
        }

        public List<Floare> GetFlori()
        {
            List<Floare> flori = new List<Floare>();

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string? linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    flori.Add(new Floare(linie));
                }
            }

            return flori;
        }

        public Floare? CautaDupaNume(string nume)
        {
            return GetFlori()
                .FirstOrDefault(f => f.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase));
        }

        public bool ModificaFloare(string numeCautat, Floare floareNoua)
        {
            var flori = GetFlori();
            bool gasit = false;

            for (int i = 0; i < flori.Count; i++)
            {
                if (flori[i].Nume.Equals(numeCautat, StringComparison.OrdinalIgnoreCase))
                {
                    flori[i] = floareNoua;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
                RescrieFisier(flori);

            return gasit;
        }

        // ✅ FIX CERUT (DELETE)
        public void StergeFloare(string nume)
        {
            var flori = GetFlori()
                .Where(f => !f.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                .ToList();

            RescrieFisier(flori);
        }

        private void RescrieFisier(List<Floare> flori)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (var f in flori)
                    sw.WriteLine(f.ConversieLaSirPentruFisier());
            }
        }
    }
}