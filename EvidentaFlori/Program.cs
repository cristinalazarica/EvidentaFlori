using System;
using System.Collections.Generic;
using NivelStocareDate;

namespace EvidentaFlori
{
    class Program
    {
        static void Main(string[] args)
        {
            AdministrareFlori adminFlori = new AdministrareFlori();

            Modele.Floare? floareNoua = null;
            Modele.Client? clientNou = null;
            Modele.Comanda? comandaNoua = null;

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
                            adminFlori.AdaugaFloare(floareNoua);
                            Console.WriteLine("Floare salvata.");
                        }
                        else
                        {
                            Console.WriteLine("Nu exista floare de salvat.");
                        }
                        break;

                    case "4":
                        AfisareFlori(adminFlori.GetFlori());
                        break;

                    case "5":
                        clientNou = CitireClient();
                        Console.WriteLine("Client salvat.");
                        break;

                    case "6":
                        AfisareClient(clientNou);
                        break;

                    case "7":
                        comandaNoua = CitireComanda();
                        Console.WriteLine("Comanda salvata.");
                        break;

                    case "8":
                        Console.WriteLine("Comenzile sunt doar afisate local.");
                        break;

                    case "9":
                        Console.Write("Introduceti numele florii: ");
                        string cauta = Console.ReadLine() ?? "";

                        var floareGasita = adminFlori.CautaDupaNume(cauta);

                        if (floareGasita != null)
                            Console.WriteLine(floareGasita.Info());
                        else
                            Console.WriteLine("Nu s-a gasit.");
                        break;

                    case "0":
                        Console.WriteLine("Aplicatia se inchide.");
                        break;

                    default:
                        Console.WriteLine("Optiune invalida.");
                        break;
                }

            } while (optiune != "0");
        }

        static Modele.Floare CitireFloare()
        {
            Console.Write("Nume floare: ");
            string nume = Console.ReadLine() ?? "";

            Console.Write("Pret: ");
            double.TryParse(Console.ReadLine(), out double pret);

            Console.Write("Stoc: ");
            int.TryParse(Console.ReadLine(), out int stoc);

            Console.WriteLine("Culoare: 0-Rosu, 1-Alb, 2-Galben , 3-Mov , 4-Albastru");
            int.TryParse(Console.ReadLine(), out int culoare);

            Console.WriteLine("Optiuni: 1-Parfumata, 2-Decorativa");
            int.TryParse(Console.ReadLine(), out int optiuni);

            return new Modele.Floare(
                nume,
                pret,
                stoc,
                (Modele.Culoare)culoare,
                (Modele.Optiuni)optiuni
            );
        }

        static Modele.Client CitireClient()
        {
            Console.Write("Nume client: ");
            string nume = Console.ReadLine() ?? "";

            Console.Write("NrComenzi: ");
            int.TryParse(Console.ReadLine(), out int nrComenzi);

            return new Modele.Client(nume, nrComenzi);
        }

        static Modele.Comanda CitireComanda()
        {
            Console.Write("Nume client: ");
            string numeClient = Console.ReadLine() ?? "";

            Console.Write("Nume floare: ");
            string numeFloare = Console.ReadLine() ?? "";

            Console.Write("Cantitate: ");
            int.TryParse(Console.ReadLine(), out int cantitate);

            return new Modele.Comanda(numeClient, numeFloare, cantitate);
        }

        static void AfisareFloare(Modele.Floare? f)
        {
            if (f == null)
                Console.WriteLine("Nu exista floare.");
            else
                Console.WriteLine(f.Info());
        }

        static void AfisareFlori(List<Modele.Floare> flori)
        {
            if (flori.Count == 0)
                Console.WriteLine("Lista goala.");
            else
                foreach (var f in flori)
                    Console.WriteLine(f.Info());
        }

        static void AfisareClient(Modele.Client? c)
        {
            if (c == null)
                Console.WriteLine("Nu exista client.");
            else
                Console.WriteLine(c.Info());
        }
    }
}