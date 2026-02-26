using Fogadoora.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fogadoora.Controllers
{
    internal class BejelentkezoController
    {
        public List<Bejelentkezo> GetBejList()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            conn.Open();
            string comd = "SELECT * FROM bejelentkezo;";
            MySqlCommand cmd = new MySqlCommand(comd, conn);
            List<Bejelentkezo> connections = new List<Bejelentkezo>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    connections.Add(new Bejelentkezo(
                        reader.GetInt32("id"),
                        reader.GetString("nev"),
                        reader.GetString("email"),
                        reader.GetString("tel")
                    ));
                }

                conn.Close();
                return connections;
            }
        }

        public bool NewBej(Bejelentkezo bejelentkezo)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"INSERT INTO bejelentkezo VALUES (@Id,@Nev,@Tel,@Email)";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", null);
            insertcmd.Parameters.AddWithValue("@Nev", bejelentkezo.Nev);
            insertcmd.Parameters.AddWithValue("@Tel", bejelentkezo.Tel);
            insertcmd.Parameters.AddWithValue("@Email", bejelentkezo.Email);



            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;
        }
    }
}
