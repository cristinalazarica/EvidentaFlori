using System;
using System.Collections.Generic;
using Client;
using Comanda;
using Floare;

namespace EvidentaFlori
{
    class Program
    {
        static void Main()
        {
            List<Floare.Floare> flori = new List<Floare.Floare>();
            List<Client.Client> clienti = new List<Client.Client>();
            List<Comanda.Comanda> comenzi = new List<Comanda.Comanda>();

            Floare.Floare floareNoua = null;
            Client.Client clientNou = null;
            Comanda.Comanda comandaNoua = null;

            string optiune;

            do
            {
                Console.WriteLine("\n--- Meniu Evidenta Flori ---");
                Console.WriteLine("1. Citire floare de la tastatura");
                Console.WriteLine("2. Afisarea ultimei flori introduse");
                Console.WriteLine("3. Salvare floare in lista");
                Console.WriteLine("4. Afisare lista de flori");
                Console.WriteLine("5. Citire client de la tastatura");
                Console.WriteLine("6. Afisare client salvat");
                Console.WriteLine("7. Citire comanda de la tastatura");
                Console.WriteLine("8. Afisare lista comenzi");
                Console.WriteLine("9. Cautare floare dupa nume");
                Console.WriteLine("0. Inchidere program");

                Console.Write("\nAlegeti o optiune: ");
                optiune = Console.ReadLine() ?? "";

                switch (optiune)
                {
                    case "1":
                        floareNoua = CitireFloare();
                        break;
                    case "2":
                        AfisareFloare(floareNoua);
                        break;
                    case "3":
                        if (floareNoua != null)
                        {
                            flori.Add(floareNoua);
                            Console.WriteLine("Floare salvata in lista.");
                        }
                        else
                            Console.WriteLine("Nu exista floare de salvat.");
                        break;
                    case "4":
                        AfisareFlori(flori);
                        break;
                    case "5":
                        clientNou = CitireClient();
                        clienti.Add(clientNou);
                        Console.WriteLine("Client salvat in lista.");
                        break;
                    case "6":
                        AfisareClient(clientNou);
                        break;
                    case "7":
                        comandaNoua = CitireComanda();
                        comenzi.Add(comandaNoua);
                        Console.WriteLine("Comanda salvata in lista.");
                        break;
                    case "8":
                        AfisareComenzi(comenzi);
                        break;
                    case "9":
                        Console.Write("Introduceti numele florii de cautat: ");
                        string cauta = Console.ReadLine() ?? "";
                        CautareFloare(flori, cauta);
                        break;
                    case "0":
                        Console.WriteLine("Aplicatia va fi inchisa.");
                        break;
                    default:
                        Console.WriteLine("Optiune inexistenta.");
                        break;
                }

            } while (optiune != "0");
        }

        static Floare.Floare CitireFloare()
        {
            Console.Write("Nume floare: ");
            string nume = Console.ReadLine() ?? "";
            Console.Write("Pret: ");
            double.TryParse(Console.ReadLine(), out double pret);
            Console.Write("Stoc: ");
            int.TryParse(Console.ReadLine(), out int stoc);
            return new Floare.Floare(nume, pret, stoc);
        }

        static Client.Client CitireClient()
        {
            Console.Write("Nume client: ");
            string nume = Console.ReadLine() ?? "";
            Console.Write("NrComenzi: ");
            int.TryParse(Console.ReadLine(), out int nrComenzi);
            return new Client.Client(nume, nrComenzi);
        }

        static Comanda.Comanda CitireComanda()
        {
            Console.Write("Nume client: ");
            string numeClient = Console.ReadLine() ?? "";
            Console.Write("Nume floare: ");
            string numeFloare = Console.ReadLine() ?? "";
            Console.Write("Cantitate: ");
            int.TryParse(Console.ReadLine(), out int cantitate);
            return new Comanda.Comanda(numeClient, numeFloare, cantitate);
        }

        static void AfisareFloare(Floare.Floare f)
        {
            if (f == null)
                Console.WriteLine("Nu exista floare introdusa.");
            else
                Console.WriteLine(f.Info());
        }

        static void AfisareFlori(List<Floare.Floare> flori)
        {
            if (flori.Count == 0)
                Console.WriteLine("Lista de flori este goala.");
            else
                foreach (var f in flori)
                    Console.WriteLine(f.Info());
        }

        static void AfisareClient(Client.Client c)
        {
            if (c == null)
                Console.WriteLine("Nu exista client introdus.");
            else
                Console.WriteLine(c.Info());
        }

        static void AfisareComenzi(List<Comanda.Comanda> comenzi)
        {
            if (comenzi.Count == 0)
                Console.WriteLine("Lista de comenzi este goala.");
            else
                foreach (var com in comenzi)
                    Console.WriteLine(com.Info());
        }

        static void CautareFloare(List<Floare.Floare> flori, string numeCautat)
        {
            bool gasit = false;
            foreach (var f in flori)
            {
                if (f.Nume.Equals(numeCautat, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(f.Info());
                    gasit = true;
                }
            }
            if (!gasit)
                Console.WriteLine("Nu s-a gasit nicio floare cu acest nume.");
        }
    }
}