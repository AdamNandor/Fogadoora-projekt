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
            UIBeallitasok.WriteLineCentered("╔══════╦═══════════════════════╦════════════════════════════════════════╦═════════════════════════════╗");
            UIBeallitasok.WriteLineCentered("║  ID  ║ Név                   ║ E-mail                                 ║ Telefonszám                 ║ ");
            foreach (var fog in fogadoorak)
            {
                UIBeallitasok.WriteLineCentered("╠══════╬═══════════════════════╬════════════════════════════════════════╬═════════════════════════════╣");
                UIBeallitasok.WriteLineCentered(ToRow(fog));
            }
            UIBeallitasok.WriteLineCentered("╚══════╩═══════════════════════╩════════════════════════════════════════╩═════════════════════════════╝");
            Console.ReadLine();
        }
        private static string ToRow(Bejelentkezo fog)
        {
            return $"║ {fog.Id,-4} ║ {fog.Nev,-21} ║ {fog.Email,-38} ║ {fog.Tel,-27} ║";
        }
    }
}
