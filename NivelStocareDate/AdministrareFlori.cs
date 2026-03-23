using System;
using System.Collections.Generic;
using System.Linq;
using Modele;

namespace NivelStocareDate
{
    public class AdministrareFlori
    {
        private List<Floare> flori = new List<Floare>();

        public void AdaugaFloare(Floare f)
        {
            flori.Add(f);
        }

        public List<Floare> GetFlori()
        {
            return flori;
        }

        public Floare CautaDupaNume(string nume)
        {
            return flori.FirstOrDefault(f =>
                f.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase));
        }
    }
}