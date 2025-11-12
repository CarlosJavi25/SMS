using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace Student_Management_System
{
   public class StudentRepository
   {
        private readonly string _connectionString;
        public StudentRepository(string connectionString)
        {
          _connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> studentsList = new List<Student>();
            using var connection = new SqlConnection(_connectionString);
            {
                connection.Open();
                string query = "SELECT StudentID, First_Name AS FirstName, Second_Name AS SecondName, Last_Name AS LastName, Age, Major FROM Students";
                using var command = new SqlCommand(query, connection) ;
                using SqlDataReader reader = command.ExecuteReader() ;
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            StudentID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            SecondName = reader.GetString(2),
                            LastName = reader.GetString(3),
                            Age = reader.GetInt32(4),
                            Major = reader.GetString(5)
                        };
                        studentsList.Add(student);
                    }
                }
                return studentsList;
            }
        }
        public void AddStudents(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = 
                "INSERT INTO Students " +
                "(First_Name, Second_Name, Last_Name, Age, Major) VALUES " +
                "(@FirstName, @SecondName, @LastName, @Age, @Major)";
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@SecondName", student.SecondName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@Major", student.Major);
          
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateStudents(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "UPDATE Students SET First_Name = @FirstName , Second_Name = @SecondName, " +
                "Last_Name = @LastName, Age = @Age, Major = @Major " +
                "WHERE StudentID = @StudentID";
               
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentID", student.StudentID);
            command.Parameters.AddWithValue("@FirstName", student.FirstName);
            command.Parameters.AddWithValue("@SecondName", student.SecondName);
            command.Parameters.AddWithValue("@LastName", student.LastName);
            command.Parameters.AddWithValue("@Age", student.Age);
            command.Parameters.AddWithValue("@Major", student.Major);

            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteStudents(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = "DELETE FROM Students WHERE StudentID = @StudentID";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StudentID", student.StudentID);

            command.ExecuteNonQuery();
            connection.Close();
        }


    } 

}
