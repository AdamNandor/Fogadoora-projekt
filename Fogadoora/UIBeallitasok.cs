using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fogadoora
{
    internal class UIBeallitasok
    {
        public static void WriteLineCentered(string sz)
        {
            int szel = Console.WindowWidth;
            int bal = (szel - sz.Length) / 2;
            if (bal < 0)
            {
                bal = 0;
            }
            Console.WriteLine(new string(' ', bal) + sz);
        }

        public static void WriteCentered(string sz)
        {
            int width = Console.WindowWidth;
            int bal = (width - sz.Length) / 2;
            if (bal < 0)
            {
                bal = 0;
            }
            Console.Write(new string(' ', bal) + sz);
        }

        public static void WriteSuccess(string sz)
        {
            var regiSzin = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            WriteLineCentered(sz);

            Console.ForegroundColor = regiSzin;
        }

        public static void WriteError(string sz)
        {
            var regiSzin = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            WriteLineCentered(sz);

            Console.ForegroundColor = regiSzin;
        }

        public static void WriteAdmin(string sz)
        {
            var regiSzin = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            WriteLineCentered(sz);

            Console.ForegroundColor = regiSzin;
        }

        public static void WriteWarning(string sz)
        {
            var regiSzin = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            WriteLineCentered(sz);

            Console.ForegroundColor = regiSzin;
        }

        public static int HatterMod()
        {
            Console.Clear();
            WriteLineCentered("=== HÁTTÉRSZÍN MÓDOSÍTÁSA ===");
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLineCentered("1. Fekete");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("2. Fehér");
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteLineCentered("3. Szürke");
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLineCentered("4. Piros");
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLineCentered("5. Zöld");
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteLineCentered("6. Kék");
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteLineCentered("7. Cián");
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLineCentered("8. Sárga");
            Console.ForegroundColor = ConsoleColor.Magenta;
            WriteLineCentered("9. Magenta");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("");
            WriteCentered("Adja meg a kivánt szín számát: ");
            int valasztas = int.Parse(Console.ReadLine());

            ConsoleColor ujHatter = ValasztasToColor(valasztas);

            if (ujHatter == Console.ForegroundColor)
            {
                WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
                Console.ReadLine();
                return -1;
            }

            Console.BackgroundColor = ujHatter;
            return valasztas;
        }

        public static int BetuszinMod()
        {
            Console.Clear();
            WriteLineCentered("=== BETŰSZÍN MÓDOSÍTÁSA ===");
            Console.ForegroundColor = ConsoleColor.Black;
            WriteLineCentered("1. Fekete");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("2. Fehér");
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteLineCentered("3. Szürke");
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLineCentered("4. Piros");
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLineCentered("5. Zöld");
            Console.ForegroundColor = ConsoleColor.Blue;
            WriteLineCentered("6. Kék");
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteLineCentered("7. Cián");
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLineCentered("8. Sárga");
            Console.ForegroundColor = ConsoleColor.Magenta;
            WriteLineCentered("9. Magenta");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLineCentered("");
            WriteCentered("Adja meg a kivánt szín számát: ");
            int valasztas = int.Parse(Console.ReadLine());

            ConsoleColor ujBetuszin = ValasztasToColor(valasztas);

            if (ujBetuszin == Console.BackgroundColor)
            {
                WriteWarning("A háttér és a betűszín nem lehet ugyanaz!");
                Console.ReadLine();
                return -1;
            }

            Console.ForegroundColor = ujBetuszin;
            return valasztas;
        }

        public static ConsoleColor ValasztasToColor(int v)
        {
            switch (v)
            {
                case 1: return ConsoleColor.Black;
                case 2: return ConsoleColor.White;
                case 3: return ConsoleColor.Gray;
                case 4: return ConsoleColor.Red;
                case 5: return ConsoleColor.Green;
                case 6: return ConsoleColor.Blue;
                case 7: return ConsoleColor.Cyan;
                case 8: return ConsoleColor.Yellow;
                case 9: return ConsoleColor.Magenta;
                default: return ConsoleColor.White;
            }
        }
    }
}
