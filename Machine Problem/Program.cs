using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Machine_Problem.Program;

namespace Machine_Problem
{
    internal class Program
    {
        public class Student
        {
            internal string username = "";
            internal string password = "";
            internal Queue<Book> bookshistory = new Queue<Book>();

            public Student(string username, string password)
            {
                this.username = username;
                this.password = password;
            }
            public Student(string username)
            {
                this.username = username;
            }
            public Student()
            {

            }
            public Student Imitate()
            {
                return new Student(this.username, this.password);
            }
        }
        public class Librarian
        {
            internal string username = "";
            internal string password = "";

            public Librarian(string username, string password)
            {
                this.username = username;
                this.password = password;
            }
            public Librarian(string username)
            {
                this.username = username;
            }
            public Librarian()
            {

            }

        }
        public class Book
        {
            internal int id;
            internal string name;
            internal string author;

            public Book(string name, string author, int id)
            {
                this.name = name;
                this.author = author;
                this.id = id;
            }
            public Book(string name, string author)
            {
                this.name = name;
                this.author = author;
            }
            public Book(string name)
            {
                this.name = name;
            }
            public Book()
            {
            }
        }
        static void Main(string[] args)
        {
            List<Student> Students = new List<Student>();
            Students.Add(new Student("Ethan", "Gymbro9000"));
            Students.Add(new Student("Patrick", "GTA6fanboy"));
            Students.Add(new Student("Luis", "MalingLuubover9000"));

            List<Librarian> Librarians = new List<Librarian>() {
                new Librarian("Mable", "Ponytails5000"),
                new Librarian("Liam", "WorldOfWarcraftAwesomeness"),
                new Librarian("Eudrick", "Islapnaughtypeople")
            };

            List<Book> Books = new List<Book>();
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("\tLogin information");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                bool StudentValidated = false;
                bool LibrarianValidated = false;
                foreach (var student in Students)
                {
                    if (username == student.username)
                    {
                        if (password.Equals(student.password))
                        {
                            StudentValidated = true;
                            Console.WriteLine("Access granted");
                        }
                        else
                        {
                            Console.WriteLine("No access to " + student.username);
                        }
                    }
                }
                foreach (var librarian in Librarians)
                {
                    if (username == librarian.username)
                    {
                        if (password.Equals(librarian.password))
                        {
                            LibrarianValidated = true;
                            Console.WriteLine("Access granted");
                        }
                        else
                        {
                            Console.WriteLine("No access to " + librarian.username);
                        }
                    }
                }
                if (StudentValidated == false && LibrarianValidated == false)
                {
                    Console.WriteLine("Invalid retrying");
                }
                else
                {
                    if (StudentValidated)
                    {
                        int num = 0;
                        foreach (var student in Students) 
                        {
                            num++;
                            if (student.username.Equals(username))
                            {
                                break;
                            }                        
                        }
                        Student currentuser = Students[num];

                    }
                    else if (LibrarianValidated)
                    {
                        int num = 0;
                        foreach (var librarian in Librarians)
                        {
                            num++;
                            if (librarian.username.Equals(username)) 
                            {
                                break;
                            }
                        }
                        Librarian currentuser = Librarians[num];
                    }
                }
            }
        }
    }
}
