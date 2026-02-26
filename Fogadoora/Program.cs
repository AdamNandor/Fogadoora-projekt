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
            Console.Write("Telefonszám: ");
            string tel = Console.ReadLine();
            Console.Write("E-mail: ");
            string email = Console.ReadLine();
            List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
            if (bejlist.Any(x => x.Nev == felhasznalonev))
            {
                Console.WriteLine("Ez a felhasználónév már foglalt. Kérjük, válasszon másikat.");
                Console.ReadLine();
                return;
            }
            bool bejAdd = new BejelentkezoController().NewBej(felhasznalonev, tel, email);
            if (bejAdd)
            {
                Console.WriteLine("Sikeres regisztráció!");
                BejelentkezettMenu(felhasznalonev);
            }
            else
            {
                Console.WriteLine("Hiba történt a regisztráció során.");
            }
            Console.ReadLine();
        }

        static void Bejelentkezes()
        {
            Console.Clear();
            Console.WriteLine("=== BEJELENTKEZÉS ===");
            Console.Write("Felhasználónév: ");
            string felhasznalonev = Console.ReadLine();
            Console.Write("E-mail: ");
            string email = Console.ReadLine();
            List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
            if (bejlist.Any(x => x.Nev == felhasznalonev && x.Email == email))
            {
                UIBeallitasok.WriteSuccess("Sikeres bejelentkezés!");
                BejelentkezettMenu(felhasznalonev);
            }
            else
            {
                UIBeallitasok.WriteError("Hibás felhasználónév vagy e-mail. Kérjük, próbálja újra.");
                Console.ReadLine();
                return;
            }
        }

        static void BejelentkezettMenu(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                Console.WriteLine($"=== ÜDVÖZÖLJÜK {felh} ===");
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
                        Beallitasok(felh);
                        break;
                    case "6":
                        Console.WriteLine("Kilépés...");
                        b = false;
                        Console.ReadLine();
                        return;
                    default:
                        Console.WriteLine("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        break;
                }
            }
        }

        static void Beallitasok(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                Console.WriteLine("=== BEÁLLÍTÁSOK ===");
                Console.WriteLine("1. Fiók beállítások");
                Console.WriteLine("2. Kinézet változtatása");
                Console.WriteLine("3. Vissza a főmenüre");
                Console.Write("Válasszon egy menüpontot: ");
                string valasztas = Console.ReadLine();
                switch (valasztas)
                {
                    case "1":
                        FiokBeall(felh);
                        break;
                    case "2":
                        KinezetBeall(felh);
                        break;
                    case "3":
                        BejelentkezettMenu(felh);
                        b = false;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        break;
                }
            }
        }
        static void FiokBeall(string felh)
        {
            Console.Clear();
            List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();

        }

        static void KinezetBeall(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                Console.WriteLine("=== KINÉZET BEÁLLÍTÁS ===");
                Console.WriteLine("1. Háttérszín módosítása");
                Console.WriteLine("2. Betűszín módosítása");
                Console.WriteLine("3. Vissza a beállításokhoz");
                Console.Write("Válasszon egy menüpontot: ");
                string valasztas = Console.ReadLine();
                switch (valasztas)
                {
                    case "1":
                        UIBeallitasok.HatterMod();
                        break;
                    case "2":
                        UIBeallitasok.BetuszinMod();
                        break;
                    case "3":
                        Beallitasok(felh);
                        b = false;
                        break;
                    default:
                        Console.WriteLine("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        break;
                }
            }
        }
    }
}
