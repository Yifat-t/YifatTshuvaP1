using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YifatTshuvaP1.Models;
using MySql.Data.MySqlClient;

namespace YifatTshuvaP1.Controllers
{
    public class ClassDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the classes table of our school database.
        /// <summary>
        /// Returns a list of Classes in the system
        /// </summary>
        /// <example>GET api/ClassData/ListClasses</example>
        /// <returns>
        /// A list of classes (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Class> ListClasses()
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Classes
            List<Class> Classes = new List<Class> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                long ClassId = Int64.Parse(ResultSet["classid"].ToString());
                string ClassName = ResultSet["classname"].ToString();
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime FinishDate = DateTime.Parse(ResultSet["finishdate"].ToString());
                DateTime StartDate = DateTime.Parse(ResultSet["startdate"].ToString());
                int TeacherId = (int)ResultSet["teacherid"];

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                NewClass.FinishDate = FinishDate;
                NewClass.StartDate = StartDate;
                NewClass.TeacherId = TeacherId;

                //Add the Class Name to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of class names
            return Classes;
        }


        /// <summary>
        /// Finds an class in the system given an ID
        /// </summary>
        /// <param name="id">The class primary key</param>
        /// <returns>An class object</returns>
        [HttpGet]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where classid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                long ClassId = Int64.Parse(ResultSet["classid"].ToString());
                string ClassName = ResultSet["classname"].ToString();
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime FinishDate = DateTime.Parse(ResultSet["finishdate"].ToString());
                DateTime StartDate = DateTime.Parse(ResultSet["startdate"].ToString());
                int TeacherId = (int)ResultSet["teacherid"];
          

                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                NewClass.FinishDate = FinishDate;
                NewClass.StartDate = StartDate;
                NewClass.TeacherId = TeacherId;
            }

            return NewClass;
        }

        public IEnumerable<Class> ListTeachersClasses(int teacherid)
        {
            List<Class> Classes = new List<Class> { };
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Classes where teacherid = " + teacherid;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                long ClassId = Int64.Parse(ResultSet["classid"].ToString());
                string ClassName = ResultSet["classname"].ToString();
                string ClassCode = ResultSet["classcode"].ToString();
                DateTime FinishDate = DateTime.Parse(ResultSet["finishdate"].ToString());
                DateTime StartDate = DateTime.Parse(ResultSet["startdate"].ToString());
                int TeacherId = (int)ResultSet["teacherid"];

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassName = ClassName;
                NewClass.ClassCode = ClassCode;
                NewClass.FinishDate = FinishDate;
                NewClass.StartDate = StartDate;
                NewClass.TeacherId = TeacherId;

                Classes.Add(NewClass);
            }

            return Classes;
        }

    }
}