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
        /// <returns>True/False hogy sikrült e vagy nem.</returns>
        public bool NewFog(Fogadoorak Fog)
        {
            MySqlConnection con = new MySqlConnection("server=localhost;user=root;password=;database=fogadoora;");
            con.Open();
            string insertSql = @"INSERT INTO fogadoora VALUES (@Id,@Hely,@Kezdet,@Hossz)";
            MySqlCommand insertcmd = new MySqlCommand(insertSql, con);
            insertcmd.Parameters.AddWithValue("@Id", null);
            insertcmd.Parameters.AddWithValue("@Hely", Fog.Hely);
            insertcmd.Parameters.AddWithValue("@Kezdet", Fog.Kezdet);
            insertcmd.Parameters.AddWithValue("@Hossz", Fog.Hossz);


            int sorok = insertcmd.ExecuteNonQuery();
            bool valasz = sorok > 0 ? true : false;
            return valasz;
        }

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

        //static void UpdConn()
        //{
        //    Console.Clear();
        //    //táblázat kiírása
        //    List<Conn> connections = new ConnController().GetConnList();
        //    new ConnView().ShowConnList(connections);

        //    #region Jelenlegi adatok
        //    MySqlConnection conn = new MySqlConnection("server=localhost;user=root;password=;database=connappyes;");

        //    conn.Open();

        //    // Módisítandó Kapcsolat ID-jának bekérése
        //    Console.WriteLine("\nAdd meg a módosítandó Kapcsolat ID-ját:");
        //    int id = int.Parse(Console.ReadLine());

        //    // Jelenlegi adatok lekérése az adatbázisból
        //    string selectSql = @"SELECT Nev, Cim, Email, Telefon FROM `connappyes` WHERE Id = @Id";
        //    MySqlCommand selectCmd = new MySqlCommand(selectSql, conn);
        //    selectCmd.Parameters.AddWithValue("@Id", id);

        //    MySqlDataReader reader = selectCmd.ExecuteReader();

        //    if (!reader.Read())
        //    {
        //        Console.WriteLine("Nincs ilyen Kapcsolat!");
        //        reader.Close();
        //        conn.Close();
        //        Console.ReadLine();
        //        UpdConn();
        //    }

        //    // Jelenlegi adatok elmentése változókba
        //    string currNev = reader.GetString("Nev");
        //    string currCim = reader.GetString("Cim");
        //    string currEmail = reader.GetString("Email");
        //    string currTelefon = reader.GetString("Telefon");

        //    reader.Close();
        //    #endregion

        //    // Módosítandó adatok bekérése
        //    Console.WriteLine("Enterrel jelenlegi adat megtartása\n");

        //    Console.WriteLine($"Új név: (Jel.: {currNev})");
        //    string nev = Console.ReadLine();
        //    if (string.IsNullOrWhiteSpace(nev))
        //        nev = currNev;

        //    Console.WriteLine($"Új cím: (Jel.: {currCim})");
        //    string cim = Console.ReadLine();
        //    if (string.IsNullOrWhiteSpace(cim))
        //        cim = currCim;

        //    Console.WriteLine($"Új E-mail: (Jel.: {currEmail})");
        //    string email = Console.ReadLine();
        //    if (string.IsNullOrWhiteSpace(email))
        //        email = currEmail;

        //    Console.WriteLine($"Új név: (Jel.: {currTelefon})");
        //    string telefon = Console.ReadLine();
        //    if (string.IsNullOrWhiteSpace(telefon))
        //        telefon = currTelefon;


        //    if (new ConnController().UpdConn(id, nev, cim, email, telefon))
        //    {
        //        Console.WriteLine("Sikeres módosítás!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Sikertelen módosítás!");
        //    }
        //    Console.ReadLine();
        //    Main();
        //}



    }
}
