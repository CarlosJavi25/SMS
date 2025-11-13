using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Student_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentRepository _repository;
        public MainWindow()
        {
            InitializeComponent();
            _repository = new StudentRepository("Server = DESKTOP-MDCGKIV;Database=StudentsManagement;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
            LoadStudents();
            
        }

       private void LoadStudents() 
       {
            var students = _repository.GetAllStudents();
            dgStudents.ItemsSource = students;
       }

        private bool ValidateInput(out int age)
        {
            age = 0;

            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show(" Please enter a valid age");
                return false;
            }
            else if (age < 0 || age > 100)
            {
                MessageBox.Show(" Please enter a valid age between 0 and 100");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFirstName.Text)
                || string.IsNullOrWhiteSpace(txtLastName.Text)
                || string.IsNullOrWhiteSpace(txtAge.Text)
                || string.IsNullOrWhiteSpace(txtMajor.Text))
            {
                MessageBox.Show(" Please fill in all fields");
                return false;
            }

            return true;
        }
        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput(out int age))
                return;

            string firstName = txtFirstName.Text;
            string secondName = txtSecondName.Text;
            string lastName = txtLastName.Text;
            string major = txtMajor.Text;
           
            var student = new Student()
            {
                FirstName = firstName,
                SecondName = secondName,
                LastName = lastName,
                Age = age,
                Major = major
            };

            try
            { 
                _repository.AddStudents(student);

                 txtFirstName.Clear();
                 txtSecondName.Clear();
                 txtLastName.Clear();
                 txtAge.Clear();
                 txtMajor.Clear();

                  LoadStudents();
            }

            catch(Exception)
            {
                MessageBox.Show($"An error occurred while adding the student");
            }
        }

        private void btnUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is not Student selectedStudent)
            {
                MessageBox.Show("Please select a student to update.");
                return;
            }

            if (!ValidateInput(out int age))
            {
                return;
            }

            var updatedstudent = new Student
            {
                StudentID = selectedStudent.StudentID,
                FirstName = txtFirstName.Text,
                SecondName = txtSecondName.Text,
                LastName = txtLastName.Text,
                Age = age,
                Major = txtMajor.Text
            };

            try
            {
                _repository.UpdateStudents(updatedstudent);

                LoadStudents();

                txtFirstName.Clear();
                txtSecondName.Clear();
                txtLastName.Clear();
                txtAge.Clear();
                txtMajor.Clear();

                MessageBox.Show("Selection changed succesfully");

                btnUpdateStudent.IsEnabled = false;

            }

            catch (Exception)
            {
                MessageBox.Show($"An error occurred while updating the student");
            }
           

        }
  

        private void dgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgStudents.SelectedItem is Student selectedStudent)
            {
                txtFirstName.Text = selectedStudent.FirstName;
                txtSecondName.Text = selectedStudent.SecondName;
                txtLastName.Text = selectedStudent.LastName;
                txtAge.Text = selectedStudent.Age.ToString();
                txtMajor.Text = selectedStudent.Major;

                btnUpdateStudent.IsEnabled = true;
                btnDeleteStudent.IsEnabled = true;
            }

            else
            {
                btnUpdateStudent.IsEnabled = false;
                btnDeleteStudent.IsEnabled = false;
            }

        

        }

        private void btnDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is not Student selectedStudent)
            {
                MessageBox.Show("Please select a student to delete.");
                return;
            }
            var deleteStudent = new Student()
            {
                StudentID = selectedStudent.StudentID,
                FirstName = selectedStudent.FirstName,
                SecondName = selectedStudent.SecondName,
                LastName = selectedStudent.LastName,
                Age = selectedStudent.Age,
                Major = selectedStudent.Major
            };
            try 
            {
                _repository.DeleteStudents(deleteStudent);
                LoadStudents();

                txtFirstName.Clear();
                txtSecondName.Clear();
                txtLastName.Clear();
                txtAge.Clear();
                txtMajor.Clear();
                MessageBox.Show("Student deleted successfully");
                 
                btnDeleteStudent.IsEnabled = false;
            }
            catch(Exception)
            {
                MessageBox.Show($"An error occurred while deleting the student");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Clear();
            txtSecondName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            txtMajor.Clear();
        }
    }
}