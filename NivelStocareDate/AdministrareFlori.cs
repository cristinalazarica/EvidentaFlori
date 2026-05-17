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
                string? linieFisier;

                while ((linieFisier = sr.ReadLine()) != null)
                {
                    flori.Add(new Floare(linieFisier));
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
            List<Floare> flori = GetFlori();
            bool gasit = false;

            for (int i = 0; i < flori.Count; i++)
            {
                if (flori[i].Nume.Equals(numeCautat, StringComparison.OrdinalIgnoreCase))
                {
                    floareNoua.DataActualizare = DateTime.Today;
                    flori[i] = floareNoua;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
            {
                RescrieFisier(flori);
            }

            return gasit;
        }

        public void StergeFloare(string nume)
        {
            List<Floare> flori = GetFlori()
                .Where(f => !f.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase))
                .ToList();

            RescrieFisier(flori);
        }

        private void RescrieFisier(List<Floare> flori)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (Floare f in flori)
                {
                    sw.WriteLine(f.ConversieLaSirPentruFisier());
                }
            }
        }
    }
}