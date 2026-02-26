using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fogadoora.Models;

namespace Fogadoora.Views
{
    internal class BejelentkezoView
    {
        public BejelentkezoView() { }

        public void KapcsolatokKiir(List<Bejelentkezo> fogadoorak)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔══════╦═══════════════════════╦════════════════════════════════════════╦═════════════════════════════╗");
            Console.WriteLine("║  ID  ║ Név                   ║ E-mail                                 ║ Telefonszám                 ║ ");
            foreach (var fog in fogadoorak)
            {
                Console.WriteLine("╠══════╬═══════════════════════╬════════════════════════════════════════╬═════════════════════════════╣");
                Console.WriteLine(ToRow(fog));
            }
            Console.WriteLine("╚══════╩═══════════════════════╩════════════════════════════════════════╩═════════════════════════════╝");
            Console.ReadLine();
        }
        private static string ToRow(Bejelentkezo fog)
        {
            return $"║ {fog.Id,-4} ║ {fog.Nev,-21} ║ {fog.Email,-38} ║ {fog.Tel,-27} ║";
        }
    }
}
