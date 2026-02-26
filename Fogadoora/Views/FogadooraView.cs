using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fogadoora.Models;

namespace Fogadoora.Views
{
    internal class FogadooraView
    {
        public FogadooraView() { }

        public void KapcsolatokKiir(List<Fogadoorak> fogadoorak)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔══════╦═══════════════════════╦════════════════════════════════════════╦═════════════════════════════╗");
            Console.WriteLine("║  ID  ║ Hely                  ║ Kezdet                                 ║ Hossz                       ║ ");
            foreach (var fog in fogadoorak)
            {
                Console.WriteLine("╠══════╬═══════════════════════╬════════════════════════════════════════╬═════════════════════════════╣");
                Console.WriteLine(ToRow(fog));
            }
            Console.WriteLine("╚══════╩═══════════════════════╩════════════════════════════════════════╩═════════════════════════════╝");
            Console.ReadLine();
        }
        private static string ToRow(Fogadoorak fog)
        {
            return $"║ {fog.Id,-4} ║ {fog.Hely,-21} ║ {fog.Kezdet,-38} ║ {fog.Hossz,-27} ║";
        }
        
    }
}
