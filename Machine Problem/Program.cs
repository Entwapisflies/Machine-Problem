using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Machine_Problem
{
    internal class Program
    {
        public class Student
        {
            internal string username = "";
            internal string password = "";

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
                new Librarian("Mable", "Ponytails5000")

            };
        }
    }
}
