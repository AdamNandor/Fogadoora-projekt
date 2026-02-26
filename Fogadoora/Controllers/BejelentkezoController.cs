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
        /// <summary>
        /// lekéri az adatbázisból a bejelentkezők adatait, és egy listában visszaadja őket
        /// </summary>
        /// <returns>egy listát az összes bejelentkezőről és adataikról</returns>
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

        /// <summary>
        /// kiírja a megadott új bejelentkezőt az adatbázisba, és visszaadja, hogy sikerült-e a művelet
        /// </summary>
        /// <param name="bejelentkezo"></param>
        /// <returns>True/False hogy sikrült e a művelet vagy sem</returns>
        public bool NewBej(string nev, string tel, string email)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"INSERT INTO bejelentkezo VALUES (@Id,@Nev,@Email,@Tel)";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", null);
            insertcmd.Parameters.AddWithValue("@Nev", nev);
            insertcmd.Parameters.AddWithValue("@Tel", tel);
            insertcmd.Parameters.AddWithValue("@Email", email);



            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;
        }

        /// <summary>
        /// kiírja a megadott bejelentkező új adatait az adatbázisba, és visszaadja, hogy sikerült-e a művelet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nev"></param>
        /// <param name="tel"></param>
        /// <param name="email"></param>
        /// <returns>True/False hogy sikrült e a művelet vagy sem</returns>
        public bool UpdBej(int id, string nev, string tel, string email)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"UPDATE bejelentkezo SET `Nev`= @Nev,`Tel`= @Tel,`Email`= @Email WHERE Id = @Id;";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", id);
            insertcmd.Parameters.AddWithValue("@Nev", nev);
            insertcmd.Parameters.AddWithValue("@Tel", tel);
            insertcmd.Parameters.AddWithValue("@Email", email);



            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;

        }

        /// <summary>
        /// kitörli a megadott id-jű bejelentkezőt az adatbázisból, és visszaadja, hogy sikerült-e a művelet
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True/False hogy sikrült e a művelet vagy sem</returns>
        public bool DelBej(int id)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"DELETE FROM bejelentkezo WHERE Id = @Id";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", id);


            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;

        }
    }
}
