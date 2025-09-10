using System;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;

namespace StudentCURD
{
    internal class Program
    {
        private static string connectionString = "Server=localhost;Database=StudentDB;Uid=root;Pwd=YourPassword;";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine(" Student Management System");
                Console.WriteLine("=====================================");
                Console.WriteLine("\n1. Add a new Student");
                Console.WriteLine("2. View all Students");
                Console.WriteLine("3. Update a Student's marks");
                Console.WriteLine("4. Delete a Student");
                Console.WriteLine("5. Exit");
                Console.Write("\nPlease select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                        Console.WriteLine("Exiting application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
        private static void AddStudent()
        {
            Console.WriteLine("------Add New Student------");
            try
            {
                Console.WriteLine("Enter Student Name:");
                String name = Console.ReadLine();

                Console.WriteLine("Enter Student Roll number:");
                String rollNo = Console.ReadLine();

                Console.WriteLine("Enter Student Marks:");
                int marks = Convert.ToInt32(Console.ReadLine());

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    String query = "INSERT INTO students (Name, RollNum, Marks) VALUES (@Name, @RollNum, @Marks)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@RollNum", rollNo);
                    command.Parameters.AddWithValue("@Marks", marks);
                    

                    connection.Open();

                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Student added sussefully");
                    }
                    else
                    {
                        Console.WriteLine("Error Occured ");
                    }

                    connection.Close();
                }
                
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }

        private static void ViewStudents()
        {
            Console.WriteLine("\n--- All Students ---");
            DataTable studentsTable = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT ID, Name, RollNum, Marks FROM students";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    adapter.Fill(studentsTable);
                }

                if (studentsTable.Rows.Count > 0)
                {
                    Console.WriteLine("ID\tName\t\tRoll No.\tMarks");
                    Console.WriteLine("---------------------------------------------------");
                    foreach (DataRow row in studentsTable.Rows)
                    {
                        Console.WriteLine($"{row["ID"]}\t{row["Name"]}\t\t{row["RollNum"]}\t\t{row["Marks"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No students found in the database.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        private static void UpdateStudent()
        {
            Console.WriteLine("\n--- Update Student Marks ---");
            try
            {
                Console.Write("Enter the Roll Number of the student to update: ");
                String rollNo = Console.ReadLine();

                Console.Write("Enter the new Marks: ");
                int newMarks = Convert.ToInt32(Console.ReadLine());

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "UPDATE students SET Marks = @newMarks WHERE RollNum = @rollNo";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@newMarks", newMarks);
                    command.Parameters.AddWithValue("@roll", rollNo);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Student marks updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Student with that Roll Number not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("\n--- Delete Student ---");
            try
            {
                Console.Write("Enter the Roll Number of the student to delete: ");
                String roll = Console.ReadLine();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "DELETE FROM students WHERE RollNum = @roll";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@roll", roll);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result > 0)
                    {
                        Console.WriteLine("Student deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Student with that Roll Number not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
