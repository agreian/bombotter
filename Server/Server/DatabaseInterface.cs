using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using BomberLoutreIce;
using System.Data;

namespace BomberLoutre.Server
{
    internal static class DatabaseInterface
    {
        private const string _DB_FILENAME = "data.db";

        private static string _databasePath = string.Empty;

        static DatabaseInterface()
        {
            string directory = System.IO.Directory.GetCurrentDirectory();
            _databasePath = System.IO.Path.Combine(directory, DatabaseInterface._DB_FILENAME);
        }

        private static SQLiteConnection Connection()
        {
            SQLiteConnection conn = new SQLiteConnection(string.Format("Data Source=\"{0}\"", _databasePath));
            conn.Open();

            return conn;
        }

        private static void Deconnection(SQLiteConnection conn)
        {
            conn.Close();
        }

        internal static User SelectUser(string login, string password)
        {
            SQLiteConnection conn = Connection();

            SQLiteCommand mycommand = new SQLiteCommand(conn);

            mycommand.CommandType = CommandType.Text;
            mycommand.CommandText = "SELECT * FROM user WHERE login = @login AND password = @password";
            mycommand.Parameters.Add(new SQLiteParameter("@login", login.ToLower()));
            mycommand.Parameters.Add(new SQLiteParameter("@password", password));

            SQLiteDataReader reader = mycommand.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

                User user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

                reader.Close();

                Deconnection(conn);

                return user;
            }

            reader.Close();

            Deconnection(conn);

            throw new BadUserInfoException();
        }

        internal static User SelectUser(string login)
        {
            SQLiteConnection conn = Connection();

            SQLiteCommand mycommand = new SQLiteCommand(conn);

            mycommand.CommandType = CommandType.Text;
            mycommand.CommandText = "SELECT * FROM user WHERE login = @login";
            mycommand.Parameters.Add(new SQLiteParameter("@login", login.ToLower()));

            SQLiteDataReader reader = mycommand.ExecuteReader();

            while (reader.Read())
            {
                //Console.WriteLine("{0}, {1}, {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

                User user = new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));

                reader.Close();

                Deconnection(conn);

                return user;
            }

            reader.Close();

            Deconnection(conn);

            throw new BadUserInfoException();
        }

        internal static List<User> SelectUsers()
        {
            List<User> users = new List<User>();

            SQLiteConnection conn = Connection();

            SQLiteCommand mycommand = new SQLiteCommand("SELECT * FROM user", conn);

            SQLiteDataReader reader = mycommand.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }

            reader.Close();

            Deconnection(conn);

            return users;
        }

        internal static User InsertUser(string login, string password)
        {
            try
            {
                SelectUser(login);

                throw new UserAlreadyExistsException();
            }
            catch (BadUserInfoException)
            {
                int lastID = LastID();

                SQLiteConnection conn = Connection();

                SQLiteCommand mycommand = new SQLiteCommand(conn);

                mycommand.CommandType = CommandType.Text;
                mycommand.CommandText = "insert into user VALUES(@id, @login, @password);";
                mycommand.Parameters.Add(new SQLiteParameter("@id", lastID + 1));
                mycommand.Parameters.Add(new SQLiteParameter("@login", login.ToLower()));
                mycommand.Parameters.Add(new SQLiteParameter("@password", password));
                
                int rowsUpdated = mycommand.ExecuteNonQuery();

                Deconnection(conn);

                return new User(lastID + 1, login.ToLower(), password);
            }
        }

        internal static int LastID()
        {
            int id = -1;

            SQLiteConnection conn = Connection();

            SQLiteCommand mycommand = new SQLiteCommand("SELECT MAX(id) FROM user", conn);

            SQLiteDataReader reader = mycommand.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    id = reader.GetInt32(0);
                }
                catch (Exception)
                {
                    reader.Close();

                    Deconnection(conn);

                    return 0;
                }
            }

            reader.Close();

            Deconnection(conn);

            if (id == -1)
                return 0;
            else
                return id;
        }
    }
}
