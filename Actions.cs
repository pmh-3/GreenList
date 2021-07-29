using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;


namespace GreenList
{

    public class Actions {

        
        public bool createDB(SQLiteCommand cmd)
        {


            cmd.CommandText = @"CREATE TABLE Items(id INTEGER PRIMARY KEY,
                name TEXT, quantity INT, meal TEXT, list TEXT, section TEXT, price REAL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE Meals(id INTEGER PRIMARY KEY,
                name TEXT, list TEXT, price REAL)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE Lists(id INTEGER PRIMARY KEY,
                name TEXT, notes TEXT, date TEXT, price REAL)";
            cmd.ExecuteNonQuery();

            Console.WriteLine("Tables Created");

            return true;
        }

        public bool addItems(SQLiteCommand cmd, string n, int q, string m, string l, string s, float p )
        {
            cmd.CommandText = "INSERT INTO Items(name, quantity, meal, list, section, price) VALUES(@name, @quantity, @meal, @list, @section, @price)";


            cmd.Parameters.AddWithValue("@name", n);
            cmd.Parameters.AddWithValue("@quantity", q);
            cmd.Parameters.AddWithValue("@meal", m);
            cmd.Parameters.AddWithValue("@list", l);
            cmd.Parameters.AddWithValue("@section", s);
            cmd.Parameters.AddWithValue("@price", p);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Console.WriteLine("item inserted");

            return true;
        }

        public bool addMeals(SQLiteCommand cmd, string n, string l, float p)
        {
            cmd.CommandText = "INSERT INTO Meals(name, list, price) VALUES(@name, @list, @price)";

            cmd.Parameters.AddWithValue("@name", n);

            cmd.Parameters.AddWithValue("@list", l);
            cmd.Parameters.AddWithValue("@price", p);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Console.WriteLine("meal inserted");

            return true;
        }

        public bool addList(SQLiteCommand cmd, string n, string no, string d, float p)
        {
            cmd.CommandText = "INSERT INTO Lists(name, notes, date, price) VALUES(@name, @notes, @date, @price)";

            cmd.Parameters.AddWithValue("@name", n);
            cmd.Parameters.AddWithValue("@notes", no);
            cmd.Parameters.AddWithValue("@date", d);
            cmd.Parameters.AddWithValue("@price", p);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Console.WriteLine("List inserted");
            return true;
        }

        public bool readItems(SQLiteCommand cmd)
        {
            cmd.CommandText = "SELECT * FROM Items LIMIT 5";
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-8} {rdr.GetName(2),8}");

            while (rdr.Read())
            {
                Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-8} {rdr.GetInt32(2),8}");
            }
            return true;
        }
        public void readLists(SQLiteCommand cmd)
        {
            cmd.CommandText = "SELECT * FROM Lists";
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-8} {rdr.GetName(2),8} {rdr.GetName(3),12} {rdr.GetName(3),16}");

            while (rdr.Read())
            {
                Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-8} {rdr.GetString(2),8} {rdr.GetString(3),12}{ rdr.GetFloat(4),16} ");
            }

        }


        public void readListItems(SQLiteCommand cmd, string ListTitle)
        {
            cmd.CommandText = "SELECT * FROM Items WHERE list = 'mixers'";
            //+ListTitle;
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine($"{rdr.GetName(0),-3} {rdr.GetName(1),-8} {rdr.GetName(2),8} {rdr.GetName(3),12} {rdr.GetName(4),16} {rdr.GetName(5),20} {rdr.GetName(6),24}");

            while (rdr.Read())
            {
                Console.WriteLine($@"{rdr.GetInt32(0),-3} {rdr.GetString(1),-8} {rdr.GetInt32(2),8} {rdr.GetValue(3),12}{rdr.GetString(4),16} {rdr.GetString(5),20} {rdr.GetFloat(6),24}");
            }

        }

    }

}


