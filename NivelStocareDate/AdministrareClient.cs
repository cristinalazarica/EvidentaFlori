using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modele;

namespace NivelStocareDate
{
    public class AdministrareClienti
    {
        private string numeFisier;

        public AdministrareClienti(string numeFisier)
        {
            this.numeFisier = numeFisier;

            if (!File.Exists(numeFisier))
            {
                File.Create(numeFisier).Close();
            }
        }

        public void AdaugaClient(Client client)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(client.ConversieLaSirPentruFisier());
            }
        }

        public List<Client> GetClienti()
        {
            List<Client> clienti = new List<Client>();

            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linieFisier;
                while ((linieFisier = sr.ReadLine()) != null)
                {
                    Client client = new Client(linieFisier);
                    clienti.Add(client);
                }
            }

            return clienti;
        }

        public Client? CautaDupaNume(string nume)
        {
            return GetClienti().FirstOrDefault(c => c.Nume.Equals(nume, StringComparison.OrdinalIgnoreCase));
        }

        public bool ModificaClient(string numeCautat, Client clientNou)
        {
            List<Client> clienti = GetClienti();
            bool gasit = false;

            for (int i = 0; i < clienti.Count; i++)
            {
                if (clienti[i].Nume.Equals(numeCautat, StringComparison.OrdinalIgnoreCase))
                {
                    clienti[i] = clientNou;
                    gasit = true;
                    break;
                }
            }

            if (gasit)
            {
                using (StreamWriter sw = new StreamWriter(numeFisier, false))
                {
                    foreach (Client c in clienti)
                    {
                        sw.WriteLine(c.ConversieLaSirPentruFisier());
                    }
                }
            }

            return gasit;
        }
    }
}