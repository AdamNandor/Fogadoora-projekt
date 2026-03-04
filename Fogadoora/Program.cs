using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fogadoora.Models;
using Fogadoora.Views;
using Fogadoora.Controllers;
using MySql.Data.MySqlClient;

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
                UIBeallitasok.WriteLineCentered("=== FOGADÓÓRA FELJEGYZŐ ===");
                UIBeallitasok.WriteLineCentered("1. Regisztrálás");
                UIBeallitasok.WriteLineCentered("2. Bejelentkezés");
                UIBeallitasok.WriteLineCentered("3. Kilépés");
                UIBeallitasok.WriteCentered("Válasszon egy menüpontot: ");
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
                        UIBeallitasok.WriteLineCentered("Kilépés...");
                        b = false;
                        Environment.Exit(0);
                        return;
                    default:
                        UIBeallitasok.WriteError("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void Regisztracio()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered("=== REGISZTRÁCIÓ ===");
                UIBeallitasok.WriteCentered("Felhasználónév: ");
                string felhasznalonev = Console.ReadLine();
                UIBeallitasok.WriteCentered("Telefonszám: ");
                string tel = Console.ReadLine();
                UIBeallitasok.WriteCentered("E-mail: ");
                string email = Console.ReadLine();
                List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
                if (string.IsNullOrWhiteSpace(felhasznalonev) || string.IsNullOrWhiteSpace(tel) || string.IsNullOrWhiteSpace(email))
                {
                    UIBeallitasok.WriteWarning("Minden mező kitöltése kötelező! Kérjük, próbálja újra.");
                    Console.ReadLine();
                }
                else
                {
                    if (bejlist.Any(x => x.Nev == felhasznalonev))
                    {
                        UIBeallitasok.WriteWarning("Ez a felhasználónév már foglalt. Kérjük, válasszon másikat.");
                        Console.ReadLine();
                    }
                    bool bejAdd = new BejelentkezoController().NewBej(felhasznalonev, tel, email);
                    if (bejAdd)
                    {
                        UIBeallitasok.WriteSuccess("Sikeres regisztráció!");
                        Console.ReadLine();
                        b = false;
                        BejelentkezettMenu(felhasznalonev);
                    }
                    else
                    {
                        UIBeallitasok.WriteError("Hiba történt a regisztráció során.");
                        Console.ReadLine();
                    }
                }
            }
        }

        static void Bejelentkezes()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered("=== BEJELENTKEZÉS ===");
                UIBeallitasok.WriteCentered("Felhasználónév: ");
                string felhasznalonev = Console.ReadLine();
                UIBeallitasok.WriteCentered("E-mail: ");
                string email = Console.ReadLine();
                List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
                if (string.IsNullOrWhiteSpace(felhasznalonev) || string.IsNullOrWhiteSpace(email))
                {
                    UIBeallitasok.WriteWarning("Minden mező kitöltése kötelező! Kérjük, próbálja újra.");
                    Console.ReadLine();
                }
                else
                {
                    if (bejlist.Any(x => x.Nev == felhasznalonev && x.Email == email))
                    {
                        UIBeallitasok.WriteSuccess("Sikeres bejelentkezés!");
                        Console.ReadLine();
                        b = false;
                        BejelentkezettMenu(felhasznalonev);
                    }
                    else if (felhasznalonev == "Admin" && email == "admin.admin@email.com")
                    {
                        UIBeallitasok.WriteAdmin("Sikeres bejelentkezés adminisztrátorként!");
                        Console.ReadLine();
                        b = false;
                        Admin();
                    }
                    else
                    {
                        UIBeallitasok.WriteError("Hibás felhasználónév vagy e-mail. Kérjük, próbálja újra.");
                        Console.ReadLine();
                        return;
                    }
                }
            }
        }

        static void BejelentkezettMenu(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered($"=== ÜDVÖZÖLJÜK {felh} ===");
                UIBeallitasok.WriteLineCentered("1. Fogadóóra hozzáadása");
                UIBeallitasok.WriteLineCentered("2. Fogadóóra szerkesztése");
                UIBeallitasok.WriteLineCentered("3. Fogadóórák megtekintése");
                UIBeallitasok.WriteLineCentered("4. Fogadóóra törlése");
                UIBeallitasok.WriteLineCentered("5. Beállítások");
                UIBeallitasok.WriteLineCentered("6. Kijelentkezés");
                UIBeallitasok.WriteCentered("Válasszon egy menüpontot: ");
                string valasztas = Console.ReadLine();
                switch (valasztas)
                {
                    case "1":
                        AddFog();
                        break;
                    case "2":
                        UpdFog();
                        break;
                    case "3":
                        ShowFog();
                        break;
                    case "4":
                        DelFog();
                        break;
                    case "5":
                        Beallitasok(felh);
                        break;
                    case "6":
                        UIBeallitasok.WriteLineCentered("Kijelentkezés...");
                        b = false;
                        Main(null);
                        Console.ReadLine();
                        return;
                    default:
                        UIBeallitasok.WriteError("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AddFog()
        {
            DateTime kezd = DateTime.MinValue;
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered("=== FOGADÓÓRA HOZZÁADÁSA ===");
                UIBeallitasok.WriteCentered("Hely: ");
                string hely = Console.ReadLine();
                UIBeallitasok.WriteCentered("Kezdés (éééé-hh-nn óó:pp): ");
                try
                {
                    kezd = DateTime.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    UIBeallitasok.WriteError("Hibás dátumformátum! Kérjük, használja az éééé-hh-nn óó:pp formátumot.");
                    Console.ReadLine();
                    continue;
                }
                UIBeallitasok.WriteCentered("Hossz (perc): ");
                int hossz = int.Parse(Console.ReadLine());
                if (string.IsNullOrWhiteSpace(hely) || string.IsNullOrWhiteSpace(hossz.ToString()))
                {
                    UIBeallitasok.WriteWarning("Minden mező kitöltése kötelező! Kérjük, próbálja újra.");
                    Console.ReadLine();
                }
                else
                {
                    if (new FogadooraController().NewFog(hely, kezd, hossz))
                    {
                        UIBeallitasok.WriteSuccess("Sikeres hozzáadás!");
                        Console.ReadLine();
                        b = false;
                    }
                    else
                    {
                        UIBeallitasok.WriteError("Hiba történt a hozzáadás során.");
                        Console.ReadLine();
                    }
                }
            }
        }

        static void UpdFog()
        {

            bool b = true;
            while (b)
            {
                Console.Clear();
                List<Fogadoorak> orak = new FogadooraController().GetFogList();
                new FogadooraView().KapcsolatokKiir(orak);
                UIBeallitasok.WriteLineCentered("Enterrel jelenlegi adat megtartása\n");
                UIBeallitasok.WriteCentered("Adja meg a módosítandó fogadóóra ID-jét: ");
                int id = int.Parse(Console.ReadLine());
                UIBeallitasok.WriteCentered($"Új hely: (Jelenlegi: {orak.FirstOrDefault(x => x.Id == id)?.Hely})");
                string hely = Console.ReadLine();
                UIBeallitasok.WriteCentered($"Új kezdés: (Jel.: {orak.FirstOrDefault(x => x.Id == id)?.Kezdet})");
                DateTime kezd = DateTime.Parse(Console.ReadLine());
                UIBeallitasok.WriteCentered($"Új hossz: (Jel.: {orak.FirstOrDefault(x => x.Id == id)?.Hossz})");
                int hossz = int.Parse(Console.ReadLine());
                if (new FogadooraController().UpdFog(id, hely, kezd, hossz))
                {
                    UIBeallitasok.WriteSuccess("Sikeres módosítás!");
                    Console.ReadLine();
                    b = false;
                }
                else
                {
                    UIBeallitasok.WriteError("Sikertelen módosítás!");
                    Console.ReadLine();
                    b = true;
                }
            }
        }

        static void ShowFog()
        {
            Console.Clear();
            List<Fogadoorak> orak = new FogadooraController().GetFogList();
            new FogadooraView().KapcsolatokKiir(orak);
        }

        static void DelFog()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                List<Fogadoorak> orak = new FogadooraController().GetFogList();
                new FogadooraView().KapcsolatokKiir(orak);
                UIBeallitasok.WriteLineCentered("Enterrel jelenlegi adat megtartása\n");
                UIBeallitasok.WriteCentered("Adja meg a törlendő fogadóóra ID-jét: ");
                int id = int.Parse(Console.ReadLine());
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    UIBeallitasok.WriteWarning("Minden mező kitöltése kötelező! Kérjük, próbálja újra.");
                    Console.ReadLine();
                }
                else
                {
                    if (new FogadooraController().DelFog(id))
                    {
                        UIBeallitasok.WriteSuccess("Sikeres törlés!");
                        Console.ReadLine();
                        b = false;
                    }
                    else
                    {
                        UIBeallitasok.WriteError("Sikertelen törlés!");
                        Console.ReadLine();
                    }
                }
            }
        }

        static void Beallitasok(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered("=== BEÁLLÍTÁSOK ===");
                UIBeallitasok.WriteLineCentered("1. Fiók beállítások");
                UIBeallitasok.WriteLineCentered("2. Kinézet változtatása");
                UIBeallitasok.WriteLineCentered("3. Vissza a főmenüre");
                UIBeallitasok.WriteCentered("Válasszon egy menüpontot: ");
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
                        UIBeallitasok.WriteError("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void FiokBeall(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered("=== ADATOK MÓDOSÍTÁSA ===");
                List<Bejelentkezo> f_adatok = new BejelentkezoController().GetBejList();
                var currNev = f_adatok.FirstOrDefault(x => x.Nev == felh)?.Nev;
                var currEmail = f_adatok.FirstOrDefault(x => x.Nev == felh)?.Email;
                var currTelefon = f_adatok.FirstOrDefault(x => x.Nev == felh)?.Tel;
                UIBeallitasok.WriteLineCentered("Enterrel jelenlegi adat megtartása\n");
                UIBeallitasok.WriteCentered($"Új név: (Jel.: {currNev})");
                string nev = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nev))
                    nev = currNev;
                UIBeallitasok.WriteCentered($"Új E-mail: (Jel.: {currEmail})");
                string email = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(email))
                    email = currEmail;
                UIBeallitasok.WriteCentered($"Új név: (Jel.: {currTelefon})");
                string telefon = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(telefon))
                    telefon = currTelefon;
                int id = f_adatok.FirstOrDefault(x => x.Nev == felh).Id;
                if (new BejelentkezoController().UpdFiok(id, nev, telefon, email))
                {
                    UIBeallitasok.WriteSuccess("Sikeres módosítás!");
                    Console.ReadLine();
                    b = false;
                    BejelentkezettMenu(nev);
                }
                else
                {
                    UIBeallitasok.WriteError("Sikertelen módosítás!");
                    Console.ReadLine();
                    b = true;
                }
            }
        }

        static void KinezetBeall(string felh)
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteLineCentered("=== KINÉZET BEÁLLÍTÁS ===");
                UIBeallitasok.WriteLineCentered("1. Háttérszín módosítása");
                UIBeallitasok.WriteLineCentered("2. Betűszín módosítása");
                UIBeallitasok.WriteLineCentered("3. Vissza a beállításokhoz");
                UIBeallitasok.WriteCentered("Válasszon egy menüpontot: ");
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
                        UIBeallitasok.WriteError("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void Admin()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                UIBeallitasok.WriteAdmin("=== ADMIN MENÜ ===");
                UIBeallitasok.WriteLineCentered("1. Összes felhasználó megtekintése");
                UIBeallitasok.WriteLineCentered("2. Összes fogadóóra megtekintése");
                UIBeallitasok.WriteLineCentered("3. Felhasználó szerkesztése");
                UIBeallitasok.WriteLineCentered("4. Felhasználó törlése");
                UIBeallitasok.WriteLineCentered("5. Vissza a főmenüre");
                UIBeallitasok.WriteCentered("Válasszon egy menüpontot: ");
                string valasztas = Console.ReadLine();
                switch (valasztas)
                {
                    case "1":
                        Console.Clear();
                        List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
                        new BejelentkezoView().KapcsolatokKiir(bejlist);
                        break;
                    case "2":
                        Console.Clear();
                        List<Fogadoorak> orak = new FogadooraController().GetFogList();
                        new FogadooraView().KapcsolatokKiir(orak);
                        break;
                    case "3":
                        UpdFelhasznalo();
                        break;
                    case "4":
                        DelFelhasznalo();
                        break;
                    case "5":
                        Main(null);
                        b = false;
                        break;
                    default:
                        UIBeallitasok.WriteError("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void UpdFelhasznalo()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
                new BejelentkezoView().KapcsolatokKiir(bejlist);
                UIBeallitasok.WriteLineCentered("Enterrel jelenlegi adat megtartása\n");
                UIBeallitasok.WriteCentered("Adja meg a módosítandó felhasználó ID-jét: ");
                int id = int.Parse(Console.ReadLine());
                UIBeallitasok.WriteLineCentered($"Új név: (Jel.: )");
                string nev = Console.ReadLine();
                UIBeallitasok.WriteLineCentered($"Új E-mail: (Jel.: )");
                string email = Console.ReadLine();
                UIBeallitasok.WriteLineCentered($"Új telefonszám: (Jel.: )");
                string tel = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(id.ToString()) || nev == "" || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(tel))
                {
                    UIBeallitasok.WriteWarning("Minden mező kitöltése kötelező! Kérjük, próbálja újra.");
                    Console.ReadLine();
                }
                else
                {
                    if (new BejelentkezoController().UpdBej(id, nev, email, tel))
                    {
                        UIBeallitasok.WriteSuccess("Sikeres módosítás!");
                        Console.ReadLine();
                        b = false;
                    }
                    else
                    {
                        UIBeallitasok.WriteError("Sikertelen módosítás!");
                        Console.ReadLine();
                        b = true;
                    }
                }
            }
        }
        static void DelFelhasznalo()
        {
            bool b = true;
            while (b)
            {
                Console.Clear();
                List<Bejelentkezo> bejlist = new BejelentkezoController().GetBejList();
                new BejelentkezoView().KapcsolatokKiir(bejlist);
                UIBeallitasok.WriteLineCentered("Adja meg a törlendő felhasználó ID-jét: ");
                int id = int.Parse(Console.ReadLine());
                if (string.IsNullOrWhiteSpace(id.ToString()))
                {
                    UIBeallitasok.WriteWarning("Minden mező kitöltése kötelező! Kérjük, próbálja újra.");
                    Console.ReadLine();
                }
                else
                {
                    if (new BejelentkezoController().DelBej(id))
                    {
                        UIBeallitasok.WriteSuccess("Sikeres törlés!");
                        Console.ReadLine();
                        b = false;
                    }
                    else
                    {
                        UIBeallitasok.WriteError("Sikertelen törlés!");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}