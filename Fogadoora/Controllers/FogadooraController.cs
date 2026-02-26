using Fogadoora.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Pqc.Crypto.Lms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Fogadoora.Controllers
{
    internal class FogadooraController
    {
        /// <summary>
        /// lekéri az adatbázisból a fogadóórák adatait, és egy listában visszaadja őket
        /// </summary>
        /// <returns>egy listát a fogadóórákról</returns>
        public List<Fogadoorak> GetFogList()
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            conn.Open();
            string comd = "SELECT * FROM fogadoora;";
            MySqlCommand cmd = new MySqlCommand(comd, conn);
            List<Fogadoorak> connections = new List<Fogadoorak>();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    connections.Add(new Fogadoorak(
                        reader.GetInt32("id"),
                        reader.GetString("hely"),
                        reader.GetDateTime("kezdet"),
                        reader.GetInt32("hossz")
                    ));
                }

                conn.Close();
                return connections;
            }
        }

        /// <summary>
        /// Kiírja a megadott új fogadóórát az adatbázisba, és visszaadja, hogy sikerült-e a művelet
        /// </summary>
        /// <param name="Fog">Egy fogadóóra adatai egy listában</param>
        /// <returns>True/False hogy sikrült e a művelet vagy sem</returns>
        public bool NewFog(string hely, DateTime kezd, int hossz)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"INSERT INTO fogadoora VALUES (@Id,@Hely,@Kezdet,@Hossz)";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", null);
            insertcmd.Parameters.AddWithValue("@Hely", hely);
            insertcmd.Parameters.AddWithValue("@Kezdet", kezd);
            insertcmd.Parameters.AddWithValue("@Hossz", hossz);


            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;
        }

        /// <summary>
        /// Kiírja a megadott fogadóóra új adatait az adatbázisba, és visszaadja, hogy sikerült-e a művelet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="hely"></param>
        /// <param name="kezdet"></param>
        /// <param name="hossz"></param>
        /// <returns>True/False hogy sikrült e a művelet vagy sem</returns>
        public bool UpdFog(int id, string hely, DateTime kezdet, int hossz)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"UPDATE fogadoora SET `Hely`= @Hely,`Kezdet`= @Kezdet,`Hossz`= @Hossz WHERE Id = @Id;";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", id);
            insertcmd.Parameters.AddWithValue("@Hely", hely);
            insertcmd.Parameters.AddWithValue("@Kezdet", kezdet);
            insertcmd.Parameters.AddWithValue("@Hossz", hossz);


            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;

        }

        /// <summary>
        /// Kitörli a megadott id-jú fogadóórát az adatbázisból, és visszaadja, hogy sikerült-e a művelet
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True/False hogy sikrült e a művelet vagy sem</returns>
        public bool DelFog(int id)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"DELETE FROM fogadoora WHERE Id = @Id";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", id);


            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;

        }
    }
}
