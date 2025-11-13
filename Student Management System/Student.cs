using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    public class Student
    {
        public int StudentID { get; set; }
        public required string  FirstName { get; set; }
        public string? SecondName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Major { get; set; }
    }
}
