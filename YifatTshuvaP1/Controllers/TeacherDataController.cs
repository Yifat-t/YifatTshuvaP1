using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YifatTshuvaP1.Models;  //allow the access to school database context in the models folder
using MySql.Data.MySqlClient; // access to series of tools that we installed when we created this project 
using System.Diagnostics;
using System.Web.Http.Cors;



namespace YifatTshuvaP1.Controllers
{
    public class TeacherDataController : ApiController
    {
        //creating a new instance of school class as  "school" object.
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext school = new SchoolDbContext();

        //This Controller Will access the teachers table of our school database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]

        //passing argument of SearchKey to search with any key and null if any key was not provided.
        public IEnumerable<Teacher> ListTeachers(string SearchKey=null)
        {
            //Calling the school.AccessDatabase(); method (return a type of mysql connection).
            //Create an instance of a connection (object instantiation of the variable connection.
            //now we can access the methods of the connection object.
            MySqlConnection Conn = school.AccessDatabase();

            //The "open" method is for opening the connection between the web server and database.
            Conn.Open();

            //Allow us to execute a command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Command object (which is mysql type command) that have a property of command text
            //Command text is set to be equal to a string representing our mysql query 
            //we augmented our search to fing results by seraching first name, last name and salary.
            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where lower(teacherfname) like lower(@key) or lower(teacherlname) " +
                "like lower(@key) or lower (concat(teacherfname,' ' ,teacherlname)) like lower(@key) or CONVERT(salary, char) like @key";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Command object has a "execute reader" method which returns of mysql data reader type.
            //Return type of mysql command is a resultset, which is a specific instantiation of the mysql data reader class.
            //Gather Result Set of Query into a variable.
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers and to navigate our result set object find each particular teacher 
            //and add teacher names to our list of teachers.
            List<Teacher> Teachers = new List<Teacher> { };

            //Read method defined in the ResultSet will return boolean data type. 
            //while loop will end when we reach the end of ResultSet.
            //Our loop will represent one row of our result set.
            //Loop Through Each Row the ResultSet.
            while (ResultSet.Read())
            {
                //In order to Access all Columns information defined by the DB we must convert or cast data type columns.
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                decimal Salary = System.Convert.ToDecimal(ResultSet["salary"]);              
                DateTime HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());

                //Setting the new object properties to match the values we retrieve from the database.
                //Creating a new teacher and making new teacher our object.
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;


        //Add the Teacher information to the List per the while loop iterations.
            Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the Web Server
            Conn.Close();

            //Return the final list of teacher data.
            return Teachers;
        }

       /// ********************************************************************************///

        //Creating a new method called "find teacher" to allow us to serach a single data row.

        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teacher primary key</param>
        /// <returns>An teacher object</returns>
        [HttpGet]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Calling the school.AccessDatabase(); method (return a type of mysql connection).
            //Create an instance of a connection (object instantiation of the variable connection.
            //now we can access the methods of the connection object.
            MySqlConnection Conn = school.AccessDatabase();

            //The "open" method is for opening the connection between the web server and database.
            Conn.Open();

            //Allow us to execute a command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Command object (which is mysql type command) that have a property of command text
            //Command text is set to be equal to a string representing our mysql query 
            //SQL QUERY will output a teacher data by its Id.
            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Command object has a "execute reader" method which returns of mysql data reader type.
            //Return type of mysql command is a resultset, which is a specific instantiation of the mysql data reader class.
            //Gather Result Set of Query into a variable.
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //While loop will iterate only one time.
            while (ResultSet.Read())
            {
                //In order to Access all Columns information defined by the DB we must convert or cast data type columns.
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                decimal Salary = System.Convert.ToDecimal(ResultSet["salary"]);
                DateTime HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());

                //Setting the new object properties to match the values we retrieve from the database.
                //Creating a new teacher and making new teacher our object.

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.Salary = Salary;
                NewTeacher.HireDate = HireDate;
            }
            Conn.Close();

            return NewTeacher;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <example>POST : /api/TeacherData/DeleteTeacher/4 </example> 
       [HttpPost]
       /// [Route("")] is not needed since we are following the convention we scecified in our API config class.
       public void DeleteTeacher(int id)
        {

            //Create an instance of a connection.
            MySqlConnection Conn = school.AccessDatabase();

            //The "open" method is for opening the connection between the web server and database.
            Conn.Open();

            //Allow us to execute a command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "Delete from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }
        /// <summary>
        /// Adds a Teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with 3 mandatory fields that map to the columns of the teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Yifat",
        ///	"TeacherLname":"Tshuva",
        ///	"Salary":"12345",
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins:"*", methods:"*" , headers:"*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)

        {
            //Create an instance of a connection.
            MySqlConnection Conn = school.AccessDatabase();

            //The "open" method is for opening the connection between the web server and database.
            Conn.Open();

            //Allow us to execute a command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();


            //SQL Query
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, salary, hiredate) values (@TeacherFname,@TeacherLname,@Salary, CURRENT_DATE())";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        /// <summary>
        /// Updates an Teacher on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="TeacherInfo">An object with fields that map to the columns of the teacher's table.</param>
        /// <example>
        /// POST api/TeacherData/UpdateTeacher/12 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Yifat",
        ///	"TeacherLname":"Tshuva",
        ///	"Salary":"123.23",

        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = school.AccessDatabase();

            //Debug.WriteLine(TeacherInfo.TeacherFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, salary=@Salary  where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

    }
}
