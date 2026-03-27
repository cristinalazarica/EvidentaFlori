using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modele;

namespace NivelStocareDate
{
    public class AdministrareComenzi
    {
        private string numeFisier;

        public AdministrareComenzi(string numeFisier)
        {
            this.numeFisier = numeFisier;

            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public void AdaugaComanda(Comanda comanda)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(comanda.ConversieLaSirPentruFisier());
            }
        }

        public List<Comanda> GetComenzi()
        {
            List<Comanda> comenzi = new List<Comanda>();

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = sr.ReadLine()) != null)
                {
                    Comanda comanda = new Comanda(linieFisier);
                    comenzi.Add(comanda);
                }
            }

            return comenzi;
        }

        public List<Comanda> CautaDupaClient(string numeClient)
        {
            return GetComenzi()
                .Where(c => c.NumeClient.Equals(numeClient, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public bool ModificaComanda(string numeClient, string numeFloare, Comanda comandaNoua)
        {
            List<Comanda> comenzi = GetComenzi();
            bool gasit = false;

            for (int i = 0; i < comenzi.Count; i++)
            {
                if (comenzi[i].NumeClient.Equals(numeClient, StringComparison.OrdinalIgnoreCase) &&
                    comenzi[i].NumeFloare.Equals(numeFloare, StringComparison.OrdinalIgnoreCase))
                {
                    comenzi[i] = comandaNoua;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
            {
                using (StreamWriter sw = new StreamWriter(numeFisier, false))
                {
                    foreach (Comanda c in comenzi)
                    {
                        sw.WriteLine(c.ConversieLaSirPentruFisier());
                    }
                }
            }

            return gasit;
        }
    }
}