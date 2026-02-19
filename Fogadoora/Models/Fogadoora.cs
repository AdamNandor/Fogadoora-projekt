using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fogadoora.Models
{
    internal class Fogadoora
    {
        public Fogadoora(int id, string hely, DateTime kezdet, int hossz)
        {
            Id = id;
            Hely = hely;
            Kezdet = kezdet;
            Hossz = hossz;
        }

        public Fogadoora() { }

        public int Id { get; set; }
        public string Hely { get; set; }
        public DateTime Kezdet { get; set; }
        public int Hossz { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nHely: {Hely}\nKezdet: {Kezdet}\nHossz: {Hossz}";
        }
    }
}
