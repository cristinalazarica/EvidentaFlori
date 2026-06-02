using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modele;

namespace NivelStocareDate
{
    public class AdministrareFlori
    {
        private string file;

        public AdministrareFlori(string file)
        {
            this.file = file;

            if (!File.Exists(file))
                File.Create(file).Close();
        }

        public void AdaugaFloare(Floare f)
        {
            File.AppendAllText(file, f.ToFile() + "\n");
        }

        public List<Floare> GetFlori()
        {
            return File.ReadAllLines(file)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new Floare(x))
                .ToList();
        }
    }
}