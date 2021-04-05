using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace YifatTshuvaP1.Models
{
    public class SchoolDbContext
    {

        //These properties are used for Database connection and they are
        //configured to be private and read-only within the SchoolDBContext class.

        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }

        //ConnectionString is a series of credentials used to connect to the database.
        protected static string ConnectionString
        {
            get
            {
                
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password
                    + "; convert zero datetime = True";
            }
        }
        //This is the method we actually use to get the database!
        /// <summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <example>
        /// private SchoolDbContext schooldb = new SchoolDbContext();
        /// MySqlConnection Conn = schooldb.AccessDatabase();
        /// </example>
        /// <returns>A MySqlConnection Object</returns>
        public MySqlConnection AccessDatabase()
        {
            //This class method is public which is accessible to all controllers. method name called AccessDatabase.
            //An object is an instantiation of a class -- mysqlconnection class is representing a connection to a mysql specific database.
            //the object is a specific connection to our school database on port 3306 of localhost.

            return new MySqlConnection(ConnectionString);
        }
    }
}