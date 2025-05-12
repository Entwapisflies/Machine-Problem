using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static Machine_Problem.Program;

namespace Machine_Problem
{
    internal class Program
    {
        public class Student
        {
            internal string username = "";
            internal string password = "";
            internal List<Book> Requests = new List<Book>();
            internal List<Book> BorrowedBooks = new List<Book>();


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

            public void UserMenu(List<Book> Booklist)
            {
                Console.Clear();
                while (true)
                {
                    Console.WriteLine("1.View available books");
                    Console.WriteLine("2.Borrow books");
                    Console.WriteLine("3.View borrowed books");
                    Console.WriteLine("4.Return Books");
                    Console.WriteLine("5.Logout");
                    Console.Write("CMD: ");

                    string input = Console.ReadLine();
                    if (input == "1")
                    {
                        Console.WriteLine("");
                        int num = 0;
                        foreach (var book in Booklist)
                        {
                            Console.WriteLine($"{++num}. {book.name} by {book.author}");
                        }
                        Console.WriteLine();
                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("");
                        int num = 0;
                        foreach (var book in Booklist)
                        {
                            if (book.Book_Status == "Available" || book.Book_Status == "Pending")
                            {
                                Console.WriteLine($"{++num}. {book.name} by {book.author}");
                            }
                        }
                        Console.WriteLine();
                        Console.Write("Pick a number from the list not the id: ");
                        bool Checknumber = int.TryParse(Console.ReadLine(), out int target);
                        target--;
                        if (Checknumber && target <= Booklist.Count)
                        {
                            if (Booklist[target].Book_Status == "Available" && Booklist[target].Book_Status == "Pending")
                            {
                                Booklist[target].Borrowlist.Add(username);
                                Booklist[target].Book_Status = "Pending";
                                Requests.Add(Booklist[target]);
                                Console.WriteLine($"Borrow request for {Booklist[target].name} has been sent to the librarian");
                            }
                            else
                            {
                                Console.WriteLine("The book is not avilable");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error");
                        }
                    }
                    else if (input == "3")
                    {
                        Console.WriteLine();
                        foreach (var book in Requests)
                        {
                            Console.Write($"{book.name} by {book.author} ({book.Book_Status})");
                            if (book.Book_Status == "Approved")
                            {
                                Console.Write(": This book is now borrowed");
                            }
                            Console.WriteLine();
                        }
                    }
                    else if (input == "4")
                    {
                        Console.WriteLine("");
                        int num = 0;
                        foreach (var book in BorrowedBooks)
                        {
                            Console.WriteLine($"{++num}. {book.name} by {book.author}");
                        }
                        Console.WriteLine();
                        Console.Write("Pick a number from the list not the id: ");
                        bool Checknumber = int.TryParse(Console.ReadLine(), out int target);
                        target--;
                        if (Checknumber && target <= BorrowedBooks.Count)
                        {
                            Console.Write("Confirm (Y/N): ");
                            input = Console.ReadLine();
                            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                            {
                                Booklist[Booklist.IndexOf(BorrowedBooks[target])].Book_Status = "Available";
                                Booklist[Booklist.IndexOf(BorrowedBooks[target])].owner = "";

                                BorrowedBooks.RemoveAt(target);
                            }
                        }
                        Console.WriteLine();
                    }
                    else if (input == "5")
                    {
                        Console.WriteLine("Logging out");
                        Thread.Sleep(1000);
                        break;
                    }
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
                public void Usermenu(List<Book> Booklist, List<Student> students)
                {
                    Console.Clear();
                    while (true)
                    {
                        Console.WriteLine("1. Add new books");
                        Console.WriteLine("2. View all books");
                        Console.WriteLine("3. View pending book borrow requests");
                        Console.WriteLine("4. Approve or decline borrow requests");
                        Console.WriteLine("5. Logout");

                        Console.Write("CMD: ");
                        string input = Console.ReadLine();
                        if (input == "1")
                        {
                            Console.WriteLine();
                            Console.Write("What is the name of the book you want to add? ");
                            string input1 = Console.ReadLine();
                            Console.Write("What is the name of the author of the book ");
                            string input2 = Console.ReadLine();
                            Console.WriteLine("Book: " + input1);
                            Console.WriteLine("Author: " + input1);
                            Console.Write("Confirm? (Y/N): ");
                            input = Console.ReadLine();
                            if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                            {
                                Booklist.Add(new Book(input1, input2));
                            }
                        }
                        else if (input == "2")
                        {
                            Console.WriteLine("");
                            foreach(var item in Booklist)
                            {
                                if (item.Book_Status == "Available" || item.Book_Status == "Pending")
                                {
                                    Console.WriteLine($"{item.name} by {item.author} Availably status (Available)");
                                }
                                else
                                {
                                    Console.WriteLine($"{item.name} by {item.author} Availably status (Unavailable)");

                                }
                            }
                        }
                        else if (input == "3")
                        {
                            foreach (var item in Booklist)
                            {
                                Console.WriteLine($"Request for {item.name}");
                                if (item.Borrowlist.Count > 0)
                                {
                                    foreach (var name in item.Borrowlist)
                                    {
                                        Console.WriteLine($" {name} wants to borrow {item.name} by {item.author}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                }

                            }
                        }
                        else if (input == "4")
                        {

                            List<string> UserRequests = new List<string>();
                            List<string> BookRequests = new List<string>();

                            foreach(var item in Booklist)
                            {
                                if (item.Borrowlist.Count > 0)
                                {
                                    foreach (var name in item.Borrowlist)
                                    {
                                        UserRequests.Add(name);
                                        BookRequests.Add(item.name);
                                    }
                                }
                            }
                            Console.WriteLine();
                            int num = 0;
                            foreach (var item in UserRequests)
                            {
                            }
                            Console.WriteLine();
                            Console.Write("Pick a number to respond: ");
                            bool Checknumber = int.TryParse(Console.ReadLine(), out num);
                            if (Checknumber && num <= BookRequests.Count)
                            {
                                Console.Write("Type A to accept D to decline: ");
                                input = Console.ReadLine();
                                if (input.Equals("A", StringComparison.OrdinalIgnoreCase))
                                {
                                    int i = 0;
                                    foreach(var item in Booklist)
                                    {
                                        if (item.name == BookRequests[num] && item.Borrowlist.Contains(UserRequests[num]))
                                        {
                                            item.Book_Status = "Unavailable";
                                            item.owner = UserRequests[num];
                                            foreach (var student in students)
                                            {
                                                if (student.username == UserRequests[num])
                                                {
                                                    item.owner = UserRequests[num];
                                                    student.BorrowedBooks.Add(item);
                                                    item.Borrowlist.Remove(student.username);
                                                    foreach (var request in student.Requests)
                                                    {
                                                        if (request.name == BookRequests[num])
                                                        {
                                                            request.Book_Status = "Approved";
                                                            break;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                            
                                        }
                                    }
                                }
                            }
                        }
                        else if (input == "5")
                        {
                            Console.WriteLine("Logging out");
                            Thread.Sleep(1000);
                            break;
                        }
                    }

                }

            }
            public class Book
            {
                internal int id;
                internal string name;
                internal string author;
                internal string Book_Status = "Available";
                internal List<string> Borrowlist = new List<string>();
                internal string owner = "";

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



                List<Book> Books = new List<Book>()
            {
                new Book("Lord of the rings", "JK Tolkien"),
                new Book("Xeelee Sequence", "Stephen Baxter")
            };
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
                    if (!(StudentValidated))
                    {
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
                                if (student.username.Equals(username))
                                {
                                    break;
                                }
                                num++;
                            }
                            Student currentuser = Students[num];
                            Console.WriteLine();
                            Console.WriteLine($"Username: {currentuser.username}");
                            Console.WriteLine($"Password: {currentuser.password}");
                            Thread.Sleep(1000);
                            currentuser.UserMenu(Books);

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
                            Console.WriteLine();
                            Console.WriteLine($"Username: {currentuser.username}");
                            Console.WriteLine($"Password: {currentuser.password}");
                            Thread.Sleep(1000);
                            currentuser.Usermenu(Books, students)
                        }
                    }
                }
            }
        }
    }
}
