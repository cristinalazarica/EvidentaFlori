using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modele;

namespace NivelStocareDate
{
    public class AdministrareComenzi
    {
        private string file;

        public AdministrareComenzi(string file)
        {
            this.file = file;

            if (!File.Exists(file))
                File.Create(file).Close();
        }

        public void AdaugaComanda(Comanda c)
        {
            File.AppendAllText(file, c.ConversieLaSirPentruFisier() + "\n");
        }

        public List<Comanda> GetComenzi()
        {
            return File.ReadAllLines(file)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new Comanda(x))
                .ToList();
        }
    }
}