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
                        Console.WriteLine("Kilépés...");
                        b = false;
                        Console.ReadLine();
                        return;
                    default:
                        UIBeallitasok.WriteError("Érvénytelen menüpont. Kérjük, próbálja újra.");
                        break;
                }
            }
        }

        static void AddFog()
        {
            Console.Clear();
            Console.WriteLine("=== FOGADÓÓRA HOZZÁADÁSA ===");
            Console.Write("Hely: ");
            string hely = Console.ReadLine();
            Console.Write("Kezdés (éééé-hh-nn óó:pp): ");
            DateTime kezd = DateTime.Parse(Console.ReadLine());
            Console.Write("Hossz (perc): ");
            int hossz = int.Parse(Console.ReadLine());
            bool addFog = new FogadooraController().NewFog(hely, kezd, hossz);
            if (addFog)
            {
                Console.WriteLine("Sikeres hozzáadás!");
            }
            else
            {
                UIBeallitasok.WriteError("Hiba történt a hozzáadás során.");
            }
            Console.ReadLine();
        }

        static void UpdFog()
        {
            Console.Clear();

            //táblázat kiírása
            List<Fogadoorak> orak = new FogadooraController().GetFogList();
            new FogadooraView().KapcsolatokKiir(orak);
            Console.WriteLine("Enterrel jelenlegi adat megtartása\n");

            Console.Write("Adja meg a módosítandó fogadóóra ID-jét: ");
            int id = int.Parse(Console.ReadLine());
            


            Console.WriteLine($"Új hely: (Jelenlegi: ");
            string hely = Console.ReadLine();
            //if (string.IsNullOrWhiteSpace(hely))
            //    hely = "";

            Console.WriteLine($"Új kezdés: (Jel.: )");
            DateTime kezd = DateTime.Parse(Console.ReadLine());
            //if (string.IsNullOrWhiteSpace(kezd))
            //    kezd = "";

            Console.WriteLine($"Új hossz: (Jel.: )");
            int hossz = int.Parse(Console.ReadLine());
            //if (string.IsNullOrWhiteSpace(telefon))
            //    telefon = "";


            if (new FogadooraController().UpdFog(id, hely, kezd, hossz))
            {
                Console.WriteLine("Sikeres módosítás!");
            }
            else
            {
                Console.WriteLine("Sikertelen módosítás!");
            }
            Console.ReadLine();
        }

        static void ShowFog()
        {
            Console.Clear();
            List<Fogadoorak> orak = new FogadooraController().GetFogList();
            new FogadooraView().KapcsolatokKiir(orak);
        }

        static void DelFog()
        {
            Console.Clear();
            //táblázat kiírása
            List<Fogadoorak> orak = new FogadooraController().GetFogList();
            new FogadooraView().KapcsolatokKiir(orak);
            Console.WriteLine("Enterrel jelenlegi adat megtartása\n");
            Console.Write("Adja meg a törlendő fogadóóra ID-jét: ");
            int id = int.Parse(Console.ReadLine());
            if (new FogadooraController().DelFog(id))
            {
                Console.WriteLine("Sikeres törlés!");
            }
            else
            {
                Console.WriteLine("Sikertelen törlés!");
            }
            Console.ReadLine();
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
                ////táblázat kiírása
                //List<Conn> connections = new ConnController().GetConnList();
                

                //// Módosítandó adatok bekérése
                //Console.WriteLine("Enterrel jelenlegi adat megtartása\n");

                //Console.WriteLine($"Új név: (Jel.: {currNev})");
                //string nev = Console.ReadLine();
                //if (string.IsNullOrWhiteSpace(nev))
                //    nev = currNev;

                //Console.WriteLine($"Új cím: (Jel.: {currCim})");
                //string cim = Console.ReadLine();
                //if (string.IsNullOrWhiteSpace(cim))
                //    cim = currCim;

                //Console.WriteLine($"Új E-mail: (Jel.: {currEmail})");
                //string email = Console.ReadLine();
                //if (string.IsNullOrWhiteSpace(email))
                //    email = currEmail;

                //Console.WriteLine($"Új név: (Jel.: {currTelefon})");
                //string telefon = Console.ReadLine();
                //if (string.IsNullOrWhiteSpace(telefon))
                //    telefon = currTelefon;


                //if (new ConnController().UpdConn(id, nev, cim, email, telefon))
                //{
                //    Console.WriteLine("Sikeres módosítás!");
                //}
                //else
                //{
                //    Console.WriteLine("Sikertelen módosítás!");
                //}
                //Console.ReadLine();
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
