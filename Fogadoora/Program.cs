using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fogadoora.Models;
using Fogadoora.Views;
using Fogadoora.Controllers;

namespace Fogadoora
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                //majd a címen még változtatok, de egyelőre ez van
                Console.WriteLine("=== FŐMENÜ ===");
                Console.WriteLine("1. Regisztrálás");
                Console.WriteLine("2. Bejelentkezés");
                Console.WriteLine("3. Kilépés");
                Console.Write("Válasszon egy menüpontot: ");
                string valasztas = Console.ReadLine();
                switch (valasztas)
                {
                    case "1":
                        Regisztracio();
                        break;
                    case "2":
                        Bejelentkezes();
                        break;
                    case "3":
                        Console.WriteLine("Kilépés...");
                        b = false;
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        break;
                }
            }
        }

        static void Regisztracio()
        {
            Console.Clear();
            Console.WriteLine("=== REGISZTRÁCIÓ ===");
            Console.Write("Felhasználónév: ");
            string felhasznalonev = Console.ReadLine();
            Console.Write("Jelszó: ");
            string jelszo = Console.ReadLine();
        }

        static void Bejelentkezes()
        {
            Console.Clear();
            Console.WriteLine("=== BEJELENTKEZÉS ===");
            Console.Write("Felhasználónév: ");
            string felhasznalonev = Console.ReadLine();
            Console.Write("Jelszó: ");
            string jelszo = Console.ReadLine();
            BejelentkezettMenu();
        }

        static void BejelentkezettMenu()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                Console.WriteLine("=== BEJELENTKEZETT MENÜ ===");
                Console.WriteLine("1. Fogadóóra hozzáadása");
                Console.WriteLine("2. Fogadóóra szerkesztése");
                Console.WriteLine("3. Fogadóórák megtekintése");
                Console.WriteLine("4. Fogadóóra törlése");
                Console.WriteLine("5. Beállítások");
                Console.WriteLine("6. Kijelentkezés");
                Console.Write("Válasszon egy menüpontot: ");
                string valasztas = Console.ReadLine();
                switch (valasztas)
                {
                    case "1":
                        // Fogadóóra hozzáadása
                        break;
                    case "2":
                        // Fogadóóra szerkesztése
                        break;
                    case "3":
                        // Fogadóóra megtekintése
                        break;
                    case "4":
                        // Fogadóóra törlése
                        break;
                    case "5":
                        // Beállítások
                        break;
                    case "6":
                        Console.WriteLine("Kilépés...");
                        b = false;
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        break;
                }
            }
        }
    }
}
