using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace BookClub_konzol
{
    class Program
    {

        MySqlCommand sql = null;
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "bookclub";
            sb.CharacterSet = "utf8";
            MySqlConnection connection = new MySqlConnection(sb.ToString());
            MySqlConnection connection2 = new MySqlConnection(sb.ToString());
            MySqlCommand sql = connection.CreateCommand();
            List<Tag> tagList = new List<Tag>();
            
            try
            {
                connection.Open();
                sql.CommandText = "SELECT * FROM tagok ;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                       // Console.WriteLine(dr.GetInt32("id") + dr.GetString("csaladnev") + dr.GetString("utonev") + dr.GetString("nem"));
                        MySqlCommand sql2 = connection2.CreateCommand();
                        
                        connection2.Open();
                        sql2.CommandText = "SELECT * FROM befizetes WHERE id = " + dr.GetInt32("id") + "; ";
                        using (MySqlDataReader dr2 = sql2.ExecuteReader())
                        {
                            List<Befizetes> befizetList = new List<Befizetes>();
                            while (dr2.Read())
                            {
                                if (Convert.ToInt32(dr2.GetString("datum").Split(".")[0]) == 2021)
                                {
                                    //Console.WriteLine(dr2.GetString("datum").Split(".")[0]);
                                    // Console.WriteLine(dr2.GetString("datum") + dr2.GetInt32("befizetes"));
                                    Befizetes tmpBefzetes = new Befizetes(Convert.ToDateTime(dr2.GetString("datum")), dr2.GetInt32("befizetes"));
                                    befizetList.Add(tmpBefzetes);
                                }
                            }
                            Tag tmpTag = new Tag(dr.GetInt32("id"), dr.GetString("csaladnev"), dr.GetString("utonev"), befizetList, dr.GetString("nem"), Convert.ToDateTime(dr.GetString("szuletett")), Convert.ToDateTime(dr.GetString("belepett")));
                            tagList.Add(tmpTag);
                                }
                        connection2.Close();
                       // Console.WriteLine("********************");
                    }
                    
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
               
            }
         /*   Console.WriteLine("++++++++++++++++++++++++++");
            foreach (var mytag in tagList)
            {
               
                Console.WriteLine("Dolgozó neve: {0} ", mytag.Nev);

                foreach (var befiz in mytag.Befizetes)
                {
                    Console.WriteLine("datum is {0} and amount {1}", befiz.Datum, befiz.Osszeg);
                }
                Console.WriteLine("+++++++++++++++++++++++++++++");
                }*/
            Console.WriteLine("A tagok befizetései: ");
            try
            {
                connection.Open();
                sql.CommandText = "SELECT Concat(csaladnev,' ', utonev) as nev, SUM(befizetes) as osszeg FROM tagok INNER JOIN befizetes USING(id) GROUP BY id ORDER by nev;";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine(dr.GetString("nev") + " "+ dr.GetInt32("osszeg") + "Ft");
                       
                    }
                }
                connection.Close();

            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("");
            try
            {
                connection.Open();
                sql.CommandText = "SELECT * FROM `tagok` order by belepett LIMIT 1; ";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Console.WriteLine("A legrégebben itt dolgozó: ");
                        Console.WriteLine(dr.GetInt32("id") + " "+dr.GetString("csaladnev") + " " +dr.GetString("utonev") +" (" +dr.GetString("belepett").Split(".")[0]+"-"+dr.GetString("belepett").Split(".")[1] +"-" +dr.GetString("belepett").Split(".")[2]+  ")");

                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("");
            try
            {
                connection.Open();
                sql.CommandText = "SELECT nem, COUNT(nem) as darab FROM `tagok` group by nem; ";
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    Console.WriteLine("A nemek szerinti megoszlás: ");
                    while (dr.Read())
                    {
                        
                        Console.WriteLine(dr.GetString("nem") + " száma: " + dr.GetInt32("darab") );

                    }
                }
                connection.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

    }
}
