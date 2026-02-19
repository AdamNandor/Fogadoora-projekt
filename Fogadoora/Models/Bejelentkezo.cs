using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fogadoora.Models
{
    internal class Bejelentkezo
    {
        public Bejelentkezo(int id, string nev, string email, string tel)
        {
            Id = id;
            Nev = nev;
            Email = email;
            Tel = tel;
        }

        public Bejelentkezo() { }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nNév: {Nev}\nEmail: {Email}\nTelefonszám: {Tel}";
        }
    }
}
